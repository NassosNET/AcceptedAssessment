using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesAPI.MappingProfile
{
    public class GamesProfile: Profile
    {
        public GamesProfile()
        {

            CreateMap<GamesAPI.DB.Match, GamesAPI.DTOs.MatchDto>()
                .ForMember(db => db.MatchOdds, dtoMtch => dtoMtch.MapFrom(dto => dto.MatchOdds));
            CreateMap<GamesAPI.DTOs.MatchDto, GamesAPI.DB.Match>()
                .ForMember(db => db.MatchOdds, dtoMtch => dtoMtch.MapFrom(dto => dto.MatchOdds));
            CreateMap<GamesAPI.DB.MatchOdds, GamesAPI.DTOs.MatchOddsDto>();
            CreateMap<GamesAPI.DTOs.MatchOddsDto, GamesAPI.DB.MatchOdds>();
        }
    }
}
