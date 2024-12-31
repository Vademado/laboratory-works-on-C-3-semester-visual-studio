using laboratory_work_14_MAUIApp.Areas.Equipments.ViewModels;

namespace laboratory_work_14_MAUIApp.Areas.Equipments.Views;

public partial class AddUpdateEquipmentDetail : ContentPage
{
	public AddUpdateEquipmentDetail(AddUpdateEquipmentDetailViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}