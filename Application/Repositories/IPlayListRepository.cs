using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IPlayListRepository:IGenericRepository<Playlist>
    {
        Playlist GetUserPlayListByName(string name, string userId);
        Task<bool> SavePlayListTracksAsync(Playlist entity,bool isNewPlayList);
        Task<List<Playlist>> GetUserPlayListAsync(string userId);
    }
}
