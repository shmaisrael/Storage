// Services
angular.module('sharedServices', []);
// Filters
angular.module('filters', []);
// Components
angular.module('components', []);
// Controllers
angular.module('controllers', []);

angular.module('StorageApp', ['ngRoute', 'filters', 'sharedServices', 'components', 'controllers']).config(['$routeProvider', '$locationProvider',
    function ($routeProvider, $locationProvider) {
        $locationProvider
            .html5Mode({
                enabled: true,
                requireBase: false
            });
        $routeProvider
            .when('/', {
                templateUrl: '/View/Order'
            })
            .when('/Material', {
                templateUrl: '/View/Material'
            })
            .when('/Detail', {
                templateUrl: '/View/Detail'
            })
            .when('/Product', {
                templateUrl: '/View/Product'
            });
    }
]);