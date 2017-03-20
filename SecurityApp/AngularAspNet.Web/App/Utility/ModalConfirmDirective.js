(function () {
    'use strict';

    angular
      .module("modalConfirm", [])
	  .directive('ngReallyClick', ['$uibModal',
    function ($uibModal) {
        var modalInstance;

        var modalInstanceCtrl = function ($scope) {
            $scope.ok = function () {
                modalInstance.close();
            };

            $scope.cancel = function () {
                modalInstance.dismiss('cancel');
            };
        };

        return {
            restrict: 'A',
            scope: {
                ngReallyClick: "&",
                item: "="
            },
            link: function (scope, element, attrs) {
                element.bind('click', function () {
                    var message = attrs.ngReallyMessage || "Are you sure ?";

                    var modalHtml = '<div class="modal-body">' + message + '</div>';
                    modalHtml += '<div class="modal-footer"><button class="btn btn-primary" ng-click="ok()">OK</button><button class="btn btn-warning" ng-click="cancel()">Cancel</button></div>';

                    modalInstance = $uibModal.open({
                        template: modalHtml,
                        controller: modalInstanceCtrl
                    });

                    modalInstance.result.then(function () {
                        scope.ngReallyClick({ item: scope.item });
                    }, function () {
                        modalInstance.dismiss('cancel');
                    });

                });

            }
        }
    }
	  ]);

})();