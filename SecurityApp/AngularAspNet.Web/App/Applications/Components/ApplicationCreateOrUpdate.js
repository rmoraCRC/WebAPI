(function () {
    "use strict";

    applicationAddOrEditController.$inject = ["$scope", "applicationsService", "$routeParams", "viewModelHelper"];

    function applicationAddOrEditController($scope, applicationsService, $routeParams, viewModelHelper) {

        var selfUserAddOrEditController = this;

        selfUserAddOrEditController.alerts = [];

        selfUserAddOrEditController.application = {
            idUser: parseInt($routeParams.idApplication) || parseInt(applicationsService.idApplication) || 0
        };

        selfUserAddOrEditController.$onInit = function () {
            if (selfUserAddOrEditController.application.idApplication > 0) {
                selfUserAddOrEditController.refreshApplication();
            }
        }

        selfUserAddOrEditController.$onDestroy = function () {
        }

        selfUserAddOrEditController.refreshApplication = function () {
            viewModelHelper.apiGet("http://localhost/SecurityAppApi/Api/Application/" + selfUserAddOrEditController.application.idApplication,
            null,
            function (result) {
                selfUserAddOrEditController.application = result.data;
            },
            function (alerts) {
                selfUserAddOrEditController.alerts = alerts;
            });
        }

        selfUserAddOrEditController.save = function () {
            if (selfUserAddOrEditController.application.idApplication > 0) {
                viewModelHelper.apiPut("http://localhost/SecurityAppApi/Api/Application/", selfUserAddOrEditController.application,
                function (result) {
                    applicationsService.navigateToApplicationList();
                    viewModelHelper.closeModal(result);
                },
                function (alerts) {
                    selfUserAddOrEditController.alerts = alerts;
                });
            }
            else {
                viewModelHelper.apiPost("http://localhost/SecurityAppApi/Api/Application/", selfUserAddOrEditController.application,
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