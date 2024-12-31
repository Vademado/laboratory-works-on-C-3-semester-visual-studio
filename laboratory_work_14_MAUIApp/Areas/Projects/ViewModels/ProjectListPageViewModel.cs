using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using laboratory_work_14_MAUIApp.Models;
using laboratory_work_14_MAUIApp.Areas.Projects.Services;
using laboratory_work_14_MAUIApp.Areas.Projects.Views;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratory_work_14_MAUIApp.Areas.Projects.ViewModels
{
    public partial class ProjectListPageViewModel
    {
        public ObservableCollection<Project> Projects { get; set; } = new ObservableCollection<Project>();
        
        private readonly IProjectService _projectService;

        public ProjectListPageViewModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [RelayCommand]
        public async Task GetProjectList()
        {
            var projectsList = await _projectService.GetAllProjects();
            if (projectsList?.Count > 0)
            {
                Projects.Clear();
                foreach (var project in projectsList)
                {
                    Projects.Add(project);
                }
            }
        }

        [RelayCommand]
        public async Task AddUpdateProject()
        {
            var navParam = new Dictionary<string, object>();
            navParam.Add("ProjectDetail", new Project());
            await AppShell.Current.GoToAsync(nameof(AddUpdateProjectDetail), navParam);
            await GetProjectList();
        }

        [RelayCommand]
        public async Task Back()
        {
            await AppShell.Current.GoToAsync($"///{nameof(NavigationPage)}");
        }

        [RelayCommand]
        public async Task DisplayAction(Project project)
        {
            var response = await AppShell.Current.DisplayActionSheet("Select Option", "OK", null, "Edit", "Delete");
            if (response == "Edit")
            {
                var navParam = new Dictionary<string, object>();
                navParam.Add("ProjectDetail", project);
                await AppShell.Current.GoToAsync(nameof(AddUpdateProjectDetail), navParam);
                await GetProjectList();
            }
            else if (response == "Delete")
            {
                var delResponse = await _projectService.DeleteProject(project);
                if (delResponse > 0)
                {
                    Projects.Remove(project);
                }
                else
                {
                    await AppShell.Current.DisplayAlert("Error", "Error in deleting project", "OK");
                }
            }
        }
    }
}