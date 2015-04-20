angular.module('components').directive('editButton', function () {
    return {
        restrict: 'E',
        replace: true,
        template: '<span class="pull-right glyphicon glyphicon-share-alt pointer margin"></span>'
    };
});