using Dotbot.Infrastructure.DTOs;
using FluentResults;

namespace Dotbot.Infrastructure.Services;

public interface IXkcdService
{
    Task<Result<XkcdComicDto>> GetLatestComic();
    Task<Result<XkcdComicDto>> GetComic(int? number = null);
    Task<Result<int>> GetLast();
    Task SetLast(int last);
}