using laboratory_work_14_MAUIApp.Areas.Equipments.ViewModels;

namespace laboratory_work_14_MAUIApp.Areas.Equipments.Views;

public partial class EquipmentListPage : ContentPage
{
	private EquipmentListPageViewModel _viewModel;
	public EquipmentListPage(EquipmentListPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		this.BindingContext = _viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetEquipmentListCommand.Execute(null);
    }
}