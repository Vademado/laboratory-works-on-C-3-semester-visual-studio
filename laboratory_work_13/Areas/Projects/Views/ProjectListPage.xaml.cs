using laboratory_work_13.Areas.Projects.ViewModels;

namespace laboratory_work_13.Areas.Projects.Views;

public partial class ProjectListPage : ContentPage
{
    private ProjectListPageViewModel _viewModel;
	public ProjectListPage(ProjectListPageViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        this.BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetProjectListCommand.Execute(null);
    }
}