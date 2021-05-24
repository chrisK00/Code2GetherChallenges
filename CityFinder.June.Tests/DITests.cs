using System;
using Xunit;
using CityFinder.June;
using Microsoft.Extensions.Hosting;

namespace CityFinder.June.Tests
{
    public class DITests
    {
        [Fact]
        public void VerifyDISetup()
        {
            Program.CreateHostBuilder(Array.Empty<string>())
                .UseDefaultServiceProvider(config => config.ValidateOnBuild = true).Build();
        }

        [Fact]
        public void VerifyDIScopes()
        {
            Program.CreateHostBuilder(Array.Empty<string>())
                .UseDefaultServiceProvider(config => config.ValidateScopes = true).Build();
        }
    }
}
