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
    public class PlayListRepository : IPlayListRepository
    {

        private readonly ChinHookDbContext _dbContext;

        public PlayListRepository(ChinHookDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Playlist> AddAsync(Domain.Entities.Playlist entity)
        {
            throw new NotImplementedException();
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
    }
}
