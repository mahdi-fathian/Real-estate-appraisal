using AutoMapper;
using Simab.Application.Common.Dtos;
using Simab.Domain.Entities;
using Simab.Domain.Enums;

namespace Simab.Application.Common.Mappings;

/// <summary>
/// AutoMapper profile for entity to DTO mappings
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Evaluation, EvaluationDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.EstimatedAmount, opt => opt.MapFrom(src => src.EstimatedValue.Amount))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.EstimatedValue.Currency))
            .ForMember(dest => dest.HasDigitalSignature, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.DigitalSignature)))
            .ForMember(dest => dest.DocumentCount, opt => opt.MapFrom(src => src.Documents.Count))
            .ForMember(dest => dest.VisitCount, opt => opt.MapFrom(src => src.Visits.Count));
    }
}
