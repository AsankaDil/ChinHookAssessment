using Application.ClientModels;
using Application.Repositories;
using ChinHook.Shared.Components;
using Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ChinHook.Components
{
    
    public class ArtistComponent : ComponentBase
    {
        [Parameter] 
        public long ArtistId { get; set; }

        public Modal PlaylistDialog { get; set; }

        public Artist Artist;
        //public List<PlaylistTrack> Tracks;
        public List<PlaylistTrack> Tracks { get; set; } = new List<PlaylistTrack>();
  
        public PlaylistTrack SelectedTrack;
        public string InfoMessage;
        public string CurrentUserId;

        [Inject]
        public IArtistRepository artistRepository { get; set; }


        [Inject]
        public ITrackRepository trackRepository { get; set; }

        private async Task<string> GetUserId()
        {
            //var user = (await authenticationState).User;
            var userId = "1"; //user.FindFirst(u => u.Type.Contains(ClaimTypes.NameIdentifier))?.Value;
            return userId;
        }

        protected override async Task OnInitializedAsync()
        {
            
            CurrentUserId = await GetUserId();

            Artist = await artistRepository.GetByIdAsync(ArtistId);

            Tracks =   trackRepository.GetTracksWithAlbums()
                .Select(t => new PlaylistTrack()
                {
                    AlbumTitle = (t.Album == null ? "-" : t.Album.Title),
                    TrackId = t.TrackId,
                    TrackName = t.Name,
                    IsFavorite = t.Playlists.Where(p => p.UserPlaylists.Any(up => up.UserId == CurrentUserId && up.Playlist.Name == "Favorites")).Any()
                })
                .ToList();
        }

        public void FavoriteTrack(long trackId)
        {
            var track = Tracks.FirstOrDefault(t => t.TrackId == trackId);
            InfoMessage = $"Track {track.ArtistName} - {track.AlbumTitle} - {track.TrackName} added to playlist Favorites.";
        }

        public void UnfavoriteTrack(long trackId)
        {
            var track = Tracks.FirstOrDefault(t => t.TrackId == trackId);
            InfoMessage = $"Track {track.ArtistName} - {track.AlbumTitle} - {track.TrackName} removed from playlist Favorites.";
        }

        public void OpenPlaylistDialog(long trackId)
        {
            CloseInfoMessage();
            SelectedTrack = Tracks.FirstOrDefault(t => t.TrackId == trackId);
            PlaylistDialog.Open();
        }

        public void AddTrackToPlaylist()
        {
            CloseInfoMessage();
            InfoMessage = $"Track {Artist.Name} - {SelectedTrack.AlbumTitle} - {SelectedTrack.TrackName} added to playlist {{playlist name}}.";
            PlaylistDialog.Close();
        }

        public void CloseInfoMessage()
        {
            InfoMessage = "";
        }
    }
}
