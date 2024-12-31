using laboratory_work_14_MAUIApp.Areas.Scientists.ViewModels;

namespace laboratory_work_14_MAUIApp.Areas.Scientists.Views;

public partial class AddUpdateScientistDetail : ContentPage
{
    public AddUpdateScientistDetail(AddUpdateScientistDetailViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}