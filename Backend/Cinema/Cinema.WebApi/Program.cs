using Autofac;
using Autofac.Extensions.DependencyInjection;
using Cinema.Repository;
using Cinema.Repository.Common;
using Cinema.Service;
using Cinema.Service.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
string connectionString = builder.Configuration.GetSection("AppSettings").GetValue<String>("ConnectionString");

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
        .RegisterType<ActorService>()
        .As<IActorService>()
        .InstancePerLifetimeScope();

    containerBuilder
        .RegisterType<ActorRepository>()
        .As<IActorRepository>()
        .InstancePerLifetimeScope();

    containerBuilder
        .RegisterType<HallService>()
        .As<IHallService>()
        .InstancePerLifetimeScope();

    containerBuilder
        .RegisterType<HallRepository>()
        .As<IHallRepository>()
        .InstancePerLifetimeScope();

    containerBuilder.RegisterInstance(connectionString).As<string>();
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();