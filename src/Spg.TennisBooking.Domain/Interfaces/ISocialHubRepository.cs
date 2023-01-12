using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface ISocialHubRepository
    {
        void Update(SocialHub socialHub);
        void Delete(SocialHub socialHub);
        void Add(SocialHub socialHub);
    }
}
