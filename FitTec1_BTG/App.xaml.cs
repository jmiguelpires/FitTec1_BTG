
using System.Runtime.InteropServices;
using WinRT.Interop;

namespace FitTec1_BTG
{
    public partial class App : Application
    {
        public App(MainPage page)
        {
            InitializeComponent();
            MainPage = new NavigationPage(page);
        }

        //dll para manipulação da janela nativa do windows
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_MAXIMIZE = 3;

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);
            window.Title = "FitTec1 - BTG Pactual";

            window.Created += (s, e) =>
            {
                var nativeWindow = (Microsoft.UI.Xaml.Window)window.Handler.PlatformView;

                IntPtr hWnd = WindowNative.GetWindowHandle(nativeWindow);

                //abre a janela maximizada
                ShowWindow(hWnd, SW_MAXIMIZE);
            };

            return window;
        }
    }
}