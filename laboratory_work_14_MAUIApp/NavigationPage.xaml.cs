namespace laboratory_work_14_MAUIApp;

public partial class NavigationPage : ContentPage
{
    public NavigationPage(NavigationPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}