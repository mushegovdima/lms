﻿using AutoMapper;
using Lms.Api.Db.Models;
using Lms.Api.Dto.CourseDto;
using Lms.Api.Dto.LessonAnswerDto;
using Lms.Api.Dto.LessonDto;
using Lms.SDK.Enums;

namespace Lms.Api.Dto
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {

            CreateMap<Course, CourseResponse>()
                .ForMember(
                    x => x.LessonsCount,
                    o => o.MapFrom(d => d.Lessons.Count()));

            CreateMap<CourseRole, CourseRoleResponse>();

            CreateMap<LessonPostRequest, Lesson>();
            CreateMap<Lesson, LessonListItemResponse>();
            CreateMap<Lesson, LessonResponse>();
            CreateMap<LessonField, LessonFieldResponse>();

            CreateMap<LessonAnswer, LessonAnswerResponse>();
            CreateMap<LessonAnswer, LessonAnswerListItemResponse>();
            CreateMap<LessonAnswerPostRequest, LessonAnswer>()
                .AfterMap((dto, res) => res.Status = LessonAnswerStatus.Send);
            CreateMap<LessonAnswerPostAsDraftRequest, LessonAnswer>()
                .AfterMap((dto, res) => res.Status = LessonAnswerStatus.Draft);
        }
    }
}
