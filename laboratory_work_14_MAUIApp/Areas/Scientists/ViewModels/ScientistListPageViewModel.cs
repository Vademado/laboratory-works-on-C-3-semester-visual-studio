using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using laboratory_work_14_MAUIApp.Areas.Scientists.Services;
using laboratory_work_14_MAUIApp.Models;
using laboratory_work_14_MAUIApp.Areas.Scientists.Views;

namespace laboratory_work_14_MAUIApp.Areas.Scientists.ViewModels
{
    public partial class ScientistListPageViewModel : ObservableObject
    {
        public ObservableCollection<Scientist> Scientists { get; set; } = new ObservableCollection<Scientist>();

        private readonly IScientistService _scientistService;

        public ScientistListPageViewModel(IScientistService scientistService)
        {
            _scientistService = scientistService;
        }

        [RelayCommand]
        public async Task GetScientistList()
        {
            var scientistList = await _scientistService.GetAllScientists();
            if (scientistList?.Count > 0)
            {
                Scientists.Clear();
                foreach (var scientist in scientistList)
                {
                    Scientists.Add(scientist);
                }
            }
        }

        [RelayCommand]
        public async Task AddUpdateScientist()
        {
            var navParam = new Dictionary<string, object>();
            navParam.Add("ScientistDetail", new Scientist());
            await AppShell.Current.GoToAsync(nameof(AddUpdateScientistDetail), navParam);
            await GetScientistList();
        }

        [RelayCommand]
        public async Task Back()
        {
            await AppShell.Current.GoToAsync($"///{nameof(NavigationPage)}");
        }

        [RelayCommand]
        public async Task DisplayAction(Scientist scientist)
        {
            var response = await AppShell.Current.DisplayActionSheet("Select Option", "OK", null, "Edit", "Delete");
            if (response == "Edit")
            {
                var navParam = new Dictionary<string, object>();
                navParam.Add("ScientistDetail", scientist);
                await AppShell.Current.GoToAsync(nameof(AddUpdateScientistDetail), navParam);
                await GetScientistList();
            }
            else if (response == "Delete")
            {
                var delResponse = await _scientistService.DeleteScientist(scientist);
                if (delResponse > 0)
                {
                    Scientists.Remove(scientist);
                }
                else
                {
                    await AppShell.Current.DisplayAlert("Error", "Error in deleting scientist", "OK");
                }
            }
        }

    }

}
