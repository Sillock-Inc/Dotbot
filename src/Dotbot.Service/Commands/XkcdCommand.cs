using MediatR;

namespace Dotbot.Service.Commands;

public class XkcdCommand : IRequest<bool>
{
    public int ComicNumber { get; private set; }

    public XkcdCommand(int comicNumber)
    {
        ComicNumber = comicNumber;
    }
}