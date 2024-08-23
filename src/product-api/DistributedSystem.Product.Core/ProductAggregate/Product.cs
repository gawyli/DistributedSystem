﻿using DistributedSystem.Shared.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Product.Core.ProductAggregate;

public enum StockLevel
{
    OutOfStock,
    Low,
    Medium,
    High
}

public class Product : BaseAggregateRoot
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public StockLevel StockLevel => GetStockLevel();
    
    public SaleOffer? SaleOffer { get; private set; }

    public Product(string name, decimal price, int quantity)
    {
        this.Name = name;
        this.Price = price;
        this.Quantity = quantity;
    }

    public void SetPrice(decimal price)
    {
        this.Price = price;
    }

    public void SetSaleOffer(SaleOffer saleOffer)
    {
        this.SaleOffer = saleOffer;
    }

    public void AddQuantity(int quantity)
    {
        this.Quantity += quantity;
    }

    public decimal GetDiscountPrice()
    {
        if (SaleOffer != null)
        {
            return Price - (Price * (SaleOffer.Discount / 100));
        }

        return Price;
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
