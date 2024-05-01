using Lms.Api.Services;
using Lms.Api.Services.Impl;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Lms.SDK.Enums;
using Lms.Api.Db.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Lms.Api.Tests.Services;

[TestFixture]
[Parallelizable]
public class LessonAnswerServiceTests
{

    [Test]
    public async Task CanChangeStatus_Forbid_IfNextStatusIsSame()
    {
        var service = GetService();
        var entity = GetAnswer(1, LessonAnswerStatus.Draft);

        var res = await service.CanChangeStatus(entity, LessonAnswerStatus.Draft);

        Assert.IsFalse(res);
    }

    [Test]
    public async Task CanChangeStatus_Available_IfUserIsAdmin()
    {
        var service = GetService(true);
        var entity = GetAnswer(1, LessonAnswerStatus.Draft);

        var res = await service.CanChangeStatus(entity, LessonAnswerStatus.Send);

        Assert.IsTrue(res);
    }

    [TestCase(LessonAnswerStatus.Send, new [] { Role.Checker, Role.Student }, false)]
    [TestCase(LessonAnswerStatus.Successfull, new [] { Role.Checker, Role.Student }, false)]
    [TestCase(LessonAnswerStatus.Cancelled, new [] { Role.Checker, Role.Student }, true)]
    [TestCase(LessonAnswerStatus.Draft, new [] { Role.Checker }, false)]
    [TestCase(LessonAnswerStatus.Draft, new Role[] {}, false)]
    [TestCase(LessonAnswerStatus.Draft, new [] { Role.Checker, Role.Student }, true)]
    public async Task CanChangeStatus_ToSend(LessonAnswerStatus status,  Role[] roles, bool result)
    {
        var service = GetService(false, roles);
        var entity = GetAnswer(1, status);

        var res = await service.CanChangeStatus(entity, LessonAnswerStatus.Send);

        Assert.IsTrue(res == result);
    }

    [TestCase(LessonAnswerStatus.Send, new [] { Role.Checker, Role.Admin }, true)]
    [TestCase(LessonAnswerStatus.Successfull, new [] { Role.Checker, Role.Admin }, false)]
    [TestCase(LessonAnswerStatus.Cancelled, new [] { Role.Checker, Role.Admin }, false)]
    [TestCase(LessonAnswerStatus.Draft, new [] { Role.Checker, Role.Admin }, false)]
    [TestCase(LessonAnswerStatus.Send, new [] { Role.Checker }, true)]
    [TestCase(LessonAnswerStatus.Send, new Role[] {}, false)]
    public async Task CanChangeStatus_ToSuccessfull(LessonAnswerStatus status,  Role[] roles, bool result)
    {
        var service = GetService(false, roles);
        var entity = GetAnswer(1, status);

        var res = await service.CanChangeStatus(entity, LessonAnswerStatus.Successfull);

        Assert.IsTrue(res == result);
    }

    [TestCase(LessonAnswerStatus.Send, new [] { Role.Checker, Role.Admin }, false)]
    [TestCase(LessonAnswerStatus.Successfull, new [] { Role.Checker, Role.Admin }, true)]
    [TestCase(LessonAnswerStatus.Cancelled, new [] { Role.Checker, Role.Admin }, false)]
    [TestCase(LessonAnswerStatus.Successfull, new [] { Role.Checker }, true)]
    [TestCase(LessonAnswerStatus.Successfull, new Role[] {}, false)]
    public async Task CanChangeStatus_ToCancelled(LessonAnswerStatus status,  Role[] roles, bool result)
    {
        var service = GetService(false, roles);
        var entity = GetAnswer(1, status);

        var res = await service.CanChangeStatus(entity, LessonAnswerStatus.Cancelled);

        Assert.IsTrue(res == result);
    }

    #region private

    private LessonAnswerService GetService(bool isAdmin = false, params Role[] roles)
    {
        var accessorMock = new Mock<IHttpContextAccessor>();
        accessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            { 
                User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("IsAdmin", isAdmin.ToString()) }))
            });

        var roleService = new Mock<ICourseRoleService>();
        roleService.Setup(x => x.UserHasRoles(It.IsAny<long>(), It.Is<Role[]>(x => x.Any(x => roles.Contains(x)))))
            .ReturnsAsync(true);

        return new(
            null,
            Mock.Of<IMapper>(),
            accessorMock.Object,
            roleService.Object);
    }

    private LessonAnswer GetAnswer(long id, LessonAnswerStatus status)
        => new() {
            Id = id,
            Status = status,
            AuthorId = 1,
            Lesson = new Lesson() 
                {
                    Id = 1,
                    Title = "Lesson 1",
                    Fields = new List<LessonField>(),
                }
        };

    #endregion

}
