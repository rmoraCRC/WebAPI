(function () {
    "use strict";

    angular
        .module("common", ["ngRoute", "chieffancypants.loadingBar", "ui.bootstrap", "formValidation", "parseDateTime",
            "modalConfirm", "kendo.directives", "base64", "ngAnimate", "toastr"])
        .factory("viewModelHelper", [
            "$http", "$q", "$window", "$location", "$timeout", "$uibModal", "$base64", "toastr",
    function ($http, $q, $window, $location, $timeout, $uibModal, $base64, toastr) {
        return window.App.viewModelHelper($http, $q, $window, $location, $timeout, $uibModal, $base64, toastr);
    }
        ]);

    angular
        .module("main", ["common"]);
    window.App.viewModelHelper = function ($http, $q, $window, $location, $timeout, $uibModal, $base64, toastr) {

        var selfViewModelHelper = this;

        function addAlert(alerts, type, msg) {
            return alerts.push({ 'type': type, 'msg': msg });
        };

        var headerConfig = {
            headers: {
                'Token': '6434da9e-e8d4-4206-80a6-b7bdcd1dca65'
            }
        };

        selfViewModelHelper.apiGet = function (uri, data, success, failure, always) {
            $http.get(uri, headerConfig, data)
                .then(function (result) { selfViewModelHelper.successCallback(result, success, always); },
                    function (result) { selfViewModelHelper.errorCallback(result, failure, always); });
        }

        selfViewModelHelper.apiPost = function (uri, data, success, failure, always) {
            $http.post(uri, data, headerConfig)
                .then(function (result) { selfViewModelHelper.successCallback(result, success, always); },
                    function (result) { selfViewModelHelper.errorCallback(result, failure, always); });
        }

        selfViewModelHelper.apiPostGenarateToken = function (uri, data, headerAuthConfig, success, failure, always) {
            $http.post(uri, data, headerAuthConfig)
                .then(function (result) { selfViewModelHelper.successCallback(result, success, always); },
                    function (result) { selfViewModelHelper.errorCallback(result, failure, always); });
        }

        selfViewModelHelper.apiPut = function (uri, data, success, failure, always) {
            $http.put(uri, data, headerConfig)
                .then(function (result) { selfViewModelHelper.successCallback(result, success, always); },
                    function (result) { selfViewModelHelper.errorCallback(result, failure, always); });
        }

        selfViewModelHelper.apiDelete = function (uri, data, success, failure, always) {
            $http.delete(uri, { headers: { 'Token': '6434da9e-e8d4-4206-80a6-b7bdcd1dca65', body: data, 'Content-Type': 'application/json; charset=utf-8' } })
                .then(function (result) { selfViewModelHelper.successCallback(result, success, always); },
                    function (result) { selfViewModelHelper.errorCallback(result, failure, always); });
        }

        selfViewModelHelper.successCallback = function (result, success, always) {
            success(result);
            if (always != null) {
                always();
            }
        }

        selfViewModelHelper.closeAlert = function (alerts, index) {
            alerts.splice(index, 1);
        };

        selfViewModelHelper.errorCallback = function (result, failure, always) {
            var alerts = [];
            if (result.status < 0) {
                addAlert(alerts, "warning", "No internet connectivity detected. Please reconnect and try again.");
            } else {
                if (result.data != null && result.data.InnerException.Message != null) {
                    var msg = result.data.InnerException.Message.replace("_UNIQUE", "");
                    addAlert(alerts, "danger", msg);
                }
                if (failure != null) {
                    failure(alerts);
                }
                if (always != null) {
                    always();
                }
            }
        }

        selfViewModelHelper.navigateTo = function (path) {
            $location.path(window.App.rootPath + path);
        }

        selfViewModelHelper.refreshPage = function (path) {
            $window.location.href = window.App.rootPath + path;
        }

        selfViewModelHelper.clone = function (obj) {
            return JSON.parse(JSON.stringify(obj));
        }

        selfViewModelHelper.openModal = function (templateUrl) {
            selfViewModelHelper.modalInstance = $uibModal.open({
                templateUrl: templateUrl
            });

            selfViewModelHelper.cancelModal = function () {
                selfViewModelHelper.modalInstance.dismiss('cancel');
            };

            selfViewModelHelper.closeModal = function (item) {
                selfViewModelHelper.modalInstance.close(item);
            };

        }

        selfViewModelHelper.getGridDataItem = function ($event, $scope) {
            var sender = $event.currentTarget;

            var row = angular.element(sender).closest("tr");

            return $scope.kendo.myGrid.dataItem(row);
        }

        selfViewModelHelper.Encode = function (stringToEnconde) {
            return $base64.encode(stringToEnconde);
        }

        selfViewModelHelper.Decode = function (stringToEnconde) {
            return $base64.decode(stringToEnconde);
        }

        selfViewModelHelper.successMessage = function (tilteMessage,messageToShow) {
            toastr.success(messageToShow, tilteMessage);
        }

        selfViewModelHelper.infoMessage = function (tilteMessage, messageToShow) {
            toastr.info(messageToShow, tilteMessage);
        }
 
        selfViewModelHelper.errorMessage = function (tilteMessage, messageToShow) {
            toastr.error(messageToShow, tilteMessage);
        }

        selfViewModelHelper.warningMessage = function (tilteMessage, messageToShow) {
            toastr.warning(messageToShow, tilteMessage);
        }

        return this;
    };

})();





