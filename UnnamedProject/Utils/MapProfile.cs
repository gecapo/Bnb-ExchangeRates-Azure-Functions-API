using System;
using AutoMapper;

namespace UnnamedProject
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<BnbExcangeRatesResponse, BnbExcangeRates>()
                .ForMember(destinationEntity => destinationEntity.CurrencyCode, options => options.MapFrom(sourceEntity => sourceEntity.Code))
                .ForMember(destinationEntity => destinationEntity.CurrencyName, options => options.MapFrom(sourceEntity => sourceEntity.Name))
                .ForMember(destinationEntity => destinationEntity.ReportDate, options => options.MapFrom(sourceEntity => DateTime.ParseExact(sourceEntity.Date, "dd.MM.yyyy", null)))
                .ForMember(destinationEntity => destinationEntity.Rate, options => options.MapFrom(sourceEntity => Convert.ToDecimal(sourceEntity.Rate)))
                .ForMember(destinationEntity => destinationEntity.Ratio, options => options.MapFrom(sourceEntity => Convert.ToInt32(sourceEntity.Ratio)))
                .ForMember(destinationEntity => destinationEntity.ReverseRate, options => options.MapFrom(sourceEntity => Convert.ToDecimal(sourceEntity.ReverseRate)));
        }
    }
}

