using AutoMapper;
using Lms.Api.Db;
using Lms.Api.Db.Models;

namespace Lms.Api.Services.Impl;

internal class CabinetService : EntityServiceBase<Cabinet>
{
    public CabinetService(DataContext db, IMapper mapper) : base(db, mapper)
    {
    }
}

