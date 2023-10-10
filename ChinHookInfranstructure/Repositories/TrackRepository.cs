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

        public TrackRepository(ChinHookDbContext dbContext)
        {
            _dbContext = dbContext;
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

        public List<Track> GetTracksWithAlbums()
        {
            return _dbContext.Tracks.Include(a => a.Album).ToList();
        }

        public Task<Track> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(long id, Track entity)
        {
            throw new NotImplementedException();
        }
    }
}
