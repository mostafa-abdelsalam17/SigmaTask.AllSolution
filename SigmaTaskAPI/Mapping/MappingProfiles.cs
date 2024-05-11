using AutoMapper;
using DataAccess.Layer.Models;
using SigmaTaskAPI.DTOs;

namespace SigmaTaskAPI.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<CandidateDto, Candidate>().ReverseMap();
        }
    }
}
