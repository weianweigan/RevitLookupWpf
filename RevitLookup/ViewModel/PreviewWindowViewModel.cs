using CommunityToolkit.Mvvm.ComponentModel;
using HelixToolkit.Wpf.SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitLookupWpf.ViewModel
{
    public class PreviewWindowViewModel : ObservableObject
    {
        public EffectsManager EffectsManager { get; }

        public Camera Camera { get; }

        public PreviewWindowViewModel()
        {
            EffectsManager = new DefaultEffectsManager();
            Camera = new PerspectiveCamera();
        }

    }
}
