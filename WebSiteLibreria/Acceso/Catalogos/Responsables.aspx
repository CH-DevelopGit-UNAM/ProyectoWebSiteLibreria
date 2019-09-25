<%@ Page Title="Responsables" Language="C#" MasterPageFile="~/ResponsiveSite.master" AutoEventWireup="true" CodeFile="Responsables.aspx.cs" Inherits="Catalogos_Responsables" Theme="SkinBase"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" Runat="Server">
    <style type="text/css">
        .campo-Numero-Pagina {
            display:inline-block;
            padding:2px;
            text-align:center;
        }
    </style>

    <script type="text/javascript">
        var idGridView = '#<%=GridViewResultado.ClientID%>';
        var idButtonAceptar = '#<%=ButtonAceptar.ClientID%>';
        var idHiddenOperacion = '#<%=HiddenTipoOperacion.ClientID%>';
        var idHiddenClave = '#<%=HiddenFieldClave.ClientID%>';
        var idHiddenOperacion = '#<%=HiddenTipoOperacion.ClientID%>';

        var idLabelClaveResponsable = '#<%=LabelClaveResponsable.ClientID%>';
        var idTextNombre = '#<%=TextBoxNombre.ClientID%>';
        var idTextApPaterno = '#<%=TextBoxApPaterno.ClientID%>';
        var idTextApMaterno = '#<%=TextBoxApMaterno.ClientID%>';
        var idTextRfc = '#<%=TextBoxRfc.ClientID%>';
        var idTextDescripcion = '#<%=TextBoxDescripcion.ClientID%>';
        
        function pageLoad() {

            // Solo los elementos en un UpdatePanel
            $(document).ready(function () {
                configurarGridView(idGridView, function (items, element) {
                    var nombre = $(items[1]).text();
                    var apPaterno = $(items[2]).text();
                    var apMaterno = $(items[3]).text();
                    var clave = $(items[4]).find("span").text();                    
                    var rfc = $(items[6]).find("span").text();
                    var descripcion = $(items[7]).find("span").text();                
                    setDataModalDetalle(clave, rfc, nombre, apPaterno, apMaterno, descripcion);
                });

            });

        }

        $(document).ready(function () {            

            setListenerValidation();
            
            //// Modal-Until-Show...
            $("#DialogoDetalleResponsable").on('shown.bs.modal', function (event) {
                var result = validarPanel($("#DialogoDetalleResponsable"));
            });

            // Modal-Show
            $("#DialogoDetalleResponsable").on('show.bs.modal', function (event) {

                $('#DialogoDetalleResponsable').on('hide.bs.modal.prevent', function (event) {
                    event.preventDefault();
                    event.stopImmediatePropagation();
                    return false;
                });

                $(this).find("input[type='button']").each(function (index, value) {
                    var dataDismiss = $(value).attr('data-dismiss');
                    if (dataDismiss != null && dataDismiss != undefined) {
                        $(value).click(function (event) {
                            $(idHiddenOperacion).val(-1);
                            cerrarPanel("#DialogoDetalleResponsable");
                            $("#DialogoDetalleResponsable").off('hide.bs.modal.prevent');
                            $("#DialogoDetalleResponsable").modal('hide');
                        });
                    }
                });

                $(this).find("button").each(function (index, value) {
                    var dataDismiss = $(value).attr('data-dismiss');
                    if (dataDismiss != null && dataDismiss != undefined) {
                        $(value).click(function (event) {
                            $(idHiddenOperacion).val(-1);
                            cerrarPanel("#DialogoDetalleResponsable");
                            $("#DialogoDetalleResponsable").off('hide.bs.modal.prevent');
                            $("#DialogoDetalleResponsable").modal('hide');
                        });
                    }
                });
            });

        });
        

        function showDialogAdd(element) {
            $(idHiddenOperacion).val(0);            
            setDataModalDetalle("-- NUEVO --", "", "", "", "", "");
            $("#DialogoDetalleResponsable").modal('show');
            return false;
        }

        function showDialogDetalle(element) {            
            $(idHiddenOperacion).val(1);
            $("#DialogoDetalleResponsable").modal('show');
            return false;
        }

        function aceptarButton() {                        
            var result = validarPanel("#DialogoDetalleResponsable");
            if (result.IsValid) {
                return confirm("¿Desea guardar los cambios?");
            }
            return false;
        }



        function validarGrupo(grupoValidacion) {
            var validationResult = true;
            if (typeof (Page_ClientValidate) == 'function') {
                validationResult = Page_ClientValidate(grupoValidacion);
            }
            if (validationResult === true) {
                return true;
            }
            return false;
        }

        function requiredFielddValidator(source, args) {
            args.IsValid = (args.Value == "");
        }


        function setDataModalDetalle(clave, rfc, nombre, apPaterno, apMaterno, descripcion) {
            $(idHiddenClave).val(clave);
            $(idLabelClaveResponsable).text(clave);
            $(idTextRfc).val(rfc);
            $(idTextNombre).val(nombre);
            $(idTextApPaterno).val(apPaterno);
            $(idTextApMaterno).val(apMaterno);
            $(idTextDescripcion).val(descripcion);
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2 class="title-page">Catálogo Responsables</h2>
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
                       <li><asp:LinkButton id="LinkBuscarRfc" runat="server" Text="RFC" CssClass="dropdown-item" OnClick="LinkBuscarRfc_Click"/></li>
                       <li><asp:LinkButton id="LinkBuscarNombre" runat="server" Text="Nombre" CssClass="dropdown-item" OnClick="LinkBuscarNombre_Click"/></li>
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
                    <asp:UpdatePanel ID="UpdatePanelBusqueda" runat="server" UpdateMode="Conditional"  ChildrenAsTriggers="true" >
                        <ContentTemplate>
                            <asp:HiddenField ID="HiddenCampoBusqueda" runat="server" Value="" />
                            <asp:GridView ID="GridViewResultado" runat="server" SkinID="GridViewPrimary" PageSize="13" AutoGenerateColumns="false" AllowPaging="true" 
                                OnPageIndexChanging="GridViewResultado_PageIndexChanging" OnRowDataBound="GridViewResultado_RowDataBound" OnRowCommand="GridViewResultado_RowCommand"  >
                                <Columns>
                                    <asp:TemplateField HeaderText="Autor" ItemStyle-CssClass="gridViewColumnHidden column-hidden-responsive-sm gridHeader" HeaderStyle-CssClass="gridViewColumnHidden column-hidden-responsive-sm" >
                                        <ItemTemplate>
                                            <span> Autor </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Nombre" ItemStyle-CssClass="gridViewColumnHidden" HeaderStyle-CssClass="gridViewColumnHidden" DataField="Nombre" />
                                    <asp:BoundField HeaderText="ApPaterno" ItemStyle-CssClass="gridViewColumnHidden" HeaderStyle-CssClass="gridViewColumnHidden" DataField="ApellidoPaterno" />
                                    <asp:BoundField HeaderText="ApMaterno" ItemStyle-CssClass="gridViewColumnHidden" HeaderStyle-CssClass="gridViewColumnHidden" DataField="ApellidoMaterno" />

                                    <asp:TemplateField HeaderText="Clave">
                                        <ItemTemplate>
                                             <b class="element-hidden-responsive-sm">Clave:</b>
                                            <asp:Label ID="LabelClave" runat="server" Text='<%#Eval("IdResponsable")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nombre">
                                        <ItemTemplate>
                                            <b class="element-hidden-responsive-sm">Nombre:</b>
                                            <asp:Label ID="LabelNombre" runat="server" Text='<%# String.Format("{0} {1} {2}",Eval("ApellidoPaterno"), Eval("ApellidoMaterno"), Eval("Nombre"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="RFC">
                                        <ItemTemplate>
                                            <b class="element-hidden-responsive-sm">RFC:</b>
                                            <asp:Label ID="LabelRfc" runat="server" Text='<%#Eval("Rfc")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Descripción">
                                        <ItemTemplate>
                                            <b class="element-hidden-responsive-sm">Descripción:</b>
                                            <asp:Label ID="LabelDescripcion" runat="server" Text='<%#Eval("Descripcion")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField ItemStyle-CssClass="center text-center" >
                                        <ItemTemplate>
                                            <div style="display:inline-grid;">
                                                <a class="btn btn-default center text-center" onclick="javascript: return showDialogDetalle(this);">Editar</a>                                                                                                
                                            </div>                                           
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerTemplate>
                                    <div  id="FooterGridResultado" class="container-fluid footer-Paging" style="padding:0px;">
                                        <div class="row">
                                            <div class="col-md-7">
                                                <%--Registros 
                                                        <asp:Label ID="LabelRegistroInicial" runat="server"></asp:Label> 
                                                    al  <asp:Label ID="LabelRegistroFinal" runat="server"></asp:Label>     --%>
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
                                                <ajaxtoolkit:filteredtextboxextender id="FilteredTextBoxExtender3" runat="server" targetcontrolid="TextBoxPaginaActual" filtertype="Numbers" />
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
                       <%-- <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ButtonBuscar" EventName="Click" />
                        </Triggers>--%>
                    </asp:UpdatePanel>                   
                </div>
            </div>
        </div>
    </div>

    <%-- POPUP Detalle Editor--%>
    <div id="DialogoDetalleResponsable" class="modal fade" role="dialog" >
        <div class="modal-dialog primaryTheme" >
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
                    <div class="form-horizontal" style="position:relative;" >                        
                        <div class="form-group" style="position:relative;" >
                            <asp:HiddenField ID="HiddenFieldClave" runat="server" Value="" />
                            <asp:Label runat="server"  CssClass="col-md-2 control-label">Clave</asp:Label>
                            <div class="col-md-10" >                                
                                <asp:Label ID="LabelClaveResponsable" runat="server" CssClass="col-md-2 control-label-left control-Validate" Width="200" data-rule-validation="LabelClaveResponsable::1::NA:: :: ::La clave no puede estar vacía"></asp:Label>
                            </div>
                        </div>

                        <div class="form-group" >
                            <asp:Label runat="server" CssClass="col-md-2 control-label">Nombre:</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="TextBoxNombre" CssClass="form-control img-responsive control-Validate" data-rule-validation="TextBoxNombre::1::NA:: :: ::Escriba un nombre" MaxLength="50"/>
                            </div>
                        </div>                        
                        
                        <div class="form-group" >
                            <asp:Label runat="server" CssClass="col-md-2 control-label">Ap. Paterno:</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="TextBoxApPaterno" CssClass="form-control img-responsive" MaxLength="50"/>
                            </div>
                        </div>

                        <div class="form-group" >
                            <asp:Label runat="server" CssClass="col-md-2 control-label">Ap. Materno:</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="TextBoxApMaterno" CssClass="form-control img-responsive" MaxLength="50" />
                            </div>
                        </div>

                        <div class="form-group" >
                            <asp:Label runat="server" CssClass="col-md-2 control-label">Descripción:</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="TextBoxDescripcion" CssClass="form-control img-responsive" />
                            </div>
                        </div>
                        
                        <div class="form-group" >
                            <asp:Label runat="server" CssClass="col-md-2 control-label">RFC:</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="TextBoxRfc" CssClass="form-control img-responsive control-Validate" data-rule-validation="TextBoxRfc::0::funcion:: :: ::Mensaje Funcion::validarRfc"  MaxLength="10"/>
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
</asp:Content>