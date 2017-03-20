(function () {
    "use strict";

    applicationListController.$inject = ["applicationsService", "$routeParams", "viewModelHelper"];

    function applicationListController(applicationsService, $routeParams, viewModelHelper) {

        var self = this;

        self.$onInit = function () {
            loadApplications();
        }

        self.$onDestroy = function () {
        }

        self.edit = function (applicationId) {
           // applicationsService.navigateToApplication(applicationId);
        };

        self.add = function () {
            alert(self.applications);
        }

        function loadApplications() {

            viewModelHelper.apiGet("http://localhost/SecurityAppApi/api/Application",
                null,
                function (result) {
                    self.applications = result.data;
                });
        }
    }

    angular
        .module("applications")
        .component("applicationList", {
            templateUrl: window.App.rootPath + "Applications/Components/ApplicationList.cshtml?v=" + window.App.version,
            controller: applicationListController
        });
})();