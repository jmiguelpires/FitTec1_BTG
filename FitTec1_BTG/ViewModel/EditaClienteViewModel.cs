using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using FitTec1_BTG.Model;
using FitTec1_BTG.Services.Abstractions;
using System.Windows.Input;

namespace FitTec1_BTG.ViewModel
{
    public partial class EditaClienteViewModel : ObservableObject
    {
        private readonly IClienteRepository _repository;
        private readonly IPopupService _popupService;

        public ICommand EditarClienteCommand { get; set; }
        public ICommand FecharEdicaoCommand { get; set; }

        [ObservableProperty]
        private Cliente cliente = new Cliente();

        public EditaClienteViewModel(IClienteRepository repository, IPopupService popupService)
        {
            _repository = repository;
            _popupService = popupService;

            EditarClienteCommand = new Command(async () => await EditarCliente());
            FecharEdicaoCommand = new Command(async () => await FecharEdicaoCliente());
        }

        private async Task EditarCliente()
        {
            await _repository.UpdateAsync(Cliente);
            await Application.Current.MainPage.DisplayAlert("Sucesso!", $"Cliente {Cliente.Name} editado com sucesso!", "Ok");
            await _popupService.ClosePopupAsync(Application.Current.MainPage, "CadastroEditado");
        }

        private async Task FecharEdicaoCliente()
        {
            var resultado = await Application.Current.MainPage.DisplayAlert("Atenção", $"Deseja desfazer as alterações feitas no cliente {Cliente.Name}?", "Sim", "Não");

            if (!resultado)
            {
                return;
            }

            await _popupService.ClosePopupAsync(Application.Current.MainPage, "EdicaoFechada");
        }
    }
}
