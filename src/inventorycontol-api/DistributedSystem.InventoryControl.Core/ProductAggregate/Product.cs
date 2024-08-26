using DistributedSystem.Shared.Core.Entities;
using DistributedSystem.Shared.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.InventoryControl.Core.ProductAggregate;

public class Product : BaseAggregateRoot
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public StockLevel StockLevel => GetStockLevel();

    public Product(string name, int quantity)
    {
        Name = name;
        Quantity = quantity;
    }

    public bool IsOutStock()
    {
        if (Quantity == 0)
        {
            return true;
        }

        return false;
    }

    public bool IsLowStock()
    {
        if (Quantity > 0 && Quantity < 5)
        {
            return true;
        }

        return false;
    }

    public StockLevel GetStockLevel()
    {
        return this.Quantity switch
        {
            < 1 => StockLevel.OutOfStock,
            < 10 => StockLevel.Low,
            < 100 => StockLevel.Medium,
            _ => StockLevel.High
        };
    }
}
