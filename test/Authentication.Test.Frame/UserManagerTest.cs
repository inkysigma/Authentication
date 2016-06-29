using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Frame;
using Authentication.Frame.Stores;
using Moq;
using Xunit;

namespace Authentication.Test.Frame
{
    public class UserManagerTest
    {
        private UserManager<TestUser, TestToken> Manager { get; }

        public UserManagerTest()
        {
            var moqEmailStore = new Mock<IUserEmailStore<TestUser>>();
            Manager = new UserManager<TestUser, TestToken>();
        }
    }
}
