using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using laboratory_work_14_MAUIApp.Areas.Scientists.Services;
using laboratory_work_14_MAUIApp.Areas.Scientists.Views;
using laboratory_work_14_MAUIApp.Models;

namespace laboratory_work_14_MAUIApp.Areas.Scientists.ViewModels
{
    [QueryProperty(nameof(ScientistDetail), "ScientistDetail")]
    public partial class AddUpdateScientistDetailViewModel : ObservableObject
    {
        [ObservableProperty]
        private Scientist _scientistDetail = new Scientist();

        private readonly IScientistService _scientistService;

        public AddUpdateScientistDetailViewModel(IScientistService scientistService)
        {
            _scientistService = scientistService;
        }

        [RelayCommand]
        public async Task<int> AddUpdateScientist()
        {
            int response = -1;
            if (ScientistDetail?.ScientistId > 0)
            {
                response = await _scientistService.UpdateScientist(ScientistDetail);
            }
            else
            {
                response = await _scientistService.AddScientist(new Scientist
                {
                    Name = ScientistDetail.Name,
                    Specialty = ScientistDetail.Specialty,
                    Email = ScientistDetail.Email,
                });
            }
            if (response > 0)
            {
                await Shell.Current.DisplayAlert("Record added", "A record has been added to the Scientists table", "Ok");
            }
            else
            {
                await Shell.Current.DisplayAlert("No record added", "Something went wrong when adding a record.", "Ok");
            }
            await Shell.Current.GoToAsync(nameof(ScientistListPage));
            return response;
        }
    }
}
