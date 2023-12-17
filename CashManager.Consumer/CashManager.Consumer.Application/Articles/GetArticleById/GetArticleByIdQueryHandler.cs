using CashManager.Consumer.Domain.Articles;
using CashManager.Consumer.Domain.ErrorHandling;
using Mapster;
using MediatR;

namespace CashManager.Consumer.Application.Articles.GetArticleById;

internal class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, Result<ArticleResponse>>
{
    private readonly IArticleService _articleService;

    public GetArticleByIdQueryHandler(IArticleService articleService)
    {
        _articleService = articleService;
    }

    public async Task<Result<ArticleResponse>> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
    {
        var article = await _articleService.Get(request.Id, cancellationToken);
        
        return article.IsFailure 
            ? Result<ArticleResponse>.Failure(article.Error) 
            : Result<ArticleResponse>.Success(article.Value.Adapt<ArticleResponse>());
    }
}