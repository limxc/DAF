using Prism.Commands;

namespace DAF.Core.ApplicationService
{
    public interface IApplicationCommands
    {
        CompositeCommand RequestClose { get; }
        CompositeCommand Save { get; }
    }
}