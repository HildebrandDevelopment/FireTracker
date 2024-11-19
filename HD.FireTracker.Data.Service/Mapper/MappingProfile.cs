using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainDTO = HD.FireTracker.Common.DTO.DomainObjects;
using DBModel = HD.FireTracker.Data.Common.Models;

namespace HD.FireTracker.Data.Service.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DBModel.FireTrackerDB.RecurringProcess, DomainDTO.RecurringProcessDTO>().ReverseMap();
            CreateMap<DBModel.FireTrackerDB.RecurringProcessDetail, DomainDTO.RecurringProcessDetailDTO>().ReverseMap();
            CreateMap<DBModel.FireTrackerDB.RecurringProcessDetailProjection, DomainDTO.RecurringProcessDetailProjectionDTO>().ReverseMap();
            CreateMap<DBModel.FireTrackerDB.RecurringProcessSummary, DomainDTO.RecurringProcessSummaryDTO>().ReverseMap();

        }

    }//end class



}
