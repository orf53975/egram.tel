using System.Reactive.Disposables;
using PropertyChanged;
using ReactiveUI;
using Tel.Egram.Graphics;
using Tel.Egram.Messaging.Chats;

namespace Tel.Egram.Components.Messenger.Informer
{
    [AddINotifyPropertyChangedInterface]
    public class InformerModel : ISupportsActivation
    {
        public bool IsVisible { get; set; } = true;
        
        public string Title { get; set; }
        
        public string Label { get; set; }
        
        public Avatar Avatar { get; set; }
        
        public InformerModel(Target target)
        {
            this.WhenActivated(disposables =>
            {
                this.BindInformer(target)
                    .DisposeWith(disposables);
            });
        }

        private InformerModel()
        {
        }
        
        public static InformerModel Hidden()
        {
            return new InformerModel
            {
                IsVisible = false
            };
        }
        
        public ViewModelActivator Activator { get; } = new ViewModelActivator();
    }
}