(function () {

    function MainCtrl($scope, filmsService) {
        var self = this;
        this.tmplt = "1 + 1 = {{1+ctrl.var}}";
        this.var = "VAR";
        this.len = 3;
        this.src = [1, 2, 3];
        this.resetSrc = function (len) {
            self.src.length = 0;
            self.src = null;
            self.src = [];
            for (var i = 0; i < len; i++) {
                self.src.push(i);
            }
        };

        this.films = filmsService;





    }

    angular.module('app', ['filmsModule']);

    angular.module('app').controller('MainCtrl', MainCtrl);

})();
