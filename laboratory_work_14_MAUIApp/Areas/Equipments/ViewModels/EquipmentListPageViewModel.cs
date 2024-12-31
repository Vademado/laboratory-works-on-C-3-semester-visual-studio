using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using laboratory_work_14_MAUIApp.Models;
using laboratory_work_14_MAUIApp.Areas.Equipments.Services;
using laboratory_work_14_MAUIApp.Areas.Equipments.Views;

namespace laboratory_work_14_MAUIApp.Areas.Equipments.ViewModels
{
    public partial class EquipmentListPageViewModel
    {
        public ObservableCollection<Equipment> Equipments { get; set; } = new ObservableCollection<Equipment>();

        private readonly IEquipmentService _equipmentService;

        public EquipmentListPageViewModel(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [RelayCommand]
        public async Task GetEquipmentList()
        {
            var equipmentList = await _equipmentService.GetAllEquipments();
            if (equipmentList?.Count > 0)
            {
                Equipments.Clear();
                foreach (var equipment in equipmentList)
                {
                    Equipments.Add(equipment);
                }
            }
        }

        [RelayCommand]
        public async Task AddUpdateEquipment()
        {
            var navParam = new Dictionary<string, object>();
            navParam.Add("EquipmentDetail", new Equipment());
            await AppShell.Current.GoToAsync(nameof(AddUpdateEquipmentDetail), navParam);
            await GetEquipmentList();
        }

        [RelayCommand]
        public async Task Back()
        {
            await AppShell.Current.GoToAsync($"///{nameof(NavigationPage)}");
        }

        [RelayCommand]
        public async Task DisplayAction(Equipment equipment)
        {
            var response = await AppShell.Current.DisplayActionSheet("Select Option", "OK", null, "Edit", "Delete");
            if (response == "Edit")
            {
                var navParam = new Dictionary<string, object>();
                navParam.Add("EquipmentDetail", equipment);
                await AppShell.Current.GoToAsync(nameof(AddUpdateEquipmentDetail), navParam);
                await GetEquipmentList();
            }
            else if (response == "Delete")
            {
                var delResponse = await _equipmentService.DeleteEquipment(equipment);
                if (delResponse > 0)
                {
                    Equipments.Remove(equipment);
                }
                else
                {
                    await AppShell.Current.DisplayAlert("Error", "Error in deleting equipment", "OK");
                }
            }
        }
    }
}
