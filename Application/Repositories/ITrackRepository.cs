using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface ITrackRepository : IGenericRepository<Track>
    {
        Task<List<Track>> GetTracksWithAlbumsAsync();
        Task<bool> SaveTrackToPlayListAsync(string userId, long trackId, string playListName);
        Task<List<Track>> GetFavouriteTracksAsync(string userId);
        Task<bool> SetToUnfavouriteAsync(long trackId);
    }
}
