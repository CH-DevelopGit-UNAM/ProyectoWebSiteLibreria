
//Definir Explicitamente la ruta de la página para los PageMethods, para todos los navegadores
//PageMethods.set_path("Modificaciones.aspx");

function pageLoad() {

    $(document).ready(function () {
        configurarGridView(idGrid, function (items, element) {
            var idTitulo = tryParseInteger($(items[1]).text(), 0);
            setDataModalDetalle(idTitulo, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", urlSite+"Images/avatar.png", "avatar.png", false);
        });


        // Código js de los controles de usuarios deben ser refrescados en la página, no en el control de usuario
        $('.carousel').carousel({
            interval: 12000
        });

    });
}

//cerrarOverlayAsync();
$(document).ready(function () {

    configurarGridView(idGrid, function (items, elements) {
        var idTitulo = tryParseInteger($(items[1]).text(), 0);
        setDataModalDetalle(idTitulo, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", urlSite + "Images/avatar.png", "avatar.png", false);
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

    // Lo asígno aquí porque... aunque el Dialog y el button están afuera del UpdatePanel, podría usar el <asp:Button> interno 
    $("#DialogoResponsables").on('show.bs.modal', function (event) {
        $('#DialogoResponsables').on('hide.bs.modal.prevent', function (event) {
            event.preventDefault();
            event.stopImmediatePropagation();
            return false;
        });

        // se puede asignar aquí el 'off', para el  'hide.bs.modal.prevent' y luego el 'hide'
        $(this).find("input[type='button']").each(function (index, value) {
            var dataDismiss = $(value).attr('data-dismiss');
            if (dataDismiss != null && dataDismiss != undefined) {
                $(value).click(function (event) {
                    $("#DialogoResponsables").off('hide.bs.modal.prevent');
                    $("#DialogoResponsables").modal('hide');
                });
            }
        });

        $(this).find("button").each(function (index, value) {
            var dataDismiss = $(value).attr('data-dismiss');
            if (dataDismiss != null && dataDismiss != undefined) {
                $(value).click(function (event) {
                    $("#DialogoResponsables").off('hide.bs.modal.prevent');
                    $("#DialogoResponsables").modal('hide');
                });
            }
        });
    });

    $("#DialogoTituloDetalle").on('shown.bs.modal', function (event) {
    });

    // Modal Detalle Titulo
    $("#DialogoTituloDetalle").on('show.bs.modal', function (event) {
        $('#DialogoTituloDetalle').on('hide.bs.modal.prevent', function (event) {
            event.preventDefault();
            event.stopImmediatePropagation();
            return false;
        });

        $(this).find("input[type='button']").each(function (index, value) {
            var dataDismiss = $(value).attr('data-dismiss');
            if (dataDismiss != null && dataDismiss != undefined) {
                $(value).click(function (event) {
                    $("#DialogoTituloDetalle").off('hide.bs.modal.prevent');
                    $("#DialogoTituloDetalle").modal('hide');
                });
            }
        });

        $(this).find("button").each(function (index, value) {
            var dataDismiss = $(value).attr('data-dismiss');
            if (dataDismiss != null && dataDismiss != undefined) {
                $(value).click(function (event) {
                    $("#DialogoTituloDetalle").off('hide.bs.modal.prevent');
                    $("#DialogoTituloDetalle").modal('hide');
                });
            }
        });
    });

    //// Modal-Until-Show...
    $("#DialogoDetalleTitulo").on('shown.bs.modal', function (event) {
        var id = $(idHiddenClave).val();
        PageMethods.CargaTitulo(id, onGetTituloSuccess, onError);
    });

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

function showDialogResponsables(element) {
    // No des-suscribo a los eventos, porque uso el PageLoad de ajax microsoft.
    //$("#DialogoNuevoUsuario").off('hide.bs.modal.prevent');
    //$("#DialogoNuevoUsuario").off('show.bs.modal');
    var title = $(element).parent().parent().children("td").eq(4).text();
    // Tabla responsables
    var html = $(element).parent().parent().parent().children("td").eq(7).find("div").html();
    $("#DialogoResponsables").modal('show');

    $("#DivContenidoResponsables").html(html);
    $("#ModalResponsableTitulo").html(title);
    $("#DivContenidoResponsables .table.gridView-default").show();

    return false;
}

function showDialogDetalleTitulo(element) {
    var contenido = $(element).parent().parent().parent().children("td").eq(2).text();
    var colofon = $(element).parent().parent().parent().children("td").eq(3).text();
    var titulo = $(element).parent().parent().parent().children("td").eq(4).text();
    $("#DialogoTituloDetalle").modal('show');
    $("#DivContenidoTituloDetalle").html(contenido);
    $("#ModaTituloDetalleTitulo").html(titulo);
    $("#ModaTituloDetalleColofon").html(colofon);

    return false;
}

function showDialogDetalleResponsable(element) {
    $("#DialogoDetalleResponsable").modal('show');
    return false;
}

function showDialogDetail(element) {
    $("#DialogoDetalleTitulo").modal('show');
    return false;
}

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


function setDataModalDetalle(idTitulo, detalleIsbn,ciudad, editor, autor, titOriginal, titulo, edicion, anioPub, paginas,
                            medidas, serie500, contenido, cualidades, colofon, tema, secundarias, uffyl, uiifl,
                            observaciones, serie, rutaArchivo, nombrearchivo, isNovedad) {
    $(idHiddenClave).val(idTitulo);
    $("#SpanIdTitulo").text(idTitulo);

    if (typeof (detalleIsbn) != 'undefined' && detalleIsbn != null) {
        if (detalleIsbn.length > 0) {
            renderTableIsbn($("#DivIsbnEdit"), detalleIsbn);
        } else {
            renderTableIsbn($("#DivIsbnEdit"), []);
        }       
    } else {
        renderTableIsbn($("#DivIsbnEdit"), []);
    }    

    if (typeof (ciudad) != 'undefined' & ciudad != null) {
        if (typeof (serie) === 'string') {
            $("#SpanCiudad").text(ciudad);
        } else {
            $("#SpanCiudad").text(ciudad.Descripcion);
        }
    } else {
        $("#SpanCiudad").text("");
    }

    if (typeof (editor) != 'undefined' & editor != null) {
        if (typeof (serie) === 'string') {
            $("#SpanEditor").text(editor);
        } else {
            $("#SpanEditor").text(editor.Nombre);
        }
    } else {
        $("#SpanEditor").text("");
    }

    if (typeof (autor) != 'undefined' & autor != null) {
        if (typeof (autor) === 'string') {
            $("#SpanAutor").text(autor);
        } else {
            var nombreAutor = autor.NombreEspanol;
            if (autor.NombreLatin.length > 1) {
                nombreAutor = nombreAutor + '(' + autor.NombreLatin + ')';
            } else if (autor.NombreGriego.length > 1) {
                nombreAutor = nombreAutor + '(' + autor.NombreGriego + ')';
            }
            $("#SpanAutor").text(nombreAutor);
        }
    } else {
        $("#SpanAutor").text("");
    }

    if (typeof (serie) != 'undefined' & serie != null) {
        if (typeof (serie) === 'string') {
            $("#SpanSerie").text(serie);
        } else {
            var nombreSerie = "";
            if (serie.NombreLatin.length > 1) {
                nombreSerie = serie.NombreLatin;
            } else if (serie.NombreGriego.length > 1) {
                nombreSerie = serie.NombreGriego;
            }
            $("#SpanSerie").text(nombreSerie);
        }
    } else {
        $("#SpanSerie").text("");
    }
    $("#SpanTituloOriginal").text(titOriginal);
    $("#SpanTitulo").text(titulo);
    $("#SpanEdicion").text(edicion);
    $("#SpanAnio").text(anioPub);
    $("#SpanPaginas").text(paginas);
    $("#SpanMedidas").text(medidas);
    $("#SpanSerie500").text(serie500);
    $("#SpanContenido").text(contenido);
    $("#SpanCualidades").text(cualidades);
    $("#SpanColofon").text(colofon);
    $("#SpanTema").text(tema);
    $("#SpanSecundarias").val(secundarias);
    $("#SpanU_FFYL").text(uffyl);
    $("#SpanU_IIFL").text(uiifl);
    $("#SpanObservaciones").text(observaciones);
    $("#ImagenTitulo").attr("src", rutaArchivo);
    $("#ImagenTitulo").attr("alt", nombrearchivo);
    $(idCheckIsNovedad).prop("checked", isNovedad);
}

function onGetTituloSuccess(titulo) {   
    setDataModalDetalle(titulo.IdTitulo,titulo.DetalleIsbn, titulo.Ciudad, titulo.Editor, titulo.Autor,
                        titulo.TituloOriginal, titulo.Titulo, titulo.Edicion, titulo.AnioPublicacion, titulo.Paginas, titulo.Medidas, titulo.Serie500,
                        titulo.Contenido, titulo.Cualidades, titulo.Colofon, titulo.Tema, titulo.Secundarias, titulo.UffYL, titulo.UiiFL, titulo.Observaciones,
                        titulo.Serie, titulo.RutaArchivo, titulo.NombreArchivo, titulo.IsNovedad);
    $("#tabContentDetalle").show();
    $("#tabContentDetalleLoading").hide();
    if (typeof (titulo.DetalleResponsables) != 'undefined' && titulo.DetalleResponsables != null) {
        if (titulo.DetalleResponsables.length > 0) {
            var listaResponsables = titulo.DetalleResponsables[0].Responsables;
            renderTableResponsables($("#DivResponsablesEdit"), listaResponsables);
        }
    } else {
        renderTableResponsables($("#DivResponsablesEdit"), []);
    }    
}

function onError(args) {
    $("#tabContentDetalle").show();
    $("#tabContentDetalleLoading").hide();    
    setDataModalDetalle("--ERROR--", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", urlSite + "Images/avatar.png", "avatar.png", false);
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
            $td1.append(listaIsbn[i].ClaveIsbn)
            
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