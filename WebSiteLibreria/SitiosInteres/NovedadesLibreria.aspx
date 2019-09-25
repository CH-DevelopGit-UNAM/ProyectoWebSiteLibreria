<%@ Page Title="Novedades" Language="C#" MasterPageFile="~/ResponsiveSite.master" AutoEventWireup="true" CodeFile="NovedadesLibreria.aspx.cs" Inherits="SitiosInteres_NovedadesLibreria" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="UserControl" TagName="Carousel" Src="~/UserControls/NovedadesCarousel-v2.0.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" Runat="Server">
    <style>
        div [id*='FooterGridResultado'] {
            bottom:0px;
            
        }
        .empty-result {
            margin:auto;border: 1px solid #ddd;text-align:left;padding:8px;font-family: Calibri;
        }
        /*.container-grid {
            background-color:none;
            padding:0px;
            text-align:center;
            vertical-align:middle;
            align-items:center;
            position:relative;
            min-height:300px;     
            / *max-height:600px;
            overflow:auto;* /       
        }*/       

        /* CSS POPUP */

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

            /*.container-grid {            
                max-height:450px;
                overflow:auto;       
            }*/

            /*.item {            
                height:210px; 
                width:210px;            
            }*/

            /*.item-container {
                height:185px; 
                width:185px;            
            }

            .item-container:hover {
                height:190px;
                width:190px;            
            }
            .item-fondoTitulo {
                opacity: 0.3; height: 73%;
            }*/
            .panel-body {
                padding-left:1px;
                padding-right:1px;
            }
        }

        .campo-Numero-Pagina {
            display:inline-block;
            padding:2px;
            text-align:center;
        }       

    </style>
    
    <script type="text/javascript">
        var idHiddenClave = "#<%=HiddenFieldClave.ClientID%>";
        var idTextoBusqueda = "<%=TextBoxBusqueda.ClientID%>";
        var IdBusqueda = "<%=HiddenCampoBusqueda.ClientID%>";
        var IdTipoBusqueda = "<%=HiddenTipoBusqueda.ClientID%>";
        var IdBusquedaHabilitada = "<%=HiddenBusquedaHabilitada.ClientID%>";
        var idTxtPaginador = undefined;//var idTextPaginaActual = ""; /*TextBoxPaginaActual.ClientID */
               
        var IdPaginaActual = "<%=HiddenPaginaActual.ClientID%>";
        var IdFilasxPagina = "<%=HiddenFilasPorPagina.ClientID%>";        
        var IdPaginasTotales = "<%=HiddenPaginasTotales.ClientID%>";
        var IdFilasTotales = "<%=HiddenFilasTotales.ClientID%>";
        
        var _Paginacion = { PaginaActual: 1, FilasPorPagina: 1, 'PaginasTotales': 0, FilasTotales: 0, TipoBusqueda: '', IsBusqueda: false, Busqueda1: '', Busqueda2: '', IsNavigating: false };
        var _ListaItems = [];
        var _CurrentIndex = 0;
        var _ListaPaginas = [];
        var idGrid = undefined;
    </script>
    <%: Scripts.Render("~/bundles/Pages-Default") %>
    <%: Scripts.Render("~/bundles/Pages-CarouselMultiple") %>   

    <script type="text/javascript">
                
        //PageMethods.set_path("NovedadesLibreria.aspx");
        function pageLoad() {
            // override
            $(document).ready(function () {
                initCarousel();
            });            
        }

        function initCarousel() {
            $(".carousel").flexCarousel({
                Size: 1170, Porcentaje: 0.19, Items: [], Interval: 15000, OnItemCreated: function (item) {
                    var itemCarousel = $.parseHTML(item);                    
                    $(itemCarousel).attr("href", "javascript: void(0);");
                    $(itemCarousel).attr("onClick", "onSelectedtitulo(" + $(itemCarousel).attr("data-id") + ");");
                    return $(itemCarousel)[0].outerHTML;
                }
            });

            var clickEvent = false;
            $('#novedades-carousel').on('click', 'a.carousel-control', function () {
                clickEvent = true;
                if ($(this).hasClass("right")) {

                } else if ($(this).hasClass("left")) {

                }
                //$('.nav li').removeClass('active');
                //$(this).parent().addClass('active');
            }).on('slid.bs.carousel', function (e) {
                if (!clickEvent) {
                    var count = $('.nav').children().length - 1;
                    var current = $('.nav li.active');
                    current.removeClass('active').next().addClass('active');
                    var id = parseInt(current.data('slide-to'));
                    if (count == id) {
                        $('.nav li').first().addClass('active');
                    }
                    //console.log("SLIDE AUTOMATICO");
                } else {
                    //console.log("SLIDE CLICK");
                }
                clickEvent = false;
            });

            //$(".item-inner>a").attr("href","javascript:void(0);");
            //$(".item-inner>a").click(function (e) {
            //    var idTitulo = $(this).attr("data-id");                
            //    setDataModalDetalle(idTitulo, "","", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", urlSite + "Images/avatar.png", "avatar.png", false,"","","","","");
            //    $("#DialogoDetalleTitulo").modal('show');
            //    e.preventDefault();
            //    return false;
            //});            
        }

        function onSelectedtitulo(idTitulo) {
            setDataModalDetalle(idTitulo, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", urlSite + "Images/avatar.png", "avatar.png", false, "", "", "", "", "");
            if (idTitulo > 0) {
                $("#DialogoDetalleTitulo").modal('show');
            } else {
                alert("No hay datos a cargar para el titulo seleccionado");
            }            
            return false;
        }

        // append to default.js
        $(document).ready(function () {
            //initCarousel();            
            var tipoBusqueda = $("#" + IdTipoBusqueda).val();            
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2 class="title-page">Novedades</h2>
    <asp:UpdatePanel ID="UpdatePanelMasterNoticias" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="true" >
        <ContentTemplate>
            <div class="jumbotron customJumbotron" style="font-size: small; padding: 30px; padding-top: 10px; padding-bottom: 10px; background-color: transparent; border-color: lightgray; border-style: none;">
                <div class="input-group">
                    <asp:TextBox ID="TextBoxBusqueda" runat="server" CssClass="form-control img-responsive" placeholder="Búsqueda"> </asp:TextBox>
                    <br />
                    <div class="input-group-btn">
                        <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                            <span id="SpanSearch" runat="server" class="glyphicon glyphicon-search" aria-hidden="true"></span>
                            <span class="caret"></span>
                        </button>
                        <ul class="dropdown-menu pull-right primary-dropdown">
                            <li>
                                <%--<a id="BtnBuscarNombre" href="javascript:void(0);" class="dropdown-item" onclick="initSearch('nombre');">Nombre</a>--%>
                                <asp:LinkButton ID="BtnBuscarNombre" runat="server" CssClass="dropdown-item" Text="Nombre" OnClick="BtnBuscarNombre_Click"></asp:LinkButton>
                            </li>
                            <li>
                                <%--<a id="BtnBuscarTema" href="javascript:void(0);" class="dropdown-item" onclick="initSearch('tema');">Tema</a>--%>
                                <asp:LinkButton ID="BtnBuscarTema" runat="server" CssClass="dropdown-item" Text="Tema" OnClick="BtnBuscarTema_Click"></asp:LinkButton>
                            </li>
                            <li>
                                <%--<a id="BtnBuscarAutor" href="javascript:void(0);" class="dropdown-item" onclick="initSearch('autor');">Autor</a>--%>
                                <asp:LinkButton ID="BtnBuscarAutor" runat="server" CssClass="dropdown-item" Text="Autor" OnClick="BtnBuscarAutor_Click"></asp:LinkButton>
                            </li>
                            <li>
                                <%--<a id="BtnBuscarResponsable" href="javascript:void(0);" class="dropdown-item" onclick="initSearch('responsable');">Responsable</a>--%>
                                <asp:LinkButton ID="BtnBuscarResponsable" runat="server" CssClass="dropdown-item" Text="Responsable" OnClick="BtnBuscarResponsable_Click"></asp:LinkButton>
                            </li>
                            <li>
                                <%--<a id="BtnBuscarCiudad" href="javascript:void(0);" class="dropdown-item" onclick="initSearch('ciudad');">Ciudad</a>--%>
                                <asp:LinkButton ID="BtnBuscarCiudad" runat="server" CssClass="dropdown-item" Text="Ciudad" OnClick="BtnBuscarCiudad_Click"></asp:LinkButton>
                            </li>
                            <li>
                                <%--<a id="BtnTodos" href="javascript:void(0);" class="dropdown-item" onclick="initSearch();">Todos</a>--%>
                                <asp:LinkButton ID="BtnTodos" runat="server" CssClass="dropdown-item" Text="Todos" OnClick="BtnTodos_Click"></asp:LinkButton>
                            </li>
                        </ul>

                    </div>
                </div>
                <asp:HiddenField ID="HiddenCampoBusqueda" runat="server" Value="" />
                <asp:HiddenField ID="HiddenTipoBusqueda" runat="server" Value="" />
                <asp:HiddenField ID="HiddenBusquedaHabilitada" runat="server" Value="false" />
                <asp:HiddenField ID="HiddenPaginaActual" runat="server" Value="1" />
                <asp:HiddenField ID="HiddenFilasPorPagina" runat="server" Value="13" />
                <asp:HiddenField ID="HiddenPaginasTotales" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenFilasTotales" runat="server" Value="0" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    

    <div class="panel panel-default primaryTheme" style="margin-top: 30px;">
        <div class="panel-heading">
            <h3 class="panel-title text-center">RESULTADOS
            </h3>
        </div>
        <div class="panel-body">
            <div style="padding:10px; padding-top:10px;padding-bottom:50px;position:relative;">                
                <UserControl:Carousel ID="Carousel" runat="server" LoadCustom="true"  />
            </div>
        </div>
    </div>

    <%-- POPUP Detalle Titulo--%>
    <div id="DialogoDetalleTitulo" class="modal fade" role="dialog">
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
                <div class="modal-body" style="position: relative; padding: 20px; padding-bottom: 8px;">
                    <div class="form-horizontal">
                        <asp:HiddenField ID="HiddenFieldClave" runat="server" Value="" />

                        <ul class="nav nav-tabs">
                            <li class="active"><a data-toggle="tab" href="#home">Descripción del título</a></li>
                        </ul>

                        <div id="tabContentDetalleLoading" class="tab-content" style="padding-top: 20px; text-align: center; vertical-align: central;">
                            <img src="../../images/gifAjax.gif" style="width: 120px; height: 120px;" />
                        </div>

                        <div id="tabContentDetalle" class="tab-content" style="padding-top: 20px; height: auto;">
                            <%--  TAB DETALLE --%>
                            <div id="home" class="tab-pane fade in active" style="display: inline-block; width: 100%;">

                                <div class="carouselContenedorImagen" style="overflow-x: auto;">
                                    <img id="ImagenTitulo" class="carouselImagen" alt="Titulo" />
                                </div>
                                <div class="modal-footer" style="margin: 0px; margin-top: 5px; padding: 0px; height: 2px;"></div>
                                <div class="col-md-12 form-group" style="margin: 0px;">
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
                                    <div class="panel-group" id="accordion" style="max-height: 320px; overflow: auto;">
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
                                                    <div class="col-md-12">
                                                        <br />
                                                    </div>
                                                    <div id="bContenido" class="col-md-12">
                                                        <b>Contenido:</b>
                                                        <div class="text-justify" style="max-height: 200px; overflow: auto;">
                                                            <span id="SpanContenido" class="control-label"></span>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <br />
                                                    </div>
                                                    <div id="bColofon" class="col-md-12">
                                                        <b>Colofón:</b>
                                                        <div class="text-justify" style="max-height: 200px; overflow: auto;">
                                                            <span id="SpanColofon" class="control-label"></span>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <br />
                                                    </div>
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

                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapse5">Recursos</a>
                                                </h4>
                                            </div>
                                            <div id="collapse5" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                    <div class="col-md-12 text-justify" style="max-height: 100px; overflow: auto;">                                                        
                                                        <div style="text-align: center; display: inline-block;">
                                                            <img id="ImagenPdf" alt="Pdf" title="Versión PDF" width="30" src="../../images/Icon-document-pdf.png" class="image-src-libro" />
                                                            <img id="ImageVirtual" alt="Virtual" title="Versión Virtual" width="30" src="../../images/Icon-document-virtual.png" class="image-src-libro"/>
                                                            <img id="ImageOnline" alt="Online" title="Versión Online" width="30" src="../../images/Icon-document-web.png" class="image-src-libro"/>
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
                </div>
                <!-- footer -->
                <div class="modal-footer" style="margin-top: 0px;">
                    <input type="button" value="Cerrar" class="btn btn-default" data-dismiss="modal" />
                </div>
            </div>

        </div>
    </div>
</asp:Content>