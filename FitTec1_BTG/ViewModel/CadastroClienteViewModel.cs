using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using FitTec1_BTG.Model;
using FitTec1_BTG.Services.Abstractions;
using System.Windows.Input;

namespace FitTec1_BTG.ViewModel
{
    public partial class CadastroClienteViewModel : ObservableObject
    {
        private readonly IPopupService _popupService;
        private readonly IClienteRepository _repository;

        public ICommand FecharCadastroCommand { get; set; }
        public ICommand SalvarCadastroCommand { get; set; }

        public Popup Popup { get; set; }

        [ObservableProperty]
        private Cliente cliente = new Cliente();

        public CadastroClienteViewModel(IPopupService popupService, IClienteRepository repository)
        {
            _popupService = popupService;
            _repository = repository;

            FecharCadastroCommand = new Command(async () => await FecharCadastro());
            SalvarCadastroCommand = new Command(async () => await SalvarCadastro());
        }

        private async Task FecharCadastro()
        {
            if (PossuiDadosPreenchidos())
            {
                var resultado = await Application.Current.MainPage.DisplayAlert("Atenção", "Existem dados preenchidos. Tem certeza que deseja fechar o cadastro?", "Sim", "Não");

                if (!resultado)
                {
                    return;
                }
            }

            await _popupService.ClosePopupAsync(Application.Current.MainPage, "CadastroFechado");
        }

        private async Task SalvarCadastro()
        {
            try
            {
                await _repository.AddAsync(Cliente);
                await Application.Current.MainPage.DisplayAlert("Sucesso!", $"Cliente {Cliente.Name} cadastrado com sucesso!", "Ok");
                await _popupService.ClosePopupAsync(Application.Current.MainPage, "CadastroFechado");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", $"Não foi possível salvar o cadastro do cliente {Cliente.Name}. Tente novamente.", "OK");
            }
        }

        private bool PossuiDadosPreenchidos()
        {
            if (!string.IsNullOrEmpty(Cliente.Name) || !string.IsNullOrEmpty(Cliente.Lastname) || !string.IsNullOrEmpty(Cliente.Address) || (Cliente?.Age ?? 0) != 0)
            {
                return true;
            }
            return false;
        }
    }
}
