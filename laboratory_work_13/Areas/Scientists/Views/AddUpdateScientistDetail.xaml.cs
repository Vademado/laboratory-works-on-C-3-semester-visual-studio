using laboratory_work_13.Areas.Scientists.ViewModels;

namespace laboratory_work_13.Areas.Scientists.Views;

public partial class AddUpdateScientistDetail : ContentPage
{
    public AddUpdateScientistDetail(AddUpdateScientistDetailViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}