<%@ Page Title="Antecedentes" Language="C#" MasterPageFile="~/ResponsiveSite.master" AutoEventWireup="true" CodeFile="Antecedentes.aspx.cs" Inherits="General_Antecedentes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="Server">
    <style>
        .body-footer-content {            
            /*padding: 20px;*/                        
        }
        .body-footer-subtitle-p {            
        }
        .body-footer-subtitle {
            font-family: inherit !important;            
            padding: 0px;
            padding-top:20px;            
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
        <div class="body-footer-content">
            <div class="body-footer-subtitle-p subtitle-small">
                <p><b>De la Bibliotheca</b></p>
            </div>
            <br />
            <div class="body-footer-content-p">                
                <p>La Bibliotheca Scriptorum Graecorum et Romanorum Mexicana, coeditada actualmente por dos entidades de la UNAM, la Coordinación de Humanidades y el Instituto de Investigaciones Filológicas a través de su Centro de Estudios Clásicos, 
                    cuenta con una trayectoria sostenida de setenta y cinco años de existencia en la vida cultural y académica de México. Su historia es parte importante de la cultura humanística del mundo hispanohablante, por ser la primera colección 
                    bilingüe que se publicó en este ámbito. Fundada por la UNAM en 1944, fue imaginada y creada como un proyecto nacional por un grupo de intelectuales mexicanos, así como de académicos españoles que llegaron a México a causa del exilio, 
                    para dar a conocer, en ediciones bilingües, los textos de los autores griegos y romanos por ser parte fundamental de la literatura universal. Esto se da en un contexto de la historia de México en que, desde los inicios del siglo XX, 
                    se promovía la edición masiva de las grandes obras de la literatura universal, en traducciones accesibles, para la educación de toda una sociedad.</p>
                <br /><br />
                <p>Su primer director fue Agustín Yáñez, novelista jalisciense que llegó a ser Gobernador de su Estado (1953-1959) y Secretario de Educación Pública de México (1964-1970); los primeros traductores que dieron vida a la Scriptorum fueron 
                    importantes humanistas, entre ellos se cuentan Agustín Millares Carlo, Juan David García Bacca, Demetrio Frangos, José Manuel Gallegos Rocafull y Alfonso Méndez Plancarte, entre otros, ellos brindaron un número importante de traducciones 
                    acompañadas de introducciones y anotaciones para hacer accesibles a estudiantes y profesores, y a todo público interesado, obras fundamentales de la cultura clásica en un momento en que México buscaba traducir, para su amplia difusión, 
                    las grandes obras literarias del mundo como parte de un proyecto educativo nacional. En este periodo son publicados poco más de treinta títulos de los principales autores griegos y latinos sobre temas de filosofía, ciencia, historia, educación 
                    y literatura principalmente. A este lapso pertenecen también las traducciones hechas por Antonio Gómez Robledo, Antonio Alatorre, Francisco Montes de Oca, Juan Antonio Ayala, Rafael Salinas y René Acuña.</p>
                <br /><br />
                <p>Una segunda etapa en la vida de la colección es la signada por el poeta y filólogo Rubén Bonifaz Nuño, quien, en 1966 funda el Centro de traductores de lenguas clásicas y confiere un impulso notable a la “bilingüe”. 
                    El desarrollo de los estudios humanísticos en la UNAM tiene un vínculo estrecho con Bonifaz Nuño, pues su gran visión contribuye a consolidarlos en el contexto universitario y el nacional. Gracias a él y a un grupo de académicos notables 
                    se funda el Instituto de Investigaciones Filológicas, y con éste el Centro de traductores se convierte en Centro de Estudios Clásicos, el cual, a la fecha, concentra el cuerpo principal de especialistas que nutre la colección, si bien no 
                    han faltado las colaboraciones de especialistas externos a la UNAM. Es ésta la etapa más productiva de la Scriptorum, y en ella se cuentan las traducciones del propio Rubén Bonifaz, así como de Tarsicio Herrera Zapién, Roberto Heredia Correa, 
                    Julio Pimentel Álvarez, José Quiñones Melgoza, Germán Viveros Maldonado, Ute Schmidt, Arturo Ramírez Trejo, Lourdes Rojas Álvarez, Amparo Gaos Schmidt, José Tapia Zúñiga, Paola Vianello de Córdova, Conrado Eggers Lan, Salvador Díaz Cíntora, 
                    Gerardo Ramírez Vidal, Pedro Constantino Tapia Zúñiga, Bulmaro Reyes Coria, Carlos Zesati Estrada, Carlos Gehard y José Molina Ayala, todos ellos mencionados en el orden cronológico de sus primeras traducciones en la colección. En este período 
                    son publicados más de cien títulos y la colección adquiere un impulso notable, la temática se amplía y se diversifica a varios géneros literarios como la épica, la lírica, la tragedia y la comedia, y a obras de ciencia, política y retórica, son 
                    trabajos que ubican esta la colección en la posición más relevante del área de Humanidades. En las últimas décadas de este periodo el doctor Bulmaro Reyes Coria, en codirección con el doctor Bonifaz, contribuye a perfeccionar el diseño editorial 
                    de los volúmenes, muchos de los cuales han sido premiados por sus atributos de forma y contenido.</p>
                <br /><br />
                <p>Tras el fallecimiento del doctor Bonifaz en 2013, la Scriptorum inicia una tercera etapa. En 2014 el rector José Narro Robles constituye un Consejo editorial para procurar, de manera colegiada, los trabajos de la Scriptorum, y a principios de 2015 
                    el rector Enrique Graue Wiechers nombra directora a la doctora Aurelia Vargas Valencia, investigadora del Centro de Estudios Clásicos. Este periodo ha significado un breve alto en el camino para poner por escrito los lineamientos y la guía de colaboradores, 
                    con el objetivo de abrir la colección a una mayor participación de los investigadores del Centro de Estudios Clásicos, así como a una nueva generación de jóvenes filólogos de la UNAM y de académicos externos. Al momento, se han publicado tres nuevos títulos 
                    y otros tantos que quedaron pendientes del período anterior. Actualmente están en proceso de edición tres nuevos títulos y está en curso la traducción de más de veinte nuevos títulos; asimismo, se ha hecho una revisión de las obras más solicitadas y que 
                    tienen muchos años sin estar al alcance de nuestros lectores en las librerías, lo cual ha derivado en el impulso de un programa intensivo de reimpresiones y reediciones, debido al cual se han logrado 7 nuevas reimpresiones y varias más están en proceso. 
                    Esto ha sido posible gracias al apoyo extraordinario que la Rectoría de la UNAM ha autorizado a la colección para esos fines. En tanto, como parte de estas medidas para hacer más visible y accesible la colección, se está creando el presente sitio web.</p>
                <br /><br />
                <p>La Scriptorum inicia ahora su era digital, con la seguridad de que este sitio web, construido bajo un modelo vanguardista y versátil y con un diseño dúctil e institucional, permitirá dar una enorme visibilidad en el mundo tanto a la propia 
                    colección como a la UNAM. Tenemos la seguridad de que las versiones electrónicas contenidas en este repositorio promoverán la adquisición de las impresas, porque la sensación de tener un libro en las manos sigue siendo incomparable. 
                    No obstante, la posibilidad de contar con todos los textos en línea, permitirá a los programas de educación a distancia su utilización abierta. Es éste el espíritu que mueve a la UNAM como institución que genera conocimiento y lo difunde 
                    en beneficio de la humanidad. Esta plataforma sienta también las bases para que pronto puedan figurar aquí los volúmenes de otras colecciones afines.</p>
            </div>
            <br />
             <div class="body-footer-subtitle-p subtitle-small">
                <p><b>Del catálogo</b></p>
            </div>
            <br />
            <div class="body-footer-content-p">                
                <p>El Catálogo de la Bibliotheca Scriptorum Graecorum et Romanorum Mexicana, publicado en 1996 por el doctor Roberto Heredia Correa, constituye el antecedente inmediato del catálogo que se incluye en el sitio web. Para ese año la colección constaba 
                    de cien títulos, y el catálogo fue muy útil en la medida que permitía un alto en el camino para hacer un recuento histórico y comentar, de manera breve, los contenidos de los diferentes títulos y el estilo de las diversas traducciones publicadas por 
                    la UNAM desde 1944, año de su creación.</p>
                <br /><br />
                <p>Con la apertura de este sitio web el 29 de marzo de 2019 en el marco de los festejos del 75.°Aniversario de la Bibliotheca Scriptorum, ponemos nuevamente al día su Catálogo gracias al trabajo de investigación y técnico de Aarón Cervantes 
                    Soria en el aspecto técnico editorial y elaboración del catálogo correspondiente a las publicaciones de 1997 a 2019, así como de Elvia Carreño Velázquez en el cuidado y supervisión de los aspectos catalográficos y bibliográficos acordes 
                    con las normas de la UNAM y las internacionales. Ambos son técnicos académicos del Instituto de Investigaciones Filológicas. En este trabajo participó también un gran de colaboradores, cuyos créditos aparecen a pie de página en la pantalla 
                    de inicio.</p>
                <br /><br />
                <p>Las publicaciones de la Bibliotheca Scriptorum suman, a la fecha, un total de 154 títulos. El trabajo que se ha llevado a cabo para lograr este objetivo implicó una auténtica búsqueda de campo para conseguir algunos de los volúmenes, 
                    en particular los más antiguos, que ya no figuraban nisiquiera en las vitrinas del acervo del Centro de Estudios Clásicos.</p>
                <br /><br />
                <p>Las pesquisas no fueron en vano, y a éstas se sumaron las visitas a las bibliotecas para ubicar otros títulos, las cuales permitieron observar la disparidad que existía en el modo de capturar los datos de los volúmenes de Scriptorum 
                    en las bases de datos de sus acervos. Fue posible incidir de alguna manera en la homogeneización de los registros y aclarar dudas de una y otra parte para unificarlos, cosa que se juzgó necesaria para evitar que los títulos perdieran 
                    visibilidad por las peculiaridades de su composición editorial, a saber, el hecho de que, por ejemplo, se trate de ediciones bilingües, con paginación en números romanos y arábigos que distinguen los estudios preliminares y las notas, 
                    del texto y su traducción propiamente dichos.</p>
                <br /><br />
                <p>Un sitio web como el presente, permitirá a nuestros lectores, entre otras cosas, visualizar más fácilmente toda la información relativa a la Bibliotheca, sea por autor, por traductor o por título, así como tener al alcance los propios textos, íntegros, 
                    de cada volumen, en un formato que puede descargarse e imprimirse. Esto seguramente derivará en la posibilidad de hacer estadísticas de diversa índole para reflejar la historia que ha tenido la colección, así como la posibilidad de actualizarla 
                    permanentemente.</p>
                <br /><br />
                <p>Nuestro agradecimiento a los colegas académicos que nos facilitaron los volúmenes de su biblioteca personal para que pudieran ser digitalizados, así como a los bibliotecarios y al personal de las distintas bibliotecas de la UNAM por su diligente
                     apoyo para recuperar un acervo que, debido a las vicisitudes propias de una historia de 75 años, había quedado un tanto disperso en distintos sectores de nuestra Universidad.</p>
                <br /><br />
                <p>Mención especial merece el apoyo del Instituto de Investigaciones Jurídicas, pues con la anuencia de su director, el doctor Pedro Salazar Ugarte, las áreas de biblioteca, cómputo y difusión nos brindaron una valiosa orientación para el inicio 
                    del proyecto de sitio web. Para ello también contamos con el respaldo del doctor Jorge Adame Goddard, coordinador de la Bibliotheca romanística, así como del doctor Jorge Mena-Brito y de su colaborador el licenciado Israel García Avilés, 
                    pues fue con los instrumentos de ese proyecto que forma parte del convenio de colaboración entre ese Instituto y el Instituto de Investigaciones Filológicas para editar la Bibliotheca Iuridica Latina Mexicana, que iniciamos los trabajos de escaneo.</p>
                <br /><br />
                <p>La Bibliotheca Scriptorum Graecorum et Romanorum Mexicana es un valioso patrimonio humanístico, tanto de México como de la humanidad, que ahora el lector tiene a su alcance bajo el principio de “Toda la UNAM en línea”.</p>
            </div>
            <br /><br /><br /><br />
            <div class="body-footer-content-p" style="text-align:right;">                
                <p>Aurelia Vargas Valencia</p>
                <p>Primavera de 2019</p>
            </div>
            <br />
        </div>
       
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SubMainContent" runat="Server">
</asp:Content>