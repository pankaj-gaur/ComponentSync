using Alchemy4Tridion.Plugins.GUI.Configuration;

namespace Alchemy4Tridion.Plugins.ComponentSync.Config
{
    public class ComponentSyncContextMenu : ContextMenuExtension
    {
        public ComponentSyncContextMenu()
        {
            AssignId = "ComponentSyncContextMenu";
            Name = "ComponentSyncContextMenu";
            InsertBefore = "cm_refresh";

            AddSubMenu("cm_compsync", "Component Synchronization")
                .AddItem("cm_compsync_all", "Synchronize", "Sync")
                //.AddSubMenu("cm_compsync_more", "More Options...")
                    .AddItem("cm_compsync_more_fn", "Fix Namespace", "FixNS")
                    //.AddSeperator("more_fns_separator")
                    .AddItem("cm_compsync_more_adm", "Apply Default - Mandatory Fields", "ADMF")
                    .AddItem("cm_compsync_more_adnm", "Apply Default - Non-Mandatory Fields", "ADNMF")
                    .AddItem("cm_compsync_more_acft", "Apply Converted Field Type", "ACFT")
                    //.AddSeperator("more_add_separator")
                    .AddItem("cm_compsync_more_ruf", "Remove Unknown Field", "RUF")
                    .AddItem("cm_compsync_more_raf", "Remove Additional Field", "RAF");

            Dependencies.Add<ComponentSyncResourceGroup>();

            Apply.ToView(Constants.Views.DashboardView);
        }
    }
}
