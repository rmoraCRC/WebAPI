(function () {
    "use strict";

    tokenUserTokenOperationsController.$inject = ["$scope", "userService", "$routeParams", "viewModelHelper"];

    function tokenUserTokenOperationsController($scope, userService, $routeParams, viewModelHelper) {

        var selfUserAddOrEditController = this;

        var urlWebApiUser = "http://localhost/SecurityAppApi/Api/User?userId=";

        selfUserAddOrEditController.alerts = [];

        var idUser = parseInt($routeParams.userId) || parseInt(userService.userId) || 0;

        selfUserAddOrEditController.$onInit = function () {
            if (idUser > 0) {
                selfUserAddOrEditController.refreshUser();
            }
        }

        selfUserAddOrEditController.$onDestroy = function () {
        }

        selfUserAddOrEditController.refreshUser = function () {
            viewModelHelper.apiGet(urlWebApiUser + idUser,
            null,
            function (result) {
                selfUserAddOrEditController.tokens = result.data;
            },
            function (alerts) {
                selfUserAddOrEditController.alerts = alerts;
            });
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
            width: 400,
            groupable: true,
            columns: [                        
                        { field: "authToken", title: "Token" },
                        { field: "status", title: "Active", template: '<input type="checkbox" #= status ? "checked=checked" : "" # disabled>' },
                        {
                            template:
                                "<div class=\"btn-group\" role=\"group\" aria-label=\"Basic example\">"
                              + "<button class=\"btn btn-default\" ng-really-message=\"Do you want to Delete user?\" type=\"button\" tooltip-placement=\"right\" uib-tooltip=\"Delete\"  ng-really-click=\"$ctrl.delete(dataItem)\">"
                              + "<span class=\"fa fa-minus-square-o\"></span>"
                              + "</button>"
                              + "<button class=\"btn btn-default\" type=\"button\" ng-click=\"$ctrl.viewTokens($event)\" tooltip-placement=\"right\" uib-tooltip=\"Edit\" >"
                              + "<span class=\"fa fa-pencil-square-o\"></span>"
                              + "</button>"
                              + "<button class=\"btn btn-default\" type=\"button\" ng-click=\"$ctrl.createToken(dataItem)\" tooltip-placement=\"left\" uib-tooltip=\"New Token\" >"
                              + "<span class=\"fa fa-user-secret\"></span>"
                              + "</button>"
                              + "</div>"
                        }
            ]
        }
    }

    angular
        .module("users")
        .component("userTokenOperations",
        {
            templateUrl: window.App.rootPath + "Users/Components/UserTokenOperations.cshtml?v=" + window.App.version,
            controller: tokenUserTokenOperationsController
        });
})();