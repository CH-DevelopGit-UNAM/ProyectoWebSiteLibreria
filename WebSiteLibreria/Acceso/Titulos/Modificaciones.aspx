<%@ Page Title="Administrar Titulos" Language="C#" MasterPageFile="~/ResponsiveSite.master" AutoEventWireup="true" CodeFile="Modificaciones.aspx.cs" Inherits="Acceso_Titulos_Modificaciones" Theme="SkinBase" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" Runat="Server">
    <style type="text/css">
        .campo-Numero-Pagina {
            display:inline-block;
            padding:2px;
            text-align:center;
        }
        /* Responsables */
        table[id*='GridResponsables'].gridView-default{
            font-size:0.9em;
            text-align:left;            
        }

        table[id*='GridResponsables'] tr.gridHeader-default {
            display:none;
        }

        table[id*='GridResponsables'] table.gridHeader-default {           
            font-size:0.95em;            
        }

        table[id*='GridResponsables'] table.gridAlternatingRow-default {            
            font-size:0.95em;
        }

        .modalDialog-Detalle {
            width: 60%;
            max-width: 1100px;
        }
        .campoSmall{
            display:inline-block;
        }

        .form-horizontal .control-label .text-left , .text-left{
            text-align:left !important;
        }

        .campoISBN {
            display: inline-block;            
            text-align:center;            
        }


        .table-isbn {
            width:100%; max-width:800px; margin:auto;
        }

        .table-isbn > tbody > tr > td{
            padding-top:5px;padding-bottom:5px;
        }
        /*.table-isbn > tbody tr:last-child > td{
            padding-top:0px;padding-bottom:0px;            
        }*/

        .table-isbn > tbody > tr > td:nth-of-type(1){
            width:100px;
        }

                
        .table-isbn tr:nth-of-type(3) > td:last-child {
            display:inline-flex;
        }

        .edicionISBN {
            display: inline-block;
            padding:3px;
            text-align:center;            
        }

        .table-isbn tr:nth-of-type(3) > td:last-child > div span{
            padding-left:5px;
        }

        @media (max-width: 1812px) {

        }

       
        @media (max-width: 1199px) {
            .modalDialog-Detalle {
                width: 75%; 
                margin:auto;
            }
        }
         /*boostrap -> (min-width:1200px)*/ 
        @media screen and (max-width: 952px) {
            #tableEdicionReimpresion.col-sm-5 {
                width:50%;
                margin:auto;
                text-align:center;
                 display: inline-table !important;
            }
            #tableAnioPaginas.col-sm-5{
                width:50%;
                margin:auto;
                text-align:center;
                display: inline-table !important;
            }

        }

        @media screen and (max-width: 819px) {
            #tableEdicionReimpresion.col-sm-5 {
                width:100%;                
            }
            #tableAnioPaginas.col-sm-5{
                width:100%;
            }

            #tableAnioPaginas table.img-responsive{
                width:auto;
                max-width: none !important;
                display: inline-table !important;               
            }

            #tableEdicionReimpresion table.img-responsive{
                width:auto;
                max-width: none !important;
                display: inline-table !important;
            }
        }
        /*boostrap -> (min-width:768px)*/ 
        @media screen and (max-width: 767px) {
            .modalDialog-Detalle {
                width: 85%;
                margin:auto;
            }

            .table-isbn tr:nth-of-type(3) > td:last-child {
                display: inline-block;
                width:100%;
            }

                .table-isbn tr:nth-of-type(3) > td:last-child > div:nth-of-type(1){
                    padding-left:41px;
                }
            .table-isbn tr:nth-of-type(3) > td:last-child > div:nth-of-type(1),
            .table-isbn tr:nth-of-type(3) > td:last-child > div:nth-of-type(2) {
                width:50%;
                float:left;
            }

            .table-isbn tr:nth-of-type(3) > td:last-child > div:nth-of-type(3) {
                width:100%;
                clear:both;
                padding-top:10px;
            }

            .table-isbn tr:nth-of-type(3) > td:last-child > div:nth-of-type(3) > span {
                padding-right: 2px;
            }

            input.edicionISBN {
                width:70px !important;
            }
        }


         @media screen and (max-width: 584px) {
            .table-isbn tr:nth-of-type(3) > td:last-child > div:nth-of-type(1),
            .table-isbn tr:nth-of-type(3) > td:last-child > div:nth-of-type(2) {
                width:100%;
                padding-bottom:10px;
            }
            
            .table-isbn tr:nth-of-type(3) > td:last-child > div:nth-of-type(3) {
                width:100%;
                clear:both;
                padding-top:0px;
            }

            .table-isbn tr:nth-of-type(3) > td:last-child > div:nth-of-type(2) > span {
                padding-right: 23px;
            }

            input.edicionISBN {
                width:80px !important;
            }
        }

        @media screen and (max-width: 526px) {
            .modalDialog-Detalle {
                width: 95%;      
                margin:auto;          
            }

        }

        .ui-autocomplete { z-index:3500; }
    </style>

    <%: Scripts.Render("~/bundles/fileInput") %>   
    <%: Scripts.Render("~/bundles/fileInputLanguage") %>       
    <script type="text/javascript">
        var opAgregar =<%=(int)Unam.CoHu.Libreria.Controller.Enums.TipoOperacion.Agregar%>;
        var opEditar =<%=(int)Unam.CoHu.Libreria.Controller.Enums.TipoOperacion.Editar%>;
        var opEliminar =<%=(int)Unam.CoHu.Libreria.Controller.Enums.TipoOperacion.Eliminar%>;
        var opInvalido =<%=(int)Unam.CoHu.Libreria.Controller.Enums.TipoOperacion.Invalido%>;
        var opSoloLectura =<%=(int)Unam.CoHu.Libreria.Controller.Enums.TipoOperacion.SoloLectura%>;

        var idGridView = '#<%=GridViewResultado.ClientID%>';
        var idButtonAceptar = '#<%=ButtonAceptar.ClientID%>';
        var idHiddenOperacion = '#<%=HiddenTipoOperacion.ClientID%>';
        var idHiddenClave = '#<%=HiddenFieldClave.ClientID%>';
        var idHiddenOperacion = '#<%=HiddenTipoOperacion.ClientID%>';

        var idLabelIdtitulo = '#<%=LabelIdtitulo.ClientID%>';
        var idHiddenIsbn = '#<%=HiddenIdIsbn.ClientID%>';
        var idTextIsbn = '#<%=TextBoxIsbn.ClientID%>';
        var idHiddenCiudad = '#<%=HiddenIdCiudad.ClientID%>';
        var idTextCiudad = '#<%=TextBoxCiudad.ClientID%>';
        var idHiddenEditor = '#<%=HiddenIdEditor.ClientID%>';
        var idTextEditor = '#<%=TextBoxEditor.ClientID%>';        
        // ISBN
        var idIsbn_1 = "#<%=TextBoxISBN_1.ClientID%>";
        var idIsbn_2 = "#<%=TextBoxISBN_2.ClientID%>";
        var idIsbn_3 = "#<%=TextBoxISBN_3.ClientID%>";
        var idIsbn_4 = "#<%=TextBoxISBN_4.ClientID%>";
        var idIsbn_5 = "#<%=TextBoxISBN_5.ClientID%>";
        var idDescripcionIsbn = "#DropDownDescripcionIsbn";
        var idReimpresion = "#<%=TextBoxReimpresion.ClientID%>";
        var idReedicion = "#<%=TextBoxReedicion.ClientID%>";
        var idEdicionISBN = "#<%=TextBoxEdicionIsbn.ClientID%>";
        // Responsable
        var idHiddenAutor = '#<%=HiddenIdAutor.ClientID%>';
        var idTextAutor= '#<%=TextBoxAutor.ClientID%>';
        var idTextTituloOriginal = '#<%=TextBoxTituloOriginal.ClientID%>';
        var idTextTitulo = '#<%=TextBoxTitulo.ClientID%>';
        <%--var idTextEdicion = '#<%=TextBoxEdicion.ClientID%>';--%>
        var idTextAnio = '#<%=TextBoxAnio.ClientID%>';
        var idTextPaginas = '#<%=TextBoxPaginas.ClientID%>';
        var idTextMedidas = '#<%=TextBoxMedidas.ClientID%>';
        var idTextSerie500 = '#<%=TextBoxSerie500.ClientID%>';
        var idTextContenido = '#<%=TextBoxContenido.ClientID%>';
        var idTextCualidades = '#<%=TextBoxCualidades.ClientID%>';
        var idTextColofon = '#<%=TextBoxColofon.ClientID%>';
        var idTextTema = '#<%=TextBoxTema.ClientID%>';
        var idTextSecundarias = '#<%=TextBoxSecundarias.ClientID%>';
        var idTextU_FFYL = '#<%=TextBoxU_FFYL.ClientID%>';
        var idTextU_IIFL = '#<%=TextBoxU_IIFL.ClientID%>';
        var idTextObservaciones = '#<%=TextBoxObservaciones.ClientID%>';
        var idHiddenSerie = '#<%=HiddenIdSerie.ClientID%>';
        var idTextSerie = '#<%=TextBoxSerie.ClientID%>';        
        var idTextResponsable ='#<%=TextBoxResponsable.ClientID%>';
        var idTextEdicionNumero = '#<%=TextBoxEdicionNumero.ClientID%>'
        var idTextReimpresion = '#<%=TextBoxReimpresionNumero.ClientID%>'
        var idTextPdf = '#<%=TextBoxUrlPdf.ClientID%>'
        var idTextVirtual = '#<%=TextBoxUrlVirtual.ClientID%>'
        var idTextOnline = '#<%=TextBoxUrlOnline.ClientID%>'
        var idRadioIsLatin = '#<%=RadioIsLatin.ClientID%>'
        var idRadioIsGriego = '#<%=RadioIsGriego.ClientID%>'

        var idHiddenResponsables = '#<%=HiddenListaResponsable.ClientID%>';
        var idHiddenDetalleIsbn = '#<%=HiddenListaIsbn.ClientID%>';
        var idHiddenIdResp = '#<%=HiddenIdResp.ClientID%>';

        var idHiddenRutaArchivo = '#<%=HiddenRutaArchivo.ClientID%>';
        var idCheckIsNovedad = '#<%=CheckIsNovedad.ClientID%>';
        
        var idInputFile = '#<%=InputFile.ClientID%>';

        //Definir Explicitamente la ruta de la página para los PageMethods, para todos los navegadores
        PageMethods.set_path("Modificaciones.aspx");

        function pageLoad() {
            // Solo los elementos en un UpdatePanel
            $(document).ready(function () {
                configurarGridView(idGridView, function (items, element) {    
                    var idTitulo = tryParseInteger($(items[1]).text(),0);    
                    // JACJ-Habilitar
                    //setDataModalDetalle(idTitulo, "", "", "", "", "","","","","","","","","","","","","","","","", "", "",false,"","","","","",false,false);
                    setDataModalDetalle(idTitulo, "", "", "", "", "","","","","","","","","","","","","","","","", "", "",false,"","","","","", null, null);
                });

            });
        }

        var HeartBeatTimer;
 
        function StartHeartBeat()
        {
            // pulse every 10 seconds
            if (HeartBeatTimer == null)
                HeartBeatTimer = setInterval("HeartBeat()", 10000);
        }
 
        function HeartBeat()
        {            
            PageMethods.PokePage();
        }

        //StartHeartBeat();

        $(document).ready(function () {
            setListenerValidation();
            setCustomListenerValidation("#PanelIsbnEditor");

            $(idTextAutor).autocomplete({
                source: function (request, response) {
                    var lista = $("body").data("AUTOR-" + request.term);
                    if (lista != undefined && lista != null) {
                        response($.map(lista, function (concepto) {  return {  
                            label: ((concepto.NombreLatin.length > 1 ) ? concepto.NombreEspanol +"("+ concepto.NombreLatin +")" :  concepto.NombreEspanol +"("+ concepto.NombreGriego+")"), 
                            value: ((concepto.NombreLatin.length > 1 ) ? concepto.NombreEspanol +"("+ concepto.NombreLatin +")" :  concepto.NombreEspanol +"("+ concepto.NombreGriego+")"), ID: concepto.IdAutor } }));
                    }
                    else {
                        PageMethods.BuscarAutores(request.term,
                                                    function (lista) {
                                                        $("body").data("AUTOR-" + request.term, lista);
                                                        response($.map(lista, function (concepto) { return { 
                                                            label: ((concepto.NombreLatin.length > 1 ) ? concepto.NombreEspanol +"("+ concepto.NombreLatin +")" :  concepto.NombreEspanol +"("+ concepto.NombreGriego+")"),  
                                                            value: ((concepto.NombreLatin.length > 1 ) ? concepto.NombreEspanol +"("+ concepto.NombreLatin +")" :  concepto.NombreEspanol +"("+ concepto.NombreGriego+")"), ID: concepto.IdAutor} }));
                                                    },
                                                    function () {
                                                        alert("Ocurrió un error al procesar la petición. Por favor inténtelo nuevamente");
                                                    });
                    }
                },
                minLength: 1,
                select: function (event, concepto) {                    
                    if (concepto.item.ID != "" ) {                          
                        $(idHiddenAutor).val(concepto.item.ID);
                        // Disparar el validador
                        $("#spanAutor").text(concepto.item.value).change();
                        // La versión 1.11.4 del autocomplete asigna automáticamente el valor de "item.value" al control asociado
                        //$(idTextAutor).val(concepto.item.value);
                        // Para evitar que se automáticamente el valor de "item.value", retornar false
                        return false;
                    } else {                        
                        return false;
                    }                    
                }
            });

            $(idTextCiudad).autocomplete({
                source: function (request, response) {
                    var lista = $("body").data("CIUDAD-" + request.term);
                    if (lista != undefined && lista != null) {
                        response($.map(lista, function (concepto) {  return {  label: concepto.Descripcion , value:   concepto.Descripcion , ID: concepto.IdCiudad } }));
                    }
                    else {
                        PageMethods.BuscarCiudades(request.term,
                                                    function (lista) {
                                                        $("body").data("CIUDAD-" + request.term, lista);
                                                        response($.map(lista, function (concepto) { return { label: concepto.Descripcion ,  value: concepto.Descripcion , ID: concepto.IdCiudad} }));
                                                    },
                                                    function () {
                                                        alert("Ocurrió un error al procesar la petición. Por favor inténtelo nuevamente");
                                                    });
                    }
                },
                minLength: 1,
                select: function (event, concepto) {
                    if (concepto.item.ID != "" ) {                        
                        $(idHiddenCiudad).val(concepto.item.ID);
                        $("#spanCiudad").text(concepto.item.value);
                        //$(idTextCiudad).val(concepto.item.label);
                        return false;
                    } else {
                        return false;
                    }                    
                }
            });

            $(idTextEditor).autocomplete({
                source: function (request, response) {
                    var lista = $("body").data("EDITOR-" +request.term);
                    if (lista != undefined && lista != null) {
                        response($.map(lista, function (concepto) {  return {  label: concepto.Nombre , value: concepto.Nombre , ID: concepto.IdEditor} }));
                    }
                    else {
                        PageMethods.BuscarEditores(request.term,
                                                    function (lista) {
                                                        $("body").data("EDITOR-" + request.term, lista);
                                                        response($.map(lista, function (concepto) { return { label: concepto.Nombre ,  value: concepto.Nombre, ID: concepto.IdEditor} }));
                                                    },
                                                    function () {
                                                        alert("Ocurrió un error al procesar la petición. Por favor inténtelo nuevamente");
                                                    });
                    }
                },
                minLength: 1,
                select: function (event, concepto) {
                    if (concepto.item.ID != "" ) {                        
                        $(idHiddenEditor).val(concepto.item.ID);
                        // Disparar el validador
                        $("#spanEditor").text(concepto.item.value).change();
                        //$(idTextEditor).val(concepto.item.label);
                        return false;
                    } else {
                        return false;
                    }                    
                }
            });

            //$(idTextIsbn).autocomplete({
            //    source: function (request, response) {
            //        var lista = $("body").data("ISBN-"+request.term);
            //        if (lista != undefined && lista != null) {
            //            response($.map(lista, function (concepto) {  return {  label: concepto.ClaveIsbn , value: concepto.ClaveIsbn, ID: concepto.IdIsbn , Objeto: concepto } }));
            //        }
            //        else {
            //            PageMethods.BuscarIsbn(request.term,
            //                                        function (lista) {
            //                                            $("body").data("ISBN-" + request.term, lista);
            //                                            response($.map(lista, function (concepto) { return { label: concepto.ClaveIsbn ,  value: concepto.ClaveIsbn, ID: concepto.IdIsbn , Objeto: concepto } }));
            //                                        },
            //                                        function () {
            //                                            alert("Ocurrió un error al procesar la petición. Por favor inténtelo nuevamente");
            //                                        });
            //        }
            //    },
            //    minLength: 1,
            //    select: function (event, concepto) {
            //        if (concepto.item.ID != "" ) {                        
            //            //$(idHiddenIsbn).val(concepto.item.ID);
            //            $("#spanIsbn").text(concepto.item.value);
            //            //$(idTextIsbn).val(concepto.item.label);
            //            isbnSelected = concepto.item.Objeto;
            //            return false;
            //        } else {
            //            return false;
            //        }                    
            //    }
            //});

            $(idTextSerie).autocomplete({
                source: function (request, response) {
                    var lista = $("body").data("SERIE-"+request.term);
                    if (lista != undefined && lista != null) {
                        response($.map(lista, function (concepto) {  return {  
                            label:  ((concepto.NombreLatin.length > 1 ) ? concepto.NombreLatin :  concepto.NombreGriego ), 
                            value: ((concepto.NombreLatin.length > 1 ) ? concepto.NombreLatin :  concepto.NombreGriego ), ID: concepto.IdSerie} }));
                    }
                    else {
                        PageMethods.BuscarSerie(request.term,
                                                    function (lista) {
                                                        $("body").data("SERIE-" + request.term, lista);
                                                        response($.map(lista, function (concepto) { return { 
                                                            label:  ((concepto.NombreLatin.length > 1 ) ? concepto.NombreLatin :  concepto.NombreGriego ) , 
                                                            value: ((concepto.NombreLatin.length > 1 ) ? concepto.NombreLatin :  concepto.NombreGriego ), ID: concepto.IdSerie} }));
                                                    },
                                                    function () {
                                                        alert("Ocurrió un error al procesar la petición. Por favor inténtelo nuevamente");
                                                    });
                    }
                },
                minLength: 1,
                select: function (event, concepto) {
                    if (concepto.item.ID != "" ) {                        
                        $(idHiddenSerie).val(concepto.item.ID);
                        // Disparar el validador
                        $("#spanSerie").text(concepto.item.value).change();
                        //$(idTextSerie).val(concepto.item.label);
                        return false;
                    } else {
                        return false;
                    }                    
                }
            });

            $(idTextResponsable).autocomplete({
                source: function (request, response) {
                    var lista = $("body").data("RESP-" +request.term);
                    if (lista != undefined && lista != null) {
                        response($.map(lista, function (concepto) {  return {  label: concepto.NombreCompleto + "("+ concepto.Rfc + ")" , value: concepto.NombreCompleto + "("+ concepto.Rfc + ")" , ID: concepto.IdResponsable, Objeto: concepto } }));
                    }
                    else {
                        //BuscarResponsables(request.term, function (lista) {
                        //    $("body").data("RESP-" + request.term, lista);
                        //        response($.map(lista, function (concepto) { return { label: concepto.Rfc + " / " +concepto.NombreCompleto,  value: concepto.Rfc + " / " +concepto.NombreCompleto, ID: concepto.IdResponsable , Objeto: concepto } }));
                        //    },function (error) {
                        //        alert("Ocurrió un error al procesar la petición. Por favor inténtelo nuevamente");
                        //    } 
                        //);
                        PageMethods.BuscarResponsables(request.term,
                                                    function (lista) {
                                                        $("body").data("RESP-" + request.term, lista);
                                                        response($.map(lista, function (concepto) { return { label: concepto.NombreCompleto + "("+ concepto.Rfc + ")",  value: concepto.NombreCompleto + "("+ concepto.Rfc + ")", ID: concepto.IdResponsable , Objeto: concepto } }));
                                                    },
                                                    function (args) {
                                                        alert("Ocurrió un error al procesar la petición. Por favor inténtelo nuevamente");
                                                    });
                    }
                },
                minLength: 1,
                select: function (event, concepto) {
                    if (concepto.item.ID != "" ) {                        
                        $("#spanResponsableSelected").text(concepto.item.value);
                        responsableSelected = concepto.item.Objeto;
                        return false;
                    } else {
                        return false;
                    }                    
                }
            });                        

            //// Modal-Until-Show...
            $("#DialogoDetalleTitulo").on('shown.bs.modal', function (event) {
                var id = $(idHiddenClave).val();
                var tipoOperacion = tryParseInteger($(idHiddenOperacion).val(), opInvalido);
                // Aquí no se selecciona el perimer panel, ya que los popup no se mostrarán correctamente
                if(tipoOperacion== opAgregar){
                    PageMethods.CargarFunciones(onGetFuncionesSuccess, onError);  
                    // JACJ-Habilitar
                    //setDataModalDetalle("-- NUEVO --", "", "", "", "", "","","","","","","","","","","","","","","","", "", "",false,"","","","","",false,false);
                    setDataModalDetalle("--NUEVO--", "", "", "", "", "","","","","","","","","","","","","","","","", "", "",false,"","","","","", null, null);
                    $("#tabContentDetalle").show();                    
                    renderTableResponsables($("#DivResponsablesEdit"),[]);                    
                    $("#tabContentDetalleLoading").hide();                    
                    $(idButtonAceptar).show();                    
                    validarPanel("#DialogoDetalleTitulo");
                }else if(tipoOperacion == opEditar){
                    PageMethods.CargarFunciones(onGetFuncionesSuccess, onError);
                    PageMethods.GetTitulo(id,  onGetTituloSuccess , onError);
                }                
            });

            // Modal-Show
            $("#DialogoDetalleTitulo").on('show.bs.modal', function (event) {
                $("#tabContentDetalle").hide();
                $("#tabContentDetalleLoading").show();
                // Seleccionar el primer panel para que los popup se muestren en su posicion correcta
                $("a[href='#home']").click();
                $(idButtonAceptar).hide();

                $('#DialogoDetalleTitulo').on('hide.bs.modal.prevent', function (event) {
                    event.preventDefault();
                    event.stopImmediatePropagation();
                    return false;
                });

                $(this).find("input[type='button']").each(function (index, value) {
                    var dataDismiss = $(value).attr('data-dismiss');
                    if (dataDismiss != null && dataDismiss != undefined) {
                        $(value).click(function (event) {
                            $(idHiddenOperacion).val(-1);
                            cerrarPanel("#DialogoDetalleTitulo");
                            $("#DialogoDetalleTitulo").off('hide.bs.modal.prevent');
                            $("#DialogoDetalleTitulo").modal('hide');
                        });
                    }
                });

                $(this).find("button").each(function (index, value) {
                    var dataDismiss = $(value).attr('data-dismiss');
                    if (dataDismiss != null && dataDismiss != undefined) {
                        $(value).click(function (event) {
                            $(idHiddenOperacion).val(-1);
                            cerrarPanel("#DialogoDetalleTitulo");
                            $("#DialogoDetalleTitulo").off('hide.bs.modal.prevent');
                            $("#DialogoDetalleTitulo").modal('hide');
                        });
                    }
                });
            });

            //// Modal-Until-Show...
            $("#DialogoImagen").on('shown.bs.modal', function (event) {
                var id = $(idHiddenClave).val();                
                
            });

            // Modal-Show
            $("#DialogoImagen").on('show.bs.modal', function (event) {
                $("#DeleteImage").hide();

                //$(idInputFile).fileinput('clear');
                //$(idInputFile).fileinput('refresh', {initialPreview: [
                //        "<img src='/images/avatar.png' style='width:150px; height:150px;' alt='Desert' title='Desert'>"   
                //] });
                $(idInputFile).fileinput('destroy');
                configurarInputFile($(idHiddenRutaArchivo).val());
                
                $('#DialogoImagen').on('hide.bs.modal.prevent', function (event) {
                    event.preventDefault();
                    event.stopImmediatePropagation();
                    return false;
                });
                
                $(this).find("input[type='button']").each(function (index, value) {
                    var dataDismiss = $(value).attr('data-dismiss');
                    if (dataDismiss != null && dataDismiss != undefined) {
                        $(value).click(function (event) {                            
                            cerrarPanel("#DialogoImagen");
                            $("#DialogoImagen").off('hide.bs.modal.prevent');
                            $("#DialogoImagen").modal('hide');
                        });
                    }
                });

                $(this).find("button").each(function (index, value) {
                    var dataDismiss = $(value).attr('data-dismiss');
                    if (dataDismiss != null && dataDismiss != undefined) {
                        $(value).click(function (event) {
                            $(idHiddenOperacion).val(-1);
                            cerrarPanel("#DialogoImagen");
                            $("#DialogoImagen").off('hide.bs.modal.prevent');
                            $("#DialogoImagen").modal('hide');
                        });
                    }
                });
            });
            
            configurarInputFile(urlSite+"images/avatar.png");
        });
        
        function configurarInputFile(urlFile){
            var id = $(idHiddenClave).val();            
            // with plugin options
            var inicializacion =  {
                initialPreview: [
                        "<img src='"+urlFile+"' style='width:150px; height:150px;' alt='Desert' title='Desert'>"   
                ],
                uploadAsync: true,   
                //showUpload:true, 
                previewFileType:'any',
                language:'es',
                required: true,    
                msgPlaceholder: 'Seleccione un archivo...',
                msgFilerequired : 'Debe seleccionar un archivo',
                autoReplace : true,
                validateInitialCount : true,
                fileActionSettings : {showUpload:false,showDownload:false , showRemove:false, showDrag: false},
                //removeIcon : '' ,
                maxFileCount : 1,
                minFileCount : 1,
                allowedFileExtensions : ['jpg', 'jpeg', 'png'],
                //uploadUrl :'Modificaciones.aspx/LoadFile',
                uploadUrl :'UploadFile.ashx',
                maxFileSize : 1500,
                initialPreviewShowDelete :false,
                //deleteExtraData : {}
                uploadExtraData :  { IdTitulo:  id }, 
                ajaxSettings : {
                    type: "POST",
                    processData :false,
                    contentType:  false, //"application/json; charset=utf-8", //"multipart/form-data",
                    dataType: "json",                             
                    data:  new FormData().append("file",$(this)),
                    //data: '{ busqueda: "'+parametro+'" }',
                }
                //allowedFileTypes : ['image'],                    
            };
            
            if(urlFile.match(/.*(\/Images\/ImagenNoDisponible\.png)/)){
                inicializacion.msgSelected = inicializacion.msgPlaceholder;
            } else {                   
                if(urlFile.length>0){
                    $("#DeleteImage").show();
                }else{
                    $("#DeleteImage").hide();
                }
            }

            $(idInputFile).fileinput(inicializacion);
            
            $(idInputFile).off('filepreajax');
            $(idInputFile).off('filepreupload');
            $(idInputFile).off('fileuploaderror');
            $(idInputFile).off('fileuploaded');
            
            $(idInputFile).on('filepreajax',function(event, previewId, index) {                
                //console.log('File pre ajax triggered');
            });

            $(idInputFile).on('filepreupload',function(event, data, previewId, index) {
                var form = data.form, files = data.files, extra = data.extra,
                    response = data.response, reader = data.reader;
                var jqXHR = data.jqXHR;                       
                //console.log('File pre upload triggered');                
            });

            $(idInputFile).on('fileuploaderror', function(event, data, msg) {
                var form = data.form, files = data.files, extra = data.extra,
                    response = data.response, reader = data.reader;
                    //console.log('File upload error');
            });

            $(idInputFile).on('fileerror', function(event, data, msg) {
                //console.log('fileerror');                
            });

            $(idInputFile).on('fileuploaded', function(event, data, previewId, index) {
                var form = data.form, files = data.files, extra = data.extra,
                    response = data.response, reader = data.reader;                
                //console.log('File uploaded triggered');
                //console.log(response.Resultado);
                $(idGridView).find("tr.gridRow-selected:eq(0)").find("td:eq(7)").text(response.Resultado);
                alert("La imagen se ha cargado");
                $("#DeleteImage").show();
            });

            $("#DeleteImage").off('click');
            $("#DeleteImage").on('click',function(){
                return deleteImage();
            });
        }
        function deleteImage(){
            var id = $(idHiddenClave).val();
            id = tryParseInteger(id,0);
            if( id > 0 ){
                if(confirm("Se quitará la imagen asociada al titulo seleccionado. ¿Desea continuar?")){
                    PageMethods.DeleteFile(id,
                                    function (webResult) {
                                        if(webResult != null & webResult != undefined){
                                            if(webResult.IsProcesado){
                                                $(idGridView).find("tr.gridRow-selected:eq(0)").find("td:eq(7)").text("/Images/ImagenNoDisponible.png");
                                                $(idHiddenRutaArchivo).val("/Images/ImagenNoDisponible.png");
                                                $(idInputFile).fileinput('destroy');
                                                configurarInputFile("/Images/ImagenNoDisponible.png");
                                                $("#DeleteImage").hide();
                                                alert("Se ha eliminado la imagen asociada.");
                                            } else {
                                                alert("No se pudo eliminar la imagen.");
                                            }
                                        } else {
                                            alert("Ocurrió un error al procesar la respuesta. Por favor inténtelo nuevamente");
                                        }
                                    },
                                    function (args) {
                                        alert("Ocurrió un error al procesar la petición. Por favor inténtelo nuevamente");
                                    });
                }                
            } else {            
                alert("Debe seleccionar un título válido");
            }
            return false;
        }

        function showDialogImagen(element){
            var id = $(element).parent().parent().find('td:eq(1)').text();
            var file = $(element).parent().parent().find('td:eq(7)').text();
            var idTitulo = tryParseInteger(id, 0);
            $(idHiddenClave).val(idTitulo);            
            //file.match(/.*(\/Images\/ImagenNoDisponible\.png)/)
            $(idHiddenRutaArchivo).val(file);
            $("#DialogoImagen").modal('show');
            return false;
        }

        function showDialogAdd(element) {
            $(idHiddenOperacion).val(opAgregar);            
            $("#DialogoDetalleTitulo").modal('show');
            return false;
        }

        function showDialogEdit(element) {            
            $(idHiddenOperacion).val(opEditar);
            $("#DialogoDetalleTitulo").modal('show');
            $("#DivFuncionesEdit").html('<img src="../../images/gifAjax.gif" style="width:37px; height:37px;" />');
            return false;
        }


        function showDialogResponsables(element) {
            $(idHiddenOperacion).val(opEditar);
            $("#DialogoResponsables").modal('show');
            return false;
        }

        function showDialogDelete(element) {
            $(idHiddenOperacion).val(opEliminar);            
            if (confirm("¿Desea borrar el título y los responsables asociados?")) {
                return true;
            }
            return false;
        }

        function aceptarButton() {                        
            var result = validarPanel("#DialogoDetalleTitulo");
            var noExistsGrid = ( typeof($gridDetalleResponsables) == 'undefined' || $gridDetalleResponsables == null);
            var hasContentGrid = false;
            var finalString = "";
            var stringIsbn = "";
            var count = 0;

            if(noExistsGrid==false){                                                
                $gridDetalleResponsables.find("tr").each(function(index, item){
                    var idDetalle =  $(idHiddenIdResp).val();
                    var idResponsable =   $(item).find("td:eq(1)").text();
                    var idFuncion =  $(item).find("td:eq(3)").text();
                    var orden =  index;     
                    finalString += idDetalle+"#"+idResponsable+ "#"+idFuncion+"#" + orden + "|";                    
                    count ++;
                });
            }
            $(idHiddenResponsables).val(finalString);  

            if( typeof($gridDetalleIsbn) != 'undefined' & $gridDetalleIsbn != null){
                $gridDetalleIsbn.find("tr").each(function(index, item){
                    
                    if(index>0){ // por la cabecera
                        var idTitulo =  $(item).find("td:eq(1)").text();
                        idTitulo = tryParseInteger(idTitulo, 0).toString();
                        var claveIsbn = $(item).find("td:eq(2)").text(); 
                        var idDescripcion =  $(item).find("td:eq(3)").text();
                        idDescripcion = tryParseInteger(idDescripcion, 0).toString();
                        
                        var edicion =   $(item).find("td:eq(5)").text();
                        edicion = tryParseInteger(edicion, 0).toString();
                        var reedicion =   $(item).find("td:eq(6)").text();
                        reedicion = tryParseInteger(reedicion, 0).toString();
                        var reimpresion =   $(item).find("td:eq(7)").text();
                        reimpresion = tryParseInteger(reimpresion, 0).toString();

                        var isbnLocal = idTitulo + "#" + claveIsbn + "#" + idDescripcion + "#" + edicion + "#" + reedicion + "#" + reimpresion + "|";                        
                        stringIsbn += isbnLocal;
                    }                    
                });
                $(idHiddenDetalleIsbn).val(stringIsbn);  
            }else{
                $(idHiddenDetalleIsbn).val("");
            }

            hasContentGrid = ( count > 0);            
            if (result.IsValid & hasContentGrid & (finalString.length > 10) ) {
                return confirm("¿Desea guardar los cambios?");
            }else{
                if(hasContentGrid==false){
                    alert("El titulo debe tener asignado al menos un responsable");
                }
            }
            return false;
        }

        function BuscarResponsables(parametro, successFunction, errorFunction){
            $.ajax({
                type: "POST",
                url: "Modificaciones.aspx/BuscarResponsables",
                contentType: "application/json; charset=utf-8",
                dataType: "json",    
                data: '{ busqueda: "'+parametro+'" }',
                cache: false,
                async: true,
                beforeSend: function (jqXHR, settings) { },
                success: successFunction ,
                error: errorFunction
            });
            return false;        
        }

        function setDataModalDetalle(idTitulo, detalleIsbn, ciudad, editor, autor, titOriginal, titulo, edicion, anioPub, paginas, 
                                     medidas, serie500, contenido, cualidades, colofon, tema, secundarias, uffyl, uiifl, 
                                     observaciones, serie, rutaArchivo, nombreArchivo, isNovedad, edicionNumero, reimpresion, urlPdf, urlVirtual, urlOnline, 
                                     isLatin, isGriego) {
            $(idHiddenClave).val(idTitulo);
            // Disparar el validador
            $(idLabelIdtitulo).text(idTitulo).change();            

            var stringIsbn = "";
            
            setDataIsbn("",0,"","", "");

            if (typeof (detalleIsbn) != 'undefined' && detalleIsbn != null) {
                if (detalleIsbn.length > 0) {
                    renderTableIsbn($("#DivIsbnEdit"), detalleIsbn);

                    for (var i = 0; i < detalleIsbn.length; i++) {                        
                        var idTituloIsbn =  detalleIsbn[i].IdTitulo;
                        var claveIsbn =  detalleIsbn[i].ClaveIsbn;
                        var idDescripcion =  detalleIsbn[i].IdDescripcion;
                        var reimpresion =  detalleIsbn[i].Reimpresion;
                        var reedicion =  detalleIsbn[i].Reedicion;
                        var edicion = detalleIsbn[i].Edicion;                        
                        
                        stringIsbn += idTituloIsbn.toString() + "#" + claveIsbn + "#" + idDescripcion + "#" + edicion + "#" + reedicion + "#" + reimpresion + "|";
                    }
                } else {
                    renderTableIsbn($("#DivIsbnEdit"), []);
                }       
            } else {
                renderTableIsbn($("#DivIsbnEdit"), []);
            }    

            $(idHiddenDetalleIsbn).val(stringIsbn);


            if(typeof(ciudad) != 'undefined' & ciudad != null){
                if(typeof(serie) === 'string'){
                    $(idHiddenCiudad).val(ciudad);
                    $("#spanCiudad").text(ciudad);
                    //$(idTextCiudad).val(ciudad);
                } else{
                    $(idHiddenCiudad).val(ciudad.IdCiudad); 
                    $("#spanCiudad").text(ciudad.Descripcion);
                    //$(idTextCiudad).val(ciudad.Descripcion);
                }                
            }else{
                $(idHiddenCiudad).val("");
                $("#spanCiudad").text("");
            }

            if(typeof(editor) != 'undefined' & editor != null){
                if(typeof(serie) === 'string'){
                    $(idHiddenEditor).val(editor);
                    $("#spanEditor").text(editor);
                    //$(idTextEditor).val(editor);
                } else{
                    $(idHiddenEditor).val(editor.IdEditor);
                    // Disparar el validador
                    $("#spanEditor").text(editor.Nombre).change();
                    //$(idTextEditor).val(editor.Nombre);
                }               
            }else{
                $(idHiddenEditor).val("");
                $("#spanEditor").text("");
            }

            if(typeof(autor) != 'undefined' & autor != null){
                if(typeof(autor) === 'string'){
                    $(idHiddenAutor).val(autor);
                    $("#spanAutor").text(autor);
                    //$(idTextAutor).val(autor);
                } else{
                    $(idHiddenAutor).val(autor.IdAutor);
                    var nombreAutor = autor.NombreEspanol;
                    if (autor.NombreLatin.length > 1) {
                        nombreAutor = nombreAutor + '(' + autor.NombreLatin + ')';
                    } else if (autor.NombreGriego.length > 1) {
                        nombreAutor = nombreAutor + '(' + autor.NombreGriego + ')';
                    }                    
                    // Disparar el validador
                    $("#spanAutor").text(nombreAutor).change();
                    //$(idTextAutor).val(autor.NombreEspanol);
                }               
            }else{
                $(idHiddenAutor).val("");
                $("#spanAutor").text("");
            }

            if(typeof(serie) != 'undefined' & serie!= null){
                if(typeof(serie) === 'string'){
                    $(idHiddenSerie).val(serie);
                    $("#spanSerie").text(serie);
                    //$(idTextSerie ).val(serie);
                } else{
                    $(idHiddenSerie).val(serie.IdSerie);
                    var nombreSerie = "";
                    if (serie.NombreLatin.length > 1) {
                        nombreSerie = serie.NombreLatin;
                    } else if (serie.NombreGriego.length > 1) {
                        nombreSerie = serie.NombreGriego;
                    }
                    // Disparar el validador
                    $("#spanSerie").text(nombreSerie).change();;
                    //$(idTextSerie ).val(serie.NombreLatin);
                }                
            }else{
                $(idHiddenSerie).val("");
                $("#spanSerie").text("");
            }

            if(typeof(rutaArchivo) != 'undefined' & serie!= null){
                $(idHiddenRutaArchivo).val(rutaArchivo);              
            }else{
                $(idHiddenRutaArchivo).val("");                
            }

            if(isNovedad === true ){
                $(idCheckIsNovedad).prop("checked", true);
            }else{
                $(idCheckIsNovedad).prop("checked", false);                
            }

            $(idTextTituloOriginal).val(titOriginal);
            $(idTextTitulo).val(titulo);
            //$(idTextEdicion).val(edicion);
            $(idTextAnio).val(anioPub);
            $(idTextPaginas).val(paginas);
            $(idTextMedidas).val(medidas);
            $(idTextSerie500).val(serie500);
            $(idTextContenido).val(contenido);
            $(idTextCualidades).val(cualidades);
            $(idTextColofon).val(colofon);
            $(idTextTema).val(tema);
            $(idTextSecundarias).val(secundarias);
            $(idTextU_FFYL).val(uffyl);
            $(idTextU_IIFL).val(uiifl);
            $(idTextObservaciones).val(observaciones);
            $(idTextEdicionNumero).val((edicionNumero == 0 ? "":edicionNumero));
            $(idTextReimpresion).val((reimpresion == 0 ? "":reimpresion));
            var dummyValue;

            if (typeof (isLatin) == 'boolean' & typeof (isGriego) == 'boolean') {
                // JACJ-Habilitar
                //dummyValue = $("#bFlagLatin").show().height();
                if (isLatin == false & isGriego == false ) {                    
                    $(idRadioIsLatin).prop('checked', false);
                    $(idRadioIsGriego).prop('checked', false);

                } else {
                    $(idRadioIsLatin).prop('checked', isLatin);
                    $(idRadioIsGriego).prop('checked', isGriego);
                }
            } else {
                dummyValue = $("#bFlagLatin").hide().height();
                $(idRadioIsLatin).prop('checked', false);
                $(idRadioIsGriego).prop('checked', false);
            }

            try {
                $(idTextPdf).val((urlPdf!= undefined ? (urlPdf.length>0? decodeURIComponent(urlPdf): ""):"" ));
                //var url_1 = (urlPdf!= undefined ? (urlPdf.length>0? decodeURIComponent(urlPdf): ""):"" );               
                //if(url_1.length>1){
                //    url_1 = url_1.split("/");
                //    url_1 = url_1[url_1.length-1];
                //}
                //$(idTextPdf).val(url_1);
            } catch (e) {
                $(idTextPdf).val("parseError");
            }
            try {
                $(idTextVirtual).val((urlVirtual!= undefined ? (urlVirtual.length>0? decodeURIComponent(urlVirtual): ""):"" ));
            } catch (e) {
                $(idTextVirtual).val("parseError");
            }
            try {
                $(idTextOnline).val((urlOnline!= undefined ? (urlOnline.length>0? decodeURIComponent(urlOnline): ""):"" ));
            } catch (e) {
                $(idTextOnline).val("parseError");
            }                        
            
            $(idTextAutor).val("");
            $(idTextEditor).val("");
            $(idTextIsbn).val("");
            $(idTextSerie).val("");
            $(idTextCiudad).val("");
            $(idTextResponsable).val("");
            $("#spanResponsableSelected").text("");
            responsableSelected = null;
            funcionSelected = null;
            isbnSelected = null;
            $(idHiddenResponsables).val("");
            //$(idHiddenDetalleIsbn).val("");
            $(idHiddenIdResp).val("");
        }

        function onGetTituloSuccess(titulo) {   
            // JACJ-Habilitar
            setDataModalDetalle(titulo.IdTitulo, titulo.DetalleIsbn, titulo.Ciudad, titulo.Editor, titulo.Autor,
                                titulo.TituloOriginal,titulo.Titulo, titulo.Edicion, titulo.AnioPublicacion, titulo.Paginas, titulo.Medidas, titulo.Serie500,
                                titulo.Contenido, titulo.Cualidades,titulo.Colofon, titulo.Tema, titulo.Secundarias, titulo.UffYL, titulo.UiiFL, titulo.Observaciones, 
                                titulo.Serie, titulo.RutaArchivo, titulo.NombreArchivo, titulo.IsNovedad, titulo.NumeroEdicion, titulo.NumeroReimpresion, titulo.UrlPdf, titulo.UrlVirtual,
                                titulo.UrlOnline, null,null); //, titulo.IsLatin, titulo.IsGriego);
            $("#tabContentDetalle").show();
            $("#tabContentDetalleLoading").hide();
            $(idButtonAceptar).show();
            validarPanel("#DialogoDetalleTitulo");
            
            var finalString = "";

            if(typeof(titulo.DetalleResponsables) != 'undefined' && titulo.DetalleResponsables != null){
                if( titulo.DetalleResponsables.length>0){
                    var listaResponsables = titulo.DetalleResponsables[0].Responsables;
                    $(idHiddenIdResp).val(titulo.DetalleResponsables[0].IdResponsableDetalle);
                    for (var i = 0; i < listaResponsables.length; i++) {
                        var idDetalle =  listaResponsables[i].IdResponsableDetalle;
                        var idResponsable =  listaResponsables[i].IdResponsable;
                        var idFuncion =  listaResponsables[i].IdFuncion;
                        var orden =  listaResponsables[i].OrdenFuncion;
                        finalString += idDetalle+"#"+idResponsable+ "#"+idFuncion+"#" + orden + "|";
                    }
                    renderTableResponsables($("#DivResponsablesEdit"), listaResponsables );
                }                
            }else{
                renderTableResponsables($("#DivResponsablesEdit"),[]);
            }
            $(idHiddenResponsables).val(finalString);            
        }

        var _ListaFunciones;
        var funcionSelected;
        var $dropFunciones;

        function onGetFuncionesSuccess(funciones){
            _ListaFunciones = funciones;
            if(typeof(funciones) != 'undefined' && funciones.length > 0){
                $("#DivFuncionesEdit").html('');                
                if(typeof($dropFunciones) != 'undefined')
                    $dropFunciones.remove();  
                //renderDropFunciones($("#DivFuncionesEdit"), funciones);                
                $dropFunciones = renderDropFuncionIn($("#DivFuncionesEdit"), _ListaFunciones, false, "", function(element){
                    funcionSelected =  $(element).val() == "" ? null : { IdFuncion :$(element).val() , TipoFuncion : $(element).find("option:selected").text() };
                });
            }
        }

        function onError(args) {         
            //console.log(args);
            $("#tabContentDetalle").show();
            $("#tabContentDetalleLoading").hide();
            $(idButtonAceptar).hide();
            // JACJ-Habilitar
            //setDataModalDetalle("--ERROR--", "", "", "", "", "","","","","","","","","","","","","","","","", "", "",false,"","","","","", false, false);
            setDataModalDetalle("--ERROR--", "", "", "", "", "","","","","","","","","","","","","","","","", "", "",false,"","","","","", null, null);
            alert("Ocurrio un error desconocido al cargar el dato.");
        }

        function autoPostbackPaginador(sender, args) {            
            if (args.keyCode === 13) {
                __doPostBack(sender.id, sender.value);
                return true;
            }
            event.preventDefault();
            event.stopPropagation();
            return false;
        }


        // Cuando se realiza una validación por medio de una funcion
        function validarRfc(valueExtra, text, resultValidation) {           
            // valueExtra           = valor extra que recibe la función (si la hay)
            // text                 = texto a validar
            // resultValidation     = objeto de la validacion : {IsValid, ResultValidation, HelpText, TitleValidation, HasErrorPopup, Rules, TitleValidation, Value }
            // Debe retonar un boolean
            if (text.length > 0) {                
                // Si se escribió algo, entonces debe validarse el valor como un RFC válido
                // Se usa la funcion de validacion.js
                var retorno = validaRFCsinHomoClave(text);
                if (!retorno) {
                    resultValidation.ResultValidation = "Escriba un RFC válido";
                } else {
                    resultValidation.ResultValidation = "";
                }
                return retorno;
            } else {
                // Si no se escribió el rfc, no se valida y se da por bueno
                resultValidation.ResultValidation = "";
                return true;
            }            
        }

        var responsableSelected;
        var isbnSelected;
        var $gridDetalleResponsables; 
        var $gridDetalleIsbn;

        function renderTableResponsables($divToAppend, listaResponsables){
            if(typeof($gridDetalleResponsables) != 'undefined')
                $gridDetalleResponsables.remove();
            
            if(typeof(listaResponsables) != 'undefined'){                
                $gridDetalleResponsables =$('<table class="table table-bordered gridView-default gridView-NoResponsive"></table>');
                var classAlternating = 'class="gridRow-default"';
                for (var i = 0; i < listaResponsables.length; i++) {
                    if( i % 2 != 0){
                        classAlternating = 'class="gridAlternatingRow-default"';    
                    }else{
                        classAlternating = 'class="gridRow-default"';
                    }
                    var $tr = $('<tr '+classAlternating+' ></tr>');
                    var $tdAction = $("<td style='width:50px;'></td>");
                    $tdAction.append('<a class="btn btn-warning center text-center" onclick="javascript: return deleteResponsable(this);">Eliminar</a>')

                    var $td0 = $("<td class='gridViewColumnHidden'>"+listaResponsables[i].IdResponsable+"</td>");
                    var $td1 = $("<td></td>");
                    $td1.append(listaResponsables[i].NombreCompletoResponsable)
                    var $td2_0 = $("<td class='gridViewColumnHidden'>" +  listaResponsables[i].IdFuncion + "</td>");
                    var $td2 = $("<td></td>");
                    renderDropFuncionIn($td2, _ListaFunciones, true , listaResponsables[i].IdFuncion, function(element){
                        onChangeTipoFuncion(element);
                    })
                    

                    var $td3 = $("<td></td>");
                    $td3.append(listaResponsables[i].OrdenFuncion)

                    $tr.append($tdAction);
                    $tr.append($td0);
                    $tr.append($td1);
                    $tr.append($td2_0);
                    $tr.append($td2);                    
                    $tr.append($td3);
                    $gridDetalleResponsables.append($tr);
                }
                if(typeof($divToAppend) != 'undefined'){
                    $divToAppend.append($gridDetalleResponsables)
                }
            }
            return listaResponsables;
        }
        
        function renderDropFunciones($divToAppend, listFunciones){
            if(typeof($dropFunciones) != 'undefined')
                $dropFunciones.remove();
            
            if(typeof(listFunciones) != 'undefined'){
                $dropFunciones =$('<select class="form-control"></select>');

                for (var i = 0; i < listFunciones.length; i++) {   
                    var $option;
                    if( i == 0 ){
                        $option = $('<option selected="selected" value="'+ listFunciones[i].IdFuncion+  '">' + listFunciones[i].TipoFuncion + '</option>');
                    }else{
                        $option = $('<option  value="' + listFunciones[i].IdFuncion+  '">'+ listFunciones[i].TipoFuncion + '</option>');
                    }                    
                    $dropFunciones.append($option);
                }
                if(typeof($divToAppend) != 'undefined'){
                    $divToAppend.append($dropFunciones)
                    $dropFunciones.change(function(){
                        funcionSelected =  $(this).val() == "" ? null : { IdFuncion :$(this).val() , TipoFuncion : $(this).find("option:selected").text() };
                    });
                }
            }
            return listFunciones;
        }

        function renderDropFuncionIn($elementToAppend, listFunciones, quitInitialOption, idFuncionSelected , onChange){
            var $drop;
            if(typeof(listFunciones) != 'undefined'){
                var funcionesLocal = listFunciones;
                if(quitInitialOption === true){
                    funcionesLocal = $.grep(listFunciones, function(item, index) { return ( item.IdFuncion != "" & item.IdFuncion != "-1"); });
                }

                $drop = $('<select class="form-control"></select>');

                for (var i = 0; i < funcionesLocal.length; i++) {   
                    var $option;
                    if( idFuncionSelected == funcionesLocal[i].IdFuncion ){
                        $option = $('<option selected="selected" value="'+ funcionesLocal[i].IdFuncion + '" >' + funcionesLocal[i].TipoFuncion + '</option>');
                    }else{
                        $option = $('<option  value="' + funcionesLocal[i].IdFuncion+  '" >'+ funcionesLocal[i].TipoFuncion + '</option>');
                    }                    
                    $drop.append($option);
                }

                if(typeof($elementToAppend) != 'undefined'){
                    $elementToAppend.append($drop)
                    $drop.change(function(){                        
                        if(typeof(onChange) === 'function'){
                            onChange(this);
                        }
                    });
                }
            }
            return $drop;
        }

        function updateListaResponsables(event, sender){
            if(typeof($gridDetalleResponsables) != 'undefined' & $gridDetalleResponsables != null){
                if(typeof(responsableSelected) != 'undefined' & responsableSelected != null){
                    if(typeof(funcionSelected) != 'undefined' & funcionSelected != null ){
                        appendResponsable($gridDetalleResponsables, responsableSelected, funcionSelected )
                    }else{
                        alert("Seleccione una funcion a asignar");
                    }
                }else{
                    alert("Seleccione un responsable");
                }
            }else{
                alert("NO existen los elementos a cargar");
            }
        }

        function appendResponsable($gridResponsables, responsable, tipoFuncion ){            
            var items = $gridResponsables.find("tr");
            var countItems = (typeof(items)!= 'undefined') ? items.length : 0 ;
            var classAlternating;
            if( countItems % 2 != 0){
                classAlternating = 'class="gridAlternatingRow-default"';    
            }else{
                classAlternating = 'class="gridRow-default"';
            }
            var $tdAction = $("<td style='width:50px;'></td>");
            $tdAction.append('<a class="btn btn-warning center text-center" onclick="javascript: return deleteResponsable(this);">Eliminar</a>')

            var $tr = $('<tr '+classAlternating+' ></tr>');
            var $td0 = $("<td class='gridViewColumnHidden'>"+responsable.IdResponsable+"</td>");
            var $td1 = $("<td></td>");
            $td1.append(responsable.NombreCompleto)
            var $td2_0 = $("<td class='gridViewColumnHidden'>" +  tipoFuncion.IdFuncion + "</td>");
            var $td2 = $("<td></td>");
            //$td2.append(funcion.TipoFuncion)            
            renderDropFuncionIn($td2, _ListaFunciones, true , tipoFuncion.IdFuncion, function (element) {onChangeTipoFuncion(element);})

            var $td3 = $("<td></td>");
            $td3.append("");
            $tr.append($tdAction);
            $tr.append($td0);
            $tr.append($td1);
            $tr.append($td2_0);
            $tr.append($td2);                    
            $tr.append($td3);
            $gridResponsables.append($tr);
        }

        function deleteResponsable(trElement){            
            if(typeof($gridDetalleResponsables) != 'undefined' & $gridDetalleResponsables != null){
                var $trCurrent = $(trElement).parent().parent();
                $trCurrent.remove()                
            }
        }

        function onChangeTipoFuncion(element){
            if(typeof($gridDetalleResponsables) != 'undefined' & $gridDetalleResponsables != null){
                var $tr = $(element).parent().parent();
                $tr.find("td").eq(3).text($(element).val());
            }            
        }

        function setDataIsbn(claveIsbn, idDescripcion, reimpresion, reedicion, edicion) {
            if(typeof(claveIsbn) != 'undefined' & claveIsbn != null){
                if(typeof(claveIsbn) === 'string'){
                    var isbns = splitIsbn(claveIsbn);
                    $(idIsbn_1).val(isbns[0]).change();
                    $(idIsbn_2).val(isbns[1]).change();
                    $(idIsbn_3).val(isbns[2]).change();
                    $(idIsbn_4).val(isbns[3]).change();
                    $(idIsbn_5).val(isbns[4]).change();
                } else{
                    $(idIsbn_1).val("").change();
                    $(idIsbn_2).val("").change();
                    $(idIsbn_3).val("").change();
                    $(idIsbn_4).val("").change();
                    $(idIsbn_5).val("").change();
                }                
            }else{
                $(idIsbn_1).val("").change();
                $(idIsbn_2).val("").change();
                $(idIsbn_3).val("").change();
                $(idIsbn_4).val("").change();
                $(idIsbn_5).val("").change();
            }

            if(typeof(idDescripcion) != 'undefined' & idDescripcion != null){
                if(idDescripcion>0){
                    $(idDescripcionIsbn).val(idDescripcion).change();
                } else{
                    $(idDescripcionIsbn).val("0").change();
                }               
            }else{
                $(idDescripcionIsbn).val("0").change();
            }

            if(typeof(reimpresion) != 'undefined' & reimpresion != null){
                if(reimpresion>0){
                    $(idReimpresion ).val(reimpresion).change();
                } else{
                    $(idReimpresion ).val("").change();
                }           
            }else{
                $(idReimpresion ).val("").change();
            }

            if(typeof(reedicion) != 'undefined' & reedicion != null){
                if(reedicion>0){
                    $(idReedicion).val(reedicion).change();
                } else{
                    $(idReedicion).val("").change();
                }           
            }else{
                $(idReedicion).val("").change();
            }

            if(typeof(edicion) != 'undefined' & edicion != null){
                if(edicion>0){
                    $(idEdicionISBN).val(edicion).change();
                } else{
                    $(idEdicionISBN).val("").change();
                }           
            }else{
                $(idEdicionISBN).val("").change();
            }
        }

        function splitIsbn(isbn) {
            var arrayIsbn = ["","","","",""]
            if(typeof(isbn) == 'string' ){
                var arreglo = isbn.split("-");
                if(arreglo.length == 5)
                    arrayIsbn = arreglo;
            }
            return arrayIsbn;
        }

        function renderTableIsbn($divToAppend, listaIsbn) {
            if (typeof ($gridDetalleIsbn) != 'undefined')
                $gridDetalleIsbn.remove();

            if (typeof (listaIsbn) != 'undefined') {
                $gridDetalleIsbn = $('<table class="table table-bordered gridView-default gridView-NoResponsive"></table>');
                var classAlternating = 'class="gridRow-default"';
                var $trHead = $('<tr ' + classAlternating + ' ></tr>');
                $trHead.append('<td>Acción</td><td>ISBN</td><td>Descripción</td><td>ed.</td><td>reed.</td><td>reimpr.</td>');
                $gridDetalleIsbn.append($trHead);
                for (var i = 0; i < listaIsbn.length; i++) {
                    if (i % 2 != 0) {
                        classAlternating = 'class="gridAlternatingRow-default"';
                    } else {
                        classAlternating = 'class="gridRow-default"';
                    }
                    

                    var $tr = $('<tr ' + classAlternating + ' ></tr>');
                    var $tdAction = $("<td style='width:50px;'></td>");
                    $tdAction.append('<a class="btn btn-warning center text-center" onclick="javascript: return deleteIsbn(this);">Eliminar</a>')
                    var idTitulo = $(idHiddenClave).val();
                    idTitulo = tryParseInteger(idTitulo, 0);
                    //var $td0 = $("<td class='gridViewColumnHidden'>" + listaIsbn[i].IdIsbn + "</td>");
                    //var $td1 = $("<td></td>");
                    //$td1.append(listaIsbn[i].ClaveIsbn)            
                    //$tr.append($tdAction);
                    //$tr.append($td0);
                    //$tr.append($td1);            

                    var $td0 = $("<td class='gridViewColumnHidden'>" + idTitulo + "</td>");
                    var $td1 = $("<td></td>");
                    $td1.append(listaIsbn[i].ClaveIsbn)
                    var $td1_1= $("<td class='gridViewColumnHidden'>" + listaIsbn[i].IdDescripcion + "</td>");
                    var $td1_2= $("<td>" + listaIsbn[i].DescripcionVersion + "</td>");
                    var $td1_3= $("<td>" + (listaIsbn[i].Edicion > 0 ? listaIsbn[i].Edicion  : "" )+ "</td>");
                    var $td1_4= $("<td>" + (listaIsbn[i].Reedicion >0 ? listaIsbn[i].Reedicion  : "" )+ "</td>");
                    var $td1_5= $("<td>" + (listaIsbn[i].Reimpresion>0 ? listaIsbn[i].Reimpresion : "" ) + "</td>");                                        

                    $tr.append($tdAction);
                    $tr.append($td0);
                    $tr.append($td1);
                    $tr.append($td1_1);
                    $tr.append($td1_2);
                    $tr.append($td1_3);
                    $tr.append($td1_4);
                    $tr.append($td1_5);                    
                    $gridDetalleIsbn.append($tr);
                }
                if (typeof ($divToAppend) != 'undefined') {
                    $divToAppend.append($gridDetalleIsbn)
                }
            }
            return listaIsbn;
        }

        function updateListaIsbn(event, sender){
            //aceptarButton();
            var result = validarCustomPanel("#PanelIsbnEditor");
            if(result != undefined && result != null){
                console.log(result);
                if (result.IsValid) {
                    var claveIsbn = $(idIsbn_1).val() + "-" + $(idIsbn_2).val() + "-" + $(idIsbn_3).val() + "-" + $(idIsbn_4).val() + "-" + $(idIsbn_5).val();
                    var idDescripcion = $(idDescripcionIsbn).val();
                    var descripcion = $(idDescripcionIsbn +" option:selected").text();
                    var reimpresion = $(idReimpresion).val();
                    var reedicion = $(idReedicion).val();
                    var edicion = $(idEdicionISBN).val();
                    var idTitulo = $(idHiddenClave).val();
                    isbnSelected = { IdTitulo: tryParseInteger(idTitulo,0), ClaveIsbn : claveIsbn , IdDescripcion : tryParseInteger(idDescripcion,0), DescripcionIsbn : descripcion, Reimpresion : tryParseInteger(reimpresion,0), 
                                     Reedicion : tryParseInteger(reedicion,0), Edicion : tryParseInteger(edicion,0)} ;

                    if(typeof($gridDetalleIsbn) != 'undefined' & $gridDetalleIsbn != null){
                        if(typeof(isbnSelected) != 'undefined' & isbnSelected != null){
                            var isbnCount = 0 ;
                            $gridDetalleIsbn.find("tr").each(function(index,item){
                                var isbn = item.childNodes[2].innerHTML;
                                var idDescripcion = tryParseInteger(item.childNodes[3].innerHTML,0);                        
                                if(isbn ==  isbnSelected.ClaveIsbn & idDescripcion == isbnSelected.IdDescripcion){
                                    isbnCount ++;
                                }                        
                            });

                            if(isbnCount>0){
                                alert("Ya existe un mismo Isbn con la misma descripción en la lista; asigne una clave o descripción diferente.");
                            }else{                                
                                appendIsbn($gridDetalleIsbn, isbnSelected)
                            }                    
                        }else{
                            alert("Seleccione un ISBN");
                        }
                    }else{
                        alert("NO existen los elementos a cargar");
                    }
                }
            }            
        }

        function appendIsbn($gridIsbn, isbn ){            
            var items = $gridIsbn.find("tr");
            var countItems = (typeof(items)!= 'undefined') ? items.length : 0 ;
            var classAlternating;
            if( countItems % 2 != 0){
                classAlternating = 'class="gridAlternatingRow-default"';    
            }else{
                classAlternating = 'class="gridRow-default"';
            }
            var $tr = $('<tr ' + classAlternating + ' ></tr>');
            var $tdAction = $("<td style='width:50px;'></td>");
            $tdAction.append('<a class="btn btn-warning center text-center" onclick="javascript: return deleteIsbn(this);">Eliminar</a>')

            var $td0 = $("<td class='gridViewColumnHidden'>" + isbn.IdTitulo + "</td>");
            var $td1 = $("<td></td>");
            $td1.append(isbn.ClaveIsbn)
            var $td1_1= $("<td class='gridViewColumnHidden'>" + isbn.IdDescripcion + "</td>");
            var $td1_2= $("<td>" + isbn.DescripcionIsbn + "</td>");            
            var $td1_3= $("<td>" + (isbn.Edicion >0 ? isbn.Edicion  : "" )+ "</td>");
            var $td1_4= $("<td>" + (isbn.Reedicion >0 ? isbn.Reedicion  : "" )+ "</td>");
            var $td1_5= $("<td>" + (isbn.Reimpresion>0 ? isbn.Reimpresion : "" ) + "</td>");

            $tr.append($tdAction);
            $tr.append($td0);
            $tr.append($td1);
            $tr.append($td1_1);
            $tr.append($td1_2);
            $tr.append($td1_3);
            $tr.append($td1_4);
            $tr.append($td1_5);
            $gridIsbn.append($tr);

        }

        function deleteIsbn(trElement){            
            if(typeof($gridDetalleIsbn) != 'undefined' & $gridDetalleIsbn != null){
                var $trCurrent = $(trElement).parent().parent();
                $trCurrent.remove()                
            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2 class="title-page">Administrar Titulos</h2>
    <div class="jumbotron customJumbotron" style="font-size: small; padding: 30px; padding-top: 10px; padding-bottom: 10px; background-color: transparent; border-color: lightgray; border-style: none; ">
            <div class="input-group">                
                <asp:TextBox ID="TextBoxBusqueda" runat="server" CssClass="form-control img-responsive" placeholder="Búsqueda"> </asp:TextBox>                
                <br />
                <div class="input-group-btn">                                      
                    <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                        <span class="caret"></span>
                    </button> 
                    <ul class="dropdown-menu pull-right primary-dropdown">
                       <li><asp:LinkButton id="LinkBuscarNombre" runat="server" Text="Nombre" CssClass="dropdown-item" OnClick="LinkBuscarNombre_Click"/></li>
                        <li><asp:LinkButton id="LinkBuscarTema" runat="server" Text="Tema" CssClass="dropdown-item" OnClick="LinkBuscarTema_Click"/></li>
                        <li><asp:LinkButton id="LinkBuscarAutor" runat="server" Text="Autor" CssClass="dropdown-item" OnClick="LinkBuscarAutor_Click"/></li>
                        <li><asp:LinkButton id="LinkBuscarResponsable" runat="server" Text="Responsable" CssClass="dropdown-item" OnClick="LinkBuscarResponsable_Click"/></li>
                        <li><asp:LinkButton id="LinkBuscarCiudad" runat="server" Text="Ciudad" CssClass="dropdown-item" OnClick="LinkBuscarCiudad_Click"/></li>
                       <li><asp:LinkButton id="LinkTodos" runat="server" Text="Todos" CssClass="dropdown-item" OnClick="LinkTodos_Click"/></li>
                    </ul>
      
                </div>
            </div>
            <div  class="input-group" style="padding:10px;">
                <%--<asp:Button ID="ButtonAdd"  runat="server" CssClass="btn btn-primary" Text="Agregar" OnClientClick="javascript: return showDialogAdd(this);"/>--%>
                 <input type="button" class="btn btn-primary" value="Agregar" onclick="javascript: return showDialogAdd(this);"/>
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
                            <asp:GridView ID="GridViewResultado" runat="server" SkinID="GridViewMediumPrimary" PageSize="10" AllowPaging="true" OnPageIndexChanging="GridViewResultado_PageIndexChanging" OnRowDataBound="GridViewResultado_RowDataBound" AllowCustomPaging="true"
                                ShowHeaderWhenEmpty="false" AllowSorting="true" OnSorting="GridViewResultado_Sorting">
                                <Columns>
                                    <asp:TemplateField HeaderText="Titulo" ItemStyle-CssClass="gridViewColumnHidden column-hidden-responsive-sm gridHeader" HeaderStyle-CssClass="gridViewColumnHidden column-hidden-responsive-sm" >
                                        <ItemTemplate>
                                            <span> TÍTULO </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Id Titulo" DataField="IdTitulo" ItemStyle-CssClass="gridViewColumnHidden" HeaderStyle-CssClass="gridViewColumnHidden" ></asp:BoundField>                                    
                                    <asp:TemplateField HeaderText=" " HeaderStyle-CssClass="gridViewColumnBlock" ItemStyle-CssClass="gridViewColumnBlock" >
                                        <ItemTemplate>
                                            <b class="element-hidden-responsive-md">Versiones:</b>
                                            <div style="width:70px;text-align:center;">
                                                <asp:Image ID="ImagenPdf" runat="server" AlternateText="Pdf" ImageUrl="~/images/Icon-document-pdf.png" Width="25" ToolTip="Versión PDF" CssClass="image-src-libro"/>
                                                <asp:Image ID="ImageVirtual" runat="server" AlternateText="Virtual" ImageUrl="~/images/Icon-document-virtual.png" Width="20" ToolTip="Versión Virtual" CssClass="image-src-libro"/>
                                                <asp:Image ID="ImageOnline" runat="server" AlternateText="Online" ImageUrl="~/images/Icon-document-web.png" Width="25" ToolTip="Versión Online" CssClass="image-src-libro"/>
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
                                    
                                    <asp:TemplateField HeaderText="Traductor" >
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
                                            <b class="element-hidden-responsive-md">Traductor:</b>
                                            <asp:Label ID="LabelTraductor" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Archivo" ItemStyle-CssClass="gridViewColumnHidden" HeaderStyle-CssClass="gridViewColumnHidden">
                                        <ItemTemplate>                                            
                                             <asp:Label ID="LabelRuta" runat="server" Text='<%# string.Format(@"{0}",Eval("RutaArchivo"))%>'></asp:Label>
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

      
                                     <asp:TemplateField HeaderText="¿Novedad?" ItemStyle-CssClass="center text-center column-display-responsive-lg" HeaderStyle-CssClass="column-display-responsive-lg">
                                        <ItemTemplate>
                                             <div class="element-hidden-responsive-md text-left" >
                                                <b>¿Es Novedad?:</b>
                                            </div>
                                             <asp:CheckBox ID="CheckNovedad_" runat="server" Checked='<%#Eval("IsNovedad")%>'  Enabled="false"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="center text-center" >
                                        <ItemTemplate>
                                            <a class="btn btn-default center text-center" onclick="javascript: return showDialogEdit(this);">Editar</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="center text-center" >
                                        <ItemTemplate>
                                            <button type="button" class="btn btn-default" role="button" aria-haspopup="true" aria-expanded="false" title="Cargar Imagen" onclick="javascript: return showDialogImagen(this);">
                                                <span class="glyphicon glyphicon-file" aria-hidden="true"></span>                                                
                                            </button>                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="center text-center" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkDelete" runat="server" CssClass="btn btn-default center text-center" Text="Borrar" OnClick="LinkDelete_Click" OnClientClick="javascript: return showDialogDelete(this);" ></asp:LinkButton>                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
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
                            <asp:AsyncPostBackTrigger ControlID="LinkBuscarNombre" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="LinkBuscarTema" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="LinkBuscarResponsable" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="LinkTodos" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cmdInicio" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cmdAnterior" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cmdSiguiente" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cmdFinal" EventName="Click" />                            
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>


    <%-- POPUP Detalle Titulo--%>
    <div id="DialogoDetalleTitulo" class="modal fade" role="dialog" >
        <div class="modal-dialog primaryTheme modalDialog-Detalle">
            <div class="modal-content form-Validate">
                <!-- Cabecera -->
                <div class="modal-header btn-primary ">
                    <button type="button" class="close" aria-label="Close" data-dismiss="modal"><span aria-hidden="true">&times;</span></button>
                    <!-- Titulo -->
                    <div class="modal-title">
                        <h4>Editar</h4>
                    </div>
                </div>
                <!-- body -->
                <div class="modal-body" style="position:relative; padding: 20px; padding-bottom:8px;">
                    <asp:HiddenField ID="HiddenTipoOperacion" runat="server" Value="-1" />
                    <asp:HiddenField ID="HiddenFieldClave" runat="server" Value="" />
                    <asp:HiddenField ID="HiddenRutaArchivo" runat="server" Value="" />                    
                    <div class="form-horizontal" style="position: relative;">

                        <ul class="nav nav-tabs">
                            <li class="active"><a data-toggle="tab" href="#home">General</a></li>
                            <li><a data-toggle="tab" href="#menu1">Contenido</a></li>
                            <li><a data-toggle="tab" href="#menu2">Descripción</a></li>
                            <li><a data-toggle="tab" href="#menu3">ISBN</a></li>
                            <li><a data-toggle="tab" href="#menu4">Responsables</a></li>
                            <li><a data-toggle="tab" href="#menu5">Otros..</a></li>
                        </ul>

                        <div id="tabContentDetalleLoading" class="tab-content" style="padding-top:20px; text-align:center; vertical-align:central; ">
                            <img src="../../images/gifAjax.gif" style="width:120px; height:120px;" />
                        </div>

                        <div id="tabContentDetalle" class="tab-content" style="padding-top:20px; ">

                            <%--  TAB CAMPOS OBLIGATORIOS --%>

                            <div id="home" class="tab-pane fade in active">
                                <div class="form-group" style="position: relative;">
                                    <asp:Label runat="server" CssClass="col-md-2 control-label">Clave</asp:Label>
                                    <div class="col-md-10">
                                        <asp:Label ID="LabelIdtitulo" runat="server" CssClass="col-md-2 control-label-left control-Validate" Width="200" data-rule-validation="LabelIdtitulo::1::NA:: :: ::Seleccione un titulo"></asp:Label>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-2 control-label">Título Original</asp:Label>
                                        <div class="col-md-10">
                                            <asp:TextBox runat="server" ID="TextBoxTituloOriginal" CssClass="form-control img-responsive control-Validate" data-rule-validation="TextBoxTituloOriginal::1::NA:: :: ::Escriba un titulo" TextMode="MultiLine" Height="30" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-2 control-label">Título:</asp:Label>
                                        <div class="col-md-10">
                                            <asp:TextBox runat="server" ID="TextBoxTitulo" CssClass="form-control img-responsive" TextMode="MultiLine" Height="30" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-2 control-label">Autor:</asp:Label>
                                        <div class="col-md-10">
                                            <asp:HiddenField ID="HiddenIdAutor" runat="server" Value="" />
                                            <span id="spanAutor" style="font-weight:bold;" class="control-Validate" data-rule-validation="spanAutor::1::NA:: :: ::Escriba un Autor"></span>
                                            <asp:TextBox runat="server" ID="TextBoxAutor" CssClass="form-control img-responsive control-validate-autocomplete" />
                                            <div class="warning-block with-No-errors">Escriba una Autor.</div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-2 control-label">Editor:</asp:Label>
                                        <div class="col-md-10">
                                            <asp:HiddenField ID="HiddenIdEditor" runat="server" Value="" />
                                            <span id="spanEditor" style="font-weight:bold;" class="control-Validate" data-rule-validation="spanEditor::1::NA:: :: ::Escriba un Editor" ></span>
                                            <asp:TextBox runat="server" ID="TextBoxEditor" CssClass="form-control img-responsive control-validate-autocomplete" MaxLength="100" />
                                            <div class="warning-block with-No-errors">Escriba una Editor.</div>
                                        </div>
                                    </div>


                                    <div class="form-group text-center">
                                        <div class="col-md-2"></div>
                                        <div id="tableAnioPaginas" class="col-sm-5">
                                            <table  class="img-responsive text-center">
                                                <tr class="text-center" style="width:100%">
                                                    <td class="text-center">
                                                         <asp:Label runat="server" CssClass="control-label">Año:</asp:Label>                                                        
                                                    </td>
                                                    <td style="width:25px;"></td>
                                                    <td>
                                                        
                                                        <asp:Label runat="server" CssClass="control-label">Páginas:</asp:Label>
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="TextBoxAnio" CssClass="form-control campoSmall control-Validate" MaxLength="5" 
                                                            data-rule-validation="TextBoxAnio::1::nocero:: :: ::Escriba un año válido" Width="70" />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredAnio" runat="server" TargetControlID="TextBoxAnio" FilterType="Numbers" />
                                                    </td>
                                                    <td style="width:25px;"></td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="TextBoxPaginas" CssClass="form-control campoSmall img-responsive" TextMode="MultiLine" Height="32" Width="210" MaxLength="190"/>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                                    
                                        <div id="tableEdicionReimpresion"  class="col-sm-5">
                                            <table class="img-responsive text-center">
                                                <tr>
                                                    <td>
                                                         <asp:Label runat="server" CssClass="control-label">Edición:</asp:Label>                                                        
                                                    </td>
                                                    <td style="width:25px;"></td>
                                                    <td>
                                                        
                                                        <asp:Label runat="server" CssClass="control-label">Reimpresión:</asp:Label>
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="TextBoxEdicionNumero" CssClass="form-control campoSmall control-Validate" MaxLength="3" 
                                                            data-rule-validation="TextBoxEdicionNumero::0::{regExp}:: ::^([1-9]|[1-9][0-9]|[1-9][0-9][0-9])*$::Escriba una edición válida" Width="60" />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredEdicion" runat="server" TargetControlID="TextBoxEdicionNumero" FilterType="Numbers" />
                                                    </td>
                                                    <td style="width:25px;"></td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="TextBoxReimpresionNumero" CssClass="form-control campoSmall control-Validate" MaxLength="3" 
                                                            data-rule-validation="TextBoxReimpresionNumero::0::{regExp}:: ::^([1-9]|[1-9][0-9]|[1-9][0-9][0-9])*$::Escriba una reimpresión válida" Width="60" />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredEdicionReimpresion" runat="server" TargetControlID="TextBoxReimpresionNumero" FilterType="Numbers" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                                                    
                                    </div>

                                    

                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-2 control-label">Medidas:</asp:Label>
                                        <div class="col-md-10">
                                            <asp:TextBox runat="server" ID="TextBoxMedidas" CssClass="form-control img-responsive" TextMode="MultiLine" Height="30" MaxLength="100" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-2 control-label">Serie:</asp:Label>
                                        <div class="col-md-10">
                                            <asp:HiddenField ID="HiddenIdSerie" runat="server" Value="" />
                                            <span id="spanSerie" style="font-weight:bold;" class="control-Validate" data-rule-validation="spanSerie::1::NA:: :: ::Escriba una Serie"></span>
                                            <asp:TextBox runat="server" ID="TextBoxSerie" CssClass="form-control img-responsive control-validate-autocomplete"  MaxLength="50" />
                                            <div class="warning-block with-No-errors">Escriba una Serie.</div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" CssClass="col-md-2 control-label">¿Es Novedad?:</asp:Label>
                                        <div class="col-md-10">                                            
                                            <asp:CheckBox runat="server" ID="CheckIsNovedad"/>
                                    </div>
                                </div>

                                </div>
                            </div>

                            <%--  TAB CAMPOS NO OBLIGATORIOS --%>

                            <div id="menu1" class="tab-pane fade">

                                <div class="form-group">
                                    <asp:Label runat="server" CssClass="col-md-2 control-label">tema:</asp:Label>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server" ID="TextBoxTema" CssClass="form-control img-responsive" TextMode="MultiLine" Height="30" />
                                    </div>
                                </div>
                                

                                 <div class="form-group">
                                    <asp:Label runat="server" CssClass="col-md-2 control-label">Contenido:</asp:Label>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server" ID="TextBoxContenido" CssClass="form-control img-responsive" TextMode="MultiLine" Height="100" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label runat="server" CssClass="col-md-2 control-label">Colofón:</asp:Label>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server" ID="TextBoxColofon" CssClass="form-control img-responsive" TextMode="MultiLine" Height="100" />
                                    </div>
                                </div>

                                
                                <div class="form-group">
                                    <asp:Label runat="server" CssClass="col-md-2 control-label">Observaciones:</asp:Label>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server" ID="TextBoxObservaciones" CssClass="form-control img-responsive" TextMode="MultiLine" Height="100" />
                                    </div>
                                </div>

                            </div>

                            <%--  TAB OTROS CAMPOS --%>

                            <div id="menu2" class="tab-pane fade">

                                 <div class="form-group">
                                    <asp:Label runat="server" CssClass="col-md-2 control-label">Cualidades:</asp:Label>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server" ID="TextBoxCualidades" CssClass="form-control img-responsive" TextMode="MultiLine" Height="100" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label runat="server" CssClass="col-md-2 control-label">Secundarias:</asp:Label>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server" ID="TextBoxSecundarias" CssClass="form-control img-responsive" TextMode="MultiLine" Height="100" />
                                    </div>
                                </div>

<%--                                <div class="form-group">
                                    <asp:Label runat="server" CssClass="col-md-2 control-label">Edición:</asp:Label>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server" ID="TextBoxEdicion" CssClass="form-control img-responsive" TextMode="MultiLine" Height="30" />
                                    </div>
                                </div>                                --%>

                                <div class="form-group">
                                    <asp:Label runat="server" CssClass="col-md-2 control-label">Serie 500:</asp:Label>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server" ID="TextBoxSerie500" CssClass="form-control img-responsive" TextMode="MultiLine" Height="30" />
                                    </div>
                                </div>

                                                              
                                <div class="form-group">
                                    <asp:Label runat="server" CssClass="col-md-2 control-label">Ciudad:</asp:Label>
                                    <div class="col-md-10">
                                        <asp:HiddenField ID="HiddenIdCiudad" runat="server" Value="" />
                                        <span id="spanCiudad" style="font-weight:bold;"></span>
                                        <asp:TextBox runat="server" ID="TextBoxCiudad" CssClass="form-control img-responsive" MaxLength="10" />
                                    </div>
                                </div>

                                <div id="bFlagLatin" class="form-group">
                                    <asp:Label runat="server" CssClass="col-md-2 control-label"></asp:Label>
                                    <div class="col-md-10">
                                        <asp:RadioButton ID="RadioIsLatin" runat="server" Text="&nbsp;&nbsp;&nbsp;Latin" GroupName="TipoTexto" />
                                        <b style="padding-left:10px"></b>
                                        <asp:RadioButton ID="RadioIsGriego" runat="server" Text="&nbsp;&nbsp;&nbsp;Griego" GroupName="TipoTexto" />
                                    </div>
                                </div>
                                
                            </div>
                            
                            <%--  TAB ISBN --%>

                            <div id="menu3" class="tab-pane fade">
                                <div id="PanelIsbnEditor" class="form-group">
                                    <div class="form-Validate-manual">
                                        <asp:HiddenField ID="HiddenIdIsbn" runat="server" Value="" />
                                        <asp:HiddenField ID="HiddenListaIsbn" runat="server" Value="" />
                                        <table class="table-isbn">
                                            <tr>
                                                <td>ISBN:</td>
                                                <td>
                                                    <div>
                                                        <asp:TextBox runat="server" ID="TextBoxISBN_1" CssClass="form-control control-Validate-manual campoISBN" data-rule-validation="TextBoxISBN_1::1::{regExp}:: ::^[a-zA-Z0-9]{1,4}$::FORMATO ISBN XXX-XXX-XX-XXXX-X" Width="50" MaxLength="4" />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredISBN_1" runat="server" TargetControlID="TextBoxISBN_1" FilterMode="InvalidChars" InvalidChars="!'$%&/()=?¡¿?*¨´*][{}-_=|°,.:;#" />
                                                        -                                                    
                                                    <asp:TextBox runat="server" ID="TextBoxISBN_2" CssClass="form-control control-Validate-manual campoISBN" data-rule-validation="TextBoxISBN_2::1::{regExp}:: ::^[a-zA-Z0-9]{1,4}$::FORMATO ISBN XXX-XXX-XX-XXXX-X" Width="50" MaxLength="4" />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredISBN_2" runat="server" TargetControlID="TextBoxISBN_2" FilterMode="InvalidChars" InvalidChars="!'$%&/()=?¡¿?*¨´*][{}-_=|°,.:;#" />
                                                        -                                                    
                                                    <asp:TextBox runat="server" ID="TextBoxISBN_3" CssClass="form-control control-Validate-manual campoISBN" data-rule-validation="TextBoxISBN_3::1::{regExp}:: ::^[a-zA-Z0-9]{1,4}$::FORMATO ISBN XXX-XXX-XX-XXXX-X" Width="50" MaxLength="4" />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TextBoxISBN_3" FilterMode="InvalidChars" InvalidChars="!'$%&/()=?¡¿?*¨´*][{}-_=|°,.:;#" />
                                                        -                                        
                                                    <asp:TextBox runat="server" ID="TextBoxISBN_4" CssClass="form-control control-Validate-manual campoISBN" data-rule-validation="TextBoxISBN_4::1::{regExp}:: ::^[a-zA-Z0-9]{1,4}$::FORMATO ISBN XXX-XXX-XX-XXXX-X" Width="50" MaxLength="4" />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TextBoxISBN_4" FilterMode="InvalidChars" InvalidChars="!'$%&/()=?¡¿?*¨´*][{}-_=|°,.:;#" />
                                                        -
                                                    <asp:TextBox runat="server" ID="TextBoxISBN_5" CssClass="form-control control-Validate-manual campoISBN" data-rule-validation="TextBoxISBN_5::1::{regExp}:: ::^[a-zA-Z0-9]{1,4}$::FORMATO ISBN XXX-XXX-XX-XXXX-X" Width="50" MaxLength="4" />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="TextBoxISBN_5" FilterMode="InvalidChars" InvalidChars="!'$%&/()=?¡¿?*¨´*][{}-_=|°,.:;#" />
                                                    </div>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Descripcion:</td>
                                                <td>
                                                    <select id="DropDownDescripcionIsbn" class="form-control control-Validate-manual" data-rule-validation="DropDownDescripcionIsbn::1::nocero:: :: ::Seleccione una descripción">
                                                        <option value="0">Seleccione una opción...</option>
                                                        <option value="1">Pasta Dura</option>
                                                        <option value="2">Pasta Blanda</option>
                                                        <option value="3">Rústica</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>   
                                                <td></td>                                             
                                                <td>                                                    
                                                    <div>
                                                        <span>Edición:</span>
                                                        <asp:TextBox runat="server" ID="TextBoxEdicionIsbn" CssClass="form-control edicionISBN" Width="50" MaxLength="3" />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="TextBoxEdicionIsbn" FilterMode="ValidChars" ValidChars="1234567890" />
                                                    </div>
                                                    <div>
                                                        <span>Reedición:</span>
                                                        <asp:TextBox runat="server" ID="TextBoxReedicion" CssClass="form-control edicionISBN" Width="50" MaxLength="3" />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="TextBoxReedicion" FilterMode="ValidChars" ValidChars="1234567890" />
                                                    </div>
                                                    <div>
                                                       <span>Reimpresión :</span>
                                                        <asp:TextBox runat="server" ID="TextBoxReimpresion" CssClass="form-control edicionISBN" Width="50" MaxLength="3" />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="TextBoxReimpresion" FilterMode="ValidChars" ValidChars="1234567890" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox runat="server" ID="TextBoxIsbn" CssClass="form-control" MaxLength="10" Visible="false" />
                                                    <span id="spanIsbn" style="font-weight: bold;"></span>
                                                    <input type="button" class="btn btn-default" value="Agregar" onclick="javascript: updateListaIsbn(event,this);" />
                                                    <div id="messageValidationIsbnEditor" class="group-validation-manual"></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                    <div id="DivIsbnEdit" style="min-height: 300px; max-height: 350px; overflow: auto;">
                                    </div>
                                </div>
                            </div>

                            <%--  TAB RESPONSABLES--%>

                            <div id="menu4" class="tab-pane fade">
                                
                                <%--  BUSCAR RESPONSABLES --%>
                                <div class="form-group">
                                    <asp:Label runat="server" CssClass="col-md-2 control-label">Responsable:</asp:Label>
                                    <div class="col-md-10">
                                        <asp:HiddenField ID="HiddenIdResp" runat="server" Value="" />
                                        <asp:HiddenField ID="HiddenListaResponsable" runat="server" Value="" />
                                        <asp:TextBox runat="server" ID="TextBoxResponsable" CssClass="form-control img-responsive" />                                                                          
                                        <span id="spanResponsableSelected" style="font-weight:bold;"></span>
                                    </div>
                                </div>

                                <%--  FUNCIONES --%>
                                <div class="form-group">
                                    <span class="col-md-2 control-label">Funcion:</span> 
                                    <div id="DivFuncionesEdit" class="col-md-10"> </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-2"> </div>
                                    <div class="col-md-10"> 
                                        <input type="button" class="btn btn-default" value="Agregar" onclick="javascript: updateListaResponsables(event,this);" />
                                    </div>                                    
                                </div>
                                <div id="DivResponsablesEdit" style="min-height:270px; max-height:330px; overflow:auto;">

                                </div>
                            </div>

                            <div id="menu5" class="tab-pane fade">
                                
                                <div class="form-group">
                                    <asp:Label runat="server" CssClass="col-md-2 control-label">U_FFYL:</asp:Label>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server" ID="TextBoxU_FFYL" CssClass="form-control img-responsive" TextMode="MultiLine" Height="30" MaxLength="100" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label runat="server" CssClass="col-md-2 control-label">U_IIFL:</asp:Label>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server" ID="TextBoxU_IIFL" CssClass="form-control img-responsive" TextMode="MultiLine" Height="30" MaxLength="100" />
                                    </div>
                                </div>
                               
                                <div class="form-group">
                                    <asp:Label runat="server" CssClass="col-md-2 control-label">Versión Pdf:</asp:Label>
                                    <div class="col-md-10">
                                        <div class="input-group">
                                            <span class="input-group-addon" id="basic-addon1" style="background-color: transparent; border: none;">
                                                <asp:Image ID="ImagenPdfEdit" runat="server" AlternateText="Pdf" ImageUrl="~/images/Icon-document-pdf.png" Width="40" ToolTip="Versión PDF" /></span>
                                                <asp:TextBox runat="server" ID="TextBoxUrlPdf" CssClass="form-control img-responsive" TextMode="MultiLine" Height="40" MaxLength="200" ToolTip="Nombre archivo (pdf)" placeholder="<http://url>"/>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label runat="server" CssClass="col-md-2 control-label">Versión Virtual:</asp:Label>
                                    <div class="col-md-10">
                                         <div class="input-group">
                                            <span class="input-group-addon" id="basic-addon2" style="background-color: transparent; border: none;">
                                                <asp:Image ID="ImageVirtualEdit" runat="server" AlternateText="Virtual" ImageUrl="~/images/Icon-document-virtual.png" Width="40" ToolTip="Versión Virtual"/>
                                            </span>
                                            <asp:TextBox runat="server" ID="TextBoxUrlVirtual" CssClass="form-control img-responsive" TextMode="MultiLine" Height="40" MaxLength="200" ToolTip="Escriba la url completa" placeholder="<http://url>"/>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label runat="server" CssClass="col-md-2 control-label">Versión Online:</asp:Label>
                                    <div class="col-md-10">
                                        <div class="input-group">
                                            <span class="input-group-addon" id="basic-addon3" style="background-color: transparent; border: none;">
                                                <asp:Image ID="ImageOnlineEdit" runat="server" AlternateText="Online" ImageUrl="~/images/Icon-document-web.png" Width="40" ToolTip="Versión Online"/>
                                            </span>
                                            <asp:TextBox runat="server" ID="TextBoxUrlOnline" CssClass="form-control img-responsive" TextMode="MultiLine" Height="40" MaxLength="200" ToolTip="Escriba la url completa" placeholder="<http://url>"/>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>


                    </div>
                        <div id="messageValidationEditor" class="group-validation">
                    </div>
                </div>                
                <div class="modal-footer" style="margin-top:0px;">
                    <asp:Button ID="ButtonAceptar" runat="server" Text="Aceptar" CssClass="btn btn-success" OnClick="ButtonAceptar_Click" OnClientClick="javascript: return aceptarButton(this);"/>
                        <input type="button" value="Cerrar" class="btn btn-default" data-dismiss="modal" />                        
                </div>
            </div>
        </div>   
    </div>


    <div id="DialogoImagen" class="modal fade" role="dialog" >
        <div class="modal-dialog primaryTheme modalDialog-Detalle">
            <div class="modal-content">
                <!-- Cabecera -->
                <div class="modal-header btn-primary ">
                    <button type="button" class="close" aria-label="Close" data-dismiss="modal"><span aria-hidden="true">&times;</span></button>
                    <!-- Titulo -->
                    <div class="modal-title">
                        <h4>Imagen</h4>
                    </div>
                </div>
                <!-- body -->
                <div class="modal-body" style="position:relative; padding: 20px; padding-bottom:8px;">                    
                    <input id="IdPathFileSelected" type="hidden" />
                    <div class="form-horizontal" style="position: relative;">
                        <div class="form-group" >                            
                            <div class="col-md-10 center text-center">                                
                                <%--<input id="InputFile" type="file" />
                                --%>
                                <asp:FileUpload ID="InputFile" runat="server"/>
                            </div>
                        </div>                           
                    </div>
                    <div id="messageValidationFile" class="group-validation">
                    </div>
                </div>                
                <div class="modal-footer" style="margin-top:0px;">
                    <input id="DeleteImage" type="button" value="Eliminar Imagen" class="btn btn-warning" />
                    <input type="button" value="Cerrar" class="btn btn-default" data-dismiss="modal" />
                </div>
            </div>
        </div>   
    </div>
</asp:Content>