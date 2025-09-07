using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using FitTec1_BTG.Services.Abstractions;

namespace FitTec1_BTG.Services.Implementations
{
    public class NavigationService : INavigationService
    {
        private INavigation Navigation => Application.Current.MainPage.Navigation;

        private readonly IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task PushAsync(Page page)
        {
            await Navigation.PushAsync(page);
        }

        public async Task PopAsync()
        {
            await Navigation.PopAsync();
        }

        public async Task PushModalAsync(Page page)
        {
            await Navigation.PushModalAsync(page);
        }

        public async Task PopModalAsync()
        {
            await Navigation.PopModalAsync();
        }

        public async Task ShowPopupAsync<TPopup>(object viewModel = null) where TPopup : Popup
        {
            var popup = _serviceProvider.GetService<TPopup>();
            if (popup == null)
            {
                throw new InvalidOperationException($"Popup {typeof(TPopup).Name} não registrado.");
            }

            if (viewModel != null)
            {
                popup.BindingContext = viewModel;
            }

            await App.Current.MainPage.ShowPopupAsync(popup);
        }
    }
}
