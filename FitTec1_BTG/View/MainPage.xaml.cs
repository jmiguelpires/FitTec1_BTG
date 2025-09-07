using FitTec1_BTG.ViewModel;

namespace FitTec1_BTG
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
