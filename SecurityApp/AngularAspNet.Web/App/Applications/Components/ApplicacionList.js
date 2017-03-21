(function () {
    "use strict";

    applicationListController.$inject = ["applicationsService", "$routeParams", "viewModelHelper", "$timeout", "$scope"];

    function applicationListController(applicationsService, $routeParams, viewModelHelper, $timeout, $scope) {

        var selfApplicatiosListController = this;

        selfApplicatiosListController.alerts = [];
        
        var urlCreateOrUpdateTemplate = "/Applications/Template/ApplicacionCreateOrUpdate.tmpl.cshtml";

        var urlWebApiApplication = "http://localhost/SecurityAppApi/Api/Application";

        selfApplicatiosListController.$onInit = function () {
            selfApplicatiosListController.loadApplications();
        }

        selfApplicatiosListController.$onDestroy = function () {
        }

        selfApplicatiosListController.edit = function ($event) {
            applicationsService.idApplication = viewModelHelper.getGridDataItem($event, $scope).idApplication;
            viewModelHelper.openModal(urlCreateOrUpdateTemplate);
        };

        selfApplicatiosListController.add = function () {
            applicationsService.idApplication = 0;
            viewModelHelper.openModal(urlCreateOrUpdateTemplate);
            selfApplicatiosListController.loadApplications();
        }

        selfApplicatiosListController.loadApplications = function () {
            viewModelHelper.apiGet(urlWebApiApplication,
                null,
                function (result) {
                    selfApplicatiosListController.applications = result.data;
                }, function (alerts) {
                    selfApplicatiosListController.alerts = alerts;
                });
        }

        selfApplicatiosListController.delete = function (application) {
            viewModelHelper.apiDelete(urlWebApiApplication, angular.toJson(application),
             function (result) {
                 selfApplicatiosListController.loadApplications();
             }, function (alerts) {
                 selfApplicatiosListController.alerts = alerts;
             });
        }

        selfApplicatiosListController.gridOptions = {
            scrollable: {
                virtual: true
            },
            sortable: true,
            selectable: true,
            height: 400,
            groupable: true,
            columns: [
                        { field: "idApplication", title: "Application Id" },
                        { field: "description", title: "Name" },
                {
                    field: "createDateTime", title: "Creation Date",
                    template: "#= kendo.toString(kendo.parseDate(createDateTime, 'yyyy-MM-dd'), 'MM/dd/yyyy') #"
                },
                        { field: "status", title: "Active", template: '<input type="checkbox" #= status ? "checked=checked" : "" # disabled>' },
                        {
                            template:
                                "<div class=\"btn-group\" role=\"group\" aria-label=\"Basic example\">"
                              + "<button class=\"btn btn-default\" ng-really-message=\"Do you want to Delete Application?\" type=\"button\" tooltip-placement=\"right\" uib-tooltip=\"Delete\"  ng-really-click=\"$ctrl.delete(dataItem)\">"
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
        .module("applications")
        .component("applicationList", {
            templateUrl: window.App.rootPath + "Applications/Components/ApplicationList.cshtml?v=" + window.App.version,
            controller: applicationListController
        });
})();