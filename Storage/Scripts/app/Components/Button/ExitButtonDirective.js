angular.module('components').directive('exitButton', function () {
    return {
        restrict: 'E',
        replace: true,
        template: '<span class="pull-right glyphicon glyphicon-pencil pointer"></span>'
    };
});