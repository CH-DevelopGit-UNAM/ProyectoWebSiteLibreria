<%@ Page Title="Guia Colaboradores" Language="C#" MasterPageFile="~/ResponsiveSite.master" AutoEventWireup="true" CodeFile="Presentacion.aspx.cs" Inherits="General_Presentacion" %>

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
<asp:Content ID="BodyFooterContent" ContentPlaceHolderID="SubMainContent" runat="server">
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
                    <p>Guía para colaboradores</p>
                </div>
                <br /><br />
                <div class="body-footer-content">
                    <div class="body-footer-subtitle-p">
                        <p>I. Introducción</p>
                    </div>
                    <div class="body-footer-content-p">                        
                        <p>
                            Con la finalidad de establecer las pautas esenciales para editar con criterios homogéneos las colaboraciones que nutrirán la Bibliotheca Scriptorum Graecorum et Romanorum Mexicana, y en concordancia con los Lineamientos generales por los que ésta se rige, los traductores deberán tomar en cuenta lo siguiente:
                        </p>
                        <p>
                            1. La traducción debe dar una versión actual del texto de origen acorde con la norma culta del español, y debe procurarse claridad y fluidez en la expresión.
                        </p>
                        <p>
                            2. El texto griego o latino original será establecido por el traductor a partir de la edición más autorizada para cada obra, justificando su elección en una sección del estudio preliminar. En éste se incluirá, además, una tabla de discrepancias que registre las variantes textuales con respecto a la edición elegida, en caso de que las hubiera.
                        </p>
                        <p>
                            3. La anotación a la traducción estará orientada a proporcionar al lector las herramientas para la cabal comprensión del texto: las notas deben ser breves y registrarse con numeración consecutiva en la traducción, marcando la llamada en la palabra, la línea o el verso anotado, según el caso.
                        </p>
                        <p>
                            4. El comentario estará orientado al especialista y al estudiante de Letras Clásicas; deberá ser un apartado sintético, con un sentido práctico y útil, que reúna una selección de los pasajes relevantes del texto de origen. Deberá justificarse la elección de variantes textuales con respecto a la edición que se haya tomado como base, en caso de que las hubiera.
                        </p>
                        <p> 5. En los índices, la información se dispondrá de la siguiente manera: </p>
                        <ol type="a">
                            <li><p>En el <i>Index locorum</i> figurarán todos los pasajes de la literatura antigua a los que se haga alusión en la obra.</p></li>
                            <li><p>En el <i>Index nominum</i>, se registrarán todos los nombres de autores y personajes antiguos, estudiosos modernos, topónimos, teónimos, etcétera, que se mencionen a lo largo del volumen.</p></li>
                            <li><p>En el <i>Index rerum</i> se incluirán los conceptos clave, tanto en griego y latín como en español, que el autor considere fundamentales para que el lector se desenvuelva con facilidad en el estudio y el manejo de la obra. El <i>Index rerum</i> podrá tomar la forma de un glosario de conceptos, si el traductor lo considera necesario.</p></li>
                        </ol>                        
                        <p id="I_6">6. La Bibliografía se presentará completa y actualizada, y estará clasificada en primaria y secundaria, y ésta a su vez podrá clasificarse por rubros (fuentes, estudios generales, estudios especializados).</p>
                    </div>
                    <br />
                    <div class="body-footer-subtitle-p">
                        <p>II. Entrega de originales</p>
                    </div>
                    <div class="body-footer-content-p">
                        <p>1. Toda propuesta de publicación para la Bibliotheca Scriptorum Graecorum et Romanorum Mexicana será enviada al director de la colección, quien la pondrá a consideración de los miembros del Consejo Editorial. Este órgano colegiado revisará y validará, en primera instancia, las propuestas que se sometan a dictaminación.</p>
                        <p>2. El autor deberá enviar, junto con su colaboración, los siguientes datos: a) dirección postal, teléfono, correo electrónico; b) síntesis curricular que no exceda de 10 líneas y que indique el máximo grado académico y lugar de obtención, experiencia docente y de investigación, institución de adscripción, y principales.</p>
                        <p>3. Los trabajos deberán estar terminados desde el punto de vista lingüístico y estilístico, y ser enviados en su versión completa en cuanto a contenido (con todos los datos, mapas, cuadros, esquemas, apéndices, referencias bibliográficas, etcétera). Los originales impresos llevarán foliación e irán acompañados del archivo electrónico correspondiente. El texto se presentará en hoja tamaño carta, formato Word, en letra
                            <span style="font-family:Times New Roman;">Times New Roman</span>, en tamaño de 12 puntos para el cuerpo del texto y de 10 puntos para las notas a pie de página, con un interlineado de 1.5. Los textos griegos se presentarán en griego politónico <i>Unicode</i>.</p>
                        <p id="II_4">4. Para referirse a los autores antiguos y a sus obras se usarán las abreviaturas establecidas en el <i><a target="_blank" rel="noopener noreferrer" href="../pdfs/Abreviaturas-Grecolatinas.pdf">Diccionario Griego-Español (DGE)</a></i>, para los autores griegos, y en <i><a target="_blank" rel="noopener noreferrer" href="../pdfs/Oxford-Latin Dictionary.pdf">Latin Dictionary (Lewis & Short)</a></i>
                            , para los autores latinos. Las referencias abreviadas a éstos se harán como sigue: abreviatura del nombre del autor (en redonda), coma, abreviatura del título de la obra (en cursiva),
                             coma, número(s) de libro(s) o de canto(s) (en romanos), coma, número(s) de capítulo(s) o de verso(s) (en arábigos).</p>
                        <p>Ejs.: Cic., Tusc., II, 7; Hom., Il., IV, 121-128.</p>
                        <p>Las citas de pasajes de un mismo libro o canto aparecerán separadas por coma.</p>
                        <p>Ejs.: Cic., Tusc., II, 7, 12; Hom., Il., IV, 121-128, 231.</p>
                        <p>En caso de que la cita incluya, además del número de capítulo, un número de párrafo (en arábigos), ambos aparecerán separados por punto.</p>
                        <p>Ej.: Cic., Tusc., II, 7.17.</p>
                        <p>Se podrán, además, citar pasajes de algunos autores griegos y latinos mediante referencia a los números de libro, verso, página, columna o fragmento que se les asigna en una edición concreta, siempre que esta práctica haya sido ratificada por el uso.</p>
                        <p>Ejs.: Enn., Ann., VII, fr. 1 Vahlen, o bien Enn., Ann., 221-224 Vahlen; Arist., Po.,1460a.</p>
                        <p>En caso de que se empleen abreviaturas para referirse a autores y/o a obras no incluidos en el presente punto, el autor deberá especificarlas.</p>

                        <%--<ol style="list-style-type:none;"><li></li></ol>--%>
                        <p>5. El texto original griego o latino y la traducción al español se imprimirán en redonda. En los textos latinos se distinguirá gráficamente la v de la u en minúscula; en mayúscula se empleará sólo <span style="font-family:Times New Roman;">V</span>.</p>
                        <p>6. En la introducción, el comentario y las notas, así como las citas de textos griegos, latinos y modernos se harán en el cuerpo del texto, de acuerdo con las normas siguientes:</p>

                        <ol style="list-style-type:none;">
                            <li><p>En griego: en caracteres griegos, en redonda, sin entrecomillar</p></li>
                            <li><p>En latín: en cursiva, sin entrecomillar.</p></li>
                            <li><p>En lenguas modernas: en redonda entre comillas inglesas dobles (“ ”).</p></li>
                            <li><p>Las comillas inglesas simples (‘ ’) se utilizarán cuando se deba entrecomillar una parte del texto que figure entre comillas inglesas dobles.</p></li>
                            <li><p>En la introducción el texto citado aparecerá sangrado y con sendas líneas en blanco por la parte superior e inferior cuando este exceda de tres versos o de tres líneas.</p></li>
                            <li><p>La omisión de una parte del texto citado se indicará mediante puntos suspensivos entre espacios, sin paréntesis.</p></li>
                        </ol>
                                                
                        <p>7. Las referencias a autores modernos se realizarán en la introducción, el comentario y las notas de forma abreviada, como sigue: apellido, año, coma, p(p)., número(s) de página (s).</p>
                        <p>Ejs.: Beuchot (1985, p. 25) [cuando en el texto se mencione al autor]; (Beuchot 1985, p.25) [cuando en el texto no se mencione al autor].</p>

                        <p>8. Las referencias bibliográficas aparecerán en un listado final después de los índices. Se clasificarán como se indica en el punto <a href="#I_6">I.6</a> de esta guía, y se redactarán según los ejemplos siguientes:</p>

                        <p>Libros:</p>
                        <p>BEUCHOT, M., 1985, <i>Ensayos marginales sobre Aristóteles</i>, México, Universidad Nacional Autónoma de México (Cuadernos del Centro de Estudios Clásicos, 22).</p>
                        <p>ANDRÉ, J. (ed.), 1987<sup>2</sup>, <i>Apicius. L’art culinaire</i>, París, Les Belles Lettres [1974<sup>1</sup>].</p>
                        <p>Capítulos de libro y participaciones en actas u homenajes:</p>
                        <p>MOLINA VALERO, C., 2010, “Las glosas licias en fuentes griegas”, en F. Cortés Gabaudán y J. V. Méndez Dosuna (eds.), <i>Dic mihi, musa, virum. Homenaje al profesor Antonio López Eire</i>, Salamanca, Ediciones Universidad de Salamanca, pp. 459464.</p>
                        <p>Artículos:</p>
                        <p>GILL, C., 1977, “The Genre of the Atlantis Story”, Classical Philology, 72.4, pp. 287304.</p>
                        <p>En caso de que se citen publicaciones electrónicas, se añadirán a los datos indicados para las impresas la dirección de internet en que están disponibles y la fecha de la consulta, según los ejemplos siguientes:</p>
                        <p style="word-break:break-all;">GARCÍA PÉREZ, D., 2015, “Educación retórica y filosófica: algunos vínculos entre Aristófanes y Eurípides”, Nova Tellus, 33.1, pp. 39-63 <<i><span>https://revistasfilologicas.unam.mx/nouatellus/index.php/nt/article/view/692/685</span></i>> (17/11/2016).</p>
                        <p>DELGADO ESCOLAR, F. L., 2002, “Los poetas latinos como críticos literarios desde Terencio hasta Juvenal: estudios estilísticos y lexicológicos”, tesis doctoral, Universidad Complutense de Madrid <<i><span>http://eprints.ucm.es/3244/</span></i>>(27/02/2011).</p>
                        <p>Nótese que se usan comas para separar los elementos de las fichas bibliográficas y que no se abrevian los títulos de las revistas. El nombre del lugar de edición se traducirá al español cuando exista una traducción de uso común (p. ej. “Londres”, y no “London”).</p>


                        <p>9. Los lemas del comentario irán en negrita y precedidos por la oportuna referencia a número (s) de libro(s) o de canto(s) y/o número(s) de capítulo(s) o de verso(s), hecha de acuerdo con la norma establecida en el punto <a href="#II_4">II.4</a> de esta guía.</p>
                        <p>10. Las abreviaturas y los tecnicismos bibliográficos, incluidos los de origen latino, como cf., id., ib., op. cit., s. v., vid., supra, infra y loc. cit., irán en redonda, no en cursiva.</p>
                        <p>11. Si se incluyen imágenes, éstas deben mandarse en archivo independiente, en formato .tiff y con resolución mínima de 300 puntos por pulgada. El material debe estar acompañado de un pie de ilustración breve y preciso. Debe incluirse la referencia al lugar (libro, postal, sitio web, etcétera) de donde 
                            las imágenes se tomaron, así como proporcionar prueba de que se cuenta con autorización para reproducir dicha imagen, en caso de requerirse.</p>

                        <p>12. Una vez formados y corregidos ortotipográficamente los trabajos, se enviarán las planas a los autores para su revisión final y validación, para lo cual dispondrán de un período determinado de tiempo dependiendo de la extensión de la obra. La Bibliotheca no acepta cambios de contenido o correcciones mayores sobre pruebas finas. 
                            Mientras subsistan problemas no resueltos por el autor, se suspenderá la publicación del texto.</p>

                    </div>
                    
                    <br />
                     <div class="body-footer-subtitle-p">
                        <p id="Forros">III. Forros</p>
                    </div>
                    <div class="body-footer-content-p">
                        <p>Los volúmenes irán encuadernados con tapa dura y llevarán una camisa que contendrá: en la solapa delantera un texto fijo, elaborado por el Consejo Editorial, que presentará la colección, y en esta solapa se registrarán también los datos completos de la ilustración usada en la portada; 
                            en la tercera de forros, un listado de los títulos y reediciones recientes; en la cuarta de forros irán dos párrafos: uno versará sobre el volumen, y otro sobre el traductor.</p>
                        <p>Siempre que el armado de pliegos lo permita, se imprimirá en las últimas páginas una réplica de la “cuarta de forros”, para evitar que la información que allí aparece se pierda, si algún bibliotecario quita la “camisa” al libro.</p>
                    </div>

                    <br />
                    <div class="body-footer-subtitle-p">
                        <p>IV. Portada</p>
                    </div>
                    <div class="body-footer-content-p">
                        <p>El traductor podrá sugerir la imagen u opciones de imágenes para ilustrar la portada del volumen en cuestión. La imagen deberá ser, en principio, de una pieza griega o romana, sea escultura, cerámica, pintura, mosaico, edificio, etcétera. La imagen deberá, además, estar relacionada 
                            con el tema y la época de la obra, tener buena calidad de resolución y, de preferencia, estar exenta de derechos de autor. En caso de que la imagen tenga derechos de autor, el traductor deberá proporcionar a los editores los datos necesarios para tramitar los derechos. 
                            Las imágenes que se propongan no podrán ser alteradas ni formar <i>collage</i> para crear una pieza nueva. Para mayor información acerca del uso de las imágenes consúltese la <i><a href="#Forros">Guía para el diseño de forros de la Bibliotheca Scriptorum Graecorum et Romanorum Mexicana</a></i>.</p>
                    </div>
                    <br />
                    <br />
                    <div class="body-footer-content-p" style="display:block;margin:auto; width:auto;text-align:center;">
                        <p>Ciudad Universitaria, a 22 de noviembre de 2016.</p>
                    </div>
                    <br />
                    <div class="body-footer-content-p" style="display:block;margin:auto; width:auto;text-align:center;">
                        <p>EL CONSEJO EDITORIAL</p>
                    </div>
					<br/>
					<br/>
					<div class="body-footer-content-p">
						<p style="font-size:0.8em;">Nota: los casos no previstos en esta guía serán resueltos por el Consejo Editorial.</p>
					</div>
                </div>                
                <div class="border-legend">
                    <a href="#top"><hr /></a>
                </div>
            </div>
        </div>
    </div>  
</asp:Content>