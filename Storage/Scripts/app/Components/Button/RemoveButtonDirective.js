angular.module('components').directive('removeButton', function () {
    return {
        restrict: 'E',
        replace: true,
        template: '<span class="pull-right glyphicon glyphicon-remove pointer margin"></span>'
    };
});