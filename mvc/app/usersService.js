(function () {

    function fn($interval) {

        var self = this;

        function newFunstionsVars() {
            return { data: null, callsCount: 0, loadsCount: 0, isBussy: false };
        }

        var sd = {
            getInvitesList: newFunstionsVars(),
            getInfo: newFunstionsVars()
        };

        this.dispose = function () {
            sd.getInfo.data = null;
            sd.getInfo.isBussy = false;
        };



        this.getInfo = function () {
            sd.getInfo.callsCount++;
            if (!sd.getInfo.isBussy) {
                sd.getInfo.isBussy = true;
                if (!sd.getInfo.data) {
                    sd.getInfo.loadsCount++;
                    sd.getInfo.data = {};
                    $.getJSON("/Users/GetInfo", { t: Math.random() }, function (data) {
                        sd.getInfo.data = null;
                        $interval(function () {
                            sd.getInfo.data = data;
                            sd.getInfo.isBussy = false;
                        }, 0, 1);
                    });
                }
            }
            return sd.getInfo.data;
        };

        this.createInvite = function (invite, email) {
            invite.invite = null;
            $.getJSON("/Users/CreateInvite", { t: Math.random(), email: email }, function (data) {
                invite.invite = data;
            });
        };

        this.createUser = function (invite, email, password, nickName) {
            bussy = true;
            $.getJSON("/Users/CreateUser", { invite: invite, email: email, password: password, nickName: nickName }, function (data) {
                bussy = false;
            });
        };


        this.getInvitesList = function () {
            sd.getInvitesList.callsCount++

            if (!sd.getInvitesList.data) {
                sd.getInvitesList.isBussy = true;
                sd.getInvitesList.data = [];
                sd.getInvitesList.loadsCount++;
                $.getJSON("/Users/InvitesList", { t: Math.random() }, function (data) {
                    sd.getInvitesList.data = null;
                    $interval(function () {
                        sd.getInvitesList.data = data;
                        sd.getInvitesList.isBussy = false;
                    }, 0, 1);
                });
            }

            return sd.getInvitesList.data;
        };

        this.login = function (email, password) {
            //console.log("getInfo");
            var login = null;

            var data = { email: email, password: password, t: Math.random() };

            $.getJSON("/Users/Login", data, function (data) {
                login = null;
                login = data;
                $interval(function () {
                    self.dispose();
                }, 0, 1);
            });

            //console.log("return ", login);
            return login;
        };

        this.logout = function () {
            $.getJSON("/Users/Logout",  function (data) {
                login = null;
                login = data;
                $interval(function () {
                    self.dispose();
                }, 0, 1);
            });
            return true;
        };


    }

    angular.module("usersModule", []);

    angular.module("usersModule").service("usersService", fn);

})();