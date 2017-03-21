(function () {
    "use strict";

    angular
        .module("applications", ["common"])
        .config(routeConfig)
        .factory("applicationsService", applicationsService);

    routeConfig.$inject = ["$routeProvider", "$locationProvider"];

    function routeConfig($routeProvider, $locationProvider) {

        var rootPath = window.App.rootPath;

        $routeProvider
            .when(rootPath + "applications", {
                templateUrl: "/Applications/Template/ApplicacionList.tmpl.cshtml",
                caseInsensitiveMatch: true
            })
             .when(rootPath + "applications/:applicationId",
            {
                templateUrl: "/Applications/Template/ApplicacionCreateOrUpdate.tmpl.cshtml",
                caseInsensitiveMatch: true
            })
            .otherwise({ redirectTo: (rootPath + "applications") });

        $locationProvider.html5Mode({ enabled: true, requireBase: false });
    }
    
    applicationsService.$inject = ["$rootScope", "$http", "$q", "$location", "viewModelHelper"];

    function applicationsService($rootScope, $http, $q, $location, viewModelHelper) {
        return window.App.applicationsService($rootScope, $http, $q, $location, viewModelHelper);
    }

    (function (app) {
        var applicationsService = function ($rootScope, $http, $q, $location, viewModelHelper) {
            var self = this;
            self.idApplication = 0;

            self.navigateToApplicationList = function () {
                viewModelHelper.navigateTo("applications");
            }                      

            self.navigateToApplication = function (idApplication) {
                viewModelHelper.navigateTo("users/" + idApplication);
            }

            self.navigateToHome = function () {
                viewModelHelper.navigateTo("home");
            }

            return this;
        };
        app.applicationsService = applicationsService;
    }(window.App));
})();