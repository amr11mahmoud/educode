namespace Educode.Core.Interfaces.Commands
{
    public interface ICommandHttpContext<T>
    {
        T HttpContext { get; set; }
    }
}
