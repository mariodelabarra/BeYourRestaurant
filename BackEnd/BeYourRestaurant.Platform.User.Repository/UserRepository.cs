using BeYourRestaurant.Platform.Core.Postgres;
using BeYourRestaurant.Platform.Core.Postgres.Repository;
using BeYourRestaurant.Platform.Core.Repository;

namespace BeYourRestaurant.Platform.User.Repository
{
    public interface IUserRepository : IBaseRepository<Domain.User>
    {
    }

    public class UserRepository : BaseRepository<Domain.User>, IUserRepository
    {
        public const string _tableName = "User";
        private readonly PostgressDbSession _session;

        public UserRepository(PostgressDbSession session) : base(session, _tableName)
        {
            _session = session;
        }
    }
}
