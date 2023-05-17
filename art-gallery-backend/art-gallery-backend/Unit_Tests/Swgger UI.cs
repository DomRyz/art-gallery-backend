using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Xunit;

namespace art_gallery_backend.Unit_Tests
{
    public class SwaggerTests
    {
        [Fact]
        public void TestSwaggerGenOptions()
        {
            // Arrange
            var builder = new WebHostBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddDateOnlyTimeOnlyStringConverters();
                    services.AddEndpointsApiExplorer();
                    services.AddSwaggerGen(options =>
                    {
                        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                        options.SwaggerDoc("v1", new OpenApiInfo
                        {
                            Title = "DDGCIT Gallery API",
                            Version = "1.0.0",
                            Description = "A new backend service for the DDGCIT Art Gallery.",
                            Contact = new OpenApiContact
                            {
                                Name = "John Doe",
                                Email = "jdoe@deakin.edu.au"
                            }
                        });
                        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                        options.UseDateOnlyTimeOnlyStringConverters();
                    });
                });

            var host = builder.Build();

            // Act
            // Perform your test actions here

            // Assert
            // Add your assertions here
        }
    }
}
