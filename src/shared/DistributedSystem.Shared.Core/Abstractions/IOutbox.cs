using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Shared.Core.Abstractions;
public interface IOutbox
{
    Task PublishAsync<T>(string eventName, T message, CancellationToken cancellationToken);

    Task PublishDelayAsync<T>(TimeSpan delay, string eventName, T message, CancellationToken cancellationToken);

    void Publish<T>(string eventName, T message);
}

