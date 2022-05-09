using Caliburn.Micro;

namespace WPF.Common.Factories
{
    internal interface IViewModelFactory
    {
        T Create<T>() => IoC.Get<T>();
    }
}
