angular.module('components').directive('editableTable', ['ObjectDifference', 'NetworkService', function (ObjectDifference, NetworkService) {
    function extendedTableController($scope, $element, $attrs) {
        // Api to call.
        var api = $attrs.api;

        // Update grid with data.
        var loadDataToGrid = function (data) {
            $scope.datas = data;
        }

        // Initial load data.
        NetworkService.get(api).success(function (data) {
            loadDataToGrid(data);
        });

        // Edit mode.
        var initialValue = [];
        $scope.isEditMode = function (id) {
            return initialValue[id] != null && initialValue[id] != undefined;
        };
        $scope.changeEditMode = function (id) {
            if (!$scope.isEditMode(id)) {
                initialValue[id] = angular.copy(_.where($scope.datas, { id: id })[0]);
            } else {
                $scope.datas[id - 1] = initialValue[id];
                initialValue[id] = null;
            }            
        };
        $scope.changeEditModeApplyChanges = function (id) {
            initialValue[id] = null;
        };

        // Api calls.
        $scope.remove = function (id) {
            NetworkService.remove(api, { id: id }).success(function (data) {
                loadDataToGrid(data);
            });
        };

        $scope.update = function (data) {
            var dataChanged = ObjectDifference.getChanges(initialValue[data.id], data);
            if (dataChanged != false) {
                NetworkService.update(api, dataChanged).success(function () {
                    $scope.changeEditModeApplyChanges(data.id);
                }).error(function () {
                    $scope.changeEditMode(data.id);
                    alert("Record could not be changed");
                });
            } else {
                $scope.changeEditMode(data.id);
            }
        };

        $scope.create = function (data) {
            NetworkService.create(api, data).success(function (data) {
                loadDataToGrid(data);
            }).error(function () {
                alert("Record could not be created");
            });
        }
    };

    function extendedTableCompile($element, $attrs) {
        // General style.
        $element.addClass('table');

        // Header.
        var thead = $element.find('thead:first');
        var theadTr = thead.find('tr:first');
        var theadTh = theadTr.find('th');
        theadTh.attr('style', 'width:' + 100 / theadTh.length + '%');
        theadTr.append('<th colspan="2"></th>');

        // Create "create row" from "edit row". 
        var tbodyShowEdit = $element.find('tbody:first');
        var tbodyCreate = angular.element('<tbody ng-form name="createForm"></tbody>').append(tbodyShowEdit.html());
        tbodyCreate.find('tr:first').remove();
        $element.append(tbodyCreate);
        // Add controls to "create row".
        var trCreate = tbodyCreate.find('tr:first');
        trCreate.append('<td colspan="2" class="td-icon"><create-button ng-show="createForm.$valid" ng-click="create(data)"></create-button></td>');

        // Ng-repeat throw all data.
        tbodyShowEdit.attr('ng-repeat', 'data in datas track by data.id');

        // Add controls for "show row".
        var trShow = tbodyShowEdit.find('tr:first');
        trShow.attr('ng-show', '!isEditMode(data.id)');
        trShow.append('<td class="td-icon"><remove-button ng-click="remove(data.id)"></remove-button></td>');
        trShow.append('<td class="td-icon"><exit-button ng-click="changeEditMode(data.id)"></exit-button></td>');

        // Add controls for "edit row".
        var trEdit = tbodyShowEdit.find('tr').eq(1);
        trEdit.attr('ng-show', 'isEditMode(data.id)');
        trEdit.attr('ng-form', '');
        trEdit.attr('name', 'editForm');
        trEdit.append('<td class="td-icon"><save-button ng-show="editForm.$valid" ng-click="update(data)"></save-button></td>');
        trEdit.append('<td class="td-icon"><edit-button ng-click="changeEditMode(data.id)"></edit-button></td>');
    };

    return {
        restrict: 'A',
        controller: extendedTableController,
        compile: extendedTableCompile
    };
}])