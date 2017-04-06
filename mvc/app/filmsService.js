(function () {

    function fn() {

        var self = this;

        var films = null;

        this.getList = function (user, party) {
            //console.log("getList");
            if (!films) {
                films = [];
                $.getJSON("/app/store/films.json", function (data) {
                    films = null;
                    films = data;
                    //console.log("load ", films);
                });
            }
            //console.log("return ", films);
            return films;
        };

        this.loadKinopoiskData = function (id) {

        };
        //http://getmovie.cc/api/kinopoisk.json?id=885316&token=037313259a17be837be3bd04a51bf678


    }

    angular.module("filmsModule", []);

    angular.module("filmsModule").service("filmsService", fn);

})();