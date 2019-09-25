<%@ Page Title="Autores" Language="C#" MasterPageFile="~/ResponsiveSite.master" AutoEventWireup="true" CodeFile="Autores.aspx.cs" Inherits="Catalogos_Autores" Theme="SkinBase" %>
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

        var idLabelClaveAutor = '#<%=LabelClaveAutor.ClientID%>';
        var idTextBoxLatin = '#<%=TextBoxLatin.ClientID%>';
        var idTextBoxGriego = '#<%=TextBoxGriego.ClientID%>';
        var idTextBoxEspanol = '#<%=TextBoxEspanol.ClientID%>';
        var idTxtPaginador = '<%=TextBoxPaginaActual.ClientID%>';
        var idUpdatePanelBusqueda = '<%=UpdatePanelBusqueda.ClientID%>';

        function pageLoad() {

            // Solo los elementos en un UpdatePanel

            $(document).ready(function () {

                configurarGridView(idGridView, function (items, element) {
                    var clave = $(items[1]).find("span").text();
                    var latin = $(items[2]).find("span").text();
                    var griego = $(items[3]).find("span").text();
                    var espanol = $(items[4]).find("span").text();

                    setDataModalDetalle(clave, latin, griego, espanol);
                });

            });

        }

        $(document).ready(function () {
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

            setListenerValidation();

            //// Modal-Until-Show...
            $("#DialogoDetalleAutor").on('shown.bs.modal', function (event) {
                var result = validarPanel($("#DialogoDetalleAutor"));
            });

            // Modal-Show
            $("#DialogoDetalleAutor").on('show.bs.modal', function (event) {

                $('#DialogoDetalleAutor').on('hide.bs.modal.prevent', function (event) {
                    event.preventDefault();
                    event.stopImmediatePropagation();
                    return false;
                });

                $(this).find("input[type='button']").each(function (index, value) {
                    var dataDismiss = $(value).attr('data-dismiss');
                    if (dataDismiss != null && dataDismiss != undefined) {
                        $(value).click(function (event) {
                            $(idHiddenOperacion).val(-1);
                            cerrarPanel("#DialogoDetalleAutor");
                            $("#DialogoDetalleAutor").off('hide.bs.modal.prevent');
                            $("#DialogoDetalleAutor").modal('hide');
                        });
                    }
                });

                $(this).find("button").each(function (index, value) {
                    var dataDismiss = $(value).attr('data-dismiss');
                    if (dataDismiss != null && dataDismiss != undefined) {
                        $(value).click(function (event) {
                            $(idHiddenOperacion).val(-1);
                            cerrarPanel("#DialogoDetalleAutor");
                            $("#DialogoDetalleAutor").off('hide.bs.modal.prevent');
                            $("#DialogoDetalleAutor").modal('hide');
                        });
                    }
                });
            });

        });
        

        function showDialogAdd(element) {
            $(idHiddenOperacion).val(0);
            setDataModalDetalle("-- NUEVO --","","","");            
            $("#DialogoDetalleAutor").modal('show');
            return false;
        }

        function showDialogDetalle(element) {            
            $(idHiddenOperacion).val(1);
            $("#DialogoDetalleAutor").modal('show');
            return false;
        }

        function aceptarButton() {                        
            var result = validarPanel("#DialogoDetalleAutor");
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


        function setDataModalDetalle(clave, latin, griego, espanol) {            
            $(idHiddenClave).val(clave);
            $(idLabelClaveAutor).text(clave);
            $(idTextBoxLatin).val(latin);
            $(idTextBoxGriego).val(griego);
            $(idTextBoxEspanol).val(espanol);
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2 class="title-page">Catálogo Autores</h2>
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
                      <li><asp:LinkButton id="LinkTodos" runat="server" Text="Todos" CssClass="dropdown-item" OnClick="LinkTodos_Click"/></li>
                    </ul>
      
                </div>
            </div>
            <div  class="input-group" style="padding:10px;">
                <asp:Button ID="ButtonAdd"  runat="server" CssClass="btn btn-primary" Text="Agregar" OnClientClick="javascript: return showDialogAdd(this);"/>
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
                                OnPageIndexChanging="GridViewResultado_PageIndexChanging" OnRowDataBound="GridViewResultado_RowDataBound" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Ciudad" ItemStyle-CssClass="gridViewColumnHidden column-hidden-responsive-sm gridHeader" HeaderStyle-CssClass="gridViewColumnHidden column-hidden-responsive-sm" >
                                        <ItemTemplate>
                                            <span> Autor </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Clave">
                                        <ItemTemplate>
                                             <b class="element-hidden-responsive-sm">Clave:</b>
                                            <asp:Label ID="LabelNombre" runat="server" Text='<%#Eval("IdAutor")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nombre Latín">
                                        <ItemTemplate>
                                            <b class="element-hidden-responsive-sm">Nombre Latín:</b>
                                            <asp:Label ID="LabelLatin" runat="server" Text='<%#Eval("NombreLatin")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nombre Griego">
                                        <ItemTemplate>
                                            <b class="element-hidden-responsive-sm">Nombre Griego:</b>
                                            <asp:Label ID="LabelGriego" runat="server" Text='<%#Eval("NombreGriego")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nombre Español">
                                        <ItemTemplate>
                                            <b class="element-hidden-responsive-sm">Nombre Español:</b>
                                            <asp:Label ID="LabelEspanol" runat="server" Text='<%#Eval("NombreEspanol")%>'></asp:Label>
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
                                        <%--AutoPostBack="true" OnTextChanged="TextBoxPaginaActual_TextChanged"--%> 
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
                            <%--<asp:AsyncPostBackTrigger ControlID="ButtonBuscar" EventName="Click" />--%>
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

    <%-- POPUP Detalle Autor--%>
    <div id="DialogoDetalleAutor" class="modal fade" role="dialog" >
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
                                <asp:Label ID="LabelClaveAutor" runat="server" CssClass="col-md-2 control-label-left control-Validate" Width="200" data-rule-validation="LabelClaveAutor::1::NA:: :: ::La clave no puede estar vacía"></asp:Label>                                
                            </div>
                        </div>

                         <div class="form-group" >
                            <asp:Label runat="server" CssClass="col-md-2 control-label">Nombre Español</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="TextBoxEspanol" CssClass="form-control img-responsive control-Validate" Height="50" TextMode="MultiLine" data-rule-validation="TextBoxEspanol::1::NA:: :: ::El nombre en español no puede estar vacío"/>                             
                            </div>
                        </div>  

                        <div class="form-group" >
                            <asp:Label runat="server" CssClass="col-md-2 control-label">Nombre Latín</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="TextBoxLatin" CssClass="form-control img-responsive" Height="50" TextMode="MultiLine" />
                             
                            </div>
                        </div>   
                        
                        <div class="form-group" >
                            <asp:Label runat="server" CssClass="col-md-2 control-label">Nombre Griego</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="TextBoxGriego" CssClass="form-control img-responsive" Height="50" TextMode="MultiLine" />
                             
                            </div>
                        </div>                                                 
                                                                     
                    </div>
                    <div id="messageValidationCiudad" class="group-validation">
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

