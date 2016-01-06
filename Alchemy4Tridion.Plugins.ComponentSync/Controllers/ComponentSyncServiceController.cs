using System;
using System.Web.Http;
using Tridion.ContentManager;
using Tridion.ContentManager.CoreService.Client;
using ItemType = Tridion.ContentManager.ItemType;
using CoreServiceItemType = Tridion.ContentManager.CoreService.Client.ItemType;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text;

namespace Alchemy4Tridion.Plugins.ComponentSync.Controllers
{
    public class SyncParams
    {
        public string Tcm { get; set; }
        public string Flag { get; set; }
    }

    [AlchemyRoutePrefix("ComponentSyncService")]
    public class ComponentSyncServiceController : AlchemyApiController
    {
        [HttpPost]
        [Route("GetSynchronized")]
        public string GetSynchronized(SyncParams syncParams)
        {
            string tcmURI = syncParams.Tcm;
            string status = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(tcmURI) && TcmUri.IsValid(tcmURI))
                {
                    SynchronizeOptions syncOptions = GetSyncOptions(syncParams.Flag);
                    TcmUri uri = new TcmUri(tcmURI);
                    switch (uri.ItemType)
                    {
                        case ItemType.Schema:
                            status = SyncAllComponentsBasedOnSchema(tcmURI, uri.GetContextRepositoryUri(), syncOptions);
                            break;
                        case ItemType.Component:
                            SynchronizationResult result = Client.SynchronizeWithSchemaAndUpdate(tcmURI, syncOptions);
                            status = string.Format("Component Synchronization is successfull for Component: {0} ({1})", result.SynchronizedItem.Title, result.SynchronizedItem.Id);
                            break;
                    }
                    return status;
                }
                else
                {
                    return "Component Synchronization Failed - Invalid TCM URI";
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occured while synchronizing components. Error Message: " + ex.Message + ex.StackTrace;
                return errorMessage;
            }
        }

        private string SyncAllComponentsBasedOnSchema(string tcmURI, string pubID, SynchronizeOptions syncOptions)
        {
            string status = string.Empty;
            try
            {
                XElement allSchemaBasedComponent = GetAllComponentsBasedOnSchema(tcmURI, pubID);
                
                if (allSchemaBasedComponent != null)
                {
                    StringBuilder success = new StringBuilder();
                    StringBuilder failure = new StringBuilder();
                    bool showFailureMessage = false;
                    success.Append("Component Synchronization is Successful for Components: ");
                    failure.Append("Component Synchronization is Failed for Components: ");
                    for (IEnumerator<XElement> e = allSchemaBasedComponent.Descendants().GetEnumerator(); e.MoveNext();)
                    {
                        string comp_tcmURI = e.Current.Attribute(XName.Get("ID")).Value;
                        try
                        {
                            SynchronizationResult result = Client.SynchronizeWithSchemaAndUpdate(comp_tcmURI, syncOptions);
                            success.Append(string.Format("{0} ({1}){2}", result.SynchronizedItem.Title, result.SynchronizedItem.Id, " , "));
                        }
                        catch (Exception ex)
                        {
                            showFailureMessage = true;
                            failure.Append(string.Format("{0}{1}", comp_tcmURI," , "));
                        }
                    }
                    status = showFailureMessage ? string.Concat(success.ToString().TrimEnd(','), failure.ToString().TrimEnd(',')) : success.ToString().TrimEnd(',');
                }
                else
                {
                    status = "No component to synchronize";
                }
            }
            catch(Exception ex)
            {
                status += "An error has occured while synchronizing components based on Schema - " + tcmURI + ex.Message+ex.StackTrace;
            }
            
            return status;
        }

        private XElement GetAllComponentsBasedOnSchema(string tcmURI, string pubID)
        {
            SearchQueryData query = new SearchQueryData()
            {
                BasedOnSchemas = new BasedOnSchemaData[]
                                {
                                    new BasedOnSchemaData()
                                    {
                                        Schema = new LinkToSchemaData(){ IdRef= tcmURI }
                                    }
                                 },

                SearchIn = new LinkToIdentifiableObjectData() { IdRef = pubID },
                ItemTypes = new CoreServiceItemType[] { CoreServiceItemType.Component }
            };

            return Client.GetSearchResultsXml(query);
        }

        private SynchronizeOptions GetSyncOptions(string syncOptionParam)
        {
            SynchronizeOptions syncOptions = new SynchronizeOptions();
            SynchronizeFlags parsedEnum;
            if(Enum.TryParse<SynchronizeFlags>(syncOptionParam, true,out parsedEnum))
            {
                syncOptions.SynchronizeFlags = parsedEnum;
            }
            else
            {
                syncOptions.SynchronizeFlags = SynchronizeFlags.UnknownByClient;
            }
            return syncOptions;
        }
    }
}
