(function () {
    "use strict";

    angular
        .module("users", ["common"])
        .config(routeConfig)
        .factory("userService", userService);

    routeConfig.$inject = ["$routeProvider", "$locationProvider"];

    function routeConfig($routeProvider, $locationProvider) {

        var rootPath = window.App.rootPath;

        $routeProvider
            .when(rootPath + "users", {
                templateUrl: "/Users/Template/UserList.tmpl.cshtml",
                caseInsensitiveMatch: true,
                requireLogin: true
            })
            .when(rootPath + "users/:userId",
            {
                templateUrl: "/Users/Template/UserCreateOrUpdate.tmpl.cshtml",
                caseInsensitiveMatch: true
            })
             .when(rootPath + "home", {
                 templateUrl: "/Users/Template/index.cshtml",
                 caseInsensitiveMatch: true
             })
            .otherwise({ redirectTo: (rootPath + "users") });

        $locationProvider.html5Mode({ enabled: true, requireBase: false });
    }
    
    userService.$inject = ["$rootScope", "$http", "$q", "$location", "viewModelHelper"];

    function userService($rootScope, $http, $q, $location, viewModelHelper) {
        return window.App.userService($rootScope, $http, $q, $location, viewModelHelper);
    }

    (function (app) {
        var userService = function ($rootScope, $http, $q, $location, viewModelHelper) {
            var self = this;

            self.userId = 0;

            self.navigateToUserList = function () {
                viewModelHelper.navigateTo("users");
            }

            self.navigateToUser = function (userId) {
                viewModelHelper.navigateTo("users/" + userId);
            }

            self.navigateToHome = function () {
                viewModelHelper.navigateTo("home");
            }            

            return this;
        };
        app.userService = userService;
    }(window.App));
})();