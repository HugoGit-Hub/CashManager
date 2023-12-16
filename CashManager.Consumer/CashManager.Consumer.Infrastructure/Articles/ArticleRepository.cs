using CashManager.Consumer.Application.Articles;
using CashManager.Consumer.Domain.Articles;
using CashManager.Consumer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CashManager.Consumer.Infrastructure.Articles;

public class ArticleRepository : IArticleRepository
{
    private readonly CashManagerConsumerContext _context;

    public ArticleRepository(CashManagerConsumerContext context)
    {
        _context = context;
    }

    public async Task<Article?> Get(int id, CancellationToken cancellationToken)
    {
        return await _context.Articles.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Article>> GetAll(CancellationToken cancellationToken)
    {
       return await _context.Articles.ToListAsync(cancellationToken);
    }
}
