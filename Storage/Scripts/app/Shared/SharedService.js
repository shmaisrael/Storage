angular.module('sharedServices').service('NetworkService', ['$http', function ($http) {
    var postify = function (value) {
        var result = {};
        var buildResult = function (object, prefix) {
            for (var key in object) {
                var postKey = isFinite(key)
                    ? (prefix != "" ? prefix : "") + "[" + key + "]"
                    : (prefix != "" ? prefix + "." : "") + key;
                switch (typeof (object[key])) {
                    case "number": case "string": case "boolean":
                        result[postKey] = object[key];
                        break;
                    case "object":
                        if (object[key].toUTCString)
                            result[postKey] = object[key].toUTCString().replace("UTC", "GMT");
                        else {
                            buildResult(object[key], postKey != "" ? postKey : key);
                        }
                }
            }
        };
        buildResult(value, "");
        return result;
    };

    return {
        remove: function (name, params) {
            return $http({
                url: name + '/Remove' + name,
                method: 'POST',
                params: postify(params)
            });
        },
        get: function (name, params) {
            return $http({
                url: name + '/Get' + name,
                method: 'POST'
            });
        },
        update: function (name, params) {
            return $http({
                url: name + '/Update' + name,
                method: 'POST',
                params: postify(params)
            });
        },
        create: function (name, params) {
            return $http({
                url: name + '/Create' + name,
                method: 'POST',
                params: postify(params)
            });
        },
        custom: function (url, params) {
            return $http({
                url: url,
                method: 'POST',
                params: postify(params)
            });
        }
    };
}]).service('ObjectDifference', function () {
    function getChanges(prev, now) {
        var changes = {}, prop;
        for (prop in now) {
            if (!prev || prev[prop] !== now[prop]) {
                if (now[prop] instanceof Array) {
                    changes[prop] = now[prop];
                } else if (typeof now[prop] == "object") {
                    if (change = getChanges(prev[prop], now[prop]))
                        changes[prop] = change;
                } else {
                    changes[prop] = now[prop];
                }
            }
        }
        for (prop in changes)
            return changes;
        return false;
    };
    return {
        getChanges: function(prev, now) {
            var result = getChanges(prev, now);
            result["id"] = now["id"];
            return result;
        }
    }
});