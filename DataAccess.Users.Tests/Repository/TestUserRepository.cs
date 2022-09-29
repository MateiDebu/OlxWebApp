using DataAccess.Database.Context;
using DataAccess.Database.Models;
using DataAccess.Users.Repository;
using Microsoft.EntityFrameworkCore;
using NuGet.Frameworks;
using NUnit.Framework;

namespace DataAccess.Users.Tests.Repository
{
    [TestFixture]
    public class TestUserRepository
    {
        private UserRepository _userRepository;
        private OlxContext _olxContext;

        [SetUp]
        public async Task SetUp()
        {
            var builder = new DbContextOptionsBuilder<OlxContext>().UseInMemoryDatabase("TestUserRepository"+
                TestContext.CurrentContext.Test.Name);
            _olxContext = new OlxContext(builder.Options);
            await  _olxContext.Database.EnsureCreatedAsync();

            _userRepository = new UserRepository(_olxContext);
        }

        [Test]
        public async Task UserRepository_Add_ValidUser_UserIsAdded()
        {
            //Arrange
            string name = "name";
            string username = "username";
            var appUser = new ApplicationUser() { Name = name, UserName = username };

            //Act
            var addedUser = await _userRepository.Add(appUser);

            //Assert
            Assert.That(addedUser, Is.Not.Null);
            Assert.That(addedUser.Id, Is.Not.Zero);
            var dbUser = _olxContext.Users.First();
            Assert.That(addedUser,Is.SameAs(dbUser));
        }
        [Test]
        public async Task UserRepository_FindById_UserFound_ReturnUser()
        {
            //Arrange
            string name = "name";
            string username = "username";
            var appUser = new ApplicationUser() { Name = name, UserName = username };
            _olxContext.Users.Add(appUser);
            await _olxContext.SaveChangesAsync();

            //Act
            var result = await _userRepository.FindById(appUser.Id);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result,Is.SameAs(appUser));
        }

        [Test]
        public async Task UserRepository_FindById_UserNotFound_ReturnNull()
        {
            //Act
            var result = await _userRepository.FindById(101);

            //Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void UserRepository_GetAll_UsersFound_ReturnUsers()
        {
            //Arrange
            string name1 = "name1";
            string username1 = "username1";
            var appUser1 = new ApplicationUser() { Name = name1, UserName = username1 };
            string name2 = "name2";
            string username2 = "username2";
            var appUser2 = new ApplicationUser() { Name = name2, UserName = username2 };
            _olxContext.Users.Add(appUser1);
            _olxContext.Users.Add(appUser2);
            _olxContext.SaveChangesAsync();

            //Act
            var result = _userRepository.GetAll(0, 2);
            var resultList = result.ToList();
            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(resultList[0].Name, Is.EqualTo(name1));
            Assert.That(resultList[0].UserName, Is.EqualTo(username1));
            Assert.That(resultList[1].Name, Is.EqualTo(name2));
            Assert.That(resultList[1].UserName, Is.EqualTo(username2));
        }

        [Test]
        public async Task UserRepository_Delete_UserFound_UserIsDeleted()
        {
            //Arrange
            string name = "name";
            string username = "username";
            var appUser = new ApplicationUser() { Name = name, UserName = username };
            _olxContext.Users.Add(appUser);
            await _olxContext.SaveChangesAsync();

            //Act
            await _userRepository.Delete(appUser.Id);

            //Assert
            Assert.That(_olxContext.Users, Is.Empty);
        }

        [Test]
        public void UserRepository_Delete_UserNotFound_DoesNothing()
        {
            //Act & assert
            Assert.That(()=>_userRepository.Delete(101), Throws.Nothing);
        }

        [Test]
        public async Task UserRepository_Update_UserFound_UserIsModified()
        {
            //Arrange
            string name = "name";
            string username = "username";
            var updateUser = new ApplicationUser() { Name = name, UserName = username };
            _olxContext.Users.Add(updateUser);
            await _olxContext.SaveChangesAsync();

            //Act
            var modifiedUser = await _userRepository.Update(updateUser);

            //Assert
            Assert.That(modifiedUser, Is.Not.Null);
            Assert.That(modifiedUser.Name, Is.EqualTo(name));
            Assert.That(modifiedUser.UserName, Is.EqualTo(username));
        }
    }
}
