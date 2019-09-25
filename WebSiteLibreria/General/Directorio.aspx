<%@ Page Title="Directorio" Language="C#" MasterPageFile="~/ResponsiveSite.master" AutoEventWireup="true" CodeFile="Directorio.aspx.cs" Inherits="General_Directorio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="Server">
    <style>
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2 class="title-page">Directorio</h2>
    <hr style="border-color: lightgray;" />
    <div style="color: black; padding: 10px; font-weight: bold; position: relative; padding-top:0px;">
        <div style="height: 100%;">
            <h3 style="padding-bottom: 0px;">
                <asp:Image ID="ImageIcon" runat="server" ImageUrl="~/images/Tools.png" Width="75" Height="75" />&nbsp;&nbsp;&nbsp;EN DESARROLLO
            </h3>
            <hr style="border-color: lightgray;" />
            <div style="padding-left: 30px;">
                <h4>El recurso solicitado está en desarrollo.</h4>
                <br />
            </div>
            <div style="width: 100%; text-align: center;">
                <asp:Image ID="ImgFondo" runat="server" ImageUrl="~/images/BajoConstruccion.png" Width="155" Height="200" />                
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SubMainContent" runat="Server">
</asp:Content>

