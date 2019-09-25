<%@ Page Title="Consejo Editorial" Language="C#" MasterPageFile="~/ResponsiveSite.master" AutoEventWireup="true" CodeFile="ConsejoEditorial.aspx.cs" Inherits="Directorio_ConsejoEditorial" Theme="SkinBase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="Server">
    <style>
        .body-footer-content {            
            /*padding: 20px;*/            
            text-align:center;
        }
        .body-footer-subtitle-p {
            text-align:center;
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

        .content-paragraph:nth-of-type(odd) {
            text-align:left !important;
        }
        .content-paragraph:nth-of-type(even) {
            text-align:right !important;
        }
		
		.element-hidden-responsive-md{
			clear:both !important;
		}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container">
        <div class="body-footer-subtitle subtitle-small" >
            <p>CONSEJO EDITORIAL</p>
            <br />
            <p>BIBLIOTHECA SCRIPTORVM GRAECORVM ET ROMANORVM MEXICANA</p>
        </div>        
        <div class="body-footer-content">
            <div class="body-footer-content-p">                
                <p>Dra. Aurelia Vargas Valencia</p>
                <p>Directora</p>
                <p>Bibliotheca Scriptorum Graecorum et Romanorum Mexicana</p>
            </div>
			<br/><br/>
			<br/><br/>
			<div class="col-md-12 column">
				<div class="col-md-6">
					<div class="body-footer-content-p" style="text-align:left !important;">
						<p><i>Dr. Germán Viveros Maldonado</i></p>
						<p>Investigador del Centro de Estudios Clásicos</p>
						<p>Instituto de Investigaciones Filológicas</p>
					</div>					
				</div>
				<br class="element-hidden-responsive-md"/>
				<br class="element-hidden-responsive-md"/>
				<div class="col-md-6">
					<div class="body-footer-content-p" style="text-align:right !important;">
						<p><i>Dr. Jorge Adame Goddard</i></p>
						<p>Investigador</p>
						<p>Instituto de Investigaciones Jurídicas</p>
					</div>
				</div>
			</div>
			<br style="clear:both;"/>
			<br />
			<div class="col-md-12">
				<div class="col-md-6">
					<div class="body-footer-content-p" style="text-align:left !important;">
						<p><i>Dr. Bulmaro Reyes Coria</i></p>
						<p>Investigador</p>
						<p>Instituto de Investigaciones Filológicas</p>
					</div>			
				</div>
				<br class="element-hidden-responsive-md"/>
				<br class="element-hidden-responsive-md"/>				
				<div class="col-md-6">
					<div class="body-footer-content-p" style="text-align:right !important;">
						<p><i>Dr. David García Pérez</i></p>
						<p>Investigador del Centro de Estudios Clásicos</p>
						<p>Instituto de Investigaciones Filológicas</p>
					</div>
				</div>
			</div>
			<br style="clear:both;"/>
			<br />
			<div class="col-md-12">
				<div class="col-md-6">
					<div class="body-footer-content-p" style="text-align:left !important;">
						<p><i>Dr. Bernardo Berruecos Frank</i></p>
						<p>Investigador del Centro de Estudios Clásicos</p>
						<p>Instituto de Investigaciones Filológicas</p>
					</div>
				</div>
				<br class="element-hidden-responsive-md"/>
				<br class="element-hidden-responsive-md"/>
				<div class="col-md-6">
					<div class="body-footer-content-p" style="text-align:right !important;">
						<p><i>Dr. Raúl Torres Martínez</i></p>
						<p>Profesor del Colegio de Letras Clásicas</p>
						<p>Facultad de Filosofía y Letras</p>
					</div>
				</div>
			</div>
			<br style="clear:both;"/>
			<br />
			<div class="col-md-12">
				<div class="col-md-6">
					<div class="body-footer-content-p" style="text-align:left !important;">
						<p><i>Dr. Ricardo Salles</i></p>
						<p>Investigador</p>
						<p>Instituto de Investigaciones Filosóficas</p>
					</div>
				</div>
				<div class="col-md-6">
			        
				</div>
			</div>
            <br style="clear:both;"/>
			<br />
			<div class="col-md-12">
				<div class="col-md-6">
					<div class="body-footer-content-p" style="text-align:left !important;">
						<p><i>Dr. Juan Antonio López Férez</i></p>
						<p>Profesor de Letras Clásicas</p>
						<p>Universidad Nacional a Distancia (UNED) Madrid, España</p>
					</div>
				</div>
                <br class="element-hidden-responsive-md"/>
				<br class="element-hidden-responsive-md"/>
				<div class="col-md-6">
			        <div class="body-footer-content-p" style="text-align:right !important;">
						<p><i>Dr. Antonio Río Torres Murciano</i></p>
						<p>Profesor de Letras Clásicas</p>
						<p>Escuela Nacional de Estudios Superiores de la UNAM en Morelia, Michoacán</p>
					</div>
				</div>
			</div>
            <br style="clear:both;"/>
			<br />
			<div class="col-md-12">
				<div class="col-md-6">
                    <div class="body-footer-content-p" style="text-align:left !important;">
						<p><i>Malena Mijares</i></p>
						<p>Directora General</p>
						<p>Divulgación de las Humanidades</p>
					</div>
				</div>
                <br class="element-hidden-responsive-md"/>
				<br class="element-hidden-responsive-md"/>
				<div class="col-md-6">
			       	<div class="body-footer-content-p" style="text-align:right !important;">
						<p><i>Diego García del Gállego</i></p>
						<p>Secretario del Consejo Editorial</p>
						<p>Bibliotheca Scriptorum</p>
					</div>
				</div>
			</div>
			<br style="clear:both;"/>
			<br />
		</div>
		
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SubMainContent" runat="Server">
</asp:Content>