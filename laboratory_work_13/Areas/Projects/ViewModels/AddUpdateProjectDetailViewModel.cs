using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using laboratory_work_13.Areas.Projects.Services;
using laboratory_work_13.Models;
using laboratory_work_13.Areas.Projects.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace laboratory_work_13.Areas.Projects.ViewModels
{
    [QueryProperty(nameof(ProjectDetail), "ProjectDetail")]
    public partial class AddUpdateProjectDetailViewModel : ObservableObject
    {
        [ObservableProperty]
        private Project _projectDetail = new Project();

        private readonly IProjectService _projectService;

        public AddUpdateProjectDetailViewModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [RelayCommand]
        public async Task<int> AddUpdateProject()
        {
            int response = -1;
            if (ProjectDetail?.ProjectId > 0)
            {
                response = await _projectService.UpdateProject(ProjectDetail);
            }
            else
            {
                response = await _projectService.AddProject(new Project
                {
                    Title = ProjectDetail.Title,
                    StartDate = ProjectDetail.StartDate,
                    EndDate = ProjectDetail.EndDate,
                    ScientistId = ProjectDetail.ScientistId,
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
            await Shell.Current.GoToAsync(nameof(ProjectListPage));
            return response;
        }
    }
}