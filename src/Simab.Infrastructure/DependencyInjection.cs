using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Simab.Application.Common.Interfaces;
using Simab.Infrastructure.Persistence;
using Simab.Infrastructure.Persistence.Repositories;

namespace Simab.Infrastructure;

/// <summary>
/// Dependency injection configuration for Infrastructure layer
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<SimabDbContext>(options =>
            options.UseSqlServer(connectionString));
            
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IEvaluationRepository, EvaluationRepository>();
        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IEvaluatorRepository, EvaluatorRepository>();
        services.AddScoped<IWorkflowRepository, WorkflowRepository>();
        
        return services;
    }
}
