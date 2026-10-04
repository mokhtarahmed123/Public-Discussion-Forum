using FluentValidation;
using MongoDB.Bson;
using RankingService.Application.Feature.Comments.Command.Model;

public class AddTopTenCommentByPostIdCommandValidator : AbstractValidator<AddTopTenCommentByPostIdCommand>
{
    public AddTopTenCommentByPostIdCommandValidator()
    {
        RuleFor(x => x.PostId)
            .NotEmpty()
            .Must(id => ObjectId.TryParse(id, out _))
            .WithMessage("رقم البوست غير صحيح.");

        RuleFor(x => x.PageNumber).GreaterThan(0);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 50);
    }
}