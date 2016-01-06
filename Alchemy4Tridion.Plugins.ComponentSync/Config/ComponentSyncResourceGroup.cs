using Alchemy4Tridion.Plugins.GUI.Configuration;
using Alchemy4Tridion.Plugins.GUI.Configuration.Elements;

namespace Alchemy4Tridion.Plugins.ComponentSync.Config
{
    public class ComponentSyncResourceGroup : ResourceGroup
    {
        public ComponentSyncResourceGroup()
        {
            AddFile("SyncCommand.js");
            AddFile("FixNSCommand.js");
            AddFile("ACFTCommand.js");
            AddFile("ADMFCommand.js");
            AddFile("ADNMFCommand.js");
            
            AddFile("RAFCommand.js");
            AddFile("RUFCommand.js");

            AddFile("SyncComp.css");
            AddFile<ComponentSyncCommandSet>();
            AddWebApiProxy();
        }
    }
}
