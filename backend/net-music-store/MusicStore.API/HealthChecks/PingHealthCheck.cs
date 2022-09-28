using System.Net.NetworkInformation;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MusicStore.API.HealthChecks;

public class PingHealthCheck : IHealthCheck
{
    private readonly string _host;

    public PingHealthCheck(string host)
    {
        _host = host;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, 
        CancellationToken cancellationToken = new CancellationToken())
    {
        var ping = new Ping();

        HealthCheckResult result;

        try
        {
            var reply = await ping.SendPingAsync(_host);

            switch (reply.Status)
            {
                case IPStatus.Success:
                    result = HealthCheckResult.Healthy("Un éxito!");
                    break;
                case IPStatus.TimedOut:
                    result = HealthCheckResult.Degraded($"El host {_host} es lento");
                    break;
                default:
                    result = HealthCheckResult.Unhealthy($"El host {_host} no funciona");
                    break;
            }
        }
        catch (Exception ex)
        {
            result = HealthCheckResult.Unhealthy("Error General", ex);
        }

        return result;
    }
}