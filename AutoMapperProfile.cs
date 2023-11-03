using AutoMapper;
using StockTracking.DTOs.Stock;
using StockTracking.Models;

namespace StockTracking
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Stock, StockDTO>();
            CreateMap<StockItem, StockItemDTO>();
            CreateMap<StockItem[], List<StockItemDTO>>();
                
        }
    }
}
