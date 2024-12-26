namespace laboratory_work_13;

public partial class NavigationPage : ContentPage
{
	public NavigationPage(NavigationPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}