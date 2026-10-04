using AutoMapper;
using RankingService.Application.Models;
using RankingService.Domain.Entities;

namespace RankingService.Application.Mapping.CommentMapping
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            Add();
        }
        private void Add()
        {
            CreateMap<CommentSnapshot, TopTenComments>();
        }
    }
}
