using CashManager.Consumer.Domain.Articles;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Consumer.Api.Controllers;

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
        var result = await _articleService.Get(id, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet(nameof(GetAll))]
    public async Task<ActionResult<IEnumerable<Article>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _articleService.GetAll(cancellationToken);
        
        return Ok(result);
    }
}