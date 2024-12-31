using laboratory_work_14_MAUIApp.Areas.Projects.Services;
using laboratory_work_14_MAUIApp.Areas.Projects.ViewModels;
using laboratory_work_14_MAUIApp.Areas.Projects.Views;
using laboratory_work_14_MAUIApp.Areas.Scientists.Services;
using laboratory_work_14_MAUIApp.Areas.Scientists.ViewModels;
using laboratory_work_14_MAUIApp.Areas.Scientists.Views;
using laboratory_work_14_MAUIApp.Areas.Equipments.Services;
using laboratory_work_14_MAUIApp.Areas.Equipments.ViewModels;
using laboratory_work_14_MAUIApp.Areas.Equipments.Views;
using laboratory_work_14_MAUIApp.Models;
using Microsoft.Extensions.Logging;

namespace laboratory_work_14_MAUIApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSqlServer<ScientificLaboratoryDBContext>("Data Source=localhost;Initial Catalog=ScientificLaboratoryDB;Persist Security Info=True;User ID=sa;Password=HelloWorld10;Trust Server Certificate=True");

            builder.Services.AddSingleton<IScientistService, ScientistService>();
            builder.Services.AddSingleton<IProjectService, ProjectService>();
            builder.Services.AddSingleton<IEquipmentService, EquipmentService>();

            builder.Services.AddSingleton<NavigationPage>();
            builder.Services.AddSingleton<ScientistListPage>();
            builder.Services.AddSingleton<AddUpdateScientistDetail>();
            builder.Services.AddSingleton<ProjectListPage>();
            builder.Services.AddSingleton<AddUpdateProjectDetail>();
            builder.Services.AddSingleton<EquipmentListPage>();
            builder.Services.AddSingleton<AddUpdateEquipmentDetail>();

            builder.Services.AddSingleton<NavigationPageViewModel>();
            builder.Services.AddSingleton<ScientistListPageViewModel>();
            builder.Services.AddSingleton<AddUpdateScientistDetailViewModel>();
            builder.Services.AddSingleton<ProjectListPageViewModel>();
            builder.Services.AddSingleton<AddUpdateProjectDetailViewModel>();
            builder.Services.AddSingleton<EquipmentListPageViewModel>();
            builder.Services.AddSingleton<AddUpdateEquipmentDetailViewModel>();

            return builder.Build();
        }
    }
}
