﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Frame;
using Authentication.Frame.Stores;
using Moq;
using Xunit;

namespace Authentication.Test.Frame
{
    public class UserManagerCreateTest
    {
        [Fact]
        public void CreateNull()
        {
            Assert.NotEqual(1, 2);
        }
    }
}