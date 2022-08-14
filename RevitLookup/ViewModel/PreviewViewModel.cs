using CommunityToolkit.Mvvm.ComponentModel;
using HelixToolkit.Wpf.SharpDX;

namespace RevitLookupWpf.ViewModel
{
    public class PreviewViewModel : ObservableObject
    {
        public PreviewViewModel()
        {
            EffectsManager = new EffectsManager();
            Camera = new PerspectiveCamera();
        }

        public EffectsManager EffectsManager { get; }

        public Camera Camera { get; }
    }
}
