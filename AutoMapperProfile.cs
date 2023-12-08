using AutoMapper;
using StockTracking.DTOs.Auth;
using StockTracking.DTOs.Solicitation;
using StockTracking.DTOs.Stock;
using StockTracking.Models;

namespace StockTracking
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, EmployeeDTO>();

            CreateMap<Stock, StockDTO>();
            
            CreateMap<StockItemEquipment, StockItemEquipmentDTO>();
            CreateMap<StockItemEquipment[], List<StockItemEquipmentDTO>>();

            CreateMap<StockItemMaterial, StockItemMaterialDTO>();
            CreateMap<StockItemMaterial[], List<StockItemMaterialDTO>>();

            CreateMap<CreateSolicitationDTO, Solicitation>();
            CreateMap<Solicitation, SolicitationDTO>();

            CreateMap<SolicitationItemDTO, SolicitationItem>();
            CreateMap<SolicitationItem, SolicitationItemDTO>();
        }
    }
}
