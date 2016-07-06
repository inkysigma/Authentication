using System;
using Authentication.Frame;
using Authentication.Frame.Configuration;
using Authentication.Test.Frame.Setup;
using Xunit;

namespace Authentication.Test.Frame
{
    public class UserManager
    {
        [Fact]
        public void CreateNull()
        {
            Assert.Throws<ArgumentNullException>(() => new UserManager<TestUser, TestClaim, TestLogin>(null, null));
            Assert.Throws<ArgumentNullException>(
                () =>
                    new UserManager<TestUser, TestClaim, TestLogin>(
                        new StoreConfiguration<TestUser, TestClaim, TestLogin>(), null));
        }
    }
}
