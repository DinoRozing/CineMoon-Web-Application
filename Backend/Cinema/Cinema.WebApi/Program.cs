using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Cinema.Mapper;
using Cinema.Repository;
using Cinema.Repository.Common;
using Cinema.Service;
using Cinema.Service.Common;
using Cinema.WebApi.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
string connectionString = builder.Configuration.GetSection("AppSettings").GetValue<String>("ConnectionString");

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new UserProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder
        .RegisterType<MovieService>()
        .As<IMovieService>()
        .InstancePerLifetimeScope();

    containerBuilder
        .RegisterType<MovieRepository>()
        .As<IMovieRepository>()
        .InstancePerLifetimeScope();
    containerBuilder
        .RegisterType<TicketService>()
        .As<ITicketService>()
        .InstancePerLifetimeScope();

    containerBuilder
        .RegisterType<TicketRepository>()
        .As<ITicketRepository>()
        .InstancePerLifetimeScope();

    containerBuilder
        .RegisterType<ProjectionService>()
        .As<IProjectionService>()
        .InstancePerLifetimeScope();

    containerBuilder
        .RegisterType<ProjectionRepository>()
        .As<IProjectionRepository>()
        .InstancePerLifetimeScope();
    containerBuilder
        .RegisterType<UserService>()
        .As<IUserService>()
        .InstancePerLifetimeScope();

    containerBuilder
        .RegisterType<UserRepository>()
        .As<IUserRepository>()
        .InstancePerLifetimeScope();

    containerBuilder.RegisterInstance(connectionString).As<string>();
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();