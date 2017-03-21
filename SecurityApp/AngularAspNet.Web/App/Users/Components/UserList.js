(function () {
    "use strict";

    userListController.$inject = ["userService", "$routeParams", "viewModelHelper", "$timeout", "$scope"];

    function userListController(userService, $routeParams, viewModelHelper, $timeout, $scope) {

        var selfUserListController = this;

        selfUserListController.alerts = [];

        selfUserListController.$onInit = function () {
            selfUserListController.loadUsers();
        }

        selfUserListController.$onDestroy = function () {
        }


        selfUserListController.edit = function ($event) {
            userService.userId = viewModelHelper.getGridDataItem($event).idUser;
            viewModelHelper.openModal("/Users/Template/UserCreateOrUpdate.tmpl.cshtml");
        };

        selfUserListController.add = function () {
            userService.userId = 0;
            viewModelHelper.openModal("/Users/Template/UserCreateOrUpdate.tmpl.cshtml");
            selfUserListController.loadUsers();
        }

        selfUserListController.delete = function (user) {
            viewModelHelper.apiDelete("http://localhost/SecurityAppApi/Api/User/", angular.toJson(user),
             function (result) {
                 selfUserListController.loadUsers();
             }, function (alerts) {
                 selfUserListController.alerts = alerts;
             });
        }

        selfUserListController.loadUsers = function () {
            viewModelHelper.apiGet("http://localhost/SecurityAppApi/Api/User",
                null,
                function (result) {
                    selfUserListController.users = result.data;
                }, function (alerts) {
                    selfUserListController.alerts = alerts;
                });
        }

        selfUserListController.closeAlert = function (index) {
            viewModelHelper.closeAlert(selfUserListController.alerts, index);
        }

        selfUserListController.gridOptions = {
            scrollable: {
                virtual: true
            },
            sortable: true,
            selectable: true,
            height: 400,
            groupable: true,
            columns: [
                        { field: "idUser", title: "User ID" },
                        { field: "userName", title: "UserName" },
                        { field: "name", title: "Name" },
                        { field: "lastName", title: "Last Name" },
                        { field: "status", title: "Active", template: '<input type="checkbox" #= status ? "checked=checked" : "" # disabled>' },
                        {
                            template:
                                "<div class=\"btn-group\" role=\"group\" aria-label=\"Basic example\">"
                              + "<button class=\"btn btn-default\" ng-really-message=\"Do you want to Delete user?\" type=\"button\" tooltip-placement=\"right\" uib-tooltip=\"Delete\"  ng-really-click=\"$ctrl.delete(dataItem)\">"
                              + "<span class=\"fa fa-minus-square-o\"></span>"
                              + "</button>"
                              + "<button class=\"btn btn-default\" type=\"button\" ng-click=\"$ctrl.edit($event)\" tooltip-placement=\"right\" uib-tooltip=\"Edit\" >"
                              + "<span class=\"fa fa-pencil-square-o\"></span>"
                              + "</button>"
                              + "</div>"
                        }
            ]
        }
    }

    angular
    .module("users")
    .component("userList", {
        templateUrl: window.App.rootPath + "Users/Components/UserList.cshtml?v=" + window.App.version,
        controller: userListController
    });

})();