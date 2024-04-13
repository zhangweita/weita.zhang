using AutoMapper;
using IPC.Model.DTO;
using IPC.Model.Entity;

namespace IPC.Common.AutoMapper;

public class IPCMapperProfile : Profile
{
    protected IPCMapperProfile()
    {
        // 命名规范约定
        //SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
        //DestinationMemberNamingConvention = new PascalCaseNamingConvention();

        // 清除默认前缀
        ClearPrefixes();


        CreateMap<ApiLogDTO, API_LOG>()
            .ForMember(dest => dest.EQUIPMENT_CODE, opt => opt.MapFrom(src => src.EquipmentCode))
            .ForMember(dest => dest.REQUEST_TIME, opt => opt.MapFrom(src => src.RequestTime))
            .ForMember(dest => dest.ACTION_NAME, opt => opt.MapFrom(src => src.ActionName))
            .ForMember(dest => dest.ACTION_ARGUMENTS, opt => opt.MapFrom(src => src.RequestJson))
            .ForMember(dest => dest.HTTP_RESULT, opt => opt.MapFrom(src => src.ResponseJson))
            .ForMember(dest => dest.MES_DURATION, opt => opt.MapFrom(src => src.MesDuration))
            .ForMember(dest => dest.EXECUTION_DURATION, opt => opt.MapFrom(src => src.TotalDuration))
            .ForMember(dest => dest.EXCEPTION, opt => opt.MapFrom(src => src.Exception))
            .ForMember(dest => dest.SERVER_IP, opt => opt.MapFrom(src => src.ServerIp));
    }
}
