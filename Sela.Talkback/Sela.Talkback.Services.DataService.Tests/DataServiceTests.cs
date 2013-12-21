using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sela.TalkBack.Common.Entities;
using Sela.TalkBack.Services.DataLayerService;
using System.Security.Cryptography;
using System.Text;
using DataContracts;

namespace Sela.TalkBack.Services.DataLayerService.Tests
{
    [TestClass]
    public class DataServiceTests
    {
        const string APPKEY = "7e240de74fb1ed08fa08d38063f6a6a91462a815";
        [TestMethod]
        public void GetUser_WithExistingUsernameByUsernameAndPassword_ReturnsUser()
        {
            string username = "Test";
            string password = "Test";
            var service = new DataService();
            var user = service.GetUser(username, password, APPKEY);
            Assert.IsNotNull(user);
            Assert.IsInstanceOfType(user, typeof(User));
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void GetUser_WithNonExistingUsername_ByHash_ThrowException()
        {
            string username = "Test15";
            string password = "1234";
            var hasher = SHA256.Create();
            var hashbytes = hasher.ComputeHash(Encoding.ASCII.GetBytes(username + password));
            var service = new DataService();
            service.GetUserByHash(Encoding.ASCII.GetString(hashbytes), APPKEY);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void GetUser_WithNonExistingUsername_ByUsernameAndPassword_ThrowException()
        {
            string username = "Test15";
            string password = "1234";
            var service = new DataService();
            service.GetUser(username, password, APPKEY);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetUser_WithNullUsernameAndPassword_ThrowException()
        {
            var service = new DataService();
            service.GetUser(null, null, APPKEY);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetUser_WithNullUsername_ThrowException()
        {
            var service = new DataService();
            service.GetUser(null, "12345", APPKEY);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetUser_WithNullPassword_ThrowException()
        {
            var service = new DataService();
            service.GetUser("foo", null, APPKEY);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetUser_WithNullHash_ThrowException()
        {
            var service = new DataService();
            service.GetUserByHash(null, APPKEY);
        }

        [TestMethod]
        public void RegisterUser_WithValidNonExistingUser_ReturnsUser()
        {
            var userToRegister = new Common.Entities.User
            {
                Username = "Test4",
                Password = "Test"
            };
            var service = new DataService();
            service.RegisterUser(userToRegister, APPKEY);
        }

        [TestMethod]
        [ExpectedException(typeof(UserExistsFault))]
        public void RegisterUser_WithValidExistingUser_ThrowException()
        {
            var userToRegister = new User
            {
                Username = "Test",
                Password = "Test"
            };
            var service = new DataService();
            service.RegisterUser(userToRegister, APPKEY);
        }
    }
}
