using CashManager.Consumer.Domain.ErrorHandling;

namespace CashManager.Consumer.Domain.Articles;

public static class ArticleErrors
{
    public static readonly Error ArticleNotFound = new(
        "Article.NotFound", "Unable to found article");
}