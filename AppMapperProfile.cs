using AutoMapper;
using InsertKph.Models;
using InsertKph.Dtos;

namespace InsertKph
{
    public class AppMapperProfile: Profile
    {
        public AppMapperProfile()
        {
            CreateMap<IpnDto, IpdProgressNote>();
            CreateMap<IpniDto, IpdProgressNoteItem>();
        }
    }
}