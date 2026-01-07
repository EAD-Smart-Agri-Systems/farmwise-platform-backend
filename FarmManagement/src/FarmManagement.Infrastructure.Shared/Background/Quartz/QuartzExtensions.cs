using FarmManagement.Infrastructure.Shared.Background.Jobs;
using FarmManagement.Infrastructure.Shared.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace FarmManagement.Infrastructure.Shared.Background.Quartz;

public static class QuartzExtensions
{
    public static IServiceCollection AddQuartzServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddQuartz(q =>
        {

            // Configure outbox processor job
            var outboxJobKey = new JobKey(nameof(OutboxProcessorJob));
            q.AddJob<OutboxProcessorJob>(opts => opts.WithIdentity(outboxJobKey));

            var outboxOptions = configuration.GetSection(OutboxOptions.SectionName).Get<OutboxOptions>() 
                ?? new OutboxOptions();

            q.AddTrigger(opts => opts
                .ForJob(outboxJobKey)
                .WithIdentity($"{nameof(OutboxProcessorJob)}-trigger")
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(outboxOptions.IntervalSeconds)
                    .RepeatForever()));
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        return services;
    }
}

