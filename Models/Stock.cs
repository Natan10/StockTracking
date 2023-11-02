﻿namespace StockTracking.Models
{
    public class Stock : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }

        public List<StockItem> StockItems { get; set; }
    }
}
