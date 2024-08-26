using DistributedSystem.Shared.Core.Abstractions;
using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Shared.Infrastructure.CapOutbox;
public class CAPOutbox : IOutbox
{
    private readonly ICapPublisher _cap;

    public CAPOutbox(ICapPublisher cap)
    {
        _cap = cap;
    }

    public void Publish<T>(string eventName, T message)
    {
        PublishAsync(eventName, message, default).GetAwaiter().GetResult();
    }

    public async Task PublishAsync<T>(string eventName, T message, CancellationToken cancellationToken)
    {
        await _cap.PublishAsync(eventName, message, cancellationToken: cancellationToken);
    }

    public async Task PublishDelayAsync<T>(TimeSpan delay, string eventName, T message, CancellationToken cancellationToken)
    {
        await _cap.PublishDelayAsync(delay, eventName, message, cancellationToken: cancellationToken);
    }
}
