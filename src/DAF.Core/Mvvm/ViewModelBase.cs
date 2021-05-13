using Prism.Mvvm;
using Prism.Navigation;

namespace DAF.Core.Mvvm
{
    public abstract class ViewModelBase : BindableBase, IDestructible
    {
        public virtual void Destroy()
        {
        }
    }
}