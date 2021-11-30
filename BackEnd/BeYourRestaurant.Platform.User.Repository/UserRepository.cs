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
        public override string _tableEntityName => "User";
        private readonly IDapperDbContext _context;

        public UserRepository(IDapperDbContext databaseContext) : base(databaseContext)
        {
            _context = databaseContext;
        }
    }
}
