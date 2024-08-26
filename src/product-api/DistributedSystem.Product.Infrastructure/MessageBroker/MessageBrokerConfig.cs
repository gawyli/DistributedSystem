using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Product.Infrastructure.MessageBroker;
public class MessageBrokerConfig
{
    public string DocumentDb { get; set; } = null!;

    public MessageBrokerConfig()
    {        
    }

    public static MessageBrokerConfig New(IConfiguration config)
    {
        MessageBrokerConfig messageBrokerConfig = new();
        config.GetSection("MessageBroker").Bind(messageBrokerConfig);

        return messageBrokerConfig;
    }
}
