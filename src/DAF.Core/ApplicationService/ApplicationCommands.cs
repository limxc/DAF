using Prism.Commands;

namespace DAF.Core.ApplicationService
{
    public class ApplicationCommands : IApplicationCommands
    {
        public CompositeCommand RequestClose { get; } = new();
        public CompositeCommand Save { get; } = new();
    }
}