using Application.Repositories;
using ChinHookInfranstructure.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinHookInfranstructure.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly ChinHookDbContext _dbContext;
        private readonly IPlayListRepository _playList;

        public TrackRepository(ChinHookDbContext dbContext, IPlayListRepository playList)
        {
            _dbContext = dbContext;
            _playList = playList;
        }

        public Task<Track> AddAsync(Track entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Track entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Track>> GetAllAsync()
        {
            return await _dbContext.Tracks.ToListAsync();
        }

        public async Task<List<Track>> GetTracksWithAlbumsAsync()
        {
            return await _dbContext.Tracks.Include(p=>p.Playlists).ThenInclude(p => p.UserPlaylists).Include(a => a.Album).ToListAsync();
        }

        public Task<Track> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(long id, Track entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveTrackToPlayListAsync(string userId, long trackId,string playListName)
        {
            try
            {
                var result = _playList.GetUserPlayListByName(playListName, userId);
                long maxPlayListId = _dbContext.Playlists.Max(x => x.PlaylistId);
                maxPlayListId = maxPlayListId > 0 ? maxPlayListId : 1;
                Domain.Entities.Playlist plyList = new Domain.Entities.Playlist();
                if (result != null)
                {
                 
                    plyList.PlaylistId = result.PlaylistId;
                    plyList.Name = playListName;
                    plyList.Tracks.Add(_dbContext.Tracks.FirstOrDefault(x => x.TrackId== trackId));
                    await _playList.SavePlayListTracksAsync(plyList, false);
                }
                else
                {
                    plyList.PlaylistId = maxPlayListId;
                    plyList.Name = playListName;
                    plyList.Tracks.Add(_dbContext.Tracks.FirstOrDefault(x => x.TrackId == trackId));
                    await _playList.SavePlayListTracksAsync(plyList,true);
                    AddUserPlayList(userId, maxPlayListId);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
            
        }

        public async Task<bool> SetToUnfavouriteAsync(long trackId)
        {
            try
            {
                Playlist playList = _dbContext.Playlists.FirstOrDefault(x => x.Name == "Favorites");
                if (playList != null)
                {
                    Track trk = playList.Tracks.FirstOrDefault(x => x.TrackId == trackId);
                    playList.Tracks.Remove(trk);
                    await _dbContext.SaveChangesAsync();
                }
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }

        }

        private void AddUserPlayList(string userId, long maxPlayListId)
        {
            UserPlaylist plyList = new UserPlaylist();
            plyList.UserId = userId;
            plyList.PlaylistId = maxPlayListId+1;
            _dbContext.UserPlaylists.Add(plyList);
            _dbContext.SaveChanges();
        }

        public async Task<List<Track>> GetFavouriteTracksAsync(string userId) {
            return await _dbContext.Tracks.Include(a => a.Album)
                .Where(t=> t.Playlists.Where(p => p.UserPlaylists.Any(up => up.UserId == userId && up.Playlist.Name == "Favorites")).Any())
                .ToListAsync();

        }
    }
}
