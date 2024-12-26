using laboratory_work_13.Areas.Equipments.ViewModels;

namespace laboratory_work_13.Areas.Equipments.Views;

public partial class AddUpdateEquipmentDetail : ContentPage
{
	public AddUpdateEquipmentDetail(AddUpdateEquipmentDetailViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}