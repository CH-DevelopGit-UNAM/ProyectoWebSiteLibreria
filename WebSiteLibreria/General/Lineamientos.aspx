<%@ Page Title="Lineamientos" Language="C#" MasterPageFile="~/ResponsiveSite.master" AutoEventWireup="true" CodeFile="Presentacion.aspx.cs" Inherits="General_Presentacion" %>

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
        .body-footer-background {
            background-size:contain;
            background-repeat:repeat-y;
        }      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubMainContent" runat="server">
    <div class="body-footer">
        <div class="container body-footer-background">
            <div class="container">
                <a id="#top"></a>
                <div  class="body-footer-title">
                        <p>BIBLIOTHECA&nbsp;</p><p>SCRIPTORVM</p><br />
                        <p>GRAECORVM ET </p><br />
                        <p>ROMANORVM&nbsp;</p><p>MEXICANA</p>
                </div>
                <br /><br />
                <div class="body-footer-subtitle">
                    <p>Lineamientos generales</p>
                </div>
                <br /><br />
                <div class="body-footer-content">
                    <div class="body-footer-subtitle-p">
                        <p>I. Objetivos de la colección</p>
                    </div>
                    <div class="body-footer-content-p">                        
                        <p>Publicada por la Coordinación de Humanidades y el Instituto de Investigaciones Filológicas de la Universidad Nacional Autónoma de México, la Bibliotheca Scriptorum Graecorum et Romanorum Mexicana tiene la finalidad de publicar los textos clásicos 
                            griegos y latinos, acompañados de su correspondiente traducción española, con la finalidad de difundir y promover la cultura clásica en el mundo hispanohablante. Sus objetivos son los siguientes:
                        </p>
                        <p>
                           1. Reunir los trabajos de traducción basados en la investigación original, el análisis y el comentario de los textos clásicos griegos y latinos, con la colaboración de los especialistas del Centro de Estudios Clásicos del Instituto de Investigaciones Filológicas de la UNAM, 
                            así como de otras entidades académicas, con la finalidad de ofrecer traducciones actuales que enriquezcan el ámbito de los estudios clásicos y muestren su vigencia en los distintos espacios del conocimiento y de la cultura contemporáneas.
                        </p>
                        <p>
                            2. Constituirse como epítome del <i>status quaestionis</i> de la recepción antigua y moderna de los textos clásicos griegos y latinos, y proveer al lector de los elementos necesarios para la comprensión e interpretación de los diversos problemas que pueden suscitar.
                        </p>
                        <p>
                            3. Proporcionar elementos de investigación de diversa índole para acceder a la comprensión de los textos en su lengua original.
                        </p>
                        <p>
                           4. Contribuir al desarrollo de los estudios humanísticos y científicos de la sociedad mexicana, como integrante de una comunidad internacional que comparte la tradición cultural griega y romana.
                        </p>
                    </div>
                    <br />
                    <div class="body-footer-subtitle-p">
                        <p>II. Destinatarios</p>
                    </div>
                    <div class="body-footer-content-p">
                        <p>Los destinatarios de esta colección son los especialistas en estudios clásicos, los estudiantes de esta área que se están formando en el conocimiento de las lenguas y las literaturas clásicas, así como los lectores de otras disciplinas 
                            interesados en la materia, además de todo aquel que desee introducirse en el conocimiento de los clásicos.</p>
                    </div>
                    
                    <br />
                    <div class="body-footer-subtitle-p">
                        <p>III. Estructura de los volúmenes</p>
                    </div>
                    <div class="body-footer-content-p">
                        <p>Cada volumen de la colección presentará:</p>
                        <p><b>1. Un estudio preliminar</b> que reunirá la información necesaria para la comprensión general de la obra, y el análisis, en su caso, de los principales testimonios sobre la vida y obra del autor. Tratará los problemas de datación, 
                            transmisión del texto e historia general de su recepción y sus ediciones críticas, todo ello basado en una bibliografía completa y actualizada.</p>
                        <p><b>2. El texto griego o latino original</b> que será establecido por el traductor a partir de la edición más autorizada para cada obra, justificando su elección en una sección del estudio preliminar. En éste se incluirá, además, 
                            una tabla de discrepancias que registre las variantes textuales con respecto a la edición elegida, en caso de que las haya.</p>
                        <p><b>3. La traducción</b> apegada y precisa, sin alterar el sentido esencial del texto ni su registro estilístico; así mismo, deberá ser escrita en lenguaje correcto y fluido de acuerdo con la norma culta del español, evitando, 
                            por un lado, el exceso de literalidad, y por el otro, la libre interpretación. Es decisión del traductor verter los textos poéticos en verso, en cuyo caso dará noticia de los criterios que aplique para el traslado métrico o rítmico, en el estudio preliminar.</p>
                        <p><b>4. La anotación a la traducción</b> consistirá en un aparato de notas concisas al pie de la página, cuyo objetivo será aclarar aquello que pueda representar un obstáculo para la comprensión inmediata del texto español.</p>
                        <p><b>5. El comentario lemático al texto</b> que incluirá información y reflexión original ubicada después del texto bilingüe. El comentario debe contener una síntesis del <i>status quaestionis</i> sobre los pasajes seleccionados y debe remitir directamente a éstos 
                            a través de una llamada a la palabra, al verso o a la sección del texto que se haya de comentar. Deberá justificarse la elección de variantes textuales con respecto a la edición que se haya tomado como base, y se tratarán, además, los diversos problemas 
                            que suscite el pasaje en cuestión, sean de orden lingüístico, literario, translatológico, histórico y filosófico, o relacionados con la temática específica de la obra.</p>
                        <p><b>6. Índices</b> Los volúmenes de la colección incluirán los siguientes índices:</p>
                         <ol type="a">
                            <li><p>Un <i>Index locorum</i> (obligatorio).</p></li>
                            <li><p>Un <i>Index nominum</i> (obligatorio).</p></li>
                            <li><p>Un <i>Index rerum</i> (opcional de acuerdo con las características de la obra y el criterio del traductor).</p></li>
                        </ol>
                        <p><b>7. Bibliografía</b> completa y actualizada.</p>
                        <p><b>8. Índice general</b> al final de la obra.</p>
                    </div>

                    <br />
                    <div class="body-footer-subtitle-p">
                        <p>IV. Política editorial de la colección</p>
                    </div>
                    <div class="body-footer-content-p">
                        <p>Con el propósito de que la Bibliotheca Scriptorum Graecorum et Romanorum Mexicana cumpla con una planeación a corto, mediano y largo plazos, el Consejo Editorial de la colección establecerá las prioridades de publicación a 
                            partir de la planeación que prefigure para cada año, partiendo de las propuestas que reciba de los académicos del Centro de Estudios Clásicos del Instituto de Investigaciones Filológicas, así como de académicos externos cuyo trabajo goce de reconocimiento.</p>
                    </div>

                    <br />
                    <div class="body-footer-subtitle-p">
                        <p>V. La dictaminación de originales</p>
                    </div>
                    <div class="body-footer-content-p">
                        <p>El Consejo Editorial recibirá las propuestas de nuevos títulos y emitirá su opinión sobre la viabilidad de la obra para turnarla a dictamen (consúltese la <a target="_blank" href="/General/GuiaColaboradores">Guía para colaboradores</a>, donde se informan a detalle los requisitos 
                            para la presentación de originales). En caso de que la obra sea aceptada por el Consejo, será enviada a dictamen de acuerdo con el procedimiento institucional: se solicitará un dictamen interno y uno externo, que deberán ser elaborados por pares académicos bajo la
                             modalidad de “doblemente ciego”. En caso de que un dictamen sea negativo, se solicitará un tercero. Si se trata de un dictamen condicionado, el trabajo se devolverá al traductor para su corrección junto con los respectivos dictámenes. Finalmente, si el trabajo es
                             publicable, el director de la colección enviará al autor la carta de aceptación.</p>
                        <p>Una vez que haya concluido el proceso de dictaminación y los dictámenes sean positivos, se dará el crédito correspondiente a los dictaminadores en la publicación del trabajo.</p>
                    </div>

                    <br />
                    <br />
                    <div class="body-footer-content-p" style="display:block;margin:auto; width:auto;text-align:center;">
                        <p>Ciudad Universitaria a 22 de noviembre de 2016</p>
                    </div>
                    <br />
                    <div class="body-footer-content-p" style="display:block;margin:auto; width:auto;text-align:center;">
                        <p>EL CONSEJO EDITORIAL</p>
                    </div>
                </div>                
                <div class="border-legend">
                    <a href="#top"><hr /></a>
                </div>
            </div>
        </div>
    </div>   
</asp:Content>