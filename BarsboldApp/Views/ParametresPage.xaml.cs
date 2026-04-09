using BarsboldApp.ViewModels;

namespace BarsboldApp.Views;

public partial class ParametresPage : ContentPage
{
	public ParametresPage()
	{
		InitializeComponent();
		BindingContext = new ParametresViewModel();
	}
}