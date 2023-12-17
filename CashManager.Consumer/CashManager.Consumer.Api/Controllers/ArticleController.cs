using CashManager.Consumer.Application.Articles;
using CashManager.Consumer.Application.Articles.GetArticleById;
using CashManager.Consumer.Application.Articles.GetArticles;
using CashManager.Consumer.Domain.Articles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Consumer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticleController : Controller
{
    private readonly IArticleService _articleService;
    private readonly ISender _sender;

    public ArticleController(IArticleService articleService, ISender sender)
    {
        _articleService = articleService;
        _sender = sender;
    }

    [HttpGet(nameof(GetById))]
    public async Task<ActionResult<ArticleResponse>> GetById(int id)
    {
        var handler = await _sender.Send(new GetArticleByIdQuery(id));
        if (handler.IsFailure)
        {
            return BadRequest(handler.Error);
        }

        return Ok(handler.Value);
    }

    [HttpGet(nameof(GetAll))]
    public async Task<ActionResult<IEnumerable<ArticleResponse>>> GetAll()
    {
        var handler = await _sender.Send(new GetArticlesQuery());
        if (handler.IsFailure)
        {
            return BadRequest(handler.Error);
        }

        return Ok(handler.Value);
    }
}