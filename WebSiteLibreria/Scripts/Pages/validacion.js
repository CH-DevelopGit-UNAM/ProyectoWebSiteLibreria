function setMessagePopup(message, typeMessage, div) {
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

        var newHtml = "<div id='message-content' class='_className' role='alert' style='text-align:left; padding:7px; margin:2px; '>";
        newHtml += "<span class='_iconClassName' aria-hidden='true' style='padding-right:5px;'></span>";
        newHtml += message;
        newHtml += "    </div>";

        $(div).html(newHtml.replace("_className", _className).replace("_iconClassName", _iconClassName));

    } catch (e) {
        alert(message);
    }
}

function setErrorOnPanel(idPanel, message) {
    var popup = "<div class='warning-block' style='position:absolute; max-width: 330px; top:3px;'>"+ message +"</div>";
    if (typeof (idPanel) == 'string') {
        $(idPanel).html(popup);
    }
}

function setInfoOnPanel(idPanel, message) {
    var popup = "<div class='info-block' style='position:absolute; max-width: 330px; top:3px;'>" + message + "</div>";
    if (typeof (idPanel) == 'string') {
        $(idPanel).html(popup);
    }
}

function clearBlockInfoPanel(idPanel) {
    $(idPanel).html("");
}

function estableceEvento(id, id_param) {
    var tb = document.getElementById(id);

    if (document.all) {
        if (id_param === undefined)
            tb.onfocusout = function () { validar(); };
        else
            tb.onfocusout = function () { validar(id_param); };
    }
    else {
        if (id_param === undefined)
            tb.onblur = function () { validar(); };
        else
            tb.onblur = function () { validar(id_param); };
    }

}

function setListenerValidation() {
    $(".form-Validate").each(function (index, divForm) {
        var $validationGroup = $(divForm).find(".group-validation");
        $(divForm).find(".control-Validate").each(function (index, control) {
            if ($(control).is("textarea")) {
                $(control).on("change onBlur", function () {
                    validateControl($(this), false, $validationGroup);                    
                });
            } else if ($(control).is("input[type='text']") || $(control).is("input[type='password']")) {
                $(control).on("change onBlur", function () {
                    validateControl($(this), false, $validationGroup);                    
                });
            } else if ($(control).is("span")) {
                $(control).on("change onBlur", function () {                    
                    validateControl($(this), false, $validationGroup);                    
                });
            } else if ($(control).is("select")) {
                $(control).on("change onBlur", function () {
                    validateControl($(this), false, $validationGroup);                    
                });
            } else {
                $(control).on("change onBlur", function () {
                    validateControl($(this), false, $validationGroup);                   
                });
            }
        });
    });
}

function setCustomListenerValidation(idPanel) {    
    if (typeof (idPanel) == 'string') {
        $(idPanel).find("div.form-Validate-manual").each(function (index, divForm) {
            var $validationGroup = $(divForm).find(".group-validation-manual");
            $(divForm).find(".control-Validate-manual").each(function (index, control) {                
                if ($(control).is("textarea")) {
                    $(control).on("change onBlur", function () {
                        validateControl($(this), false, $validationGroup);
                    });
                } else if ($(control).is("input[type='text']") || $(control).is("input[type='password']")) {
                    $(control).on("change onBlur", function () {                        
                        validateControl($(this), false, $validationGroup);
                    });
                } else if ($(control).is("span")) {
                    $(control).on("change onBlur", function () {
                        validateControl($(this), false, $validationGroup);
                    });
                } else if ($(control).is("select")) {
                    $(control).on("change onBlur", function () {
                        validateControl($(this), false, $validationGroup);
                    });
                } else {
                    $(control).on("change onBlur", function () {
                        validateControl($(this), false, $validationGroup);
                    });
                }
            });
        });
    }    
}

function validarPanel(idModalPanel) {
    var validGroup = false;
    var lastError = "";
    var validacionGeneral = { IsValid: false, LastError: "" };
    if (typeof (idModalPanel) != 'undefined') {
        var divForm = $(idModalPanel).find("div.form-Validate");
        var groupValidation = $(idModalPanel).find("div.group-validation");
        var groupResult = false;
        if (typeof (divForm) != 'undefined') {
            groupResult = true;
            divForm.find(".control-Validate").each(function (index, item) {
                var result = validateControl($(item), false, $(groupValidation));
                if (result.HasErrorPopup == false) {
                    lastError = result.ResultValidation;
                }
                groupResult &= result.IsValid;                
            });
        } else {
            groupResult = false;
        }
        validGroup = groupResult;        
        if (lastError.length > 0) {            
            //setErrorOnPanel("#"+groupValidation[0].id, lastError);
            setMessagePopup(lastError, "Warning", "#" + groupValidation[0].id);
        } else {
            clearBlockInfoPanel("#" + groupValidation[0].id);
        }

    } else {
        console.log("Sin reglas de validación");
    }
    validacionGeneral.IsValid = (validGroup === 1 ? true : false);
    validacionGeneral.LastError = lastError;    
    return validacionGeneral;
}

function validarCustomPanel(idModalPanel) {
    var validGroup = false;
    var lastError = "";
    var validacionGeneral = { IsValid: false, LastError: "" };
    if (typeof (idModalPanel) != 'undefined') {
        var divForm = $(idModalPanel).find("div.form-Validate-manual");
        var groupValidation = $(idModalPanel).find("div.group-validation-manual");
        var groupResult = false;
        if (typeof (divForm) != 'undefined') {
            groupResult = true;
            divForm.find(".control-Validate-manual").each(function (index, item) {
                var result = validateControl($(item), false, $(groupValidation));
                if (result.HasErrorPopup == false) {
                    lastError = result.ResultValidation;
                }
                groupResult &= result.IsValid;
            });
        } else {
            groupResult = false;
        }
        validGroup = groupResult;
        if (lastError.length > 0) {
            //setErrorOnPanel("#"+groupValidation[0].id, lastError);
            setMessagePopup(lastError, "Warning", "#" + groupValidation[0].id);
        } else {
            clearBlockInfoPanel("#" + groupValidation[0].id);
        }

    } else {
        console.log("Sin reglas de validación");
    }
    validacionGeneral.IsValid = (validGroup === 1 ? true : false);
    validacionGeneral.LastError = lastError;
    return validacionGeneral;
}

function cerrarPanel(idModalPanel) {
    if (typeof (idModalPanel) != 'undefined') {
        var divForm = $(idModalPanel).find("div.form-Validate");
        var groupValidation = $(idModalPanel).find("div.group-validation");
        if (typeof (divForm) != 'undefined') {
            divForm.find(".control-Validate").each(function (index, item) {
                validateControl($(item), true, $(groupValidation));
            });
        }
    } else {
        console.log("Sin reglas de validación");
    }
}

function cerrarCustomPanel(idModalPanel) {
    if (typeof (idModalPanel) != 'undefined') {
        var divForm = $(idModalPanel).find("div.form-Validate-manual");
        var groupValidation = $(idModalPanel).find("div.group-validation-manual");
        if (typeof (divForm) != 'undefined') {
            divForm.find(".control-Validate-manual").each(function (index, item) {
                validateControl($(item), true, $(groupValidation));
            });
        }
    } else {
        console.log("Sin reglas de validación");
    }
}

function validateControl($control, clearError, $validationGroup) {
    var value = "";
    var rules = "";
    var result = { IsValid: false, ResultValidation: "", HelpText: "", TitleValidation: "", Value: "", Rules: "", HasErrorPopup : false };
    if (typeof ($control) != 'undefined') {
        rules = (typeof ($control.attr("data-rule-validation")) != 'undefined') ? $control.attr("data-rule-validation") : "";

        if (($control.is("input[type='text']") || $control.is("input[type='password']"))) { //&& $control.is(":visible")
            value= $control.val();
        } else if ($control.is("textarea")) { //&& $control.is(":visible")
            value = $control.val();            
        } else if ($control.is("input")) { //&& $control.is(":visible")
            value= $control.val();            
        } else if ($control.is("span")) { //&& $control.is(":visible")
            value = $control.text();            
        } else if ($control.is("select")) { //&& $control.is(":visible")
            value = $control.find("option:selected").val();
        } else {
            value= $control.val();
        }        
        if (rules.split("::").length > 1) {
            var newRules = rules.split("::");
            newRules[0] = $control[0].id;
            var stringRules = "";
            var separator = "::";
            for (var i = 1; i <= newRules.length; i++) {
                if (i < newRules.length) {
                    separator = "::";
                } else {
                    separator = "";
                }
                stringRules += newRules[i - 1] + separator;                
            }            
            rules = stringRules;
            
        }

        var resultValidation ;
        if (clearError === true) {
            clearErrors($control, $validationGroup);
        } else {            
            result = validator(rules, value);
            $control.attr("title", result.TitleValidation);
            if (result.IsValid == false) {
                var resultPopup = showErrors($control, result, $validationGroup);
                result.HasErrorPopup = resultPopup.HasErrorPopup;
            } else {
                clearErrors($control, $validationGroup);
            }
        }
    }
    return result;
}

function showErrors($control, optionResultValidation, $validationGroup) {
    if (typeof ($control) != 'undefined') {
        var $autocompleteControl = $control.siblings(".control-validate-autocomplete");
        var offsetControl;
        var offsetSize;
        var offsetRelative;
        if (typeof($autocompleteControl[0]) != 'undefined') {            
            offsetControl = $autocompleteControl.offset();
            offsetSize = { width: $autocompleteControl[0].offsetWidth, height: $autocompleteControl[0].offsetHeight };
            offsetRelative = { top: $autocompleteControl[0].offsetTop, left: $autocompleteControl[0].offsetLeft };
        } else {
            offsetControl = $control.offset();
            offsetSize = { width: $control[0].offsetWidth, height: $control[0].offsetHeight };
            offsetRelative = { top: $control[0].offsetTop, left: $control[0].offsetLeft };
        }        
        var coordinates = { top: 0, left: 0 };
        //console.log(offsetControl);
        //console.log(offsetSize);
        //console.log(offsetRelative);
        //console.log($control[0].offsetParent);
        var $popup = $control.siblings(".warning-block");
        var hasPopup = false;
        var errorText ="";
        coordinates = { top: offsetControl.top + offsetSize.height, left: offsetControl.left + 25 };

        if (($control.is("input[type='text']") || $control.is("input[type='password']") ) && $control.is(":visible")) {
            $control.addClass("controlFocus-OnError");
            //coordinates.top = offsetSize.height;
            //coordinates.left = offsetRelative.left + 20;
        } else if ($control.is("textarea") && $control.is(":visible")) {
            $control.addClass("controlFocus-OnError");
            //coordinates.top = offsetSize.height;
            //coordinates.left = offsetRelative.left + 20;
        }else if ($control.is("span") && $control.is(":visible")) {
            $control.addClass("controlFocus-label-OnError");
            //coordinates.top = offsetSize.height;
            //coordinates.left = offsetRelative.left + 20;
        } else if ($control.is("select") && $control.is(":visible")) {
            $control.addClass("controlFocus-OnError");
            //coordinates.top = offsetSize.height;
            //coordinates.left = offsetRelative.left + 20;
        } else {
            $control.addClass("controlFocus-OnError");
        }
        hasPopup = $popup.hasClass("warning-block");

        if (typeof (optionResultValidation) != 'undefined') {
            $popup.text(optionResultValidation.ResultValidation);
            optionResultValidation.HasErrorPopup = hasPopup;            
        }

        if ($popup.is(":visible") == false && hasPopup) {
            // Primero mostrar
            $popup.show();
            // luego posicion
            $popup.offset(coordinates);
        } else {
            if (!hasPopup) {                
                if (typeof (optionResultValidation) != 'undefined') {
                    //setErrorOnPanel("#"+$validationGroup[0].id, optionResultValidation.ResultValidation);
                    setMessagePopup(optionResultValidation.ResultValidation, "Warning", "#" + $validationGroup[0].id);
                } else {
                    //setErrorOnPanel("#" + $validationGroup[0].id, "");
                    setMessagePopup("", "Warning", "#" + $validationGroup[0].id);
                }
            }
        }
    }
    return optionResultValidation;
}

function clearErrors($control, $validationGroup) {
    var hasPopup = false;

    if (typeof ($control) != 'undefined') {
        var $popup = $control.siblings(".warning-block");
        var coordinates = { top: 0, left: 0 };
        hasPopup = $popup.hasClass("warning-block");

        if ($control.hasClass("controlFocus-OnError")) {
            $control.removeClass("controlFocus-OnError");            
        }
        if ($control.hasClass("controlFocus-OnError-border") ) {
            $control.removeClass("controlFocus-OnError-border");
        }

        if ($control.is(":visible")) {
            $popup.offset(coordinates);
            $popup.hide();
        }
    }

    if (typeof ($validationGroup) != 'undefined' && hasPopup == false ) {
        clearBlockInfoPanel("#" + $validationGroup[0].id);
    }

}


function validator(rules, valueText) {
    var isValid = false;
    var titleValidation = "No Rules";
    var resultValidation = { IsValid: false, ResultValidation: "", HelpText: "", TitleValidation : "", Value : valueText, Rules : rules  };
    var protoTypeRule = "controlId::isRequired::typeValidation::valueExtra::regularExpression::customMessage";
    
    if (typeof (rules) === 'string') {
        var reglas = rules.split("::");
        if (reglas.length >= 2 ) {
            //var regExp_dd_MM_YYYY = '^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$';
            
            var isRequired = false;
            var controlId = reglas[0];
            var isRequiredInt = reglas[1];
            var typeValidation = reglas[2];
            var valueExtra = (reglas.length > 2) ? reglas[3] : "";
            var regularExpression = (reglas.length > 3) ? reglas[4] : "";
            var customMessage = (reglas.length > 4) ? reglas[5] : "";
            var customFunction = (reglas.length > 5) ? reglas[6] : "";

            isRequired = (isRequiredInt == 1);

            var yearReg = "([1-9]\d{3})";
            var montReg = "(0?[1-9]|1[012])";
            var dayReg = "(0?[1-9]|[12][0-9]|3[01])";
            var separatorDiagonal = "[\/]";
            var separatorGuion = "[\-]";
            var separatorAmbos = "[\/\-]";
            var dtCh = "";
            
            var diferenteCero;
            var diferenteMenosUno;

            var regExp_dd_MM_YYYY = '^' + dayReg + separatorDiagonal + montReg + separatorDiagonal + yearReg + '$';
            var regExp_MM_dd_YYYY = '^' + montReg + separatorDiagonal + dayReg + separatorDiagonal + yearReg + '$';
            var regExp_YYYY_MM_dd = '^' + yearReg + separatorDiagonal + montReg + separatorDiagonal + dayReg + '$';
            var regExp_YYYY_MM_dd_ISO = '^' + yearReg + separatorGuion + montReg + separatorGuion + dayReg + '$';
            var validar = true;
            var fecha = "";
            
            valueText = replaceAll(valueText, " ", "");
            if (valueText == "") {
                validar = false;
            }
            var ban = true;

            if (isRequired == 1) {
                resultValidation.TitleValidation = "Campo requerido";
                resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : resultValidation.TitleValidation;                
                // false = campo vacio = No valido
                isValid = (validar == false) ? false : true;
            } else {
                validar = true;
                //isValid = false;
            }

            switch (typeValidation) {
                case "NA":                    
                    if (validar == true) {
                        isValid = true;
                    } else {
                        isValid = false;
                    }
                    break;
                case "entero":
                    resultValidation.TitleValidation = "Escriba sólo números enteros";
                    if (validar == true) {                        
                        if (isNumeric(valueText) == false) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "Formato no válido";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;
                case "decimal":
                    resultValidation.TitleValidation = "Escriba sólo números decimales";
                    if (validar) {
                        if (!isDouble(quitaComasDecimal(valueText))) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "Formato no válido";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;
                case "fecha":
                    resultValidation.TitleValidation = "Escriba una fecha (DD/MM/YYYY)";
                    if (validar) {
                        if (!isDate(valueText)) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "El formato no es correcto";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;

                case "fechaI5":
                    resultValidation.TitleValidation = "Escriba una fecha (ddMMyyyy)";
                    if (validar) {
                        try {
                            fecha = valueText.substring(0, 2) + dtCh + valueText.substring(2, 4) + dtCh + valueText.substring(4);
                        }
                        catch (ex) {
                            fecha = "";
                        }
                        if (!isDate(fecha)) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "El formato no es correcto";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;

                case "fechaI5inv":
                    resultValidation.TitleValidation = "Escriba una fecha (yyyyMMdd)";

                    if (validar) {
                        try {
                            fecha = valueText.substring(6, 8) + dtCh + valueText.substring(4, 6) + dtCh + valueText.substring(0, 4);
                        }
                        catch (ex) {
                            fecha = "";
                        }
                        if (!isDate(fecha)) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "El formato no es correcto";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;

                case "fechaHoy":
                    resultValidation.TitleValidation = "Escriba una fecha (DD/MM/YYYY) no menor a la fecha actual";
                    if (validar) {
                        if (!isDate(valueText)) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "El formato no es correcto";
                            isValid = false;
                        }
                        else {
                            if (!fechaHoy(valueText)) {
                                resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "Escriba una fecha no menor a la fecha actual";
                                isValid = false;
                            }
                            else {
                                try {
                                    diasAnticipacion = parseInt(valueExtra);
                                    if (!isNaN(diasAnticipacion)) {
                                        resultValidation.ResultValidation = "Con " + diasAnticipacion + " días de diferencia.";
                                        var result1 = validaFecha(tb, diasAnticipacion);
                                        isValid = result1.IsValid;
                                        resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : result1.ResultValidation;
                                    }
                                }
                                catch (e) {
                                    resultValidation.ResultValidation = "Días de diferencia: ParseError";
                                    isValid = false;
                                }
                            }
                        }
                    }
                    break;

                case "anio":
                    resultValidation.TitleValidation = "Escriba un año válido";
                    if (validar) {
                        if (!isNumeric(valueText) || valueText < 1900 || valueText > 2100) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "Escriba un año válido";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;

                case "mes":
                    resultValidation.TitleValidation = "Escriba un mes válido";
                    if (validar) {
                        if (!isNumeric(valueText) || valueText < 1 || valueText > 12) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "Escriba un mes válido";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;

                case "semana":
                    resultValidation.TitleValidation = "Escriba una semana válida";
                    if (val) {
                        if (!isNumeric(valueText) || valueText < 1 || valueText > 53) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "Escriba una semana válida";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;

                case "hora":
                    resultValidation.TitleValidation = "Escriba una hora (HH:MM)";
                    if (validar) {
                        if (!isTime(valueText)) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "El formato es incorrecto";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;
                case "correo":
                    resultValidation.TitleValidation = "Escriba un correo válido";
                    if (validar) {
                        if (!isCorreo(valueText)) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "El correo electrónico no es válido";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;
                case "porcentaje":
                    resultValidation.TitleValidation = "Escriba sólo números menores a 100";
                    if (val) {
                        if (!isDoubleMenor100(quitaComasDecimal(tb.value))) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "Porcentaje menor a 100";
                            isValid = false;
                        } else {
                            isValid = true;
                        }

                    }
                    break;
                case "check":
                    resultValidation.TitleValidation = "";
                    isValid = true;
                    break;
                case "curp":
                    resultValidation.TitleValidation = "Escriba una curp válida";
                    if (validar) {
                        if (!isCurp(valueText)) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "Formato no válido";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;

                case "rfc":
                    resultValidation.TitleValidation = "Escriba un RFC válido";
                    if (validar) {
                        if (!validaRFC(valueText)) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "Formato RFC no válido";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;

                case "rfcFis":
                    resultValidation.TitleValidation = "Escriba un RFC válido";
                    if (validar) {
                        if (!isRFCFis(tb.value)) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "Formato RFC no válido";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;
                case "rfcMor":
                    resultValidation.TitleValidation = "Escriba un RFC válido";
                    if (validar) {
                        if (!isRFCMor(valueText)) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "Formato RFC no válido";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;

                case "rfcSinHomoClave":
                    resultValidation.TitleValidation = "Escriba un RFC válido";
                    if (validar) {
                        if (!validaRFCsinHomoClave(valueText)) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "Formato RFC no válido";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;

                case "rfcFisSinHomoClave":
                    resultValidation.TitleValidation = "Escriba un RFC válido";
                    if (validar) {
                        if (!isRFCfisSinHomoClave(tb.value)) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "Formato RFC no válido";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;
                case "rfcMorSinHomoClave":
                    resultValidation.TitleValidation = "Escriba un RFC válido";
                    if (validar) {
                        if (!isRFCMorSinHomoClave(valueText)) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "Formato RFC no válido";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;


                case "funcion":
                    resultValidation.TitleValidation = "Validación por función";
                    try {
                        if (validar) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "Error en la función";
                            if (!self[customFunction](valueExtra, valueText, resultValidation)) {                               
                                isValid = false;
                            } else {
                                resultValidation.ResultValidation = "OK";
                                isValid = true;
                            }

                        }
                    }
                    catch (err) {
                        resultValidation.ResultValidation = (customMessage.length > 1) ? "Catch: " + customMessage : "Catch : Error en la función";                        
                        isValid = false;
                    }

                    break;
                case "cuenta":
                    resultValidation.TitleValidation = "Escriba una cuenta";
                    if (validar) {
                        if (!isCta(valueText)) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "El formato es incorrecto";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;

                case "telefono":
                    resultValidation.TitleValidation = "Escriba un Teléfono Valido";
                    if (validar) {
                        if (!isTelefono(valueText)) {
                            resultValidation.ResultValidation = "Formato no valido";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;
                case "celular":
                    resultValidation.TitleValidation = "Escriba un Teléfono Valido";
                    if (validar) {
                        if (!isCelular(valueText)) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "Formato no valido";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;
                case "clabe":
                    resultValidation.TitleValidation = "Escriba una clabe Valida";
                    if (validar) {
                        if (!isclabeInterbancaria(valueText)) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "Formato no valido";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;
                case "cuenta":
                    resultValidation.TitleValidation = "Escriba una cuenta Valida";
                    if (validar) {
                        if (!iscuentaBancaria(valueText)) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "Formato no valido";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;
                case "rbl":  //RadioButtonList
                    resultValidation.TitleValidation = "Seleccione una opcion valida";
                    //var res = $('#' + tb.value + ' input:checked').val();
                    // Operación no implementada
                    if (validar) {
                        isValid = true;
                    } else {
                        isValid = false;
                    }
                    break;
                case "nocero":
                    resultValidation.TitleValidation = "Seleccione una campo valido";
                    if (validar) {
                        if ((!isNumeric(valueText)) || (valueText == "0")) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;
                case "moneda":
                    //las comas son opcionales, ya que hay que eliminarles antes de guardar en la base
                    resultValidation.TitleValidation = "Formato incorrecto";
                    if (validar) {
                        if (!validaMoneda(valueText)) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "Formato no valido, debe ser #,###.##";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;
                case "AlfaNumerico":
                    //para alfanumericos
                    resultValidation.TitleValidation = "Formato incorrecto";
                    if (validar) {
                        if (AlfaNumerico(valueText)) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "Formato no valido, debe ser Alfanumérico";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;
                case "Categoria":
                    //para alfanumericos
                    resultValidation.TitleValidation = "Formato incorrecto";
                    if (validar) {
                        if (Categoria(valueText)) {
                            resultValidation.ResultValidation = (customMessage.length > 1) ? customMessage : "Formato no valido, debe ser H,I o en blanco";
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
                    break;
                default:
                    resultValidation.TitleValidation = "Escriba un valor que cumpla con el formato requerido";
                    var expRegular = new RegExp(regularExpression);
                    if (validar) {
                        if (!expRegular.test(valueText)) {                            
                            resultValidation.ResultValidation = customMessage;
                            isValid = false;
                        } else {
                            isValid = true;
                        }
                    }
            }            
        } else {
            resultValidation.TitleValidation = "La validación no se inicializó correctamente";
            resultValidation.ResultValidation = "La validación no se inicializó correctamente";
        }
    } else {
        resultValidation.TitleValidation = "La validación no se inicializó correctamente";
        resultValidation.ResultValidation = "La validación no se inicializó correctamente";
    }
    resultValidation.IsValid = isValid;

    return resultValidation;
}



function isCta(sText) {
    expRegular = /\b(?:[0-1]*|[0-1]+\.[0-1]{0,3}\.(?:[0-1]*|[0-1]+\.[0-1]{0,4}))\b/;
    return expRegular.test(sText);
}

function isTime(sText) {
    //expRegular = /(^([0-9]|[0-1][0-9]|[2][0-3]):([0-5][0-9])$)|(^([0-9]|[1][0-9]|[2][0-3])$)/;
    expRegular = /(^([0-9]|[0-1][0-9]|[2][0-3]):([0-5][0-9])$)/;
    return expRegular.test(sText);
}

function isTelefono(sText) {
    expRegular = /(^[0-9]{8,8}$)/;
    return expRegular.test(sText);
}

function isCelular(sText) {
    expRegular = /(^[0-9]{10,10}$)/;
    return expRegular.test(sText);
}

function isNumeric(sText) {
    expRegular = /^\d+$/;
    return expRegular.test(sText);
}


function isDouble(sText) {
    expRegular = /^[-+]?\d+(\.\d+)?$/;
    return expRegular.test(sText);
}

function isDoubleMenor100(sText) {
    if (isDouble(sText)) {
        var dato = parseFloat(sText);
        if (dato <= 100) {
            return true
        }
        else {
            return false
        }

    }
    return false
}


function replaceAll(text, busca, reemplaza) {
    while (text.toString().indexOf(busca) != -1) {
        text = text.toString().replace(busca, reemplaza);
    }
    return text;
}

var dtCh = "/";
var minYear = 1900;
var maxYear = 2100;


function stripCharsInBag(s, bag) {
    var i;
    var returnString = "";
    // Search through string's characters one by one.
    // If character is not in bag, append to returnString.
    for (i = 0; i < s.length; i++) {
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}

function daysInFebruary(year) {
    // February has 29 days in any year evenly divisible by four,
    // EXCEPT for centurial years which are not also divisible by 400.
    return (((year % 4 == 0) && ((!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28);
}
function DaysArray(n) {
    for (var i = 1; i <= n; i++) {
        this[i] = 31
        if (i == 4 || i == 6 || i == 9 || i == 11) { this[i] = 30 }
        if (i == 2) { this[i] = 29 }
    }
    return this
}

function isDate(dtStr) {
    var daysInMonth = DaysArray(12)
    var pos1 = dtStr.indexOf(dtCh)
    var pos2 = dtStr.indexOf(dtCh, pos1 + 1)

    var strDay = dtStr.substring(0, pos1)
    var strMonth = dtStr.substring(pos1 + 1, pos2)

    var strYear = dtStr.substring(pos2 + 1)
    strYr = strYear
    if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1)
    if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1)
    for (var i = 1; i <= 3; i++) {
        if (strYr.charAt(0) == "0" && strYr.length > 1) strYr = strYr.substring(1)
    }
    month = parseInt(strMonth)
    day = parseInt(strDay)
    year = parseInt(strYr)
    if (pos1 == -1 || pos2 == -1) {
        //alert("The date format should be : dd/mm/yyyy")
        return false
    }
    if (strMonth.length < 1 || month < 1 || month > 12) {
        //alert("Please enter a valid month")
        return false
    }
    if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) {
        //alert("Please enter a valid day")
        return false
    }
    if (strYear.length != 4 || year == 0 || year < minYear || year > maxYear) {
        //alert("Please enter a valid 4 digit year between " + minYear + " and " + maxYear)
        return false
    }
    if (dtStr.indexOf(dtCh, pos2 + 1) != -1 || isNumeric(stripCharsInBag(dtStr, dtCh)) == false) {
        //alert("Please enter a valid date")
        return false
    }
    return true
}

var errorFormato;
function fechaHoy(dtStr, FechaBase) {
    errorFormato = true;
    var daysInMonth = DaysArray(12);
    var pos1 = dtStr.indexOf(dtCh);
    var pos2 = dtStr.indexOf(dtCh, pos1 + 1);

    var strDay = dtStr.substring(0, pos1);
    var strMonth = dtStr.substring(pos1 + 1, pos2);

    var strYear = dtStr.substring(pos2 + 1);
    strYr = strYear;
    if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1);
    if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1);
    for (var i = 1; i <= 3; i++) {
        if (strYr.charAt(0) == "0" && strYr.length > 1) strYr = strYr.substring(1);
    }
    var month = parseInt(strMonth);
    var day = parseInt(strDay);
    var year = parseInt(strYr);
    if (pos1 == -1 || pos2 == -1) {
        return false;
    }
    if (strMonth.length < 1 || month < 1 || month > 12) {
        return false;
    }
    if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) {
        return false;
    }
    if (strYear.length != 4 || year == 0 || year < minYear || year > maxYear) {
        return false;
    }
    if (dtStr.indexOf(dtCh, pos2 + 1) != -1 || isNumeric(stripCharsInBag(dtStr, dtCh)) == false) {
        return false;
    }

    errorFormato = false;
    var fechaActual;
    if (FechaBase === undefined) {
        fechaActual = new Date();
    }
    else {
        if (!isDate(FechaBase))
            return false;
        else
            var fechaA = FechaBase.toString().split("/");
        FechaBase = fechaA[1] + "/" + fechaA[0] + "/" + fechaA[2];
        fechaActual = new Date(FechaBase);
    }
    var mesActual = fechaActual.getMonth() + 1;
    var diaActual = fechaActual.getDate();
    var anioActual = fechaActual.getFullYear();

    if (year < anioActual || (year == anioActual && month < mesActual) ||
        (year == anioActual && month == mesActual && day < diaActual))
        return false;
    else
        return true;
}


function isCorreo(sText) {
    //expRegular = /(^[\_]*([a-zA-Z0-9]+(\.|\_*)?)+@([a-zA-Z][a-zA-Z0-9\-]+(\.|\-*\.))+[a-zA-Z]{2,6}$)/; //revisa que sea un correo electronico con formato valido algo@provedor.dominio
    expRegular = /(.+.*@.+.*\..+.*)/;
    return expRegular.test(sText);
}


function isCurp(sText) {
    //Modificada por Luis
    //La expresion solo valida parcialmente el criterio de construccion, faltaria validar el criterio de exclusion y la logica faltante de la construccion,
    //que se realiza en el codebehind por la info que se necesita
    //expRegular = /(^[A-Z]{4}\d{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])[HM]{1}(AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[EL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[BCDFGHJKLMNPQRSTVWXYZ]{3}[0-9A-Z]{1}[0-9]{1})/;
    expRegular = /(^[A-Z]{4}\d{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])[HM]{1}(AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[BCDFGHJKLMNPQRSTVWXYZ]{3}[0-9A-Z]{1}[0-9]{1})/;
    var exp = expRegular.test(sText.toUpperCase());
    if (exp) {
        //Lista de Palabras Inconvenientes que se excluiran en el armado (segun Anexo 2 RENAPO)
        var pos = sText.substring(0, 4).toUpperCase();
        if ((pos == 'BACA') || (pos == 'BAKA') || (pos == 'BUEI') || (pos == 'BUEY') || (pos == 'CACA') || (pos == 'CACO') || (pos == 'CAGA') ||
            (pos == 'CAGO') || (pos == 'CAKA') || (pos == 'CAKO') || (pos == 'COGE') || (pos == 'COGI') || (pos == 'COJA') || (pos == 'COJE') ||
            (pos == 'COJI') || (pos == 'COJO') || (pos == 'COLA') || (pos == 'CULO') || (pos == 'FALO') || (pos == 'FETO') || (pos == 'GETA') ||
            (pos == 'GUEI') || (pos == 'GUEY') || (pos == 'JETA') || (pos == 'JOTO') || (pos == 'KACA') || (pos == 'KACO') || (pos == 'KAGA') ||
            (pos == 'KAGO') || (pos == 'KAKA') || (pos == 'KAKO') || (pos == 'KOGE') || (pos == 'KOGI') || (pos == 'KOJA') || (pos == 'KOJE') ||
            (pos == 'KOJI') || (pos == 'KOJO') || (pos == 'KOLA') || (pos == 'KULO') || (pos == 'LILO') || (pos == 'LOCA') || (pos == 'LOCO') ||
            (pos == 'LOKA') || (pos == 'LOKO') || (pos == 'MAME') || (pos == 'MAMO') || (pos == 'MEAR') || (pos == 'MEAS') || (pos == 'MEON') ||
            (pos == 'MIAR') || (pos == 'MION') || (pos == 'MOCO') || (pos == 'MOKO') || (pos == 'MULA') || (pos == 'MULO') || (pos == 'NACA') ||
            (pos == 'NACO') || (pos == 'PEDA') || (pos == 'PEDO') || (pos == 'PENE') || (pos == 'PIPI') || (pos == 'PITO') || (pos == 'POPO') ||
            (pos == 'PUTA') || (pos == 'PUTO') || (pos == 'QULO') || (pos == 'RATA') || (pos == 'ROBA') || (pos == 'ROBE') || (pos == 'ROBO') ||
            (pos == 'RUIN') || (pos == 'SENO') || (pos == 'TETA') || (pos == 'VACA') || (pos == 'VAGA') || (pos == 'VAGO') || (pos == 'VAKA') ||
            (pos == 'VUEI') || (pos == 'VUEY') || (pos == 'WUEI') || (pos == 'WUEY')) {
            return false;
        } else {
            var y = sText.substring(4, 6);
            var m = sText.substring(6, 8);
            var d = sText.substring(8, 10);
            if (y == "00") y = "2000";
            var x = new Date(y, m * 1 - 1, d);
            var z = new Date();
            if ((m * 1) != (x.getMonth() + 1)) return false
            if ((d * 1) != x.getDate()) return false
            return true;
        }
    } else {
        return false;
    }
}


function validaRFC(rfc) {
    return isRFCFis(rfc) || isRFCMor(rfc);
}

function isRFCFis(sText) {
    var y = sText.substring(4, 6);
    var m = sText.substring(6, 8);
    var d = sText.substring(8, 10);
    var x = new Date(y, m * 1 - 1, d);
    var z = new Date();
    if ((m * 1) != (x.getMonth() + 1)) return false
    if ((d * 1) != x.getDate()) return false
    //expRegular = /(^[a-zA-Z]{4}\d{6}[a-zA-Z0-9]{2}[0-9aA]{1})/;
    expRegular = /(^[a-zA-Z]{4}\d{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])[a-zA-Z0-9]{2}[0-9aA]{1})/;

    return expRegular.test(sText);
}

function isRFCMor(sText) {
    var y = sText.substring(3, 5);
    var m = sText.substring(5, 7);
    var d = sText.substring(7, 9);
    var x = new Date(y, m * 1 - 1, d);
    var z = new Date();
    //if ((z.getFullYear() - x.getFullYear()) < 18) return false
    if ((m * 1) != (x.getMonth() + 1)) return false
    if ((d * 1) != x.getDate()) return false
    //expRegular = /(^[a-zA-Z&]{3}\d{6}[a-zA-Z0-9]{2}[0-9aA]{1})/;
    expRegular = /(^[a-zA-Z&]{3}\d{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])[a-zA-Z0-9]{2}[0-9aA]{1})/;

    return expRegular.test(sText);
}

// RFC Sin Homoclave
function validaRFCsinHomoClave(rfc) {
    return isRFCfisSinHomoClave(rfc) || isRFCMorSinHomoClave(rfc);
}

// RFC Sin Homoclave
function isRFCfisSinHomoClave(sText) {
    var y = sText.substring(4, 6);
    var m = sText.substring(6, 8);
    var d = sText.substring(8, 10);
    var x = new Date(y, m * 1 - 1, d);
    var z = new Date();
    if ((m * 1) != (x.getMonth() + 1)) return false
    if ((d * 1) != x.getDate()) return false    
    expRegular = /(^[a-zA-Z]{4}\d{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])([a-zA-Z0-9]{2}[0-9aA]{1})?)/;

    return expRegular.test(sText);
}

// RFC Sin Homoclave
function isRFCMorSinHomoClave(sText) {
    var y = sText.substring(3, 5);
    var m = sText.substring(5, 7);
    var d = sText.substring(7, 9);
    var x = new Date(y, m * 1 - 1, d);
    var z = new Date();
    //if ((z.getFullYear() - x.getFullYear()) < 18) return false
    if ((m * 1) != (x.getMonth() + 1)) return false
    if ((d * 1) != x.getDate()) return false    
    expRegular = /(^[a-zA-Z&]{3}\d{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])([a-zA-Z0-9]{2}[0-9aA]{1})?)/;

    return expRegular.test(sText);
}

function validaLongitud(sDecima, longitud, rango) {

    var numero = sDecima.toString().split(".");
    var entero = numero[0];
    var fraccion = "";
    var cantidad = 0;
    var auxiliar = rango.toString().replace("[", "");
    var auxrango = auxiliar.toString().replace("]", "");
    var rangoval = auxrango.toString().split("-");
    if ((sDecima.toString().indexOf(",", 0) == -1) && (rangoval.length == 2)) {
        cantidad = parseFloat(sDecima);
        if ((cantidad >= parseFloat(rangoval[0])) && (cantidad <= parseFloat(rangoval[1]))) {
            if (numero.length > 1) {
                fraccion = numero[1];
                if (fraccion.length > longitud) {
                    return false;
                }
            }
            return true;
        } else {
            return false;
        }
    }
    if (rangoval.length == 3) {
        cantidad = parseFloat(quitaComasDecimal(sDecima));
        if ((cantidad >= parseFloat(rangoval[0])) && (cantidad <= parseFloat(rangoval[1]))) {
            if (numero.length > 1) {
                fraccion = numero[1];
                if (fraccion.length > longitud) {
                    return false;
                }
            }
            return true;
        } else {
            return false;
        }
    }
    return false;
}

function quitaComasDecimal(val) {
    var a = val.toString().replace('$', '').split(',')
    val = '';

    for (var count = 0; count < a.length; count++) {
        val += a[count];
        //                if(lon!=''){
        //                    lon=(parseInt(lon)+1);
        //                }
    }
    return val;
}

function VerificaCheck(id){
    var tb = document.getElementById(id);
    for (var j = 0; j < tb.cells.length; j++) {
        var tb_cell = tb.cells[j];
        var tb_Rdo, tb_Chk;
        for (var xx = 0; xx < tb_cell.childNodes.length; xx++) {
            if (tb_cell.childNodes[xx].type == "radio") {
                tb_Rdo = tb_cell.childNodes[xx];
                if (tb_Rdo.checked == true) {
                    return false;
                }
            } else if (tb_cell.childNodes[xx].type == "checkbox") {
                tb_Chk = tb_cell.childNodes[xx];
                if (tb_Chk.checked == true) {
                    return false;
                }
            }
        }
    }
    return true; 
}

//valida q la fecha no sea menor a la hoy y cumpla con los dias de anticipacion;
function validaFecha(objCtrTxt, dias) {
    var retorno = { IsValid: false, ResultValidation: "" };
    var strTxt = objCtrTxt.value;
    var fechaHoy = new Date();
    var fechaTxt = new Date(strTxt.split('/')[2], parseFloat(strTxt.split('/')[1]) - 1, strTxt.split('/')[0], fechaHoy.getHours(), fechaHoy.getMinutes(), fechaHoy.getSeconds());
    if (fechaTxt.getTime()) {
        if (dias != null && dias != '') {          
            fechaHoy.setTime(fechaHoy.getTime() + ConvierteEnteroAMilisegundos(dias));
        }
        
        if (fechaTxt.getTime() < fechaHoy.getTime()) {
            retorno.ResultValidation = "Fecha no valida";
        } else {
            retorno.IsValid = true;
        }
    }
    return retorno;
}

function ConvierteEnteroAMilisegundos(entero) {
    return entero * (24 * 60 * 60 * 1000);
}   

function isclabeInterbancaria(sText)
{
    expRegular = /([0-9]{18})/;
    return expRegular.test(sText);

}
  
function iscuentaBancaria(sText)
{
    expRegular = /([0-9]{10})/;
    return expRegular.test(sText);
}
  
function validaMoneda(sText) {
    //las comas no estan del todo bien ya que puedes meter algo como #,#,#.## pero no me afecta porque las elimino antes de guardar.
    //cualquier queja no sera aceptada, mejor arreglen la funcion. XD
       
    expRegular = /^(\d{1,3}\,)*(\d{1,3})+(.\d{1,2})?$/;
    return expRegular.test(sText);
}

function AlfaNumerico(sText) {
    // Replace invalid characters with empty strings.
    expRegular = /([^a-zA-Z0-9])/;
    return expRegular.test(sText);
}
    
function Categoria(sText) {
    // Replace invalid characters with empty strings.
    expRegular = /([^h-iH-I)])/;

    return expRegular.test(sText);
}