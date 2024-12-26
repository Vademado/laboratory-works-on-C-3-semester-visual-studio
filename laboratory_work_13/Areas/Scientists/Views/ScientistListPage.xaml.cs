using laboratory_work_13.Areas.Scientists.ViewModels;

namespace laboratory_work_13.Areas.Scientists.Views;

public partial class ScientistListPage : ContentPage
{
    private ScientistListPageViewModel _viewModel;
    public ScientistListPage(ScientistListPageViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        this.BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetScientistListCommand.Execute(null);
    }
}