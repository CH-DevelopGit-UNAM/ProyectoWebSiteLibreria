<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NovedadesCarousel-v2.0.ascx.cs" Inherits="UserControls_NovedadesCarousel_v2_0" %>

<asp:UpdatePanel ID="UpdatePanelNoticias" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="true" >
    <ContentTemplate>        
       <%--<asp:LinkButton ID="LinkUpdateNoticias" runat="server" Text="Actualizar" OnClick="LinkUpdateSource_Click" CssClass="" Visible="true"></asp:LinkButton>--%>
        <div id="novedades-carousel" class="carousel flexible slide" data-ride="carousel" data-interval="15000" data-wrap="true">
            <div id="DivNoticias" class="items" runat="server">
                <%--<div class="flex-item">
                         <img class="img-responsive" src="/images/novedades1.png">
                    </div>
                    <div class="flex-item">
                         <img class="img-responsive" src="/images/novedades2.png">
                    </div>
                    <div class="flex-item">
                         <img class="img-responsive" src="/images/novedades3.png">
                    </div>
                    <div class="flex-item">
                         <img class="img-responsive" src="/images/novedades4.png">
                    </div>
                    <div class="flex-item">
                         <img class="img-responsive" src="/images/novedades5.png">
                    </div>
                    <div class="flex-item">
                         <img class="img-responsive" src="/images/novedades2.png">
                    </div>
                    <div class="flex-item">
                         <img class="img-responsive" src="/images/novedades4.png">
                    </div>--%>
            </div>

            <div class="carousel-inner" role="listbox"></div>

            <a class="left carousel-control" href="#novedades-carousel" role="button" data-slide="prev">
                <span class="fa fa-angle-left" aria-hidden="true">
                    <img style="width: 10px;" src="<%=ResolveClientUrl("~/")%>images/Flecha-izquierda.png" />
                </span>
                <span class="sr-only">Previous</span>
            </a>

            <a class="right carousel-control" href="#novedades-carousel" role="button" data-slide="next">
                <span class="fa fa-angle-right" aria-hidden="true">
                    <img style="width: 10px;" src="<%=ResolveClientUrl("~/")%>images/Flecha-derecha.png" />
                </span>
                <span class="sr-only">Next</span>
            </a>

        </div>
    </ContentTemplate>    
    <%-- <Triggers>
        <asp:AsyncPostBackTrigger ControlID="LinkUpdateNoticias" EventName="Click"  />
    </Triggers> --%>
</asp:UpdatePanel>

<asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanelNoticias" DisplayAfter="400">
    <ProgressTemplate>
        <div id="wait" style="text-align: center; display: block; width: 50px; height: 50px; border: none; position: fixed; top: 45%; left: 45%; padding: 0px; background-color: none; color: none; vertical-align: middle;">
            <span class="" style="vertical-align: middle; text-align:center;display:inline-block;">
                <img src="<%=ResolveClientUrl("~/")%>images/gifAjax.gif" width="40" />
                Cargando..
            </span>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
