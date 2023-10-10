using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

// Add profile data for application users by adding properties to the ChinookUser class
public class ChinookUser : IdentityUser
{
    public virtual ICollection<UserPlaylist> UserPlaylists { get; set; }
}

