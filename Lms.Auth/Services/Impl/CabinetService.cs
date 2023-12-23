using AutoMapper;
using Lms.Auth.Db;
using Lms.Auth.Db.Models;
using Lms.Auth.Services;

namespace Lms.Api.Services.Impl;

internal class CabinetService : EntityServiceBase<Cabinet>
{
    public CabinetService(DataContext db, IMapper mapper) : base(db, mapper)
    {
    }
}

