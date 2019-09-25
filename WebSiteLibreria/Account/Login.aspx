<%@ Page Title="Acceso" Language="C#" MasterPageFile="~/ProtectedSite.Master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Account_Login" Async="true"  %>

<asp:Content runat="server" ContentPlaceHolderID="HeaderPlaceHolder">
    <style type="text/css">
        #body {
            position: relative;
        }

        .top-element {
            position: relative;
            height: 100%;
            width: 70%;
            margin: auto;
        }

        .formContainer {
            width: 100%;
            height: 100%;
        }


        @media screen and (max-width: 1200px) {
            .top-element {
                width: 80%;
                max-width: 80%;
            }
        }


        /* Imagen */
        @media screen and (max-width: 768px) {
            .top-element {
                width: 90%;
                max-width: 90%;
            }
        }

        /* Imagen */
        @media screen and (max-width: 525px) {
            .top-element {
                width: 100%;
                max-width: 100%;
            }
        }
    </style>

    <script>
        var idTextPass = "<%=TextBoxPassword.ClientID%>";
        var idTextName = "<%=TextBoxUserName.ClientID%>";

        function pageLoad() {
           

            $(document).ready(function () {

                setListenerValidation();

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

                $("#" + idTextPass + ", #" + idTextName).on("keypress", function (e) {
                    var keycode = (e.keyCode ? e.keyCode : e.which);
                    if (keycode == '13') {
                        if ($(e.target).prop("type") === "text") {
                            e.preventDefault();
                            e.stopPropagation();
                            return aceptarButton();
                        }
                    }
                });
            });
        }       

        $(document).ready(function () {

            setListenerValidation();

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

            $("#"+idTextPass +", #" +idTextName).on("keypress", function (e) {
                var keycode = (e.keyCode ? e.keyCode : e.which);
                if (keycode == '13') {
                    if ($(e.target).prop("type") === "text") {
                        e.preventDefault();
                        e.stopPropagation();
                        return aceptarButton();
                    }
                }
            });

        });

        function validarLogin(grupoValidacion) {            
            var validationResult = true;            
            if (typeof (Page_ClientValidate) == 'function') {
                validationResult = Page_ClientValidate(grupoValidacion);
            }            
            if (validationResult === true) {
                return true;
            }
            return false;
        }

        function aceptarButton() {
            var result = validarPanel("#FormLogin");            
            if (result.IsValid) {
                return true;
            }
            return false;
        }

    </script>

</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <%-- CONTENEDOR --%>
    <div id="FormLogin" class="top-element">
        <%-- FORMULARIO A VALIDAR --%>
        <div class="container center formContainer form-Validate">
        <h2 class="title-page"><%: Title %></h2>        
        <section>
            <h4>Utilizar una cuenta local para ingresar.</h4>
            <hr />
            <!-- En Rows para evitar overlays -->
            <div class="form-group row">
                <asp:Label runat="server" AssociatedControlID="TextBoxUserName" CssClass="col-md-2  control-label">Usuario:</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="TextBoxUserName" CssClass="form-control img-responsive control-Validate" data-rule-validation="TextBoxUserName::1::NA:: :: ::Escriba un nombre de usuario" />
                    <!-- placeholder="Usuario" -->
                    <!--<small id="usuarioHelp" class="form-text text-muted">* Es obligatorio</small>-->
                    <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="TextBoxUserName" CssClass="text-danger form-text text-muted" ErrorMessage="* El nombre de usuario es obligatorio" ValidationGroup="LoginForm"  EnableClientScript="true"   />--%>
                </div>
            </div>

            <div class="form-group row">
                <asp:Label runat="server" AssociatedControlID="TextBoxPassword" CssClass="col-md-2 control-label">Contraseña:</asp:Label>
               
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="TextBoxPassword" TextMode="Password" CssClass="form-control img-responsive control-Validate" data-rule-validation="TextBoxPassword::1::NA:: :: ::Escriba una contraseña"/>
                    <!--<small id="passwordHelp" class="form-text text-muted">* La contraseña es obligatoria</small>-->
                    <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="TextBoxPassword" CssClass="text-danger form-text text-muted" ErrorMessage="* La contraseña es obligatoria" ValidationGroup="LoginForm" EnableClientScript="true"   />--%>
                </div>
            </div>


                         
            <div class="form-group row">
                <div class="col-md-12 text-center">
                    <%--  CausesValidation="true" ValidationGroup="LoginForm"  OnClientClick="return validarLogin('LoginForm');" --%>
                    <asp:Button ID="ButtonLogin" runat="server" OnClick="ButtonLogin_Click"  Text="Aceptar" CssClass="btn btn-primary" OnClientClick="javascript: return aceptarButton(this);"/>
                </div>
            </div>
            
            <div id="messageValidationEditor" class="group-validation">
            </div>


            <p>
                <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled" Visible="false">Registrar</asp:HyperLink>
                <%--si no cuenta aún con una cuenta local.--%>
            </p>
        </section>
    </div>

    </div>

</asp:Content>

