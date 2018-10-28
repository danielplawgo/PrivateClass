using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using FluentAssertions;
using Xunit;

namespace PrivateClass.Web.UnitTests.Extensions
{
    public class ControllerExtensionsTest
    {
        private string _claimName = "userId";
        private int _claimValue = 123;

        private TestController Create()
        {
            var controller = new TestController();

            controller.User = new ClaimsPrincipal(new ClaimsIdentity(new [] {new Claim(_claimName, _claimValue.ToString())}));

            return controller;
        }
        [Fact]
        public void GetUserClaim_Return_Null_When_User_Is_Null()
        {
            var controller = Create();
            controller.User = null;

            var claim = controller.GetUserClaim("claimName");

            claim.Should().BeNull();
        }

        [Fact]
        public void GetUserClaim_Return_Null_When_Claim_Does_Not_Exist()
        {
            var controller = Create();

            var claim = controller.GetUserClaim("claimName");

            claim.Should().BeNull();
        }

        [Fact]
        public void GetUserClaim_Return_Claim()
        {
            var controller = Create();

            var claim = controller.GetUserClaim(_claimName);

            claim.Should().NotBeNull();
            claim.Should().Be(_claimValue.ToString());
        }

        [Fact]
        public void Generic_GetUserClaim_Return_Claim()
        {
            var controller = Create();

            var claim = controller.GetUserClaim<int>(_claimName);

            claim.Should().Be(_claimValue);
        }

        private class TestController : ApiController
        {

        }
    }
}
