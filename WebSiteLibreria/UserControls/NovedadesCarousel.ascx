<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NovedadesCarousel.ascx.cs" Inherits="NovedadesCarousel" %>
<script>
    
</script>
<asp:UpdatePanel ID="UpdatePanelNoticias" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="true" >
    <ContentTemplate>        
        <div  class="carouselJumbotron"  style="margin: auto;">
            <div id="DivNoticias" runat="server">
            </div>
            <asp:LinkButton ID="LinkUpdateNoticias" runat="server" Text="Actualizar" OnClick="LinkUpdateSource_Click" CssClass="" Visible="false"></asp:LinkButton>
        </div>        
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="LinkUpdateNoticias"/>
    </Triggers>
</asp:UpdatePanel>

<asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanelNoticias" DisplayAfter="400">
    <ProgressTemplate>
        <div id="wait" style="text-align: center; display: block; width: 200px; height: 50px; border: 1px solid #F78181; position: fixed; top: 45%; left: 45%; padding: 10px; background-color: #FBF2EF; color: #5f3f3f; vertical-align: middle;">
            <i class="fa fa-spinner fa-pulse fa-2x fa-fw"></i>
            <span class="" style="vertical-align: middle">
                <img src="../images/uploading.gif" width="30" />
                <br />
                Cargando...
            </span>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>

<%--<div class="col-sm-12" style="height:100%;min-height:250px;max-height:650px;"> 
    <h4>NOVEDADES EDITORIALES</h4>
    <div id="ContenedorCarousel" class="carousel slide" data-interval="15000" data-ride="carousel" style="width: 100%; height: 100%;margin-top: 30px;background-color:blue;">
        <!-- Indicadores -->
        <ol class="carousel-indicators" style="margin-bottom: -15px;">
            <li data-target="#ContenedorCarousel" data-slide-to="0" class="active"></li>
            <li data-target="#ContenedorCarousel" data-slide-to="1"></li>
            <li data-target="#ContenedorCarousel" data-slide-to="2"></li>
        </ol>
        <!-- Items (imagenes) -->
        <div class="carousel-inner" style="background-color: black;" role="listbox">
            <div class="active item" style=" max-height: 100%;min-height:100%;">
                <div class="center text-center">
                    <img src="Novedades/Imagenes/gammal-grekisk-inskrift-27895707.jpg" alt="100 US Dollar" style="height:450px;">
                    <div class="carousel-caption navbar-fixed-bottom" style="margin-bottom: -20px; color: yellow;">
                        <h4>Elemento 1</h4>
                        <p>Se han encontrado los elementos más importantes de la utltima decada de la ciencia moderna</p>
                    </div>
                </div>
            </div>
            <div class="item" style=" height: 100%;min-height:100%;">
                <div class="center text-center" style="height: 100%;">
                    <div style="display:flex; height:100%; text-align:center; ">
                        <img src="/Novedades/Imagenes/9789703229246.jpg" alt="100 US Dollar" style="height:450px;margin:auto;">
                    </div>                    
                    <div class="carousel-caption navbar-fixed-bottom" style="margin-bottom: -20px; color: yellow;">
                        <h4>Elemento 2</h4>
                        <p>Se han encontrado los elementos más importantes de la utltima decada de la ciencia moderna</p>
                    </div>
                </div>
            </div>
            <div class="item" style=" height: 100%;min-height:100%;">
                <div class="center text-center" style=" height: 100%; position;relative;">
                    <img src="/Novedades/Imagenes/inscripciongriega.jpg" alt="100 US Dollar" style="min-height:250px;max-height:600px;height:450px;">
                    <div class="carousel-caption navbar-fixed-bottom" style="margin-bottom: -20px; color: yellow;">
                        <h4>Elemento 3</h4>
                        <p>DESCRIPCION IMAGEN 3</p>
                    </div>
                </div>
            </div>
        </div>

        <!-- Controles Prev y Next -->
        <a class="carousel-control left" href="#ContenedorCarousel" data-slide="prev">
            <span class="glyphicon glyphicon-chevron-left"></span>
        </a>
        <a class="carousel-control right" href="#ContenedorCarousel" data-slide="next">
            <span class="glyphicon glyphicon-chevron-right"></span>
        </a>
        <!-- Fondo descripcion -->
        <div class="navbar-fixed-bottom" style="background-color: black; opacity: 0.4; height: 120px; position: absolute;"></div>
    </div>
</div>--%>
