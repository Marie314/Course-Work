using Caliburn.Micro;

namespace WPF.Common.Factories.Impl
{
    internal class ViewModelFactory : IViewModelFactory
    {
        public T Create<T>() => IoC.Get<T>();
    }
}
