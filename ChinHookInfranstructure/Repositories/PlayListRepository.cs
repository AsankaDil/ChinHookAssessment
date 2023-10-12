using Application.Repositories;
using ChinHookInfranstructure.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChinHookInfranstructure.Repositories
{
    public class PlayListRepository : IPlayListRepository
    {

        private readonly ChinHookDbContext _dbContext;

        public PlayListRepository(ChinHookDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Playlist> AddAsync(Playlist entity)
        {
            _dbContext.Playlists.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public Task DeleteAsync(Domain.Entities.Playlist entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Playlist>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Playlist> GetByIdAsync(long id)
        {
            return await _dbContext.Playlists
           .Include(a => a.Tracks).ThenInclude(a => a.Album).ThenInclude(a => a.Artist)
           .FirstAsync(p => p.PlaylistId == id);
        }

        public Task UpdateAsync(long id, Playlist entity)
        {
            throw new NotImplementedException();
        }

        public Playlist GetUserPlayListByName(string name, string userId)
        {
            return _dbContext.Playlists.Where(p => p.UserPlaylists.Any(up => up.UserId == userId && up.Playlist.Name == name)).FirstOrDefault();
        }

        public async Task<bool> SavePlayList(string userId,long playListId) {
            try
            {

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> SavePlayListTracksAsync(Playlist entity, bool isNewPlayList)
        {
            try
            {
                if (isNewPlayList)
                {
                    var playList = new Playlist { PlaylistId = entity.PlaylistId+1, Name = entity.Name };
                    var tracks = entity.Tracks.FirstOrDefault();
                    Track trk = _dbContext.Tracks?.FirstOrDefault(s => s.TrackId == entity.Tracks.FirstOrDefault().TrackId);
                    playList.Tracks.Add(trk);
                    _dbContext.Add(playList);
                   await _dbContext.SaveChangesAsync();
                }
                else
                {
                    Playlist playList = _dbContext.Playlists.FirstOrDefault(x => x.PlaylistId == entity.PlaylistId);
                    if (playList != null) {

                        Track trk = _dbContext.Tracks?.FirstOrDefault(s => s.TrackId == entity.Tracks.FirstOrDefault().TrackId);
                        playList.Tracks.Add(trk);
                        //_dbContext.Add(playList);
                        await _dbContext.SaveChangesAsync();
                    }
                }


                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<List<Playlist>> GetUserPlayListAsync(string userId)
        {
            return await _dbContext.Playlists.Where(p => p.UserPlaylists.Any(up => up.UserId == userId)).ToListAsync();
        }


    }
}
