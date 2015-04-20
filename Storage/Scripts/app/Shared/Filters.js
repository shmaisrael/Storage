angular.module('filters').filter('name', [function () {
    return function (value) {
        return value ? value.name : '';
    };
}]).filter('multiSelectValues', [function () {
    return function (values) {
        var result = '';
        if (values) {
            values.forEach(function (entry, index) {
                result += index == values.length - 1 ? entry.name : entry.name + ', ';
            });
        }
        return result;
    };
}]).filter('countExist', [function () {
    return function (value) {
        return value != undefined ? value : '?';
    };
}]);