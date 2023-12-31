﻿using CashManager.Consumer.Domain.ErrorHandling;
using MediatR;

namespace CashManager.Consumer.Application.Articles.GetArticleById;

public record GetArticleByIdQuery(int Id) : IRequest<Result<ArticleResponse>>;