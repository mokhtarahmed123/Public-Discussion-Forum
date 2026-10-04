using FluentValidation;
using MongoDB.Bson;
using RankingService.Application.Feature.Comments.Query.Model;

namespace RankingService.Application.Feature.Comments.Query.Validator
{
    public class GetTopTenCommentByPostIdQueryValidator : AbstractValidator<GetTopTenCommentByPostIdQuery>
    {
        public GetTopTenCommentByPostIdQueryValidator()
        {
            RuleFor(x => x.PostId)
                .NotEmpty()
                .Must(id => ObjectId.TryParse(id, out _))
                .WithMessage("رقم البوست غير صحيح.");
        }
    }
}
