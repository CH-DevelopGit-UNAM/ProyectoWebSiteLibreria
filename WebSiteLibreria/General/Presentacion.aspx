<%@ Page Title="Presentación" Language="C#" MasterPageFile="~/ResponsiveSite.master" AutoEventWireup="true" CodeFile="Presentacion.aspx.cs" Inherits="General_Presentacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" Runat="Server">
    <style>
        
        #body {            
            height:0px;
            min-height: 0px; 
            padding:5px;
        }
         #body-footer {
            padding:0px;            
            height: auto;
            min-height:100%;
            width:100%;            
            position:relative;            
        }
    </style>
</asp:Content>
<asp:Content ID="BodyFooterContent" ContentPlaceHolderID="SubMainContent" runat="server">
    <div class="body-footer">
        <div class="container body-footer-background">
            <div class="container">
                <div class="body-footer-title">
                    <p>BIBLIOTHECA&nbsp;</p><p>SCRIPTORVM</p><br />
                    <p>GRAECORVM ET </p><br />
                    <p>ROMANORVM&nbsp;</p><p>MEXICANA</p>
                </div>
                <br /><br />
                <div class="body-footer-subtitle">
                    <p>Presentación</p>
                </div>
                <br /><br />
                <div class="body-footer-content">
                    <p>
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