using CashManager.Consumer.Domain.Articles;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Consumer.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticleController : Controller
{
    private readonly IArticleService _articleService;

    public ArticleController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [HttpGet(nameof(Get))]
    public async Task<ActionResult<Article>> Get(int id, CancellationToken cancellationToken)
    {
        var article = await _articleService.Get(id, cancellationToken);
        if (article == null)
        {
            return NotFound();
        }

        return Ok(article);
    }

    [HttpGet(nameof(GetAll))]
    public async Task<ActionResult<IEnumerable<Article>>> GetAll(CancellationToken cancellationToken)
    {
        var articles = await _articleService.GetAll(cancellationToken);
        
        return Ok(articles);
    }
}