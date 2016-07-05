using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Frame;
using Authentication.Frame.Stores;
using Authentication.Test.Frame.Setup;
using Moq;
using Xunit;

namespace Authentication.Test.Frame
{
    public class UserManagerCreateTest
    {
        [Fact]
        public void CreateNull()
        {
            Assert.Throws<ArgumentNullException>(() => new UserManager<TestUser, TestClaim, TestLogin>(null, null));
        }
    }
}
