using System;
using System.IO;
using System.Linq;
using BD.Contexts;
using Core.Repositories;
using Core.Services;
using CoreGraphQL.Resolvers;
using CoreGraphQL.Types.Client;
using CoreGraphQL.Types.User;
using GraphQL;
using GraphQL.Types;
using JwtSecurity.Classes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Repositories.Repositories;
using Services.Services;

namespace ServiceCollections
{
    public static class ServiceCollectionExtension
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Entities>(options =>
            {
                options
                    //.UseLazyLoadingProxies()
                    .UseMySql(configuration.GetConnectionString("Entities"));
            });
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            // INJEÇÃO DOS REPOSITORIOS
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            // INJEÇÃO DOS SERIVÇOS
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICriptService, CriptService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IClientService, ClientService>();
        }

        public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            var signingConfigurations = new SigningConfigurations(tokenConfigurations.SecretKey);
            services.AddSingleton(signingConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Obriga o token ter tempo de expiração
                paramsValidation.RequireExpirationTime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.FromSeconds(30);
            });

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorizationCore(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }

        public static void AddResolvers(this IServiceCollection services)
        {
            // ADD DOS RESOLVERS
            services.AddScoped<ClientResolvers>();
            services.AddScoped<UserResolvers>();
        }

        public static void AddGraphQlService(this IServiceCollection services, IWebHostEnvironment hostingEnvironment)
        {
            // ARQUIVOS DO GRAPHQL
            var rootPath = hostingEnvironment.WebRootPath;

            var schemasDirectory = Path.Combine(rootPath, "schemas");

            var graphqlFiles = Directory.EnumerateFiles(schemasDirectory).Select(File.ReadAllText).ToArray();

            // ADD DO EXECUTER
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();

            // ADD DO SERVICE
            services.AddScoped<DbLoggerCategory.Query>();

            // ADD DOS TYPES
            services.AddScoped<UserType>();
            services.AddScoped<ClientType>();


            // ADD DOS SCHEMAS
            services.AddScoped(provider =>
            {
                return Schema.For(graphqlFiles, builder =>
                {
                    builder.Types.Include<UserType>();
                    builder.Types.Include<ClientType>();
                    builder.Types.Include<DbLoggerCategory.Query>();
                    builder.DependencyResolver = new FuncDependencyResolver(provider.GetRequiredService);
                });
            });
        }
    }
}
