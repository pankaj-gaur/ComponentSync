<%@ Control Language="C#" %>
<%@ Import Namespace="Tridion.Web.UI" %>

<c:RibbonButton runat="server" CommandName="ComponentSync" IsDropdownButton="true" Title="Component Synchronizer" Label="Component Synchronizer" ID="CompSyncBtn">
    <c:RibbonContextMenuItem runat="server" Command="Sync" Title="Sync" Label="Sync" />
</c:RibbonButton>
