using Alchemy4Tridion.Plugins.GUI.Configuration;

namespace Alchemy4Tridion.Plugins.ComponentSync.Config
{
    public class ComponentSyncCommandSet : CommandSet
    {
        public ComponentSyncCommandSet()
        {
            AddCommand("Sync");     // Synchronize All
            AddCommand("FixNS");    // Fix Namespace
            AddCommand("ADMF");     // Apply Default Missing Mandatory Field
            AddCommand("ADNMF");    // Apply Default Missing Non-Mandatory Field
            AddCommand("ACFT");     // Apply Converted Field Type
            AddCommand("RUF");      // Remove Unnecessary Fields
            AddCommand("RAF");      // Remove Additional Fields
        }
    }
}
