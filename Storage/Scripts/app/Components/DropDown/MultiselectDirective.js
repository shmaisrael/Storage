angular.module('components').directive('dropdownMultiselect', function () {
    return {
        restrict: 'E',
        require: 'ngModel',
        scope: {
            ngModel: '=',
            ngDropdown: '='
        },
        templateUrl: "/Scripts/app/Components/DropDown/Multiselect.html",
        controller: function ($scope) {
            $scope.setSelectedItem = function () {
                var id = this.option.id;

                if (_.findWhere($scope.ngModel, { id: id })) {
                    $scope.ngModel = _.without($scope.ngModel, _.findWhere($scope.ngModel, { id: id }));
                } else {
                    if(!$scope.ngModel) {
                        $scope.ngModel = [];
                    }
                    $scope.ngModel.push({ id: this.option.id, name: this.option.name });
                }
                //  Not correct
                $scope.required = $scope.ngModel.length == 0 ? '' : ' ';
            };


            $scope.isChecked = function (id) {
                if (_.findWhere($scope.ngModel, { id: id })) {
                    return 'pull-right glyphicon glyphicon-ok';
                }
                return false;
            };
        }
    }
});