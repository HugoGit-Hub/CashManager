using CashManager.Consumer.Domain.Articles;
using CashManager.Consumer.Domain.ErrorHandling;
using MediatR;

namespace CashManager.Consumer.Application.Articles.GetArticleById;

internal class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, Result<Article>>
{
    private readonly IArticleService _articleService;

    public GetArticleByIdQueryHandler(IArticleService articleService)
    {
        _articleService = articleService;
    }

    public async Task<Result<Article>> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
    {
        var article = await _articleService.Get(request.Id, cancellationToken);
        
        return article.IsFailure 
            ? Result<Article>.Failure(article.Error) 
            : Result<Article>.Success(article.Value);
    }
}