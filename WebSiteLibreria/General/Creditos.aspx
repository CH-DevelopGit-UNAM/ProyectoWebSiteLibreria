<%@ Page Title="Creditos" Language="C#" MasterPageFile="~/ResponsiveSite.master" AutoEventWireup="true" CodeFile="Creditos.aspx.cs" Inherits="General_Creditos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="Server">
    <style>
        .body-footer-content {            
            /*padding: 20px;*/            
        }

        .body-footer-subtitle {
            font-family: inherit !important;            
            padding: 0px;
            padding-top:20px;
            text-align:center !important;
        }

            .body-footer-subtitle p::first-letter, .body-footer-subtitle span::first-letter {
                font-size: 100% !important;
            }

            .body-footer-subtitle p, .body-footer-subtitle span {
                font-family: inherit !important;
                margin: -5px !important;
                padding:0px !important;
            }

                .body-footer-subtitle-p {  
                    font-family: inherit !important;                  
                    margin: -5px !important;
                    padding: 0px !important;
                }
                .body-footer-subtitle-p p, .body-footer-subtitle-p span{                    
                    margin:-5px !important;                    
                    padding: 0px !important;
                }
               
                .body-footer-subtitle-p p::first-letter, .body-footer-subtitle-p span::first-letter{
                    font-size: 100% !important;
                }
                   
                .body-footer-content-p p, .body-footer-content-p span{                    
                    padding:0px !important;    
                    margin:-8px !important;                                      
                }

                .body-footer-content-p > ul p, .body-footer-content-p > ul span{                    
                    padding:0px !important;  
                    margin:-8px !important;                    
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container">
        <div class="body-footer-subtitle" >
            <p style="color:black !important;">CRÉDITOS A COLABORADORES</p>
            <p style="color:black !important;">SITIO WEB SCRIPTORVM</p>
        </div>
        <div class="body-footer-content">
                        
            <div class="body-footer-content-p title-content">
                <p><b>COORDINACIÓN GENERAL</b></p>
                <p><i>Dra. Aurelia Vargas Valencia</i></p>
                <p>Directora</p>
                <p>Bibliotheca Scriptorum Graecorum et Romanorum Mexicana</p>                
            </div>
            <br />

            <div class="body-footer-subtitle-p subtitle-small">
                <p><b>DISEÑO WEB DEL SITIO SCRIPTORVM</b></p>
            </div>
            <br />
            <div class="body-footer-content-p">
                <p><i>Lic. Rubén Dimitrio Cervantes Cruz</i></p>
                <p>Secretario Técnico de Cómputo y Sistemas</p>
                <p>Coordinación de Humanidades</p>
                <p>Líder técnico del proyecto del sitio web Scriptorum</p>
            </div>
            <br />
            <div class="body-footer-content-p">
                <p><i>Lic. Ricardo Rodríguez Avendaño</i></p>
                <p>Responsable de la arquitectura y el diseño del sistema del sitio</p>
            </div>
            <br />
            <div class="body-footer-content-p">
                <p><i>Ing. José Antonio Cruz Jiménez</i></p>
                <p>Programación del sitio</p>
            </div>
            <br />
            

            <div class="body-footer-content-p title-content">
                <p><b>DISEÑO GRÁFICO DEL SITIO WEB</b></p>
                <p><i>Lic. Mercedes Flores Reyna</i></p>
                <p>Técnica Académica del Departamento de Cómputo</p>
                <p>Instituto de Investigaciones Filológicas</p>                
            </div>
            <br />

           <div class="body-footer-subtitle-p subtitle-small">
                <p><b>DIGITALIZACIÓN Y EDICIÓN DEL CATÁLOGO DE LA COLECCIÓN</b></p>
            </div>
            <br />
            <div class="body-footer-content-p title-content">
                <p><b>Coordinador del catálogo y su digitalización</b></p>
                <p><i>Aarón Cervantes Soria</i></p>
                <p>Técnico Académico</p>
                <p>Instituto de Investigaciones Filológicas</p>
            </div>
            <br />
            <div class="body-footer-content-p title-content">
                <p><b>Asesora en la catalogación del acervo</b></p>
                <p><i>Mtra. Elvia Carreño Velázquez</i></p>
                <p>Técnica Académica</p>
                <p>Centro de Estudios Clásicos, IIFL.</p>
            </div>
            <br />
            <div class="body-footer-content-p title-content">
                <p><b>Asesor en la digitalización y edición del acervo</b></p>
                <p><i>Israel García Avilés</i></p>
                <p>Licenciado en Filosofía, FFyL-UNAM</p>                
            </div>
            <br />
            <div class="body-footer-content-p title-content">
                <p><b>Apoyo logístico para la digitalización del acervo</b></p>
                <ul style="list-style:circle;" >
                    <li><p><i>Erick Vargas Cruz</i></p></li>
                    <li><p><i>Yasmín Pérez Juárez</i></p></li>
                    <li><p><i>Karina Camargo Pineda</i></p></li>                    
                </ul>
            </div>

            <div class="body-footer-content-p title-content">
                <p><b>Alumnos de Servicio Social que colaboraron en la digitalización y edición del acervo</b></p>
                <ul style="list-style:circle;" >                    
                    <li><p><i>Erick Vargas Cruz</i>, Licenciatura en Derecho, DF_UNAM.</p></li>
                    <li><p><i>Estephani Monserrat Rosales Llanos</i>, Licenciatura en Letras Clásicas, FFyL-UNAM.</p></li>
                    <li><p><i>Erick Palmero Palacios</i>, Licenciatura en Historia, FFyL-UNAM.</p></li>
                    <li><p><i>Iñaki Imanol Olalde Vergara</i>, Licenciatura en Letras Clásicas, FFyL-UNAM.</p></li>
                    <li><p><i>Jorge Isaac García Nava</i>, Licenciatura en Historia, Escuela Nacional de Antropología e Historia (INAH).</p></li>
                    <li><p><i>Raúl Manzano Tapia</i>, Licenciatura en Derecho, FD-UNAM.</p></li>
                </ul>
            </div>
            <br />
            <div class="body-footer-content-p title-content">
                <p><b>Apoyo secretarial en la digitalización del acervo</b></p>
                <ul style="list-style:circle;" >
                    <li><p><i>Karina Camargo Pineda</i></p></li>
                </ul>
            </div>            
            <br />
        </div>
       
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SubMainContent" runat="Server">
</asp:Content>