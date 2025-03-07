﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Api.StartUp
{
  /// <summary>
  /// Api Swagger
  /// </summary>
  public static class Swagger
  {
    /// <summary>
    /// Register swagger
    /// </summary>
    /// <param name="services">Service collection</param>
    public static void AddSwagger(this IServiceCollection services)
    {
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API" });
      });
    }

    /// <summary>
    /// Configure Swagger
    /// </summary>
    /// <param name="app">App builder</param>
    public static void ConfigureSwagger(this IApplicationBuilder app)
    {
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
      });
    }
  }
}
