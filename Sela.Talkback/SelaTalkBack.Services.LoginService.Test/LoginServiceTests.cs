using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sela.TalkBack.Common.Entities;
using Sela.TalkBack.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelaTalkBack.Services.Test
{
    [TestClass]
    public class LoginServiceTests
    {
        [TestMethod]
        public void LoginUser_WithUserCredentialsValid_ReturnsListOfUsers()
        {
            var user = new User()
            {
                Username = "Test",
                Password = "Test"
            };
            var service = new LoginService();
            var users = service.LoginUser(user);
            Assert.IsNotNull(users);
            Assert.IsInstanceOfType(users, typeof(List<User>));
            Assert.IsTrue(users.All(x => x.IsLoggedIn));
        }

        [TestMethod]
        [ExpectedException(typeof(LoginFailedException))]
        public void LoginUser_WithUserCredentialsInValid_ThrowException()
        {
            var user = new User()
            {
                Username = "Test",
                Password = "1234"
            };
            var service = new LoginService();

            service.LoginUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoginUser_WithUserNoPassword_ThrowsException()
        {
            var user = new User()
            {
                Username = "Test"
            };
            var service = new LoginService();
            service.LoginUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoginUser_WithUserNoUsername_ThrowsException()
        {
            var user = new User()
            {
                Password = "Test"
            };
            var service = new LoginService();
            service.LoginUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoginUser_WithNoUser_ThrowsException()
        {
            var service = new LoginService();
            service.LoginUser(null);
        }

        [TestMethod]
        public void SignUpUser_WithUserCredentialsValid_ReturnsListOfUsers()
        {
            var user = new User()
            {
                Username = "Test",
                Password = "Test"
            };
            var service = new LoginService();
            Assert.IsInstanceOfType(service.SignUpUser(user), typeof(List<User>));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SignUpUser_WithUserNoPassword_ThrowsException()
        {
            var user = new User()
            {
                Username = "Test"
            };
            var service = new LoginService();
            service.SignUpUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SignUpUser_WithUserNoUsername_ThrowsException()
        {
            var user = new User()
            {
                Password = "Test"
            };
            var service = new LoginService();
            service.SignUpUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SignUpUser_WithNoUser_ThrowsException()
        {
            var service = new LoginService();
            service.SignUpUser(null);
        }

        //[TestMethod]
        //[ExpectedException(typeof(UserNotLoggedException))]
        //public void LogoutUser_WithUserBeforeLogIn_ThrowsException()
        //{
        //    var user = new User()
        //    {
        //        Username = "Foo"
        //    };
        //    var service = new LoginService();
        //    service.LogoutUser(user);
        //}

        [TestMethod]
        public void LogoutUser_WithLoggedInUser_ReturnsNull()
        {
            var user = new User()
            {
                Username = "Test"
            };
            var service = new LoginService();
            service.LogoutUser(user);
        }
    }
}
