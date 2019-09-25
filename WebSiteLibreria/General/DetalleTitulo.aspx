<%@ Page Title="Detalle" Language="C#" MasterPageFile="~/ResponsiveSite.master" AutoEventWireup="true" CodeFile="DetalleTitulo.aspx.cs" Inherits="General_DetalleTitulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" Runat="Server">
    <style type="text/css">
       
        a[id*='LinkUpdateNoticias']:hover {
            cursor:pointer;
        }

        a[id*='LinkUpdateNoticias'] {
            text-decoration:underline;
        }

        .customJumbotron {
            width:100%;
            height:auto;
        }

        .carouselJumbotron {
            width:77%;            
        }

        .carouselContenedor{
            background-color: black;
        }

        .carouselContenedorImagen {
            display:flex; height:100%; text-align:center; 
        }

        .carouselImagen {
            margin:auto;
            max-height:370px;
            -webkit-transition: all 0.2s ease-in-out;
            transition: all 0.2s ease-in-out;
        }

        .carouselContenedorTitulo a{
            color:yellow;
            text-decoration:none;
        }

        .carouselContenedorTitulo {
            margin-bottom: -20px; color: yellow;
        }
        .carouselContenedorTitulo p{
            font-size:small;
        }
        .carouselFondoTitulo {
            background-color: black; opacity: 0.4; height: 120px; position: absolute; z-index:0
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

        a[id*='LinkResponsables'] {
                display:none;
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

            .carouselJumbotron {
                 width:85%;
            }

            .carouselContenedor{
                width:100%;
            }

            .jumbotron.customJumbotron {
                 padding:5px; padding-top:7px;padding-bottom:5px;
            }

            .jumbotron.customJumbotron h4{
                 padding-left:15px;
                 padding-right:15px;
            }

            table[id*='GridResponsables'].gridView-default{
                display:none;
            }

            a[id*='LinkResponsables'] {
                display:inline;
            }
           

        }

        /*boostrap -> (min-width:992px)*/ 
        @media (max-width: 991px) {
            .text-right {
                text-align:left;
            }
        }

        /* Imagen : boostrap -> (min-width:768px) */
        @media (max-width: 767px) {    
            /*.carouselContenedorImagen {
                display:block; height:100%; text-align:center;
            }*/

            /*.carouselImagen {
                width:400px;        
                height:350px;                
            }*/
            
            .carouselJumbotron {
                 width:89%;
            }

            .modalDialog-Detalle {
                width: 85%;
                margin:auto;
            }

            .carouselContenedorImagen {
                display:flex; height:100%; text-align:center; 
            }

            .carouselImagen {
                margin:auto;
                height:270px;                
            }            

            .jumbotron.customJumbotron {
                 width:100%;
                 padding:5px; padding-top:7px;padding-bottom:5px;
            }

            .jumbotron.customJumbotron h4{
                 padding-left:15px;
                 padding-right:15px;
            }

            .carouselFondoTitulo {
                height: 140px;
            }
            
           
        }

        /* Imagen */
        @media (max-width: 526px) {    
            /*.carouselContenedorImagen {
                display:block; height:100%; text-align:center;
            }*/ 

            /*.carouselImagen {
                width:300px;        
                height:250px;
            }*/
            
            .carouselJumbotron {
                 width:100%;
            }

            .modalDialog-Detalle {
                width: 95%;      
                margin:auto;          
            }
            .carouselContenedorImagen {
                display:flex; height:100%; text-align:center; 
            }

            .carouselImagen {
                margin:auto;
                height:180px;                
            }                              

            .jumbotron.customJumbotron {
                padding:0px; padding-top:5px;padding-bottom:0px; height:auto;
            }

            .jumbotron.customJumbotron h4{
                 padding-left:15px;
                 padding-right:15px;    
            }

            .carouselFondoTitulo {
                height: 160px;
            }     
               
        }

        .campo-Numero-Pagina {
            display:inline-block;
            padding:2px;
            text-align:center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="modal-body" style="position: relative; padding: 20px; padding-bottom: 8px;">
        <div class="form-horizontal" style="position: relative;">
            <asp:HiddenField ID="HiddenFieldClave" runat="server" Value="" />
            <ul class="nav nav-tabs">
                <li class="active"><a data-toggle="tab" href="#home">Descripción del título</a></li>
            </ul>
            <div id="tabContentDetalle" class="tab-content" style="padding-top: 20px;">
                <%--  TAB CAMPOS "CONTENIDO" --%>

                <div id="home" class="tab-pane fade in active" style="display:inline-block;width:100%;">
                    <asp:HiddenField ID="HiddenId" runat="server" Value="-1" />
                    <div class="carouselContenedorImagen" style="overflow-x:auto;">
                        <asp:Image  ID="ImagenTitulo" runat="server" CssClass="carouselImagen" AlternateText="Titulo"/>
                    </div>                    
                    <div class="modal-footer" style="margin:0px;margin-top:5px;padding:0px;height:2px;"></div>
                    <div class="col-md-12 form-group" style="margin:0px;">                        
                        <div class="text-center" style="max-height: 170px; overflow: auto;">
                            <asp:Label ID="LabelTitulo" runat="server" CssClass="control-label" ></asp:Label>
                        </div>                        
                        <div id="bAutor" runat="server" class="text-center" style="max-height: 170px; overflow: auto;">
                            <b>Autor:</b>
                            <asp:Label ID="LabelAutor" runat="server" CssClass="control-label" ></asp:Label>
                        </div>
                         <div id="bEditor"  runat="server" class="text-center">
                             <b>Editor:</b>
                            <asp:Label ID="LabelEditor" runat="server" CssClass="control-label" ></asp:Label>
                        </div>
                        <div id="bEdicion" runat="server" class="text-center">
                            <%--<b>Edición:</b>--%>
                            <asp:Label ID="LabelEdicion" runat="server" CssClass="control-label"></asp:Label>
                        </div> 

                       <%-- <div class="text-center" style="max-height: 170px; overflow: auto;">
                            <asp:Label ID="LabelPaginas" runat="server" CssClass="control-label" ></asp:Label>
                        </div>--%>
                       
                        <div class="panel-group" id="accordion">                            
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapse1">Descripción</a>
                                    </h4>
                                </div>
                                <div id="collapse1" class="panel-collapse collapse">
                                    <div class="panel-body">
                                         <div id="bCiudad" runat="server" class="col-md-12 text-justify" style="max-height: 100px; overflow: auto;">
                                            <b>Ciudad:</b>
                                            <asp:Label ID="LabelCiudad" runat="server" CssClass="control-label"></asp:Label>
                                        </div>
                                        <div id="bCualidades" runat="server" class="col-md-12 text-justify"  style="max-height: 170px; overflow: auto;">
                                            <b>Cualidades:</b>
                                            <asp:Label ID="LabelCualidades" runat="server" CssClass="control-label"></asp:Label>
                                        </div>
                                        <div id="bUFFYL" runat="server" class="col-md-12 text-justify" style="max-height: 100px; overflow: auto;">
                                            <b>UFFYL:</b>
                                            <asp:Label ID="Label_UffYL" runat="server" CssClass="control-label"></asp:Label>
                                        </div>                                        
                                        <div id="bUIIFL" runat="server" class="col-md-12 text-justify" style="max-height: 100px; overflow: auto;">
                                            <b>UIIFL:</b>
                                            <asp:Label ID="Label_UiiFL" runat="server" CssClass="control-label"></asp:Label>
                                        </div>
                                        <div id="bFlagLatin" runat="server" class="col-md-12 text-justify" style="max-height: 100px; overflow: auto;">                                            
                                            <asp:RadioButton ID="RadioIsLatin" runat="server" Text="Latin" GroupName="TipoTexto" Enabled="false" />
                                            <b style="padding-left:10px"></b>
                                            <asp:RadioButton ID="RadioIsGriego" runat="server" Text="Griego" GroupName="TipoTexto" Enabled ="false" />
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
                                        <div id="bTema" runat="server" class="col-md-12" >
                                            <b>Tema:</b>
                                            <div class="text-justify"  style="max-height: 170px; overflow: auto;">
                                                <asp:Label ID="LabelTema" runat="server" CssClass="control-label"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-12"><br /></div>
                                        <div id="bContenido" runat="server" class="col-md-12">
                                            <b>Contenido:</b>
                                            <div class="text-justify" style="max-height: 200px; overflow: auto;">
                                                <asp:Label ID="LabelContenido" runat="server" CssClass="control-label"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-12"><br /></div>
                                        <div id="bColofon" runat="server" class="col-md-12">
                                            <b>Colofón:</b>
                                            <div class="text-justify" style="max-height: 200px; overflow: auto;">
                                                <asp:Label ID="LabelColofon" runat="server" CssClass="control-label"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-12"><br /></div>
                                        <div id="bObservaciones" runat="server" class="col-md-12">
                                            <b>Observaciones:</b>
                                            <div class="text-justify" style="max-height: 200px; overflow: auto;">
                                               <asp:Label ID="LabelObservaciones" runat="server" CssClass="control-label"></asp:Label>
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
                                            <asp:Label ID="LabelIsbn" runat="server"></asp:Label>
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
                                            <asp:Label ID="LabelResponsables" runat="server"></asp:Label>
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
                                        <div class="col-md-12" style="max-height: 60px;">
                                            <br />
                                            <asp:Image ID="ImagenPdf" runat="server" AlternateText="Pdf" ImageUrl="~/images/Icon-document-pdf.png" Width="45" ToolTip="Versión PDF" Visible="false"  class="image-src-libro" />
                                            <asp:Image ID="ImageVirtual" runat="server" AlternateText="Virtual" ImageUrl="~/images/Icon-document-virtual.png" Width="40" ToolTip="Versión Virtual" Visible="false"  class="image-src-libro" />
                                            <asp:Image ID="ImageOnline" runat="server" AlternateText="Online" ImageUrl="~/images/Icon-document-web.png" Width="45" ToolTip="Versión Online" Visible="false"  class="image-src-libro" />
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
    <div class="modal-footer" style="margin-top: 0px;">        
        <asp:Button ID="ButtonReturn" runat="server" CssClass="btn btn-default" OnClick="ButtonReturn_Click" Text="Regresar" TabIndex="20"/>
    </div>
</asp:Content>

