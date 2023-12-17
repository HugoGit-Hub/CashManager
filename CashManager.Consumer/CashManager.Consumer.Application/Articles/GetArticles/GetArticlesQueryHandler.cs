using CashManager.Consumer.Domain.Articles;
using CashManager.Consumer.Domain.ErrorHandling;
using Mapster;
using MediatR;

namespace CashManager.Consumer.Application.Articles.GetArticles;

internal class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, Result<IEnumerable<ArticleResponse>>>
{
    private readonly IArticleService _articleService;

    public GetArticlesQueryHandler(IArticleService articleService)
    {
        _articleService = articleService;
    }

    public async Task<Result<IEnumerable<ArticleResponse>>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
    {
        var articles = await _articleService.GetAll(cancellationToken);

        return Result<IEnumerable<ArticleResponse>>.Success(articles.Adapt<IEnumerable<ArticleResponse>>());
    }
}