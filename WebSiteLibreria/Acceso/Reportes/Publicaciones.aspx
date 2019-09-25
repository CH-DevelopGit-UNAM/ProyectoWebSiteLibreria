<%@ Page Title="Publicaciones" Language="C#" MasterPageFile="~/ResponsiveSite.master" AutoEventWireup="true" CodeFile="Publicaciones.aspx.cs" Inherits="Acceso_Reportes_Publicaciones" Theme="SkinBase" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" Runat="Server">
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

        /* Imagen : boostrap -> (min-width:768px)  */
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
        @media screen and (max-width: 526px) {    
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
            .busqueda-Anio {
                 float:none;
            }

            div [id*='DivRangosBusqueda'].busqueda-Anio {
                float:none;
                text-align:center;                
                width:100%; 
                margin:auto;                
            }

             .busqueda-tipo {
                 float:none;
             }

             div [id*='DivContenedorTipoTexto'].busqueda-tipo {
                float:none;
               
            }

            div [id*='DivBotonBusqueda'].busqueda-custom  {                
                float:none;
                text-align:center;                
                width:100%;
                padding-top:15px;                
            }

            div [id*='DivBotonBusqueda'] > input[type=button] {                
            }

        }

        .campo-Numero-Pagina {
            display:inline-block;
            padding:2px;
            text-align:center;
        }

         .busqueda-Anio {
             float: left;             
         }

        div [id*='DivRangosBusqueda'].busqueda-Anio ul{
             list-style: none; display:table-row;
        }

        div [id*='DivRangosBusqueda'].busqueda-Anio ul li{
             list-style: none; display:table-row;
             display: table-cell;vertical-align:middle;
             padding-left: 7px;
             padding-right:7px;
        }

        div [id*='DivRangosBusqueda'].busqueda-Anio ul li > input.form-control{
             border-radius:4px;
        }
         .busqueda-tipo {
             width: 200px; float: left;
         }
        div [id*='DivBotonBusqueda'].busqueda-custom  {                
                float:left; padding-left:30px;

        }

         input[id*='TextBoxBusqueda'].clase {
             width:300px;
             float:left;
         }



         /*.clase2 {
             width:300px;
             border-radius: 5px;
             padding: 6px 12px;
             font-size: 14px;
             line-height: 1.42857143;
             color: #555;
             background-color: #fff;
             background-image: none;
             border: 1px solid #ccc;
             border-radius: 4px;
             -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
             box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
             -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
             -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
             transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
         }*/

    </style>
    <script type="text/javascript">
        var idGrid = '#<%=GridViewResultado.ClientID%>';        
        var idTxtBusqueda = '<%=TextBoxBusqueda.ClientID%>';
        var idUpdatePanelBusqueda = '<%=UpdatePanelBusqueda.ClientID%>';
        
        var idHiddenClave = '#<%=HiddenFieldClave.ClientID%>';        
        var $gridDetalleResponsables;
        var $gridDetalleIsbn;
        var idImgExcel = '<%=ButtonExportar.ClientID%>';
        var idTipoReporte = '<%=HiddenTipoReporte.ClientID%>';    
        var idCampoBusqueda = '<%=HiddenCampoBusqueda.ClientID%>';    
        var idCampoFuncion = '<%=HiddenNombreFuncion.ClientID%>';    
        var idCampoInicio = '<%=HiddenCampoInicio.ClientID%>';        
        var idCampoFin = '<%=HiddenCampoFin.ClientID%>';

        PageMethods.set_path("Publicaciones.aspx");

        $(document).ready(function () {            
            //var idRpt = $("#" + idTipoReporte).val();
            //if (idRpt == "0") {
            //    $("#DivContenedorBusquedas").attr("style", "margin:auto;");
            //    $("#DivBotonBusqueda").removeClass("input-group-btn");
            //    $("#DivBotonBusqueda").attr("style", "float:left; padding-left:30px;");
            //} else if (idRpt == "2") {
            //    $("#DivContenedorBusquedas").attr("style", "margin:auto;");
            //    $("#DivBotonBusqueda").removeClass("input-group-btn");
            //    $("#DivBotonBusqueda").attr("style", "float:left; padding-left:30px;");
            //}
        });
    </script>

    <%--<script src="/Scripts/Pages/default.js"></script>--%>
    <%: Scripts.Render("~/bundles/Pages-Default") %>

    <script type="text/javascript">
        function autoPostbackPaginador(sender, args) {
            if (args.keyCode === 13) {
                __doPostBack(sender.id, sender.value);
                return true;
            }
            event.preventDefault();
            event.stopPropagation();
            return false;
        }
        
        function pageLoad() {
            $(document).ready(function () {
                configurarGridView(idGrid, function (items, element) {
                    var idTitulo = tryParseInteger($(items[1]).text(), 0);
                    setDataModalDetalle(idTitulo, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", urlSite + "Images/avatar.png", "avatar.png", false, "", "", "", "", "");
                    if (idTitulo > 0) {
                        $("#DialogoDetalleTitulo").modal('show');
                    } else {
                        alert("No hay datos a cargar para el titulo seleccionado");
                    }
                });

                $("#" + idImgExcel).click(function (e) {
                    var busqueda = $("#" + idCampoBusqueda).val();
                    var inicio = $("#" + idCampoInicio).val();
                    var fin = $("#" + idCampoFin).val();                                        
                    var idRpt= $("#" + idTipoReporte).val();
                    var funcion = $("#" + idCampoFuncion).val();
                    window.open("GenerarReportePublicaciones.ashx?tipoRpt=" + idRpt + "&tipo=excel&param1=" + busqueda + "&param2=" + inicio + "&param3=" + fin + "&param4=" + funcion + "&ord=");
                    return false;
                });

            });
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2 class="title-page"><asp:Label ID="LabelTipoReporte" runat="server"></asp:Label></h2>
    <div class="jumbotron customJumbotron" style="font-size: small; padding: 30px; padding-top: 10px; padding-bottom: 10px; background-color: transparent; border-color: lightgray; border-style: none; ">
        <%-- Default: Autor --%>
        <asp:HiddenField ID="HiddenTipoReporte" runat="server" Value="1" />
        <div id="DivContenedorBusquedas" runat="server">
            <div  id="DivGroupBusqueda" runat="server" class="input-group" style="margin:auto;" >
                <asp:TextBox ID="TextBoxBusqueda" runat="server" CssClass="form-control" placeholder="Búsqueda" MaxLength="50"> 
                </asp:TextBox>
                <div id="DivRangosBusqueda" runat="server" class="busqueda-Anio">
                    <ul>
                        <li><b>Año Inicial:</b></li>
                        <li>
                            <asp:TextBox ID="TextBoxInicio" runat="server" CssClass="form-control" placeholder="Inicio" MaxLength="6" Width="75"> </asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredInicio" runat="server" TargetControlID="TextBoxInicio" FilterType="Numbers" />
                        </li>
                        <li><b>Año Final:</b></li>
                        <li>
                            <asp:TextBox ID="TextBoxFinal" runat="server" CssClass="form-control" placeholder="Fin" MaxLength="6" Width="75"> </asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredFin" runat="server" TargetControlID="TextBoxFinal" FilterType="Numbers" />
                        </li>
                    </ul>
                </div>
                <div id="DivContenedorTipoTexto" runat="server" class="busqueda-tipo">
                    <table style="width:100%">
                        <tr>
                            <td><b style="padding-left:7px; padding-right:7px;">Tipo:</b></td>
                            <td>
                                <%--data-selected-text-format="count" multiple --%>
                                <asp:DropDownList ID="DropDownTipoTexto" runat="server" CssClass="selectpicker show-tick form-control" data-style="btn-default">
                                    <asp:ListItem Text="Español" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Griego" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Latin" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="DivContenedorFuncion" runat="server" style="width: 220px; float: left;">
                    <table style="width:100%">
                        <tr>
                            <td style="width:75px;"><b style="padding-left:7px; padding-right:7px;">Funcion:</b></td>
                            <td>
                                <asp:DropDownList ID="DropDownFuncion" runat="server" CssClass="selectpicker show-tick form-control" data-style="btn-default">
                                    <asp:ListItem Text="No especificado..." Value=""></asp:ListItem>
                                    <asp:ListItem Text="Traductor" Value="FUN-000002"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>               
                <div id="DivBotonBusqueda" runat="server" class="input-group-btn">
                    <asp:Button ID="ButtonBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary index" UseSubmitBehavior="false" OnClick="ButtonBuscar_Click"  />
                </div>
            </div>           

        </div>
    </div>

    <div class="panel panel-default primaryTheme" style="margin-top: 30px;">
        <div class="panel-heading">
            <h3 class="panel-title text-center">RESULTADOS
            </h3>
        </div>        
        <div class="panel-body" style="min-height: 200px;">
            <div class="row">
                <div class="col-md-12" style="position:relative;">                    
                     <asp:UpdatePanel ID="UpdatePanelBusqueda" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" >
                        <ContentTemplate>
                            <asp:HiddenField ID="HiddenCampoBusqueda" runat="server" Value="" />
                            <asp:HiddenField ID="HiddenNombreFuncion" runat="server" Value="" />
                            <asp:HiddenField ID="HiddenCampoInicio" runat="server" Value="" />
                            <asp:HiddenField ID="HiddenCampoFin" runat="server" Value="" />

                            <asp:Button ID="ButtonExportar" runat="server" Text="Descargar" CssClass="btn btn-default" Visible="false" />
                            <br />
                            <br />
                            <%--AllowSorting="true" OnSorting="GridViewResultado_Sorting"--%>
                            <asp:GridView ID="GridViewResultado" runat="server" SkinID="GridViewPrimary" PageSize="13" AllowPaging="true" OnPageIndexChanging="GridViewResultado_PageIndexChanging" OnRowDataBound="GridViewResultado_RowDataBound" 
                                ShowHeaderWhenEmpty="false" OnRowCommand="GridViewResultado_RowCommand"  >
                                <Columns>
                                    <asp:TemplateField HeaderText="Título" ItemStyle-CssClass="gridViewColumnHidden column-hidden-responsive-sm gridHeader" HeaderStyle-CssClass="gridViewColumnHidden column-hidden-responsive-sm" >
                                        <ItemTemplate>
                                            <span> TÍTULO </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Id Titulo" DataField="IdTitulo" ItemStyle-CssClass="gridViewColumnHidden" HeaderStyle-CssClass="gridViewColumnHidden" ></asp:BoundField>

                                    <asp:TemplateField HeaderText=" " HeaderStyle-CssClass="gridViewColumnBlock" ItemStyle-CssClass="gridViewColumnBlock" >
                                        <ItemTemplate>
                                            <b class="element-hidden-responsive-sm">Versiones:</b>
                                            <div style="width:70px;text-align:center;">
                                                <asp:Image ID="ImagenPdf" runat="server" AlternateText="Pdf" ImageUrl="~/images/Icon-document-pdf.png" Width="25" ToolTip="Versión PDF" CssClass="image-src-libro" />
                                                <asp:Image ID="ImageVirtual" runat="server" AlternateText="Virtual" ImageUrl="~/images/Icon-document-virtual.png" Width="20" ToolTip="Versión Virtual" CssClass="image-src-libro" />
                                                <asp:Image ID="ImageOnline" runat="server" AlternateText="Online" ImageUrl="~/images/Icon-document-web.png" Width="25" ToolTip="Versión Online" CssClass="image-src-libro" />
                                            </div>                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--HeaderStyle-CssClass="gridHeader"--%>
                                    <asp:TemplateField HeaderText="Título Original" >
                                       <%-- <HeaderTemplate>
                                            <asp:LinkButton ID="LinkSortTituloOriginal" runat="server"  CommandArgument="tituloOriginal" CommandName="Sort" >
                                                <ul style="list-style:none;display:block;padding:0px;margin:0px;list-style-type:none;" >
                                                    <li style="display:block;padding:0px;margin:0px; position:relative; color:white;font-size:medium;">                                                        
                                                        <span style="float:left; width:100%;">Titulo Original</span>
                                                         <span style="float: right; height:15px;right:0px;top:10px; position:absolute;" class="caret"></span>
                                                    </li>
                                                </ul>
                                            </asp:LinkButton>
                                        </HeaderTemplate>--%>
                                        <ItemTemplate>
                                            <b class="element-hidden-responsive-sm">Título Original:</b>
                                            <asp:Label ID="LabelNombre" runat="server" Text='<%#Eval("TituloOriginal")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--HeaderStyle-CssClass="gridHeader"--%>
                                    <asp:TemplateField HeaderText="Titulo" >
                                         <%--<HeaderTemplate>
                                            <asp:LinkButton ID="LinkSortTitulo" runat="server"  CommandArgument="titulo" CommandName="Sort" >
                                                <ul style="list-style:none;display:block;padding:0px;margin:0px;list-style-type:none;" >
                                                    <li style="display:block;padding:0px;margin:0px; position:relative; color:white;font-size:medium;">                                                        
                                                        <span style="float:left; width:100%;">Titulo</span>
                                                         <span style="float: right; height:15px;right:0px;top:10px; position:absolute;" class="caret"></span>
                                                    </li>                                                    
                                                </ul>                                                                                                                
                                            </asp:LinkButton>                                            
                                        </HeaderTemplate>--%>
                                        <ItemTemplate>
                                            <b class="element-hidden-responsive-sm">Título:</b>
                                            <asp:Label ID="LabelDescripcion" runat="server" Text='<%#Eval("Titulo")%>' ></asp:Label>                                          
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--HeaderStyle-CssClass="gridHeader"--%>
                                    <asp:TemplateField HeaderText="Autor" >
                                        <%--<HeaderTemplate>
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
                                            <b class="element-hidden-responsive-sm">Autor:</b>
                                            <asp:Label ID="LabelAutor" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Traductor/responsables" HeaderStyle-CssClass="column-display-responsive-md" ItemStyle-CssClass="column-display-responsive-md">
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
                                            <b class="element-hidden-responsive-sm">Traductor/responsables:</b>
                                            <asp:Label ID="LabelTraductor" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                                 
                                    <asp:TemplateField HeaderText="Archivo" ItemStyle-CssClass="gridViewColumnHidden" HeaderStyle-CssClass="gridViewColumnHidden">
                                        <ItemTemplate>                                            
                                             <asp:Label ID="LabelRuta" runat="server" Text='<%# string.Format(@"{0}",Eval("RutaArchivo"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <%--HeaderStyle-CssClass="gridHeader"--%>
                                    <asp:TemplateField HeaderText="Año">
                                         <%--<HeaderTemplate>
                                            <asp:LinkButton ID="LinkSortAnio" runat="server"  CommandArgument="año" CommandName="Sort" >
                                                <ul style="list-style:none;display:block;padding:0px;margin:0px;list-style-type:none;" >
                                                    <li style="display:block;padding:0px;margin:0px; position:relative; color:white;font-size:medium;">                                                        
                                                        <span style="float:left; width:100%;">Año&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                                         <span style="float: right; height:15px;right:0px;top:10px; position:absolute;" class="caret"></span>
                                                    </li>                                                    
                                                </ul>                                                                                                                
                                            </asp:LinkButton>
                                        </HeaderTemplate>--%>
                                        <ItemTemplate>
                                             <div class="element-hidden-responsive-sm" >
                                                <b>Año:</b>
                                            </div>
                                             <asp:Label ID="LabelAnio" runat="server" Text='<%#Eval("AnioPublicacion")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                                                       
                                                                                                                                                   
                                </Columns>
                                <PagerTemplate>
                                    <div  id="FooterGridResultado" class="container-fluid footer-Paging" style="padding:0px;">
                                        <div class="row">
                                            <div class="col-md-7">
                                                    Registros
                                                    <%=(GridViewResultado.PageIndex * GridViewResultado.PageSize) + 1%>
                                                    al
                                                    <%= ((GridViewResultado.PageIndex * GridViewResultado.PageSize) + GridViewResultado.Rows.Count)%>
                                            </div>
                                            <div class="col-md-5 center text-center" style="padding: 0px;">
                                                <asp:LinkButton runat="server" ID="cmdInicio" CommandName="Page" CommandArgument="First" Text="<<" CssClass="btn btn-primary">
                                                </asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="cmdAnterior" CommandName="Page" CommandArgument="Prev" Text="<" CssClass="btn btn-primary"></asp:LinkButton>
                                                Página 
                                        <%--AutoPostBack="true" OnTextChanged="TextBoxPaginaActual_TextChanged"--%>
                                                <asp:TextBox ID="TextBoxPaginaActual" runat="server" CssClass="form-control campo-Numero-Pagina" Width="50" Text="<%# GridViewResultado.PageIndex + 1 %>"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender id="FilteredTextBoxExtender3" runat="server" targetcontrolid="TextBoxPaginaActual" filtertype="Numbers" />
                                                de
                                                <asp:Label ID="LabelPaginaFinal" runat="server" Text="<%# GridViewResultado.PageCount%>"></asp:Label>

                                                <asp:LinkButton runat="server" ID="cmdSiguiente" CommandName="Page" CommandArgument="Next" CssClass="btn btn-primary" Text=">" ></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="cmdFinal" CommandName="Page" CommandArgument="Last" CssClass="btn btn-primary" Text=">>"></asp:LinkButton>

                                            </div>
                                        </div>
                                    </div>
                                </PagerTemplate>
                            </asp:GridView>                            
                        </ContentTemplate>
                        <Triggers>    
                            <asp:AsyncPostBackTrigger ControlID="ButtonBuscar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
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
                                                    <div  id="bColofon" class="col-md-12">
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