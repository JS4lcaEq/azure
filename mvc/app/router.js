
angular.module('app')
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider
            .when('/Book/:bookId', {
                templateUrl: 'book.html'
            })
            .when('/person', {
                templateUrl: '/app/templates/person.template.html'
            })
            .when('/parties', {
                templateUrl: '/app/templates/parties.template.html'
            })
            .when('/', {
                templateUrl: '/app/templates/index.template.html'
            });

        // configure html5 to get links working on jsfiddle
        //$locationProvider.html5Mode(true);
    });