
//Definir Explicitamente la ruta de la página para los PageMethods, para todos los navegadores
//PageMethods.set_path("Modificaciones.aspx");

function pageLoad() {

    $(document).ready(function () {
        configurarGridView(idGrid, function (items, element) {
            var idTitulo = tryParseInteger($(items[1]).text(), 0);
            setDataModalDetalle(idTitulo, "","", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", urlSite + "Images/avatar.png", "avatar.png", false,"","","","","",false,false);
            $("#DialogoDetalleTitulo").modal('show');
        });
    });
}

//cerrarOverlayAsync();
$(document).ready(function () {
    
    configurarGridView(idGrid, function (items, elements) {
        var idTitulo = tryParseInteger($(items[1]).text(), 0);
        setDataModalDetalle(idTitulo, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", urlSite + "Images/avatar.png", "avatar.png", false, "", "", "", "", "", false, false);
        if (idTitulo > 0) {
            $("#DialogoDetalleTitulo").modal('show');
        } else {
            alert("No hay datos a cargar para el titulo seleccionado");
        }
    });

    // Deshabilitar "autopostback" predeterminado del formulario en controles text
    $("#formPrincipal").on("keypress", function (e) {
        var keycode = (e.keyCode ? e.keyCode : e.which);
        if (keycode == '13') {
            if ($(e.target).prop("type") === "text") {
                e.preventDefault();
                e.stopPropagation();
                return false;
            }
        }
    });

    //// Modal-Until-Show...
    $("#DialogoDetalleTitulo").on('shown.bs.modal', function (event) {
        var id = $(idHiddenClave).val();
        //PageMethods.CargaTitulo(id, onGetTituloSuccess, onError);
        if (id > 0) {
            CargaTitulo(id);
        } else {
            $("#tabContentDetalle").show();
            $("#tabContentDetalleLoading").hide();
            setDataModalDetalle(id, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", urlSite + "Images/avatar.png", "avatar.png", false, "", "", "", "", "", false, false);
            alert("No hay datos a cargar");
        }        
    });

    function CargaTitulo(id) {
        $.ajax({
            url: urlSite + "WebServices/WebSiteServices.asmx/CargaTitulo",
            async: true,
            data: JSON.stringify({ idTitulo: id }),
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: onGetTituloSuccess,
            error: onGetTituloError,
            complete: function (xhr, status) { }
        });
    }

    // Modal-Show
    $("#DialogoDetalleTitulo").on('show.bs.modal', function (event) {
        $("#tabContentDetalle").hide();
        $("#tabContentDetalleLoading").show();
        // Seleccionar el primer panel para que los popup se muestren en su posicion correcta
        $("a[href='#home']").click();

        $('#DialogoDetalleTitulo').on('hide.bs.modal.prevent', function (event) {
            event.preventDefault();
            event.stopImmediatePropagation();
            return false;
        });

        $(this).find("input[type='button']").each(function (index, value) {
            var dataDismiss = $(value).attr('data-dismiss');
            if (dataDismiss != null && dataDismiss != undefined) {
                $(value).click(function (event) {
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
                    cerrarPanel("#DialogoDetalleTitulo");
                    $("#DialogoDetalleTitulo").off('hide.bs.modal.prevent');
                    $("#DialogoDetalleTitulo").modal('hide');
                });
            }
        });
    });
});


function showDialogDetalleResponsable(element) {
    $("#DialogoDetalleResponsable").modal('show');
    return false;
}

//function showDialogDetail(element) {
//    $("#DialogoDetalleTitulo").modal('show');
//    return false;
//}

function validarBusqueda(grupoValidacion) {
    var validationResult = true;
    if (typeof (Page_ClientValidate) == 'function') {
        validationResult = Page_ClientValidate(grupoValidacion);
    }
    if (validationResult === true) {
        return true;
    }
    return false;
}

function autoPostbackPaginador(event) {
    if (event.keyCode === 13) {
        console.log(idTxtPaginador);
        __doPostBack(idTxtPaginador, '0');
        return true;
    }
    event.preventDefault();
    event.stopPropagation();
    return false;
}


function setDataModalDetalle(idTitulo, detalleIsbn, detalleResponsables, ciudad, editor, autor, tituloOriginal, titulo, edicion, anioPub, paginas,
                            medidas, serie500, contenido, cualidades, colofon, tema, secundarias, uffyl, uiifl,
                            observaciones, serie, rutaArchivo, nombrearchivo, isNovedad, edicionNumero, reimpresion, urlPdf, urlVirtual, urlOnline, isLatin, isGriego) {
    $(idHiddenClave).val(idTitulo);
    var dummyValue = null;
    // Al ejecutar el evento, se habilita la programación manual del Collapse
    $('#accordion div.collapse').collapse("hide");
    //$('#accordion div[id*="collapse"]').collapse({ toggle: false });
    //$('#accordion div[id*="collapse"]').collapse("hide");    
    //$('#accordion div[id*="collapse"]').each(function (index, item) {
    //    if (index == 1) {
    //        $(item).collapse("show");
    //    } else {
    //        $(item).collapse("hide");
    //    }        
    //});
    // Se programa manualmente los cierres de los demás paneles al abrirse el actual
    // 'show.bs.collapse'  --> Cuando se dispara el evento collapse (apertura)
    // 'shown.bs.collapse'  --> Cuando se muestra el elemento
    // Se ejecuta la acción en el evento 'show.bs.collapse', para que seá rápido el efecto
    $('#accordion div.collapse').on('show.bs.collapse', function (item) {
        var idSeccion = this.id;
        $('#accordion div[id*="collapse"]').each(function (index, item) {
            if (item.id != idSeccion) {
                $(item).collapse("hide");
            }        
        });
    })
    // titulo.DetalleResponsables
    if (typeof (detalleResponsables) != 'undefined' && detalleResponsables != null) {
        if (typeof (detalleResponsables) === 'string') {
            $("#SpanResponsables").text(detalleResponsables);
        } else {
            if (detalleResponsables.length > 0) {
                var listaResponsables = detalleResponsables[0].Responsables;
                // TO-DO : SpanResponsables
                //renderTableResponsables($("#DivResponsablesEdit"), listaResponsables);
                var resp = "";
                for (var i = 0; i < listaResponsables.length; i++) {
                    resp += listaResponsables[i].NombreCompletoResponsable + "  (" + listaResponsables[i].TipoFuncion + ") " + " <br/>";
                }
                $("#SpanResponsables").html(resp);
            } else {
                $("#SpanResponsables").html("");
            }
        }
    } else {
        //renderTableResponsables($("#DivResponsablesEdit"), []);
        $("#SpanResponsables").html("");
    }

    if (typeof (detalleIsbn) != 'undefined' && detalleIsbn != null) {
        if (detalleIsbn.length > 0) {
            // TO-DO : SpanIsbn
            //renderTableIsbn($("#DivIsbnEdit"), detalleIsbn);
            var isbns = "";
            for (var i = 0; i < detalleIsbn.length; i++) {
                var edicion = detalleIsbn[i].Edicion;
                edicion = (edicion > 0) ? edicion + "<sup><u>a</u></sup>" +" ed." : "";
                var reed = detalleIsbn[i].Reedicion;
                reed = (reed > 0) ? reed + "<sup><u>a</u></sup>" + "  reed." : "";
                var reimpr = detalleIsbn[i].Reimpresion;
                reimpr = (reimpr > 0) ? reimpr + "<sup><u>a</u></sup>" + " reimpr." : "";

                var ediciones = (edicion.length > 0 ? ", " + edicion : "") + (reed.length > 0 ? ", " + reed : "") + (reimpr.length > 0 ? ", " + reimpr : "");

                if (detalleIsbn[i].IdDescripcion > 0) {
                    isbns += detalleIsbn[i].ClaveIsbn + " (" + detalleIsbn[i].DescripcionVersion + ediciones+ ")" + "<br/>";
                }else{
                    isbns += detalleIsbn[i].ClaveIsbn + (ediciones.length>0? "("+ediciones+")": "") + "<br/>";
                }
            }
            $("#SpanIsbn").html(isbns);
        } else {
            //renderTableIsbn($("#DivIsbnEdit"), []);
            $("#SpanIsbn").html("");
        }       
    } else {
        //renderTableIsbn($("#DivIsbnEdit"), []);
        $("#SpanIsbn").text("");
    }    

    if (typeof (ciudad) != 'undefined' & ciudad != null) {
        if (typeof (ciudad) === 'string') {
            $("#SpanCiudad").text(ciudad);
            if (ciudad.length > 0) {
                $("#bCiudad").show();
            } else {
                $("#bCiudad").hide();
            }
            
        } else {
            $("#SpanCiudad").text(ciudad.Descripcion);
            if (ciudad.Descripcion.length > 0) {
                $("#bCiudad").show();
            } else {
                $("#bCiudad").hide();
            }
        }
    } else {
        $("#SpanCiudad").text("");
        $("#bCiudad").hide();
    }

    if (typeof (editor) != 'undefined' & editor != null) {
        if (typeof (editor) === 'string') {
            $("#SpanEditor").text(editor);
            dummyValue = (editor.length > 0 ? $("#bEditor").show().height() : $("#bEditor").hide().height());
        } else {
            $("#SpanEditor").text(editor.Nombre);
            dummyValue = (editor.Nombre.length > 0 ? $("#bEditor").show().height() : $("#bEditor").hide().height());
        }
    } else {
        $("#SpanEditor").text("");
        $("#bEditor").text("");
    }

    if (typeof (autor) != 'undefined' & autor != null) {
        if (typeof (autor) === 'string') {
            $("#SpanAutor").text(autor);
            if (autor.length > 0) {
                $("#bAutor").show();
            } else {
                $("#bAutor").hide();
            }

        } else {
            var nombreAutor = autor.NombreEspanol;
            //if (autor.NombreLatin.length > 1) {
            //    nombreAutor = nombreAutor + '(' + autor.NombreLatin + ')';
            //} else if (autor.NombreGriego.length > 1) {
            //    nombreAutor = nombreAutor + '(' + autor.NombreGriego + ')';
            //}
            $("#SpanAutor").text(nombreAutor);
            if (nombreAutor.length > 0) {
                $("#bAutor").show();
            } else {
                $("#bAutor").hide();
            }
        }
    } else {
        $("#SpanAutor").text("");
        $("#bAutor").hide();
    }

    if (typeof (serie) != 'undefined' & serie != null) {
        if (typeof (serie) === 'string') {
            $("#SpanSerie").text(serie);
            if (serie.length > 0) {
                $("#bSerie").show();
            } else {
                $("#bSerie").hide();
            }
        } else {
            var nombreSerie = "";
            if (serie.NombreLatin.length > 1) {
                nombreSerie = serie.NombreLatin;
            } else if (serie.NombreGriego.length > 1) {
                nombreSerie = serie.NombreGriego;
            }
            $("#SpanSerie").text(nombreSerie);
            if (nombreSerie.length > 0) {
                $("#bSerie").show();
            } else {
                $("#bSerie").hide();
            }
        }
    } else {
        $("#SpanSerie").text("");
        $("#bSerie").hide();
    }    
    $("#SpanTitulo").html(titulo + " (<i>" + tituloOriginal + "</i>) , " + anioPub);
    var edicionStr = obtenerRomanoDeNumero(edicionNumero, "Edición");    
    //$("#SpanEdicion").text(edicionStr);
    $("#SpanEdicion").html(edicionNumero + " <sup><u>a</u></sup>.", "Edición");
    dummyValue = (edicionNumero > 0 ? $("#bEdicion").show().height() : $("#bEdicion").hide().height());
    edicionStr = obtenerRomanoDeNumero(reimpresion, "Reimpresión");
    //$("#SpanReimpresión").text(edicionStr);
    $("#SpanReimpresión").html(reimpresion + " <sup><u>a</u></sup>.", "Reimpresión");
    dummyValue = (reimpresion > 0 ? $("#bReimpresion").show().height() : $("#bReimpresion").hide().height());
    $("#SpanAnio").text(anioPub);
    dummyValue = (anioPub > 0 ? $("#bAnio").show().height() : $("#bAnio").hide().height());
    $("#SpanPaginas").text(paginas);
    dummyValue = (paginas.length > 0 ? $("#bPaginas").show().height() : $("#bPaginas").hide().height());
    $("#SpanMedidas").text(medidas);
    dummyValue = (medidas.length > 0 ? $("#bMedidas").show().height() : $("#bMedidas").hide().height());
    $("#SpanSerie500").text(serie500);
    dummyValue = (serie500.length > 0 ? $("#bSerie500").show().height() : $("#bSerie500").hide().height());
    $("#SpanContenido").text(contenido);
    dummyValue = (contenido.length > 0 ? $("#bContenido").show().height() : $("#bContenido").hide().height());
    $("#SpanCualidades").text(cualidades);
    dummyValue = (cualidades.length > 0 ? $("#bCualidades").show().height() : $("#bCualidades").hide().height());
    $("#SpanColofon").text(colofon);
    dummyValue = (colofon.length > 0 ? $("#bColofon").show().height() : $("#bColofon").hide().height());
    $("#SpanTema").text(tema);
    dummyValue = (tema.length > 0 ? $("#bTema").show().height() : $("#bTema").hide().height());
    $("#SpanSecundarias").val(secundarias);
    dummyValue = (secundarias.length > 0 ? $("#bSecundarias").show().height() : $("#bSecundarias").hide().height());
    $("#SpanU_FFYL").text(uffyl);
    dummyValue = (uffyl.length > 0 ? $("#bUFFYL").show().height() : $("#bUFFYL").hide().height());
    $("#SpanU_IIFL").text(uiifl);
    dummyValue = (uiifl.length > 0 ? $("#bUIIFL").show().height() : $("#bUIIFL").hide().height());
    $("#SpanObservaciones").text(observaciones);
    dummyValue = (observaciones.length > 0 ? $("#bObservaciones").show().height() : $("#bObservaciones").hide().height());


    if (typeof (isLatin) == 'boolean' & typeof (isGriego) == 'boolean') {        
        if ((isLatin | isGriego) == true) {
            dummyValue = $("#bFlagLatin").show().height();
            if (isLatin) {
                $("#bRadioIsLatin").prop('checked', true);
                $("#bRadioIsGriego").prop('checked', false);
            } else {
                $("#bRadioIsLatin").prop('checked', false);
                $("#bRadioIsGriego").prop('checked', true);
            }

        } else {
            dummyValue = $("#bFlagLatin").hide().height();
            $("#bRadioIsLatin").prop('checked', false);
            $("#bRadioIsGriego").prop('checked', false);
        }
    } else {
        dummyValue = $("#bFlagLatin").hide().height();
        $("#bRadioIsLatin").prop('checked', false);
        $("#bRadioIsGriego").prop('checked', false);
    }
    
   

    $("#ImagenTitulo").attr("src", rutaArchivo);
    $("#ImagenTitulo").attr("alt", nombrearchivo);
    if (urlPdf != undefined && urlPdf.length > 0) {
        $("#ImagenPdf").show();
        $("#ImagenPdf").attr("data-url", urlPdf);
        $("#ImagenPdf").attr("onclick","onSelectedResource('pdf',\'"+urlPdf+"\')");
    } else {
        $("#ImagenPdf").hide();
        $("#ImagenPdf").removeAttr("data-url");
        $("#ImagenPdf").removeAttr("onclick");
    }

    if (urlVirtual != undefined && urlVirtual.length > 0) {
        $("#ImageVirtual").show();
        $("#ImageVirtual").attr("data-url", urlVirtual);
        $("#ImageVirtual").attr("onclick", "onSelectedResource('virtual',\'" + urlVirtual + "\')");
    } else {
        $("#ImageVirtual").hide();
        $("#ImageVirtual").removeAttr("data-url");
        $("#ImageVirtual").removeAttr("onclick");
    }

    if (urlOnline != undefined && urlOnline.length > 0) {
        $("#ImageOnline").show();
        $("#ImageOnline").attr("data-url", urlOnline);
        $("#ImageOnline").attr("onclick", "onSelectedResource('online',\'" + urlOnline + "\')");
    } else {
        $("#ImageOnline").hide();
        $("#ImageOnline").removeAttr("data-url");
        $("#ImageOnline").removeAttr("onclick");
    }
}

function onGetTituloSuccess(data) {
    var titulo = data.d;    
    setDataModalDetalle(titulo.IdTitulo, titulo.DetalleIsbn, titulo.DetalleResponsables, titulo.Ciudad, titulo.Editor, titulo.Autor,
                        titulo.TituloOriginal, titulo.Titulo, titulo.Edicion, titulo.AnioPublicacion, titulo.Paginas, titulo.Medidas, titulo.Serie500,
                        titulo.Contenido, titulo.Cualidades, titulo.Colofon, titulo.Tema, titulo.Secundarias, titulo.UffYL, titulo.UiiFL, titulo.Observaciones,
                        titulo.Serie, titulo.RutaArchivo, titulo.NombreArchivo, titulo.IsNovedad, titulo.NumeroEdicion, titulo.NumeroReimpresion, titulo.UrlPdf, titulo.UrlVirtual, titulo.UrlOnline, titulo.IsLatin, titulo.IsGriego);
    $("#tabContentDetalle").show();
    $("#tabContentDetalleLoading").hide();
   
}

function onGetTituloError(xhr, status) {
    $("#tabContentDetalle").show();
    $("#tabContentDetalleLoading").hide();    
    setDataModalDetalle("--ERROR--", "","", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", urlSite + "Images/avatar.png", "avatar.png", false,"","","","","", false, false);
    alert("Ocurrio un error desconocido al cargar el dato.");
}

function renderTableResponsables($divToAppend, listaResponsables) {
    if (typeof ($gridDetalleResponsables) != 'undefined')
        $gridDetalleResponsables.remove();

    if (typeof (listaResponsables) != 'undefined') {
        $gridDetalleResponsables = $('<table class="table table-bordered gridView-default gridView-NoResponsive"></table>');
        var classAlternating = 'class="gridRow-default"';
        for (var i = 0; i < listaResponsables.length; i++) {
            if (i % 2 != 0) {
                classAlternating = 'class="gridAlternatingRow-default"';
            } else {
                classAlternating = 'class="gridRow-default"';
            }
            var $tr = $('<tr ' + classAlternating + ' ></tr>');
            //var $tdAction = $("<td style='width:50px;'></td>");
            //$tdAction.append('<a class="btn btn-warning center text-center" onclick="javascript: return deleteResponsable(this);">Eliminar</a>')

            var $td0 = $("<td class='gridViewColumnHidden'>" + listaResponsables[i].IdResponsable + "</td>");
            var $td1 = $("<td></td>");
            $td1.append(listaResponsables[i].NombreCompletoResponsable)
            var $td2_0 = $("<td class='gridViewColumnHidden'>" + listaResponsables[i].IdFuncion + "</td>");
            var $td2 = $("<td >" + listaResponsables[i].TipoFuncion + "</td>");

            //var $td3 = $("<td></td>");
            //$td3.append(listaResponsables[i].OrdenFuncion)

            //$tr.append($tdAction);
            $tr.append($td0);
            $tr.append($td1);
            $tr.append($td2_0);
            $tr.append($td2);
            //$tr.append($td3);
            $gridDetalleResponsables.append($tr);
        }
        if (typeof ($divToAppend) != 'undefined') {
            $divToAppend.append($gridDetalleResponsables)
        }
    }
    return listaResponsables;
}


function renderTableIsbn($divToAppend, listaIsbn) {
    if (typeof ($gridDetalleIsbn) != 'undefined')
        $gridDetalleIsbn.remove();

    if (typeof (listaIsbn) != 'undefined') {
        $gridDetalleIsbn = $('<table class="table table-bordered gridView-default gridView-NoResponsive"></table>');
        var classAlternating = 'class="gridRow-default"';
        for (var i = 0; i < listaIsbn.length; i++) {
            if (i % 2 != 0) {
                classAlternating = 'class="gridAlternatingRow-default"';
            } else {
                classAlternating = 'class="gridRow-default"';
            }
            var $tr = $('<tr ' + classAlternating + ' ></tr>');
            //var $tdAction = $("<td style='width:50px;'></td>");
            //$tdAction.append('<a class="btn btn-warning center text-center" onclick="javascript: return deleteResponsable(this);">Eliminar</a>')

            var $td0 = $("<td class='gridViewColumnHidden'>" + listaIsbn[i].IdIsbn + "</td>");
            var $td1 = $("<td></td>");
            
            var edicion = listaIsbn[i].Edicion;            
            edicion = (edicion > 0) ? obtenerRomanoDeNumero(edicion, "Edición"): "";    
            var reed = listaIsbn[i].Reedicion;
            reed = (reed > 0) ? obtenerRomanoDeNumero(reed, "Reedición") : "";
            var reimpr = listaIsbn[i].Reimpresion;
            reimpr = (reimpr > 0) ? obtenerRomanoDeNumero(reimpr, "Reimpresión") : "";

            $td1.append(listaIsbn[i].ClaveIsbn + (edicion.length > 0 ? "," + edicion : "") + (reed.length > 0 ? "," + reed : "") + (reimpr.length > 0 ? "," + reimpr : ""))
            
            //$tr.append($tdAction);
            $tr.append($td0);
            $tr.append($td1);            
            $gridDetalleIsbn.append($tr);
        }
        if (typeof ($divToAppend) != 'undefined') {
            $divToAppend.append($gridDetalleIsbn)
        }
    }
    return listaIsbn;
}