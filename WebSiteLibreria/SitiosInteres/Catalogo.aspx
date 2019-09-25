<%@ Page Title="Catalogo" Language="C#" MasterPageFile="~/ResponsiveSite.master" AutoEventWireup="true" CodeFile="Catalogo.aspx.cs" Inherits="SitiosInteres_Catalogo" Theme="SkinBase" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content runat="server" ContentPlaceHolderID="HeaderPlaceHolder">
    <style type="text/css">              
        .customJumbotron {
            width:100%;
            height:auto;
        }

        .carouselContenedor{
            background-color: black;
        }

        .carouselContenedorImagen {
            display:flex; height:100%; text-align:center; 
        }

        .carouselImagen {
            margin:auto;
            height:270px;
            -webkit-transition: all 0.2s ease-in-out;
            transition: all 0.2s ease-in-out;
        }

        .modalDialog-Detalle {
            width: 60%;
            max-width: 1100px;
        }

        /*boostrap -> (min-width:1200px)*/ 
        @media (max-width: 1199px) {
            .modalDialog-Detalle {
                width: 75%; 
                margin:auto;
            }
            .carouselContenedor{
                width:100%;
            }            
        }
        /*boostrap -> (min-width:992px)*/ 
        @media (max-width: 991px) {
            .text-right {
                text-align:left;
            }
        }

        /* Imagen : boostrap -> (min-width:768px) */
        @media (max-width: 767px) {               
            .carouselContenedorImagen {
                display:flex; height:100%; text-align:center; 
            }
            .carouselImagen {
                margin:auto;
                height:190px;                
            }
            .modalDialog-Detalle {
                width: 85%;
                margin:auto;
            }
        }

        /* Imagen */
        @media (max-width: 526px) {    
            .carouselContenedorImagen {
                display:flex; height:100%; text-align:center; 
            }
            .carouselImagen {
                margin:auto;
                height:150px;
            }
            .modalDialog-Detalle {
                width: 95%;      
                margin:auto;          
            }
        }

        .campo-Numero-Pagina {
            display:inline-block;
            padding:2px;
            text-align:center;
        }
    </style>

    <script type="text/javascript">
        var idGrid = '#<%=GridViewResultado.ClientID%>';
        var idTxtPaginador = '<%=TextBoxPaginaActual.ClientID%>';
        var idTxtBusqueda = '<%=TextBoxBusqueda.ClientID%>';
        var idUpdatePanelBusqueda = '<%=UpdatePanelBusqueda.ClientID%>';
        var idButtonBuscar = '<%=ButtonBuscar.ClientID%>';
        var idHiddenClave = '#<%=HiddenFieldClave.ClientID%>';
        //var idCheckIsNovedad = '#< % =CheckIsNovedad.ClientID%>';
        
        var $gridDetalleResponsables;
        var $gridDetalleIsbn;
        
    </script>
    <%: Scripts.Render("~/bundles/Pages-Default") %>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title-page">Catálogo</h2>   
    <div class="jumbotron customJumbotron" style="font-size:small; padding:20px; padding-top:10px;padding-bottom:10px;background-color:transparent;border-color:lightgray;border-style:solid;border-width:thin;">
        <div class="input-group">
            <span class="input-group-addon" id="basic-addon1">Q</span>
            <asp:TextBox ID="TextBoxBusqueda" runat="server" CssClass="form-control img-responsive" aria-describedby="basic-addon1" placeholder="Búsqueda"> </asp:TextBox>
            <%--<asp:RequiredFieldValidator ID="Req" runat="server" ControlToValidate="TextBoxBusqueda" Text="REQUERIDO"  ValidationGroup="BusquedaGroup" ErrorMessage="REQUERDIO" EnableClientScript="true" ></asp:RequiredFieldValidator>--%>
            <br />
            <span class="input-group-btn">
                
                <!-- Al usar una validacion, al form del master page se la asigna al evento "onsubmit" la funcion "WebForm_OnSubmit" -->
                <!-- Al estar asignado el boton al UpdatePanel  "UpdatePanelBusqueda", en "AsyncPostBackTrigger", realiza un postback asincrono -->                
                <!-- Al realizar un postback asincrono, es necesario validar manualmente antes de que se realice un postback asincrono , mediante el "ValidationGroup" -->
                <!-- CausesValidation="true" ValidationGroup="BusquedaGroup" OnClientClick="return validarBusqueda('BusquedaGroup');"  -->
                <asp:Button ID="ButtonBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="ButtonBuscar_Click" UseSubmitBehavior="false"/>
            </span>
        </div>
    </div>

    <%-- RESULTADOS--%>

  <div class="panel panel-default primaryTheme" style="margin-top: 30px;">
        <div class="panel-heading">
            <h3 class="panel-title text-center">RESULTADOS
            </h3>
        </div>
        <div class="panel-body" style="min-height: 200px;">
            <div class="row">
                <div class="col-md-12" style="position:relative;">
                    <asp:UpdatePanel ID="UpdatePanelBusqueda" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <asp:HiddenField ID="HiddenCampoBusqueda" runat="server" Value="" />
                            <asp:GridView ID="GridViewResultado" runat="server" SkinID="GridViewMediumPrimary" PageSize="10" AllowPaging="true" OnPageIndexChanging="GridViewResultado_PageIndexChanging" OnRowDataBound="GridViewResultado_RowDataBound" AllowCustomPaging="true"
                                ShowHeaderWhenEmpty="false" AllowSorting="true" OnSorting="GridViewResultado_Sorting" OnRowCommand="GridViewResultado_RowCommand" OnSorted="GridViewResultado_Sorted" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Título" ItemStyle-CssClass="gridViewColumnHidden column-hidden-responsive-sm gridHeader" HeaderStyle-CssClass="gridViewColumnHidden column-hidden-responsive-sm" >
                                        <ItemTemplate>
                                            <span> TÍTULO </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Id Titulo" DataField="IdTitulo" ItemStyle-CssClass="gridViewColumnHidden" HeaderStyle-CssClass="gridViewColumnHidden" ></asp:BoundField>
                                    <asp:TemplateField HeaderText=" " HeaderStyle-CssClass="gridViewColumnBlock" ItemStyle-CssClass="gridViewColumnBlock" >
                                        <ItemTemplate>
                                            <b class="element-hidden-responsive-md">Versiones:</b>
                                            <div style="width:70px;text-align:center;">
                                                <asp:Image ID="ImagenPdf" runat="server" AlternateText="Pdf" ImageUrl="~/images/Icon-document-pdf.png" Width="25" ToolTip="Versión PDF" CssClass="image-src-libro" />
                                                <asp:Image ID="ImageVirtual" runat="server" AlternateText="Virtual" ImageUrl="~/images/Icon-document-virtual.png" Width="20" ToolTip="Versión Virtual" CssClass="image-src-libro" />
                                                <asp:Image ID="ImageOnline" runat="server" AlternateText="Online" ImageUrl="~/images/Icon-document-web.png" Width="25" ToolTip="Versión Online" CssClass="image-src-libro" />
                                            </div>                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Título Original" HeaderStyle-CssClass="gridHeader">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="LinkSortTituloOriginal" runat="server"  CommandArgument="tituloOriginal" CommandName="Sort" >
                                                <ul style="list-style:none;display:block;padding:0px;margin:0px;list-style-type:none;" >
                                                    <li style="display:block;padding:0px;margin:0px; position:relative; color:white;font-size:medium;">                                                        
                                                        <span style="float:left; width:100%;">Título Original</span>
                                                         <span style="float: right; height:15px;right:0px;top:10px; position:absolute;" class="caret"></span>
                                                    </li>
                                                </ul>
                                            </asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <b class="element-hidden-responsive-md">Título Original:</b>
                                            <asp:Label ID="LabelNombre" runat="server" Text='<%#Eval("TituloOriginal")%>'></asp:Label>
                                        </ItemTemplate>                                        
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Título" HeaderStyle-CssClass="gridHeader">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="LinkSortTitulo" runat="server"  CommandArgument="titulo" CommandName="Sort" >
                                                <ul style="list-style:none;display:block;padding:0px;margin:0px;list-style-type:none;" >
                                                    <li style="display:block;padding:0px;margin:0px; position:relative; color:white;font-size:medium;">                                                        
                                                        <span style="float:left; width:100%;">Título</span>
                                                         <span style="float: right; height:15px;right:0px;top:10px; position:absolute;" class="caret"></span>
                                                    </li>                                                    
                                                </ul>                                                                                                                
                                            </asp:LinkButton>
                                            <%--
                                                 SortExpression="Titulo"
                                                <a style="color:white;font-size:medium;display:block;" href="javascript:__doPostBack('<%=GridViewResultado.UniqueID%>','Sort$Titulo')">
                                                <ul style="list-style:none;display:block;padding:0px;margin:0px;list-style-type:none;" >
                                                    <li style="display:block;padding:0px;margin:0px; position:relative;">                                                        
                                                        <span style="float:left; width:100%;">Titulo</span>
                                                         <span style="float: right; height:15px;right:0px;top: 10px; position:absolute;" class="caret"></span>
                                                    </li>
                                                </ul>
                                            </a>--%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <b class="element-hidden-responsive-md">Título:</b>
                                            <asp:Label ID="LabelDescripcion" runat="server" Text='<%#Eval("Titulo")%>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Autor" HeaderStyle-CssClass="gridHeader">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="LinkSortAutor" runat="server"  CommandArgument="autor" CommandName="Sort" >
                                                <ul style="list-style:none;display:block;padding:0px;margin:0px;list-style-type:none;" >
                                                    <li style="display:block;padding:0px;margin:0px; position:relative; color:white;font-size:medium;">                                                        
                                                        <span style="float:left; width:100%;">Autor</span>
                                                         <span style="float: right; height:15px;right:0px;top:10px; position:absolute;" class="caret"></span>
                                                    </li>                                                    
                                                </ul>                                                                                                                
                                            </asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <b class="element-hidden-responsive-md">Autor:</b>
                                            <asp:Label ID="LabelAutor" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Traductor/responsables" >
<%--                                        <HeaderTemplate>
                                            <asp:LinkButton ID="LinkSortAutor" runat="server"  CommandArgument="autor" CommandName="Sort" >
                                                <ul style="list-style:none;display:block;padding:0px;margin:0px;list-style-type:none;" >
                                                    <li style="display:block;padding:0px;margin:0px; position:relative; color:white;font-size:medium;">                                                        
                                                        <span style="float:left; width:100%;">Autor</span>
                                                         <span style="float: right; height:15px;right:0px;top:10px; position:absolute;" class="caret"></span>
                                                    </li>                                                    
                                                </ul>                                                                                                                
                                            </asp:LinkButton>
                                        </HeaderTemplate>--%>
                                        <ItemTemplate>
                                            <b class="element-hidden-responsive-md">Traductor/responsables:</b>
                                            <asp:Label ID="LabelTraductor" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Año" HeaderStyle-CssClass="gridHeader">
                                         <HeaderTemplate>
                                            <asp:LinkButton ID="LinkSortAnio" runat="server"  CommandArgument="año" CommandName="Sort" >
                                                <ul style="list-style:none;display:block;padding:0px;margin:0px;list-style-type:none;" >
                                                    <li style="display:block;padding:0px;margin:0px; position:relative; color:white;font-size:medium;">                                                        
                                                        <span style="float:left; width:100%;">Año&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                                        <span style="float: right; height:15px;right:0px;top:10px; position:absolute;" class="caret"></span>
                                                    </li>                                                    
                                                </ul>                                                                                                                
                                            </asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                             <div class="element-hidden-responsive-md" >
                                                <b>Año:</b>
                                            </div>
                                             <asp:Label ID="LabelAnio" runat="server" Text='<%#Eval("AnioPublicacion")%>'></asp:Label>                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                </Columns>                                
                                <%--La asignación automática de la dirección (SortDirection) solamente funciona con una fuente de datos SqlDataSource, así como la asignación de los estilos. --%>
                                <%--<SortedAscendingHeaderStyle CssClass="caret.caret-up" /> --%>
                                <PagerTemplate>
                                    <div class="container">
                                        <div class="row">
                                            <div class="col-sm-7">
                                                Registros XXX al XXXX 
                                            </div>
                                            <div class="col-sm-2 text-center" style="padding:0px;">
                                                <asp:LinkButton runat="server" ID="cmdInicio" CommandName="Page" CommandArgument="First" Text="<<" CssClass="btn btn-primary">
                                                </asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="cmdAnterior" CommandName="Page" CommandArgument="Prev" Text="<" CssClass="btn btn-primary"></asp:LinkButton>
                                            </div>
                                            <div class="col-sm-1 center text-center" style="padding:0px;">
                                                 Página N de N
                                            </div>
                                            <div class="col-sm-2 text-center" style="padding:0px;">
                                                <asp:LinkButton runat="server" ID="cmdSiguiente" CommandName="Page" CommandArgument="Next" CssClass="btn btn-primary" Text=">"></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="cmdFinal" CommandName="Page" CommandArgument="Last" CssClass="btn btn-primary" Text=">>"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    
                                </PagerTemplate>
                            </asp:GridView>
                            <div id="FooterGridResultado" runat="server" class="container-fluid footer-Paging" style="padding:0px;">
                                <div class="row" >
                                    <div class="col-md-7" >
                                     Registros 
                                            <asp:Label ID="LabelRegistroInicial" runat="server"></asp:Label> 
                                        al  <asp:Label ID="LabelRegistroFinal" runat="server"></asp:Label>
                                        
                                    </div>
                                    <div class="col-md-5 center text-center" style="padding: 0px;">
                                        <asp:LinkButton runat="server" ID="cmdInicio" CommandName="Page" CommandArgument="First" Text="<<" CssClass="btn btn-primary" OnClick="CommandPage_Click">
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="cmdAnterior" CommandName="Page" CommandArgument="Prev" Text="<" CssClass="btn btn-primary" OnClick="CommandPage_Click"></asp:LinkButton>
                                        Página 
                                        <asp:TextBox ID="TextBoxPaginaActual" runat="server" CssClass="form-control campo-Numero-Pagina" Width="50" ></asp:TextBox>                                         
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="TextBoxPaginaActual" FilterType="Numbers" />
                                        de <asp:Label ID="LabelPaginaFinal" runat="server"></asp:Label>

                                        <asp:LinkButton runat="server" ID="cmdSiguiente" CommandName="Page" CommandArgument="Next" CssClass="btn btn-primary" Text=">" OnClick="CommandPage_Click"></asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="cmdFinal" CommandName="Page" CommandArgument="Last" CssClass="btn btn-primary" Text=">>" OnClick="CommandPage_Click"></asp:LinkButton>

                                    </div>
                                   
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>                            
                            <asp:AsyncPostBackTrigger ControlID="ButtonBuscar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cmdInicio" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cmdAnterior" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cmdSiguiente" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cmdFinal" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="UpdateBusquedaProgress" runat="server" AssociatedUpdatePanelID="UpdatePanelBusqueda" DisplayAfter="400">
                        <ProgressTemplate>
                            <div id="wait" style="text-align: center; display: block; width: 200px; height: 50px; border: 1px solid #F78181; position: relative; margin: auto;padding: 10px; background-color: #FBF2EF; color: #5f3f3f; vertical-align: middle;">
                                <i class="fa fa-spinner fa-pulse fa-2x fa-fw"></i>
                                <span class="" style="vertical-align: middle">
                                    <img src="../images/uploading.gif" width="30" />
                                    <br />
                                    Cargando...
                                </span>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>
            </div>
        </div>
    </div>
    

    <%-- POPUP Detalle Titulo--%>
    <div id="DialogoDetalleTitulo" class="modal fade" role="dialog" >
        <div class="modal-dialog primaryTheme modalDialog-Detalle">
            <div class="modal-content">
                <!-- Cabecera -->
                <div class="modal-header btn-primary ">
                    <button type="button" class="close" aria-label="Close" data-dismiss="modal"><span aria-hidden="true">&times;</span></button>
                    <!-- Titulo -->
                    <div class="modal-title">
                        <h4>Detalle</h4>
                    </div>
                </div>
                <!-- body -->
                <div class="modal-body" style="position:relative; padding: 20px; padding-bottom:8px;">                                 
                    <div class="form-horizontal" >
                        <asp:HiddenField ID="HiddenFieldClave" runat="server" Value="" />
                        
                        <ul class="nav nav-tabs">
                            <li class="active"><a data-toggle="tab" href="#home">Descripción del título</a></li>
                        </ul>

                        <div id="tabContentDetalleLoading" class="tab-content" style="padding-top:20px; text-align:center; vertical-align:central; ">
                            <img src="../../images/gifAjax.gif" style="width:120px; height:120px;" />
                        </div>

                        <div id="tabContentDetalle" class="tab-content" style="padding-top:20px; height:auto;">
                            <%--  TAB DETALLE --%>
                            <div id="home" class="tab-pane fade in active" style="display:inline-block;width:100%;">

                                <div class="carouselContenedorImagen" style="overflow-x: auto;">
                                    <img id="ImagenTitulo" class="carouselImagen" alt="Titulo"/>
                                </div>
                                 <div class="modal-footer" style="margin:0px;margin-top:5px;padding:0px;height:2px;"></div>
                                <div class="col-md-12 form-group" style="margin:0px;">
                                    <div class="text-center" style="max-height: 170px; overflow: auto;">
                                        <span id="SpanTitulo" class="control-label"></span>
                                    </div>
                                    <div id="bAutor" class="text-center" style="max-height: 170px; overflow: auto;">
                                        <b>Autor:</b>
                                        <span id="SpanAutor" class="control-label"></span>
                                    </div>
                                    <div id="bEditor" class="text-center">
                                        <b>Editor:</b>
                                        <span id="SpanEditor" class="control-label"></span>
                                    </div>
                                    <div id="bEdicion" class="text-center">
                                        <b>Edición:</b>
                                        <span id="SpanEdicion" class="control-label"></span>
                                    </div>
                                    <div class="panel-group" id="accordion" style="max-height:320px; overflow:auto;">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapse1">Descripción</a>
                                                </h4>
                                            </div>
                                            <div id="collapse1" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                    <div id="bReimpresion" class="col-md-12 text-justify" style="max-height: 100px; overflow: auto;">
                                                        <b>Reimpresión:</b>
                                                        <span id="SpanReimpresión" class="control-label"></span>
                                                    </div>
                                                    <div id="bCiudad" class="col-md-12 text-justify" style="max-height: 100px; overflow: auto;">
                                                        <b>Ciudad:</b>
                                                        <span id="SpanCiudad" class="control-label"></span>
                                                    </div>
                                                    <div id="bCualidades" class="col-md-12 text-justify" style="max-height: 170px; overflow: auto;">
                                                        <b>Cualidades:</b>
                                                        <span id="SpanCualidades" class="control-label"></span>
                                                    </div>
                                                    <div id="bUFFYL" class="col-md-12 text-justify" style="max-height: 100px; overflow: auto;">
                                                        <b>UFFYL:</b>
                                                        <span id="SpanU_FFYL" class="control-label"></span>
                                                    </div>
                                                    <div id="bUIIFL" class="col-md-12 text-justify" style="max-height: 100px; overflow: auto;">
                                                        <b>UIIFL:</b>
                                                        <span id="SpanU_IIFL" class="control-label"></span>
                                                    </div>
                                                    <div id="bFlagLatin" class="col-md-12 text-justify" style="max-height: 100px; overflow: auto;">
                                                         <span class="aspNetDisabled">
                                                             <input id="bRadioIsLatin" type="radio" value="RadioIsLatin" disabled="disabled">
                                                             <label for="bRadioIsLatin">Latin</label>
                                                         </span>
                                                        <b style="padding-left:10px"></b>
                                                         <span class="aspNetDisabled">
                                                             <input id="bRadioIsGriego" type="radio" value="RadioIsGriego" disabled="disabled">
                                                             <label for="bRadioIsGriego">Griego</label>
                                                         </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapse2">Contenido</a>
                                                </h4>
                                            </div>
                                            <div id="collapse2" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                    <div id="bTema" class="col-md-12">
                                                        <b>Tema:</b>
                                                        <div class="text-justify" style="max-height: 170px; overflow: auto;">
                                                            <span id="SpanTema" class="control-label"></span>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12"><br /></div>
                                                    <div id="bContenido" class="col-md-12">
                                                        <b>Contenido:</b>
                                                        <div class="text-justify" style="max-height: 200px; overflow: auto;">
                                                            <span id="SpanContenido" class="control-label"></span>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12"><br /></div>
                                                    <div id="bColofon" class="col-md-12">
                                                        <b>Colofón:</b>
                                                        <div class="text-justify" style="max-height: 200px; overflow: auto;">
                                                            <span id="SpanColofon" class="control-label"></span>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12"><br /></div>
                                                    <div id="bObservaciones" class="col-md-12">
                                                        <b>Observaciones:</b>
                                                        <div class="text-justify" style="max-height: 200px; overflow: auto;">
                                                            <span id="SpanObservaciones" class="control-label"></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapse3">ISBN</a>
                                                </h4>
                                            </div>
                                            <div id="collapse3" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                    <div class="col-md-12 text-justify" style="max-height: 320px; overflow: auto;">
                                                        <span id="SpanIsbn" class="control-label"></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapse4">Responsables</a>
                                                </h4>
                                            </div>
                                            <div id="collapse4" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                    <div class="col-md-12 text-justify" style="max-height: 320px; overflow: auto;">
                                                        <span id="SpanResponsables" class="control-label"></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                </div>

                            </div>
                        </div>

                    </div>
                </div>
                <!-- footer -->
                <div class="modal-footer" style="margin-top:0px;">                    
                    <input type="button" value="Cerrar" class="btn btn-default" data-dismiss="modal" />                        
                </div>
            </div>
            
        </div>               
    </div>
        
</asp:Content>