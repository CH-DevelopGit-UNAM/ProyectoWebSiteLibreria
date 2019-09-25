<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/ResponsiveSite.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Theme="SkinBase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="UserControl" TagName="Carousel" Src="~/UserControls/NovedadesCarousel-v2.0.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="HeaderPlaceHolder">        
    
    <style type="text/css" >
        #body {
            padding:0px;
            margin-bottom:0px;
            
        }
         #body-footer {
            padding:0px;            
            height: auto;
            /*min-height:100%;*/
            width:100%;            
            position:relative;            
        }
        .container-carousel {
            padding:10px; padding-top:10px;padding-bottom:110px;position:relative;
        }       
    
        @media (max-width: 320px) {
           .container-carousel {
                padding-bottom:10px;
           }           
        } 
    </style>
        
    <%: Scripts.Render("~/bundles/Pages-CarouselMultiple") %>   
    <script type="text/javascript">        
        function pageLoad() {
            initCarousel();
        }

        initCarousel();

        function initCarousel() {

            $(document).ready(function () {

                $(".carousel").flexCarousel({ Size: 1170, Porcentaje: 0.19, Items: [], Interval: 15000 });

                var clickEvent = false;
                $('#novedades-carousel').on('click', 'a.carousel-control', function () {
                    clickEvent = true;
                    if ($(this).hasClass("right")) {

                    } else if ($(this).hasClass("left")) {

                    }
                    //$('.nav li').removeClass('active');
                    //$(this).parent().addClass('active');
                }).on('slid.bs.carousel', function (e) {
                    if (!clickEvent) {
                        var count = $('.nav').children().length - 1;
                        var current = $('.nav li.active');
                        current.removeClass('active').next().addClass('active');
                        var id = parseInt(current.data('slide-to'));
                        if (count == id) {
                            $('.nav li').first().addClass('active');
                        }
                        //console.log("SLIDE AUTOMATICO");
                    } else {
                        //console.log("SLIDE CLICK");
                    }
                    clickEvent = false;
                });
            });
        }        

        

    </script>
        
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width:100%;height:auto;text-align:center;">
        <a href="javascript:void(0);">
            <img style="width:100%;" src="<%=ResolveClientUrl("~/")%>images/Banner-actividades.jpg" />
        </a>        
    </div>
    <div class="title-section">
        <div>
            <span>NOVEDADES EDITORIALES</span>
        </div>
    </div>
    <%-- Más espacio entre el carousel y el Footer de descripción (padding-bottom:110px), cuando existe el body y el footer al mismo tiempo --%>
    <div class="container-carousel">
        <UserControl:Carousel ID="Carousel" runat="server" />
    </div>
</asp:Content>

<asp:Content ID="BodyFooterContent" ContentPlaceHolderID="SubMainContent" runat="server">
     <div class="body-footer">
        <div class="container body-footer-background" >
                <div class="container">
                    <div class="body-footer-title">
                        <p>BIBLIOTHECA&nbsp;</p><p>SCRIPTORVM</p><br />
                        <p>GRAECORVM ET </p><br />
                        <p>ROMANORVM&nbsp;</p><p>MEXICANA</p>
                    </div>
                    <div class="body-footer-content" >
                        <p>
                            <%--El Diccionario de escritores mexicanos en línea abarca, en el presente periodo 2016, los datos actualizados hasta 2012 de los autores comprendidos en los primeros
                            cinco tomos del Diccionario de escritores mexicanos del sigo XX, además, de otros autores cuyos apellidos paternos comienzan con las letras de la A
                            a la M. Debe aclararse que este sitio está en proceso, por lo que continuamente se harán las actualizaciones a los registros de los autores, correcciones pertinentes
                            y la inclusión de nuevos escritores (nacidos antes de 1970). Es importante  señalar, además, que el Diccionario coordinado por la Mtra. Ocampo, desde su... --%>
                            LA BIBLIOTHECA SCRIPTORVM GRAECORVM ET ROMANORVM MEXICANA, la primera colección bilingüe en el mundo de habla hispana, se inició en 1944. 
                            Nutrida desde entonces por especialistas en filología clásica y dirigida tanto al público universitario como a todos los que deseen introducirse en el conocimiento de los clásicos griegos y latinos, 
                            tiene como objetivo publicar las obras humanísticas y científicas que, desde su creación hasta nuestros días, han sido motivo de admiración, de recreo o de análisis.
                        </p>
                    </div>
                    <div class="border-legend">
                        <hr />
                    </div>
                </div>
            </div>
    </div>    
</asp:Content>