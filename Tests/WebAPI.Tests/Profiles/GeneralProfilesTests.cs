using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using WebAPI.Profiles;
using Xunit;

namespace UnitTests.Components.Profiles
{
    public class GeneralProfilesTests
    {
        private readonly MapperConfiguration _config;

        public GeneralProfilesTests()
        {
            IEnumerable<Profile> GetProfiles()
            {
                var assembly = Assembly.GetAssembly(typeof(TodoProfile));

                var profiles = assembly!.GetExportedTypes()
                    .Where(t => t.BaseType == typeof(Profile))
                    .Select(type => (Profile) Activator.CreateInstance(type))
                    .ToList();

                return profiles;
            }
            
            
            _config = new MapperConfiguration(cfg => cfg.AddProfiles(GetProfiles()));
        }

        [Fact]
        public void ShouldConfigurationBeValid()
        {
            _config.AssertConfigurationIsValid();
        }
    }
}