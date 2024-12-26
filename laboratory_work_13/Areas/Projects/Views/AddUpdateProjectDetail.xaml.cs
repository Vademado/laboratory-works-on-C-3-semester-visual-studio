using laboratory_work_13.Areas.Projects.ViewModels;

namespace laboratory_work_13.Areas.Projects.Views;

public partial class AddUpdateProjectDetail : ContentPage
{
	public AddUpdateProjectDetail(AddUpdateProjectDetailViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
    }
}