using Business.Users.Services;
using Core.Repositories;
using DataAccess.Database.Models;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;

namespace Business.Users.Tests.Services
{
    [TestFixture]
    public class TestUserService
    {
        private IUserRepository _userRepositoryMock;
        private UserManager<ApplicationUser> _userManagerMock;
        private UserService _userService;
        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = Substitute.For<IUserRepository>();
            IUserStore<ApplicationUser> userStoreMock=Substitute.For<IUserStore<ApplicationUser>>();
            _userManagerMock = Substitute.For<UserManager<ApplicationUser>>(userStoreMock,null,null,null,null,null,null,null,null);
            _userService=new UserService(_userRepositoryMock,_userManagerMock);
        }

        [Test]
        public async Task UserService_CreateUser_ValidParameter_UserIsCreated()
        {
            //Arrange
            string name = "name";
            string username = "username";
            string password="password";

            _userManagerMock.CreateAsync(Arg.Is<ApplicationUser>(appUser =>
                appUser.Name == name && appUser.UserName  == username), password).ReturnsForAnyArgs(IdentityResult.Success);

            //Act

            var result=await _userService.CreateUser(name, username, password);

            //Assert

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(name));
        }

        [Test]

        public void UserService_CreateUser_InvalidParameter_ExceptionIsThrown()
        {
            //Arrange
            string name = "name";
            string username = "username";
            string password = "";

            _userManagerMock.CreateAsync(Arg.Is<ApplicationUser>(appUser =>
                appUser.Name == name && appUser.UserName == username), password).ReturnsForAnyArgs(IdentityResult.Failed(new IdentityError() { Description="Password is too short"}));

            //Act & assert
            Assert.That(() => _userService.CreateUser(name, username, password), Throws.InvalidOperationException);
        }

    }
}
