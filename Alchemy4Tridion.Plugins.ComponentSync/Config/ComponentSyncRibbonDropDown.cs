using Alchemy4Tridion.Plugins.GUI.Configuration;

namespace Alchemy4Tridion.Plugins.ComponentSync.Config
{
    public class ComponentSyncRibbonDropDown : RibbonToolbarExtension
    {
        public ComponentSyncRibbonDropDown()
        {
            AssignId = "CompSyncRibbonToolbar";
            Group = "ComponentSyncGroup.ascx";
            Name = "Component Synchronizer";
            PageId = Constants.PageIds.HomePage;
            Dependencies.Add<ComponentSyncResourceGroup>();
            Apply.ToView(Constants.Views.DashboardView, "DashboardToolbar");
        }
    }
}
