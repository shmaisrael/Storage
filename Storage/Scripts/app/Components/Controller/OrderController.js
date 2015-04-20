angular.module('controllers').controller('OrderController', ['$scope', 'NetworkService', function ($scope, NetworkService) {
    NetworkService.custom("Order/GetOrder").success(function (data) {
        $scope.order = data;
    });

    $scope.buyProduct = function (productId)
    {
        NetworkService.custom("Order/BuyProduct", { productId: productId }).success(function (data) {
            $scope.order = data;
        }).error(function () {
            alert("It is impossible to make a purchase");
        });
    }
}]);