using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using laboratory_work_13.Areas.Scientists.Views;
using laboratory_work_13.Areas.Projects.Views;
using laboratory_work_13.Areas.Equipments.Views;

namespace laboratory_work_13
{
    public partial class NavigationPageViewModel
    {

        [RelayCommand]
        public async Task NavigateToTableScientists()
        {
            await AppShell.Current.GoToAsync(nameof(ScientistListPage));
        }

        [RelayCommand]
        public async Task NavigateToTableProjects()
        {
            await AppShell.Current.GoToAsync(nameof(ProjectListPage));
        }

        [RelayCommand]
        public async Task NavigateToTableEquipments()
        {
            await AppShell.Current.GoToAsync(nameof(EquipmentListPage));
        }
    }
}
