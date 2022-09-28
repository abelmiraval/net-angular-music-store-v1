using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MusicStore.API.HealthChecks;

public class DiskHealthCheck : IHealthCheck
{
    private readonly IConfiguration _configuration;

    public DiskHealthCheck(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        var driveInfo = new DriveInfo("C:");

        var maxSpace = _configuration.GetValue<long>("StorageConfiguration:FreeDiskSpaceMax");

        var espacioSuficiente = (driveInfo.AvailableFreeSpace >= 
                                 maxSpace);

        return await Task.FromResult(espacioSuficiente
            ? HealthCheckResult.Healthy("Tienes suficiente espacio")
            : HealthCheckResult.Degraded($"Tienes poco espacio: {driveInfo.AvailableFreeSpace:N2} bytes, deberias tener al menos {maxSpace:N2} bytes"));

    }
}