﻿(function () {
    "use strict";

    userAddOrEditController.$inject = ["$scope", "userService", "$routeParams", "viewModelHelper"];

    function userAddOrEditController($scope, userService, $routeParams, viewModelHelper) {

        var selfUserAddOrEditController = this;

        var urlWebApiUser = "http://localhost/SecurityAppApi/Api/User/";

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
            viewModelHelper.apiGet(urlWebApiUser + selfUserAddOrEditController.user.idUser,
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
                viewModelHelper.apiPut(urlWebApiUser, selfUserAddOrEditController.user,
                function (result) {
                    userService.navigateToUserList();
                    viewModelHelper.closeModal(result);
                },
                function (alerts) {
                    selfUserAddOrEditController.alerts = alerts;
                });
            }
            else {
                viewModelHelper.apiPost(urlWebApiUser, selfUserAddOrEditController.user,
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

        selfUserAddOrEditController.gridOptions = {
            scrollable: {
                virtual: true
            },
            sortable: true,
            selectable: true,
            height: 400,
            groupable: true,
            columns: [
                        { field: "tokenId", title: "Token ID" },
                        { field: "authToken", title: "Token" },
                        { field: "status", title: "Active", template: '<input type="checkbox" #= status ? "checked=checked" : "" # disabled>' },
                        {
                            template:
                                "<div class=\"btn-group\" role=\"group\" aria-label=\"Basic example\">"
                              + "<button class=\"btn btn-default\" ng-really-message=\"Do you want to Delete user?\" type=\"button\" tooltip-placement=\"right\" uib-tooltip=\"Delete\"  ng-really-click=\"$ctrl.delete(dataItem)\">"
                              + "<span class=\"fa fa-minus-square-o\"></span>"
                              + "</button>"                             
                              + "</div>"
                        }
            ]
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