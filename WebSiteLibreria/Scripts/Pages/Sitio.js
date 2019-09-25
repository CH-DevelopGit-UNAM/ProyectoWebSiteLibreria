var idControlAjax;
var ignore_onbeforeunload = false;

window.onbeforeunload = function (e) {
    if (!ignore_onbeforeunload) {
        mostrarOverlayAsync();
    }
    ignore_onbeforeunload = false;    
};

$(document).ready(function () {
    window.onscroll = function () {
        scrollShowItems();
    };

    window.onresize = function () {
        updateTopButton();

        //updateSubMenus();
    };

    if (typeof (Sys) != 'undefined' && Sys != null) {
        var pageRequestManager = Sys.WebForms.PageRequestManager.getInstance();
        pageRequestManager.add_beginRequest(iniciarPeticion);
        pageRequestManager.add_endRequest(peticionTerminada);
    }    

    $("#PopupUserSession").hide();
    $("#ButtonPopupUserSession").click(function () {
        console.log($("#PopupUserSession").is(":visible"));
        var offsetControl = $(this).offset();
        var offsetSize = { width: $(this)[0].offsetWidth, height: $(this)[0].offsetHeight };
        var offsetRelative = { top: $(this)[0].offsetTop, left: $(this)[0].offsetLeft };
        var coordinates = { top: 0, left: 0 };
        coordinates = { top: offsetControl.top + offsetSize.height, left: offsetControl.left };
        $("#PopupUserSession").slideToggle(5, function () {
            $("#PopupUserSession").offset(coordinates);
        });
        //if ($("#PopupUserSession").is(":visible")) {
        //    $("#PopupUserSession").slideUp(5);
        //} else {
        //    $("#PopupUserSession").slideDown(5);
        //}
    });
    $('[data-submenu]').submenupicker();
    var max = 0;
    $("div[id*='LogoAvatar']").each(function (index, value) {
        if ($(value).height() > max) {
            max = $(value).height();
        }        
    });
    if (max > 0) {
        $(".body-section").addClass("body-section-logged");
    }

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

    $('a[href^=mailto]').on('click', function () {
        ignore_onbeforeunload = true;
    });

    //$('.dropdown-submenu a.dropdown-submenu-item').on("click", function (e) {
    //    $(this).next('ul').toggle();
    //    e.stopPropagation();
    //    e.preventDefault();
    //});
});


function iniciarPeticion(sender, args) {
    // Dialogo Bootstrap
    mostrarOverlayAsync();
    var idControl = "";
    if (args._postBackElement != 'undefined') {
        idControl = args._postBackElement.id;
        idControlAjax = idControl;
    }

};

function peticionTerminada(sender, args) {
    cerrarOverlayAsync();
};

function inicializarOverlayModalAsync() {
    // Des-suscribir cualquier event handler
    $('#DivAsyncPostback').off('show.bs.modal');
    // Bootstrap: suscripción el evento "show.bs.modal", ejecutado por bootstrap al momento de usar el método "modal('show')" o "modal('toggle')", para el objeto Jquery Bootstrap.            
    $('#DivAsyncPostback').on('show.bs.modal', function (event) {
        // jquer-ui , progressbar                    
    });

    // Bootstrap: suscripción el evento "hide.bs.modal.prevent", ejecutado por bootstrap al momento de quitar el "Dialog". El evento se puede disparar por el usuario, por medio de un botón específico
    // o dando clic al boton "X" para cerrarlo, o dando clic al Overlay. Principalmente al darle clic al overlay (sección oscura) el "Dialog" desaparece de forma automática, lo que hago con este evento
    // es evitar que se cierre.
    $('#DivAsyncPostback').on('hide.bs.modal.prevent', function (event) {
        // prevenir que se cierre el Dialog ante cualquier interacción con el usuario. Ya que debo cerralo manualmente con javascript (jquery)
        event.preventDefault();
        event.stopImmediatePropagation();
        return false;
    });
}

function mostrarOverlayAsync() {    
    inicializarOverlayModalAsync();
    // Dialogo Bootstrap
    $("#DivAsyncPostback").modal('show');
}

function cerrarOverlayAsync(fnCallback) {
    // Al terminar la petición... cancelar la suscripción al evento "hide.bs.modal.prevent", y de esta forma poder cerrar el Dialog Bootstrap
    $('#DivAsyncPostback').off('hide.bs.modal.prevent');
    // Cerrar el Dialog
    $("#DivAsyncPostback").modal('hide');
    $("#DivAsyncPostback").hide();
    //$("#progressBar").progressbar("value", false);

    if (typeof fnCallback == 'function') {
        fnCallback();
    }
}

function cerrarOverlayRedirect(fnCallback) {    
    $("#DivModal").hide();

    if (typeof fnCallback == 'function') {
        fnCallback();
    }
   
}

function submitBody(element) {
    mostrarOverlayAsync();
    return true;
}

// Funcion no usada : Ya no uso la validación ni las funciones de Microsoft para los formularios web
function WebForm_OnSubmit() {
    // Parece ser un Bug de Microsoft Ajax : Primero debe obtenerse el resultado
    var result = ValidatorOnSubmit();
    // Se valida el resultado
    if (typeof (result) == "function" && result == false) {
        return false;
    } else {
        mostrarOverlayAsync();
    }
    return true;
}

// Funcion no usada : Ya no uso la validación ni las funciones de Microsoft para los formularios web
function WebForm_DoPostBackWithOptions(options) {
    var validationResult = true;
    if (options.validation) {
        if (typeof (Page_ClientValidate) == 'function') {
            validationResult = Page_ClientValidate(options.validationGroup);
        }
    }
    if (validationResult) {
        if ((typeof (options.actionUrl) != "undefined") && (options.actionUrl != null) && (options.actionUrl.length > 0)) {
            theForm.action = options.actionUrl;
        }
        if (options.trackFocus) {
            var lastFocus = theForm.elements["__LASTFOCUS"];
            if ((typeof (lastFocus) != "undefined") && (lastFocus != null)) {
                if (typeof (document.activeElement) == "undefined") {
                    lastFocus.value = options.eventTarget;
                }
                else {
                    var active = document.activeElement;
                    if ((typeof (active) != "undefined") && (active != null)) {
                        if ((typeof (active.id) != "undefined") && (active.id != null) && (active.id.length > 0)) {
                            lastFocus.value = active.id;
                        }
                        else if (typeof (active.name) != "undefined") {
                            lastFocus.value = active.name;
                        }
                    }
                }
            }
        }
    }
    if (options.clientSubmit) {
        __doPostBack(options.eventTarget, options.eventArgument);
    }
}


// Configura el GridView con los estilos, si tiene el skin definido. Además, devuelve un arreglo con los td que tiene
function configurarGridView(idGridView, fnSelectedRow) {
    if (idGridView != null && idGridView != 'undefined') {
        var $grid = $(idGridView);
        configurarObjetoGridView($grid, fnSelectedRow);
    }
}

function configurarObjetoGridView($grid, fnSelectedRow) {
    if ($grid != null && typeof ($grid) != 'undefined') {        
        if ($grid.hasClass("gridView") == true) {
            //console.log("Si");
            $grid.find("tr").each(function (index, itemTr) {
                if ($(itemTr).hasClass("gridRow")) {
                    setListenerClickRow($grid, $(itemTr), "gridRow", "gridRow-selected", "gridAlternatingRow-selected", fnSelectedRow);
                }
                if ($(itemTr).hasClass("gridAlternatingRow")) {
                    setListenerClickRow($grid, $(itemTr), "gridAlternatingRow", "gridRow-selected", "gridAlternatingRow-selected", fnSelectedRow);
                }
            });

        } else if ($grid.hasClass("gridView-default") == true) {
            //console.log("NO");
            $grid.find("tr").each(function (index, itemTr) {                
                if ($(itemTr).hasClass("gridRow-default")) {                    
                    setListenerClickRow($grid, $(itemTr), "gridRow-default", "gridRow-default-selected", "gridAlternatingRow-default-selected", fnSelectedRow);
                }
                if ($(itemTr).hasClass("gridAlternatingRow-default")) {                    
                    setListenerClickRow($grid, $(itemTr), "gridAlternatingRow-default", "gridRow-default-selected", "gridAlternatingRow-default-selected", fnSelectedRow);
                }
            });
        } else {
            //console.log("SEPA");
        }
    }
}

function setListenerClickRow($grid, $currentTr, nameCssRow, nameCssRowSelected, nameCssAlternatingRowSelected, callbackfn) {
    if (typeof ($currentTr) != 'undefined') {
        $currentTr.off("onclick");
        $currentTr.on("click", function () {
            $grid.find("tr." + nameCssRowSelected).each(function (index, item) {
                $(item).removeClass(nameCssRowSelected);
            });

            $grid.find("tr." + nameCssAlternatingRowSelected).each(function (index, item) {
                $(item).removeClass(nameCssAlternatingRowSelected);
            });

            $(this).addClass(nameCssRowSelected);            
            if (typeof (callbackfn) === 'function') {
                if (callbackfn.length > 0 && callbackfn.length < 2) {
                    callbackfn($(this)[0].children);
                } else if (callbackfn.length > 1) {
                    callbackfn($(this)[0].children, this);
                }
            }
        });
    }
}


function message(message, typeMessage, div) {
    try {        
        var _className = "";
        var _iconClassName = "";

        switch (typeMessage) {

            case "Info":
                _className = "alert alert-info";
                _iconClassName = "glyphicon glyphicon-bell";
                break;
            case "Success":
                _className = "alert alert-success";
                _iconClassName = "glyphicon glyphicon-ok-sign";
                break;
            case "Warning":
                _className = "alert alert-warning";
                _iconClassName = "glyphicon glyphicon-warning-sign";
                break;
            case "Error":
                _className = "alert alert-danger";
                _iconClassName = "glyphicon glyphicon-exclamation-sign";
                break;
            default:
                _className = "alert alert-success";
                _iconClassName = "glyphicon glyphicon-ok-sign";
        }

        var newHtml = "<div id='message-content' class='_className' role='alert' style='text-align:center'>";
        newHtml += "<span class='_iconClassName' aria-hidden='true' style='float:left;'></span>";
        newHtml += message;
        newHtml += "<button id='btnCerrar' type='button' class='close' data-dismiss='alert' aria-label='Close'>";
        newHtml += "        <span aria-hidden='true'>&times;</span>";
        newHtml += "        </button>";
        newHtml += "    </div>";

        
        if (div == "" || typeof(div) == 'undefined' || div == null) {
            $("#message").html(newHtml.replace("_className", _className).replace("_iconClassName", _iconClassName));
        }
        else {
            $("#" + div + "").html(newHtml.replace("_className", _className).replace("_iconClassName", _iconClassName));
        }

        $("#message").on('click', function () {
            $("#message").hide(500);
        });

        //var scrollPos = $("#message").offset().top;
        //$(window).scrollTop(scrollPos);

    } catch (e) {
        alert(message);
    }
}


function tryParseInteger(value, defaultValue) {
    var integerNumber = 0;
    var defaultInner = 0;
    if (isNaN(defaultValue) == false) {
        defaultInner = defaultValue;
    } else {
        defaultInner = 0;
    }

    if (value != undefined && value != null) {
        value = value.toString();
        value = value.replace(/[$]+/g, "");
        value = value.replace(/[,]+/g, "");
        value = value.replace(/[\s]+/g, "");
    }
    
    integerNumber = parseInt(value);
    integerNumber = (isNaN(integerNumber) == true) ? defaultInner : integerNumber;
    return integerNumber;
}

function padLeftPersonalizado(texto, cantidadTotal) {
    var longitud = cantidadTotal;
    var padding = "";
    if (texto != undefined && texto != null) {
        texto = texto.toString().trim();
        var relleno = longitud - texto.length;
        if (texto.length <= longitud) {
            for (var i = 0; i < relleno; i++) {
                padding = padding + "0";
            }
        } else {
            texto = texto.substring(texto.length - longitud, texto.length);
        }
    } else {
        texto = "";
        for (var i = 0; i < cantidadTotal; i++) {
            padding = padding + "0";
        }
    }
    texto = padding + texto;
    return texto;
}

function unidadesRomanas(idUnidad) {
    var unidad = "";
    switch (idUnidad)
    {
        case 1:
            unidad = "Primera";
            break;
        case 2:
            unidad = "Segunda";
            break;
        case 3:
            unidad = "Tercera";
            break;
        case 4:
            unidad = "Cuarta";
            break;
        case 5:
            unidad = "Quinta";
            break;
        case 6:
            unidad = "Sexta";
            break;
        case 7:
            unidad = "Séptima";
            break;
        case 8:
            unidad = "Octava";
            break;
        case 9:
            unidad = "Novena";
            break;
        default:
            break;
    }
    return unidad;
}

function decenasRomanas(idUnidad)
{
    var decena = "";
    switch (idUnidad)
    {
        case 10:
            decena = "Décima";
            break;
        case 20:
            decena = "Vigésima";
            break;
        case 30:
            decena = "Trigésima";
            break;
        case 40:
            decena = "Cuadragésima";
            break;
        case 50:
            decena = "Quincuagésima";
            break;
        case 60:
            decena = "Sexagésima";
            break;
        case 70:
            decena = "Septuagésima";
            break;
        case 80:
            decena = "Octogésima";
            break;
        case 90:
            decena = "Nonagésima";
            break;
        default:
            break;
    }
    return decena;
}


function obtenerRomanoDeNumero(numeroImpresion, cadenaConcatenar) {
    var unidad = "";
    var decena = "";
    var resultado = "";
    numeroImpresion = tryParseInteger(numeroImpresion, 0);
    if (numeroImpresion <= 0)
    {
        resultado = "";
    }
    else if (numeroImpresion >= 1 & numeroImpresion < 10)
    {
        unidad = unidadesRomanas(numeroImpresion);
        resultado = unidad + " " + cadenaConcatenar;
    }
    else if (numeroImpresion >= 10 & numeroImpresion < 100)
    {
        var numero = (numeroImpresion / 100.00).toFixed(2);        
        var charNumero = (numero * 10.00).toFixed(1).toString();        
        var numeros = charNumero.split(".");
        var cadena = "";
        var enteroUnidad = 0;
        var enteroDecena = 0;        
        if (numeros.length > 1)
        {
            cadena = numeros[1];
            enteroUnidad = tryParseInteger(cadena , 0);
        }                
        unidad = unidadesRomanas(enteroUnidad);
        enteroDecena = Math.floor(numero * 10) * 10;        
        decena = decenasRomanas(enteroDecena);
        resultado = decena + " " + unidad.toLowerCase() + " " + cadenaConcatenar;
        
    }
    else
    {
        resultado = cadenaConcatenar + " # " + numeroImpresion.toString();
    }
    return resultado;
}


function onSelectedResource(tipoRecurso, url) {
    var urlDecoded = "";
    try {
        urlDecoded = (typeof (url === "string") ? decodeURIComponent(url) : "");
    } catch (e) {

    }
    
    if (tipoRecurso != undefined) {
        switch (tipoRecurso) {
            case 'pdf':
                // prevenir propagación del evento click
                event.stopPropagation();
                window.open(urlDecoded);
                break;
            case 'online':
                // prevenir propagación del evento click
                event.stopPropagation();
                window.open(urlDecoded);
                break;
            case 'virtual':
                // prevenir propagación del evento click
                event.stopPropagation();
                window.open(urlDecoded);
                break;
            default:
                // prevenir propagación del evento click
                event.stopPropagation();
                break;
        }
    }
}

function scrollShowItems() {
    var body = document.getElementById("body-content");
    var widthWindow = window.outerWidth;
    var widthBody = $("#body-content").width();
    var posicion = (widthWindow > widthBody ? ((widthWindow - widthBody) / 2) + widthBody : 0);    
    if (document.body.scrollTop > 150 || document.documentElement.scrollTop > 150) {
        document.getElementById("ButtonTop").style.display = "block";
        document.getElementById("ButtonTop").style.bottom = 0;
        document.getElementById("ButtonTop").style.left = (posicion-100).toString()+"px";
    } else {
        document.getElementById("ButtonTop").style.display = "none";
    }
}


function scrollInicio() {
    $('html, body').animate({ scrollTop: 0 }, 500);
    //document.body.scrollTop = 0; // Safari
    //document.documentElement.scrollTop = 0; // Chrome, Firefox, IE, Opera
}

function updateTopButton() {    
    var widthWindow = window.outerWidth;
    var widthBody = $("#body-content").width();
    var posicion = (widthWindow > widthBody ? ((widthWindow - widthBody) / 2) + widthBody : 0);
    if ($("#ButtonTop").is(":visible")) {
        document.getElementById("ButtonTop").style.left = (posicion - 100).toString() + "px";
    }
}

function updateSubMenus() {
    var widthWindow = window.outerWidth;
    var widthBody = $("#body-content").width();

    $('.dropdown-submenu a.dropdown-submenu-item').off("hover");
    $('.dropdown-submenu a.dropdown-submenu-item').off("click");

    if (widthBody > 750) {        
        $('.dropdown-submenu a.dropdown-submenu-item').hover(function (e) {
            $(this).next('ul').toggle();
            e.stopPropagation();
            e.preventDefault();
        });
    } else {
        $('.dropdown-submenu a.dropdown-submenu-item').on("click", function (e) {
            $(this).next('ul').toggle();
            e.stopPropagation();
            e.preventDefault();
        });
    }      
}