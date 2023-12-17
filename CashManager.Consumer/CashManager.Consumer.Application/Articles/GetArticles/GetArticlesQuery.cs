using CashManager.Consumer.Domain.ErrorHandling;
using MediatR;

namespace CashManager.Consumer.Application.Articles.GetArticles;

public record GetArticlesQuery : IRequest<Result<IEnumerable<ArticleResponse>>>;
