angular.module('components').directive('dropDown', function () {
    function dropDownController($scope, $element, $attrs) {
        // Set value for select
        var setValue = function () {
            $scope.ngModel = $scope.ngModel ? _.findWhere($scope.ngDropdown, { id: $scope.ngModel.id }) : undefined;
        }
        setValue();

        $scope.$watch('ngModel', function () {
            setValue();
        });
    }

    return {
        restrict: 'E',
        scope: {
            ngModel: '=',
            ngDropdown: '='
        },
        replace: true,
        template: '<select class="edit-input" ng-options="data as data.name for data in ngDropdown" required></select>',
        controller: dropDownController
    };
});