(function () {
    'use strict';

    var app = angular.module('microCMS', []);

    angular.module('microCMS').controller('dashboardController', dashboardController);

    dashboardController.$inject = ['$scope', 'options'];

    function dashboardController($scope, options) {
        $scope.productImages = [];
        $scope.tags = [];
        
        //For image upload
        $scope.browseImage = function (textField) {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $("#" + textField + "").val(fileUrl);
            };
            finder.popup();
        }

        $scope.addProductImage = function () {
            $scope.productImages.push("");
        }

        $scope.removeProductImage = function (index) {
            $scope.productImages.splice(index, 1);
            if (!$scope.$$phase) $scope.$apply();
        }
        //End For image upload

        $scope.initProductImages = function() {
            for (var i = 0; i < 5; i++) {
                $scope.productImages.push("");
            }

            return $scope.productImages;
        }

        $scope.bindingProductImages = function() {
            $scope.productImages = options.productImages;
        }

        $scope.addTag = function (tagVal) {
            if (tagVal == undefined || tagVal.length === 0) {
                return;
            }

            $scope.tag = "";
            var index = $scope.tags.indexOf(tagVal);
            if (index < 0) {
                $scope.tags.push(tagVal);
            }
        }
    }

    app.directive("ckfinderBrowse", [function () {
        return {
            restrict: 'A',
            scope: {},
            link: function (scope, element, attrs) {
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

    app.directive("ngCkEditor",[function() {
        return {
            restrict: 'A',
            scope: {},
            link: function(scope, element, attrs) {
                CKEDITOR.replace(attrs.refId, {
                    toolbar: 'Full'
                });
            }
        }
    }]);
})();