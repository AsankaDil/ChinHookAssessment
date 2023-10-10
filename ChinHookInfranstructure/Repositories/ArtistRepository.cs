using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using ChinHookInfranstructure.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChinHookInfranstructure.Repositories
{
    public class ArtistRepository : IArtistRepository
    {

        private readonly ChinHookDbContext _dbContext;

        public ArtistRepository(ChinHookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Artist> AddAsync(Artist entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Artist entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Artist>> GetAllAsync()
        {
            return await _dbContext.Artists.ToListAsync();
        }

        public async Task<Artist> GetByIdAsync(long id)
        {
            return await _dbContext.Set<Artist>().FindAsync(id);
        }

        public Task UpdateAsync(long id, Artist entity)
        {
            throw new NotImplementedException();
        }
    }
}
