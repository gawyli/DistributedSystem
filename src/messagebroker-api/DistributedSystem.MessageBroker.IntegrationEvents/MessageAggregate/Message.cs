using DistributedSystem.Shared.Core.Entities;

namespace DistributedSystem.MessageBroker.Core.MessageAggregate;
public class Message : BaseEntity
{
    public string EventId { get; set; }
    public string EventName { get; set; }

    public Message(string id, string eventId, string eventName)
    {
        this.Id = id;
        this.EventId = eventId;
        this.EventName = eventName;
    }

}
