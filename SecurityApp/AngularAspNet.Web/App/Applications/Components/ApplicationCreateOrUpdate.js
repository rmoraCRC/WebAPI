(function () {
    "use strict";

    applicationAddOrEditController.$inject = ["$scope", "applicationsService", "$routeParams", "viewModelHelper"];

    function applicationAddOrEditController($scope, applicationsService, $routeParams, viewModelHelper) {

        var applicationAddOrEditController = this;

        applicationAddOrEditController.alerts = [];

        var urlWebApiApplication = "http://localhost/SecurityAppApi/Api/Application";

        applicationAddOrEditController.application = {
            idApplication: parseInt($routeParams.idApplication) || parseInt(applicationsService.idApplication) || 0
        };

        applicationAddOrEditController.$onInit = function () {
            if (applicationAddOrEditController.application.idApplication > 0) {
                applicationAddOrEditController.refreshApplication();
            }
        }

        applicationAddOrEditController.$onDestroy = function () {
        }

        applicationAddOrEditController.refreshApplication = function () {
            viewModelHelper.apiGet(urlWebApiApplication + applicationAddOrEditController.application.idApplication,
            null,
            function (result) {
                applicationAddOrEditController.application = result.data;
            },
            function (alerts) {
                applicationAddOrEditController.alerts = alerts;
            });
        }

        applicationAddOrEditController.save = function () {
            if (applicationAddOrEditController.application.idApplication > 0) {
                viewModelHelper.apiPut(urlWebApiApplication, applicationAddOrEditController.application,
                function (result) {
                    applicationsService.navigateToApplicationList();
                    viewModelHelper.closeModal(result);
                },
                function (alerts) {
                    applicationAddOrEditController.alerts = alerts;
                });
            }
            else {
                viewModelHelper.apiPost(urlWebApiApplication, applicationAddOrEditController.application,
                function (result) {
                    viewModelHelper.closeModal(result);
                },
                function (alerts) {
                    applicationAddOrEditController.alerts = alerts;
                });
            }
        }

        applicationAddOrEditController.cancelModal = function () {
            viewModelHelper.cancelModal();
        }

        applicationAddOrEditController.closeAlert = function (index) {
            viewModelHelper.closeAlert(applicationAddOrEditController.alerts, index);
        }

        applicationAddOrEditController.backToList = function () {
            viewModelHelper.navigateTo("applications");
        }
    }

    angular
        .module("applications")
        .component("applicationCreateOrUpdate",
        {
            templateUrl: window.App.rootPath + "Applications/Components/ApplicationCreateOrUpdate.cshtml?v=" + window.App.version,
            controller: applicationAddOrEditController
        });
})();