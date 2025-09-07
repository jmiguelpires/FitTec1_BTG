using CommunityToolkit.Maui.Views;

namespace FitTec1_BTG.Services.Abstractions
{
    public interface INavigationService
    {
        /*Navegação para páginas do tipo ContentPage*/
        Task PushAsync(Page page);
        Task PopAsync();
        Task PushModalAsync(Page page);
        Task PopModalAsync();

        /*Navegação para páginas do tipo Popup*/
        Task ShowPopupAsync<TPopup>(object viewModel = null) where TPopup : Popup;
    }
}
