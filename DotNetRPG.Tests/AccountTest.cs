using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetRPG.Models;
using Xunit;

namespace DotNetRPG.Tests
{
    public class AccountTest
    {
       [Fact]
       public void GetUserNameTest()
        {
            var user = new ApplicationUser();
            user.UserName = "TestUser";
            var result = user.UserName;
            Assert.Equal("TestUser", result);
        }
    }
}
