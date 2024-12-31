using laboratory_work_14_MAUIApp.Areas.Projects.ViewModels;

namespace laboratory_work_14_MAUIApp.Areas.Projects.Views;

public partial class AddUpdateProjectDetail : ContentPage
{
	public AddUpdateProjectDetail(AddUpdateProjectDetailViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
    }
}