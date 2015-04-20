angular.module('components').directive('saveButton', function () {
    return {
        restrict: 'E',
        replace: true,
        template: '<span class="pull-right glyphicon glyphicon-ok pointer"></span>'
    };
});