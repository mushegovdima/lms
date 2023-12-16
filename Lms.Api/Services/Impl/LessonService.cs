using AutoMapper;
using Lms.Api.Db;
using Lms.Api.Db.Models;

namespace Lms.Api.Services.Impl
{
    public class UserService : EntityServiceBase<User>
    {
        public UserService(DataContext db, IMapper mapper) : base(db, mapper)
        {
        }
    }
}

