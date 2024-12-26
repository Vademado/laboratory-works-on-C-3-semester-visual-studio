using laboratory_work_13.Areas.Projects.Views;
using laboratory_work_13.Areas.Scientists.Views;
using laboratory_work_13.Areas.Equipments.Views;

namespace laboratory_work_13
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(NavigationPage), typeof(NavigationPage));
            Routing.RegisterRoute(nameof(ScientistListPage), typeof(ScientistListPage));
            Routing.RegisterRoute(nameof(ProjectListPage), typeof(ProjectListPage));
            Routing.RegisterRoute(nameof(EquipmentListPage), typeof(EquipmentListPage));
            Routing.RegisterRoute(nameof(AddUpdateScientistDetail), typeof(AddUpdateScientistDetail));
            Routing.RegisterRoute(nameof(AddUpdateProjectDetail), typeof(AddUpdateProjectDetail));
            Routing.RegisterRoute(nameof(AddUpdateEquipmentDetail), typeof(AddUpdateEquipmentDetail));
        }
    }
}