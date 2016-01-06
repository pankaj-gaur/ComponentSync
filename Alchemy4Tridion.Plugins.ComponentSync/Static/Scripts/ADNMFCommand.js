var purpose = "Component";
Alchemy.command("${PluginName}", "ADNMF", {
    init: function () {
        console.log("Init Method");
    },

    isEnabled: function (selection) {
        return this.isAvailable(selection);
    },

    isAvailable: function (selection)
    {
        if (selection.getCount() == 1) {
            var item = $models.getItem(selection.getItem(0));
            var itemType = item.getItemType();
            if (itemType == $const.ItemType.COMPONENT) {
                return true;
            }
            else if (itemType == $const.ItemType.SCHEMA) {
                $evt.addEventHandler(item, 'load', function (e) {
                    purpose = item.getPurpose();
                });
                item.load(true);
                return true;
            }
        }
        return false;
    },

    execute: function (selection) {
        if (purpose == "Component") {
            var progress = $messages.registerProgress("Syncing Components...", null);

            Alchemy.Plugins["${PluginName}"].Api.ComponentSyncService.getSynchronized({ tcm: selection.getItem(0), flag: "ApplyDefaultValuesForMissingNonMandatoryFields" })
                .success(function (synchronized) {
                    $messages.registerGoal(synchronized);
                })
                .error(function (type, error) {
                    $messages.registerError("There was an error in Component Synchronization", error.message);
                })
                .complete(function () {
                    progress.finish();
                });
        }
        else {
            $messages.registerProgress("Incorrect selection. Supported Type: Component Schema and Components", null);
        }
    }
});