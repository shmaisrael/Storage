angular.module('components').directive('textInput', function () {
    return {
        restrict: 'E',
        replace: true,
        template: '<input class="edit-input" required>'
    };
});