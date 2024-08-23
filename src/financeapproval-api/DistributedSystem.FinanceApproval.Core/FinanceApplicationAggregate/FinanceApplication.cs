using DistributedSystem.Shared.Core.Entities;

namespace DistributedSystem.FinanceApproval.Api.FinanceApplicationAggregate;

public enum FinanceApplicationStatus
{
    Pending,
    Approved,
    Rejected
}


[Serializable]
public class FinanceApplication : BaseAggregateRoot
{
    public static class Invariants
    {
        // TODO: Add invariants
        public const decimal MaximumAmount = 5000.50M;
        public const decimal MinimumAmount = 19.99M;
    }

    public string CustomerId { get; set; }
    public string ProductId { get; set; }
    public FinanceApplicationStatus Status { get; set; }
    public decimal Amount { get; set; }

    public FinanceApplication(string customerId, string productId, FinanceApplicationStatus status, decimal amount)
    {
        CustomerId = customerId;
        ProductId = productId;
        Status = status;
        Amount = amount;
    }
}
