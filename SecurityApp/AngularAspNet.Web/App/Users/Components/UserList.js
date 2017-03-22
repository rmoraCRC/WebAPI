(function () {
    "use strict";

    userListController.$inject = ["userService", "$routeParams", "viewModelHelper", "$timeout", "$scope"];

    function userListController(userService, $routeParams, viewModelHelper, $timeout, $scope) {

        var selfUserListController = this;

        var urlWebApiUser = "http://localhost/SecurityAppApi/Api/User/";

        var urlTokenAuthApiUser = "http://localhost/SecurityAppApi/Api/Authenticate";

        var urlCreateOrUpdateTemplate = "/Users/Template/UserCreateOrUpdate.tmpl.cshtml";

        selfUserListController.alerts = [];

        selfUserListController.$onInit = function () {
            selfUserListController.loadUsers();
        }

        selfUserListController.$onDestroy = function () {
        }

        selfUserListController.edit = function ($event) {
            userService.userId = viewModelHelper.getGridDataItem($event, $scope).idUser;
            viewModelHelper.openModal(urlCreateOrUpdateTemplate);
        };

        selfUserListController.add = function () {
            userService.userId = 0;
            viewModelHelper.openModal(urlCreateOrUpdateTemplate);
            selfUserListController.loadUsers();
        }

        selfUserListController.delete = function (user) {
            viewModelHelper.apiDelete(urlWebApiUser, angular.toJson(user),
             function (result) {
                 selfUserListController.loadUsers();
             }, function (alerts) {
                 selfUserListController.alerts = alerts;
             });
        }

        selfUserListController.loadUsers = function () {
            viewModelHelper.apiGet(urlWebApiUser,
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

        selfUserListController.createToken = function (user) {
            var headerAuthConfig = {
                headers: {
                    'Authorization': 'Basic ' + viewModelHelper.Encode(user.userName + ":" + user.password)
                }
            };

            viewModelHelper.apiPostGenarateToken(urlTokenAuthApiUser, null, headerAuthConfig,
               function (result) {
                   viewModelHelper.successMessage("Successful", "The Token Has Been Created!");
               },
               function (alerts) {
                   viewModelHelper.errorMessage("Error", "The Token Has Not Been Created!");
                   selfUserListController.alerts = alerts;
               });

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
    .component("userList", {
        templateUrl: window.App.rootPath + "Users/Components/UserList.cshtml?v=" + window.App.version,
        controller: userListController
    });

})();