using System;
using AutoMapper;

namespace UnnamedProject
{
    public class MapProfile : Profile
    {
        private const string DateFormat = "dd.MM.yyyy";
        public MapProfile()
        {
            CreateMap<BnbExcangeRatesResponse, BnbExcangeRates>()
                .ForMember(destinationEntity => destinationEntity.CurrencyCode, options => options.MapFrom(sourceEntity => sourceEntity.Code))
                .ForMember(destinationEntity => destinationEntity.CurrencyName, options => options.MapFrom(sourceEntity => sourceEntity.Name))
                .ForMember(destinationEntity => destinationEntity.ReportDate, options => options.MapFrom(sourceEntity => DateTime.ParseExact(sourceEntity.Date, DateFormat, null).ToUniversalTime()))
                .ForMember(destinationEntity => destinationEntity.Rate, options => options.MapFrom(sourceEntity => Convert.ToDecimal(sourceEntity.Rate)))
                .ForMember(destinationEntity => destinationEntity.Ratio, options => options.MapFrom(sourceEntity => Convert.ToInt32(sourceEntity.Ratio)))
                .ForMember(destinationEntity => destinationEntity.ReverseRate, options => options.MapFrom(sourceEntity => Convert.ToDecimal(sourceEntity.ReverseRate)))
                .ForMember(destinationEntity => destinationEntity.PartitionKey, options => options.MapFrom(sourceEntity => DateTime.ParseExact(sourceEntity.Date, DateFormat, null).ToUniversalTime().ToString("yyyyMMdd")))
                .ForMember(destinationEntity => destinationEntity.RowKey, options => options.MapFrom(sourceEntity => sourceEntity.Code));
        }
    }
}

