<%@ Page Title="Contacto" Language="C#" MasterPageFile="~/ResponsiveSite.master" AutoEventWireup="true" CodeFile="Contacto.aspx.cs" Inherits="SitiosInteres_Contacto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="Server">
    <style>
        .image-contanto {
            height:350px;
            display:inline-block;
            margin-left:-150px;
        }
        .titulo-contacto {
            display:inline-block;
        }
            .titulo-contacto h1 {
                color:#224F86;
            }

            .titulo-contacto h3 {
                padding-bottom: 0px;word-break:break-word; width:auto;
                display:inline-block;
                margin:0px;
                color:black;
            }
            .titulo-contacto h3 a {                
                color:black;
            }
        @media (max-width: 1191px) {
            .image-contanto {
                height:300px;
            }
        }

        @media (max-width: 1039px) {
            .image-contanto {
                height:250px;
            }
        }
        @media (max-width: 987px) {
            .image-contanto {
                height:250px;
                margin:auto;
                text-align:center;
                align-items:center;
            }

            .titulo-contacto h1 {
                font-size:28px;
            }

            .titulo-contacto h3 {
                font-size:20px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2 class="title-page">Contacto</h2>
    <%--<hr style="border-color: lightgray;" />--%>
    <div style="color: black; font-weight: bold; position: relative; padding-top:20px;margin:auto;text-align:center;" class="body-footer-content">
        <div style="height: 100%;">
            <asp:Image ID="ImageIcon" runat="server" ImageUrl="~/images/Hermes.png" CssClass="image-contanto" />
            <div class="titulo-contacto">                
                <h1>¿Te gustaría comunicarte con nosotros?</h1>                
                <h3><i>Escríbenos al correo</i></h3><br />
                <h3><a  href="mailto:scriptorum.contacto@humanidades.unam.mx"><b>scriptorum.contacto@humanidades.unam.mx</b></a></h3>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SubMainContent" runat="Server">
</asp:Content>

