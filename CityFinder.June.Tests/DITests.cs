using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CityFinder.June.Tests
{
    public class DITests
    {
        [Fact]
        public void VerifyDISetup()
        {
            Program.ConfigureServices()
                .BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true });
        }

        [Fact]
        public void VerifyDIScopes()
        {
            Program.ConfigureServices()
                  .BuildServiceProvider(new ServiceProviderOptions { ValidateScopes = true });
        }
    }
}
