(function () {
    "use strict";

    userAddOrEditController.$inject = ["$scope", "userService", "$routeParams", "viewModelHelper"];

    function userAddOrEditController($scope, userService, $routeParams, viewModelHelper) {

        var selfUserAddOrEditController = this;

        selfUserAddOrEditController.alerts = [];

        selfUserAddOrEditController.user = {
            idUser: parseInt($routeParams.userId) || parseInt(userService.userId) || 0
        };

        selfUserAddOrEditController.$onInit = function () {
            if (selfUserAddOrEditController.user.idUser > 0) {
                selfUserAddOrEditController.refreshUser();
            }
        }

        selfUserAddOrEditController.$onDestroy = function () {
        }

        selfUserAddOrEditController.refreshUser = function () {
            viewModelHelper.apiGet("http://localhost/SecurityAppApi/Api/User/" + selfUserAddOrEditController.user.idUser,
            null,
            function (result) {
                selfUserAddOrEditController.user = result.data;
            },
            function (alerts) {
                selfUserAddOrEditController.alerts = alerts;
            });
        }

        selfUserAddOrEditController.save = function () {
            if (selfUserAddOrEditController.user.idUser > 0) {
                viewModelHelper.apiPut("http://localhost/SecurityAppApi/Api/User/", selfUserAddOrEditController.user,
                function (result) {
                    userService.navigateToUserList();
                    viewModelHelper.closeModal(result);
                },
                function (alerts) {
                    selfUserAddOrEditController.alerts = alerts;
                });
            }
            else {
                viewModelHelper.apiPost("http://localhost/SecurityAppApi/Api/User/", selfUserAddOrEditController.user,
                function (result) {
                    viewModelHelper.closeModal(result);
                },
                function (alerts) {
                    selfUserAddOrEditController.alerts = alerts;
                });
            }
        }

        selfUserAddOrEditController.cancelModal = function () {
            viewModelHelper.cancelModal();
        }

        selfUserAddOrEditController.closeAlert = function (index) {
            viewModelHelper.closeAlert(selfUserAddOrEditController.alerts, index);
        }
        selfUserAddOrEditController.backToList = function () {
            viewModelHelper.navigateTo("users");
        }
    }

    angular
        .module("users")
        .component("userCreateOrUpdate",
        {
            templateUrl: window.App.rootPath + "Users/Components/UserCreateOrUpdate.cshtml?v=" + window.App.version,
            controller: userAddOrEditController
        });
})();