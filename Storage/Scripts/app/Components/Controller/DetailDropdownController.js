angular.module('controllers').controller('DetailDropdown', ['$scope', 'NetworkService', function ($scope, NetworkService) {
    NetworkService.custom("Detail/GetDropDown").success(function (data) {
        $scope.dropdownData = data;
    });
}]);