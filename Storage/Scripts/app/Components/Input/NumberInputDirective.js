angular.module('components').directive('integer', function () {
    var INTEGER_REGEXP = /^\-?\d+$/;
    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ctrl) {
            ctrl.$validators.integer = function (modelValue, viewValue) {
                if (ctrl.$isEmpty(modelValue)) {
                    // consider empty models to be valid
                    return true;
                }

                if (INTEGER_REGEXP.test(viewValue)) {
                    // it is valid
                    return true;
                }

                // it is invalid
                return false;
            };
        }
    };
}).directive('numberInput', function () {
    return {
        restrict: 'E',
        replace: true,
        template: '<input type="number" integer min="0" class="edit-input" required>'
    };
});