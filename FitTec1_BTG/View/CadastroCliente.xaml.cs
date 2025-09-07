using CommunityToolkit.Maui.Views;
using FitTec1_BTG.ViewModel;

namespace FitTec1_BTG.View;

public partial class CadastroCliente : Popup
{
    public CadastroCliente(CadastroClienteViewModel vm)
    {
        InitializeComponent();
        vm.Popup = this;
        BindingContext = vm;
    }
}