angular.module('controllers').controller('MaterialDropdown', ['$scope', 'NetworkService', function ($scope, NetworkService) {
    NetworkService.custom("Material/GetDropDown").success(function (data) {
        $scope.dropdownData = data;        
    });
}]);