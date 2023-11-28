namespace CashManager.Consumer.Domain.Articles;

public interface IArticleRepository
{
    public Task<Article?> Get(int id, CancellationToken cancellationToken);
    public Task<IEnumerable<Article>> GetAll(CancellationToken cancellationToken);
}
