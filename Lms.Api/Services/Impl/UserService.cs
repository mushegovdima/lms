﻿using AutoMapper;
using Lms.Api.Db;
using Lms.Api.Db.Models;

namespace Lms.Api.Services.Impl
{
    public class LessonService : EntityServiceBase<Lesson>
    {
        public LessonService(DataContext db, IMapper mapper) : base(db, mapper)
        {
        }
    }
}

