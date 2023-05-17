using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace YourNamespace.Tests
{
    public class AuthorizationTests
    {
        [Fact]
        public void AuthorizationPolicies_ContainUserOnlyPolicy()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserOnly", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "User"));
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
            });

            // Act
            var serviceProvider = services.BuildServiceProvider();
            var authorizationService = serviceProvider.GetService<IAuthorizationService>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "User") }));
            var userOnlyPolicy = authorizationService.AuthorizeAsync(user, null, "UserOnly").GetAwaiter().GetResult();

            // Assert
            Assert.True(userOnlyPolicy.Succeeded);
        }

        [Fact]
        public void AuthorizationPolicies_ContainAdminOnlyPolicy()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserOnly", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "User"));
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
            });

            // Act
            var serviceProvider = services.BuildServiceProvider();
            var authorizationService = serviceProvider.GetService<IAuthorizationService>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "Admin") }));
            var adminOnlyPolicy = authorizationService.AuthorizeAsync(user, null, "AdminOnly").GetAwaiter().GetResult();

            // Assert
            Assert.True(adminOnlyPolicy.Succeeded);
        }

        [Fact]
        public void SecurityScheme_UsesBearerAuthorization()
        {
            // Arrange
            var securityScheme = new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
            };

            // Act
            var schemeType = securityScheme.Type;
            var schemeScheme = securityScheme.Scheme;

            // Assert
            Assert.Equal(SecuritySchemeType.Http, schemeType);
            Assert.Equal("bearer", schemeScheme);
        }

        [Fact]
        public void SecurityRequirement_ReferencesBearerSecurityScheme()
        {
            // Arrange
            var securityRequirement = new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "bearer"
                        }
                    },
                    new List<string>()
                }
            };

            // Act
            var reference = securityRequirement.First().Key.Reference;
            var referenceType = reference.Type;
            var referenceId = reference.Id;

            // Assert
            Assert.Equal(ReferenceType.SecurityScheme, referenceType);
            Assert.Equal("bearer", referenceId);
        }
    }
}
