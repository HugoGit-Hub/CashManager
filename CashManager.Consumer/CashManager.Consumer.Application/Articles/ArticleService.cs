using CashManager.Consumer.Domain.Articles;
using CashManager.Consumer.Infrastructure.Articles;


namespace CashManager.Consumer.Application.Articles;

public class ArticleService : IArticleService
{

    private readonly IArticleRepository _articleRepository;

    public ArticleService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public Task<Article?> Get(int id, CancellationToken cancellationToken)
    {
        return _articleRepository.Get(id, cancellationToken);
    }

    public Task<IEnumerable<Article>> GetAll(CancellationToken cancellationToken)
    {
        return _articleRepository.GetAll(cancellationToken);
    }
}
