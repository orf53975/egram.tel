﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Tel.Egram.Components.Workspace.Navigation;

namespace Tel.Egram.Gui.Views.Workspace.Navigation
{
    public class NavigationControl : BaseControl<NavigationModel>
    {
        public NavigationControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
