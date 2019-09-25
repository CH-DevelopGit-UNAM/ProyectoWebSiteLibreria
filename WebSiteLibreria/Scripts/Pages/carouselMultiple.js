(function ($, window) {

    $.fn.flexCarousel = function (options) {
        var settings = $.extend({

        }, options);

        return this.each(function () {
            var carousel = $(this);
            var items = settings["Items"];
            var intervalCarousel = settings["Interval"];

            $(window).resize(adjuster);
            adjuster();
            function adjuster() {
                var items = 0;

                $(carousel).find(".items .flex-item").each(function () {
                    items++;
                });

                var item_width = 0;
                var tamanoBase = parseInt(settings["Size"]);
                var porcentaje = parseFloat(settings["Porcentaje"]);
                var onItemCreated = settings["OnItemCreated"];
                                
                var window_width = $(window).width();
                var carousel_width = $(carousel).width();
                var carouselInner_width = $(carousel).find(".carousel-inner").width();
                var carouselControls_width = $(carousel).find(".left.carousel-control").width() + $(carousel).find(".right.carousel-control").width();
                var real_width_carousel = carousel_width - carouselControls_width;

                var itemBase_width = Math.floor((tamanoBase - carouselControls_width) * porcentaje);
                //var numero_columnas = Math.floor(real_width_carousel / itemBase_width);

                if ($(window).width() > 1200) {
                    numero_columnas = 5;
                }
                else if ($(window).width() > 993 && $(window).width() < 1200) {
                    numero_columnas = 4;
                }
                else if ($(window).width() > 650 && $(window).width() < 993) {
                    numero_columnas = 3;
                } else if ($(window).width() > 526 && $(window).width() < 650) {
                    numero_columnas = 2;
                } else {
                    numero_columnas = 1;
                }
                var items_for_the_range = numero_columnas;
                item_width = Math.floor(real_width_carousel / numero_columnas) - 10;
                adjustCarousel($(carousel), real_width_carousel, items, item_width, items_for_the_range, onItemCreated);
            }


            function adjustCarousel(carousel, carousel_width, num_of_items, item_width, items_for_the_range, onItemCreated) {
                // numero de páginas a crear
                var number_pages = Math.ceil(num_of_items / items_for_the_range);
                // items (lista de elementos)
                var $items = $(carousel).find(".items .flex-item");
                // cantidad de elementos
                var length_of_$items = $items.length;
                // limpiar carousel
                $(carousel).find(".carousel-inner").html("");
                //  variable control del item actual
                var current_item = 0;
                //por cada página
                for (var i = 0; i < number_pages; i++) {
                    var item = "<div class='item'><div class='item-inner-container'>";
                    //var item = "<div class='item'><div class='row'>";
                    // elementos por página : total items - (pagina actual * columnas )
                    var itemsPage = Math.abs(length_of_$items - (i * items_for_the_range)); //Math.floor(carousel_width / item_width);                            
                    if (itemsPage >= items_for_the_range) {
                        itemsPage = items_for_the_range;
                    }

                    // Por cada elemento a tomar
                    for (var j = 0; j < itemsPage; j++) {
                        // se toma el objeto actual
                        //var item_body = $($items[current_item]).clone().wrap("<p>").parent().html();
                        //if (item_body !== "") {
                        //    item += "<div class='item-inner' style=\"width:" + item_width + "px; height:" + item_width + "px;\">" + item_body + "</div>";
                        //}
                        ////item += "<div class='col-xs-" + Math.floor(12/itemsPage) + "'>" + item_body + "</div>";

                        var item_noticia = $($items[current_item]);
                        var href = "<a href=\"" + item_noticia.attr("data-url") + "\" ";
                        if (item_noticia[0].attributes != undefined && item_noticia[0].attributes != null) {
                            var atributes = "";
                            for (var k = 0; k < item_noticia[0].attributes.length; k++) {
                                var atribute = item_noticia[0].attributes[k];
                                if (atribute.nodeName.indexOf("data", 0) >= 0) {
                                    //console.log(atribute.nodeName);
                                    //console.log(atribute.nodeValue);
                                    atributes += " " + atribute.nodeName + "=\"" + atribute.nodeValue + "\" ";
                                }
                            }
                        }
                        href += atributes + " >";
                        
                        var titulo = "<div class=\"flex-item-title\"> <div></div> <div> " + item_noticia.attr("data-title") + " </div> </div>";

                        var item_body = href + "<div class=\"flex-item\">" + "<img class=\"img-responsive\" src=\"" + item_noticia.attr("data-src") + "\" /> " + titulo + " </div>" + "</a>";

                        if (onItemCreated instanceof Function) {
                            var result = onItemCreated(item_body);
                            if(typeof (result) === "string"){
                                item_body = result;
                            }
                        }

                        item += "<div class='item-inner' style=\"width:" + item_width + "px; height:" + item_width + "px; position:relative; \">" + item_body + "</div>";

                        // la variable se incrementa
                        current_item++;
                    }
                    item += "</div></div>";
                    
                    $(carousel).find(".carousel-inner").append(item);
                    if (i == 0) {
                        $(carousel).find(".carousel-inner .item").addClass("active");
                    }
                }

                // alignItemsInsideACarousel();

                var theCarousel = document.getElementById($(carousel).attr("id"));
                theCarouselHasBeenAdjusted(theCarousel);
            }


            function theCarouselHasBeenAdjusted(carousel) {
                var evt = new CustomEvent("aCarouselHasBeenAdjusted", {});
                // asegurarse de que se ejecute de nuevo el interval automáticamente
                $(carousel).carousel({
                    interval: intervalCarousel
                });
                carousel.dispatchEvent(evt);
            }            

        });
    };

})(jQuery, window);