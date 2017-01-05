﻿(function () {
    'use strict';

    var app = angular.module('microCMS', []);

    angular.module('microCMS').controller('dashboardController', dashboardController);

    dashboardController.$inject = ['$scope'];

    function dashboardController($scope) {
        $scope.productImages = [];
        for (var i = 0; i < 5; i++) {
            $scope.productImages.push("");
        }

        $scope.browseImage = function (textField) {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $("#" + textField + "").val(fileUrl);
            };
            finder.popup();
        }

        $scope.addProductImage = function() {
            $scope.productImages.push("");
        }

        $scope.removeProductImage = function (index) {
            $scope.productImages.splice(index, 1);
            if (!$scope.$$phase) $scope.$apply();
        }
    }

    app.directive("ckfinderBrowse", [function () {
        return {
            restrict: 'A',
            scope: {},
            link: function(scope, element, attrs) {
                element.bind('click', function () {
                    var finder = new CKFinder();
                    finder.selectActionFunction = function (fileUrl) {
                        $("#" + attrs.refId + "").val(fileUrl);
                    };
                    finder.popup();
                });
            }
        }
    }]);
})();