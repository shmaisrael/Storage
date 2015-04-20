angular.module('components').directive('createButton', function () {
    return {
        restrict: 'E',
        replace: true,
        template: '<span class="pull-right glyphicon glyphicon-plus pointer"></span>'
    };
});