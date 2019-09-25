<%@ Page Title="Registrar" Language="C#" MasterPageFile="~/ProtectedSite.Master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Account_Register" Theme="ThemeBase" %>

<asp:Content runat="server" ContentPlaceHolderID="HeaderPlaceHolder">
    <style type="text/css">
              
        .formContainer {
           width:70%;max-width:70%;
        }


        @media screen and (max-width: 1200px) {                
            .formContainer {
                width:80%;max-width:80%;
            }
        }


        /* Imagen */
        @media screen and (max-width: 768px) {    
             .formContainer {
                width:90%;max-width:90%;
            }
        }

        /* Imagen */
        @media screen and (max-width: 525px) {    
            .formContainer {
                width:100%;max-width:100%;
            }
        }
    </style>

     <script>         
        function pageLoad() {
           
            $(document).ready(function () {
                

                
               
            });

        }       

        function validarRegistro(grupoValidacion) {            
            var validationResult = true;
            if (typeof (Page_ClientValidate) == 'function') {
                validationResult = Page_ClientValidate(grupoValidacion);
            }            
            if (validationResult === true) {
                return true;
            }
            return false;
        }

        function cancelOption() {
            // Al ser una opcion de cancelar, se omite la validacion.
            Page_ClientValidate("");
            return true;
        }
    </script>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="container center formContainer">
        <h2><%: Title %>.</h2>
        <p class="text-danger">
            <asp:Literal runat="server" ID="ErrorMessage" />
        </p>
        <section>
            <h4>Crear una nueva cuenta.</h4>
            <hr />
            <asp:ValidationSummary runat="server" CssClass="text-danger" />
            <div class="form-group row">
                <asp:Label runat="server" AssociatedControlID="TextBoxUserName" CssClass="col-md-2 control-label">Usuario: </asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="TextBoxUserName" CssClass="form-control img-responsive" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBoxUserName"
                        CssClass="text-danger" ErrorMessage="El nombre de usuario es obligatorio." ValidationGroup="RegistroForm"  EnableClientScript="true"   />
                </div>
            </div>
            <div class="form-group row">
                <asp:Label runat="server" AssociatedControlID="TextBoxPassword" CssClass="col-md-2 control-label">Contraseña:</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="TextBoxPassword" TextMode="Password " CssClass="form-control img-responsive" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="TextBoxPassword"
                        CssClass="text-danger" ErrorMessage="La contraseña es obligatoria." ValidationGroup="RegistroForm"  EnableClientScript="true"   />
                </div>
            </div>
            <div class="form-group row">
                <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Confirmar contraseña:</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control img-responsive" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                        CssClass="text-danger" Display="Dynamic" ErrorMessage="El campo confirmación de contraseña es obligatorio." ValidationGroup="RegistroForm"  EnableClientScript="true"   />
                    <asp:CompareValidator runat="server" ControlToCompare="TextBoxPassword" ControlToValidate="ConfirmPassword"
                        CssClass="text-danger" Display="Dynamic" ErrorMessage="La contraseñas no coinciden." ValidationGroup="RegistroForm"  EnableClientScript="true"   />
                </div>
            </div>
            <div class="form-group row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="ButtonAceptar" runat="server" OnClick="CreateUser_Click" Text="Aceptar" CssClass="btn btn-primary" CausesValidation="true"   ValidationGroup="RegistroForm"  OnClientClick="return validarRegistro('RegistroForm');"  />
                    <asp:Button ID="ButtonCancelar" runat="server" OnClick="Cancelar_Click" Text="Cancelar" CssClass="btn btn-default" OnClientClick="javascript:return cancelOption();" />
                </div>
            </div>
        </section>
    </div>    
</asp:Content>

