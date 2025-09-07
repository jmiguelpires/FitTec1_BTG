using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using FitTec1_BTG.Model;
using FitTec1_BTG.Services.Abstractions;
using FitTec1_BTG.Services.Implementations;
using FitTec1_BTG.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FitTec1_BTG.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IClienteRepository _repository;
        private readonly INavigationService _navigationService;

        public ICommand CadastrarClienteCommand { get; set; }
        public ICommand ExcluirClienteCommand { get; set; }
        public ICommand EditarClienteCommand { get; set; }

        [ObservableProperty]
        private ObservableCollection<Cliente> listaClientes = new();

        private int _totalClientes;
        public int TotalClientes
        {
            get => _totalClientes;
            set => SetProperty(ref _totalClientes, value);
        }

        public MainPageViewModel(INavigationService navigationService, IServiceProvider serviceProvider, IClienteRepository repository)
        {
            _serviceProvider = serviceProvider;
            _repository = repository;
            _navigationService = navigationService;

            CadastrarClienteCommand = new Command(async () => await CadastrarCliente()); ;
            ExcluirClienteCommand = new Command<Cliente>(async (cliente) => await ExcluirCliente(cliente));
            EditarClienteCommand = new Command<Cliente>(async (cliente) => await EditarCliente(cliente));

            Task.Run(CarregarClientes);
        }

        private async Task EditarCliente(Cliente cliente)
        {
            try
            {
                var vm = _serviceProvider.GetService<EditaClienteViewModel>();
                vm.Cliente = cliente;

                await _navigationService.ShowPopupAsync<EditaCliente>(vm);
                await CarregarClientes();
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", $"Não foi possível editar o cliente {cliente.Name}. Tente novamente.", "OK");
            }
        }

        private async Task ExcluirCliente(Cliente cliente)
        {
            try
            {
                var resultado = await Application.Current.MainPage.DisplayAlert("Atenção", $"Tem certeza que deseja excluir o cadastro do cliente {cliente.Name}?", "Sim", "Não");

                if (!resultado)
                {
                    return;
                }

                await _repository.DeleteAsync(cliente.Id);
                await Application.Current.MainPage.DisplayAlert("Sucesso", $"Cliente {cliente.Name} excluído com sucesso!", "Ok");
                await Task.Run(CarregarClientes);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", $"Não foi possível excluir o cliente {cliente.Name}. Tente novamente.", "OK");
            }
        }

        private async Task CadastrarCliente()
        {
            await _navigationService.ShowPopupAsync<CadastroCliente>();
            await CarregarClientes();
        }

        private async Task CarregarClientes()
        {
            try
            {
                var clientesDb = await _repository.GetAllAsync();
                ListaClientes = new ObservableCollection<Cliente>(clientesDb);
                TotalClientes = ListaClientes.Count;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Não foi possível carregar os clientes. Tente novamente.", "OK");
            }
        }
    }
}
