using AutoMapper;
using RankingService.Application.Models;
using RankingService.Domain.Entities;

namespace RankingService.Application.Mapping.PostsMapping
{
    public class PostsProfile : Profile
    {
        public PostsProfile()
        {
            Add();
        }

        private void Add()
        {
            CreateMap<PostSnapshot, TopTenPosts>();
        }
    }


}
