(function () {

    function MainCtrl($scope, $route, $routeParams, $location, filmsService, usersService) {

        this.$route = $route;
        this.$location = $location;
        this.$routeParams = $routeParams;

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

        this.bussy = false;

        this.films = filmsService;
        this.usersService = usersService;

        this.userInfo = {info:null};
        this.fillUserInfo = function () {
            self.userInfo = self.usersService.getInfo(self.bussy);
        };

        this.invite = { invite: "invite" };


    }

    angular.module('app', ['ngRoute', 'filmsModule', 'usersModule']);

    angular.module('app').controller('MainCtrl', MainCtrl);

})();
