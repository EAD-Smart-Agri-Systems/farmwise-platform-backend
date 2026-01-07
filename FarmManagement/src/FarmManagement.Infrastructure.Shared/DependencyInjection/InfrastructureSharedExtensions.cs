using FarmManagement.Infrastructure.Shared.Background.Quartz;
using FarmManagement.Infrastructure.Shared.Configuration;
using FarmManagement.Infrastructure.Shared.Messaging.RabbitMQ;
using FarmManagement.Infrastructure.Shared.Outbox.Abstractions;
using FarmManagement.Infrastructure.Shared.Outbox.Processing;
using FarmManagement.Infrastructure.Shared.Outbox.Persistence;
using FarmManagement.Infrastructure.Shared.Persistence.Abstractions;
using FarmManagement.Infrastructure.Shared.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FarmManagement.Infrastructure.Shared.DependencyInjection;

public static class InfrastructureSharedExtensions
{
    public static IServiceCollection AddInfrastructureShared(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<DbContextOptionsBuilder>? dbContextOptions = null)
    {
        // Register configuration options
        services.Configure<OutboxOptions>(configuration.GetSection(OutboxOptions.SectionName).Bind);
        services.Configure<Configuration.QuartzOptions>(configuration.GetSection(Configuration.QuartzOptions.SectionName).Bind);
        services.Configure<RabbitMQOptions>(configuration.GetSection(RabbitMQOptions.SectionName).Bind);

        // Register RabbitMQ services
        services.AddSingleton<RabbitMQConnectionFactory>();
        services.AddScoped<Messaging.Abstractions.IEventPublisher, RabbitMQEventPublisher>();
        services.AddScoped<Messaging.Abstractions.IEventSubscriber, RabbitMQEventSubscriber>();

        // Register Outbox services
        services.AddScoped<IOutboxMessageRepository, OutboxMessageRepository>();
        services.AddScoped<OutboxProcessor>();

        // Register Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register Quartz services
        services.AddQuartzServices(configuration);

        return services;
    }

    public static IServiceCollection AddInfrastructureSharedDbContext<TContext>(
        this IServiceCollection services,
        string connectionString)
        where TContext : BaseDbContext
    {
        services.AddDbContext<TContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}

