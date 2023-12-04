using CashManager.Consumer.Domain.Articles;
using CashManager.Consumer.Domain.ErrorHandling;

namespace CashManager.Consumer.Application.Articles;

internal class ArticleService : IArticleService
{

    private readonly IArticleRepository _articleRepository;

    public ArticleService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<Result<Article>> Get(int id, CancellationToken cancellationToken)
    {
        var result = await _articleRepository.Get(id, cancellationToken);
        
        return result is null 
            ? Result<Article>.Failure(ArticleErrors.ArticleNotFound)
            : Result<Article>.Success(result);
    }

    public Task<IEnumerable<Article>> GetAll(CancellationToken cancellationToken)
    {
        return _articleRepository.GetAll(cancellationToken);
    }
}
