using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using laboratory_work_13.Areas.Equipments.Services;
using laboratory_work_13.Models;
using laboratory_work_13.Areas.Equipments.Views;

namespace laboratory_work_13.Areas.Equipments.ViewModels
{
    [QueryProperty(nameof(EquipmentDetail), "EquipmentDetail")]
    public partial class AddUpdateEquipmentDetailViewModel : ObservableObject
    {
        [ObservableProperty]
        private Equipment _equipmentDetail = new Equipment();

        private readonly IEquipmentService _equipmentService;

        public AddUpdateEquipmentDetailViewModel(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [RelayCommand]
        public async Task<int> AddUpdateEquipment()
        {
            int response = -1;
            if (EquipmentDetail?.EquipmentId > 0)
            {
                response = await _equipmentService.UpdateEquipment(EquipmentDetail);
            }
            else
            {
                response = await _equipmentService.AddEquipment(new Equipment
                {
                    Name = EquipmentDetail.Name,
                    Status = EquipmentDetail.Status,
                    ProjectId = EquipmentDetail.ProjectId,
                });
            }
            if (response > 0)
            {
                await Shell.Current.DisplayAlert("Record added", "A record has been added to the Equipments table", "Ok");
            }
            else
            {
                await Shell.Current.DisplayAlert("No record added", "Something went wrong when adding a record.", "Ok");
            }
            await Shell.Current.GoToAsync(nameof(EquipmentListPage));
            return response;
        }
    }

}