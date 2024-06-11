using Autofac;
using Autofac.Extensions.DependencyInjection;
using Cinema.Repository;
using Cinema.Repository.Common;
using Cinema.Service;
using Cinema.Service.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

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
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();