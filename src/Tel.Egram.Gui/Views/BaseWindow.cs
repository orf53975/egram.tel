using System;
using System.Reactive.Disposables;
using Avalonia;
using ReactiveUI;

namespace Tel.Egram.Gui.Views
{
    /// <summary>
    /// Workaround for model activation
    /// </summary>
    public class BaseWindow<TViewModel> : ReactiveWindow<TViewModel>
        where TViewModel : class
    {   
        public BaseWindow()
        {
            this.WhenActivated(disposables =>
            {
                Disposable.Create(() => { }).DisposeWith(disposables);
            });
        }
    }
}