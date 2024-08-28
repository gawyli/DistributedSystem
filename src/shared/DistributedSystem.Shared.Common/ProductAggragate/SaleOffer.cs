using DistributedSystem.Shared.Core.Entities;

namespace DistributedSystem.Shared.Common.Aggregates.ProductAggragate;
public class SaleOffer : BaseEntity
{
    public static class Invariants
    {
        public const int NameMaxLength = 50;
        public const decimal DiscountMaxLength = 60;
        public const bool StartDateIsRequired = true;
    }

    public string Name { get; private set; }
    public int Discount { get; private set; } // in percentage, must be divded by 100 to get the actual discount
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }


    public SaleOffer(string id, string name, int discount, DateTime startDate, DateTime endDate)
    {
        this.Id = id;
        this.Name = name;
        this.Discount = discount;
        this.StartDate = startDate;
        this.EndDate = endDate;
    }
}