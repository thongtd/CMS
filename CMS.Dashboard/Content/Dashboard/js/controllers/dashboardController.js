(function () {
    'use strict';

    var app = angular.module('microCMS', []);

    angular.module('microCMS').controller('dashboardController', dashboardController);

    dashboardController.$inject = ['$scope', 'options'];

    function dashboardController($scope, options) {
        $scope.productImages = [];
        $scope.tags = [];
        $scope.questions = [];

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

        $scope.initProductImages = function () {
            for (var i = 0; i < 5; i++) {
                $scope.productImages.push("");
            }

            return $scope.productImages;
        }

        $scope.bindingProductImages = function () {
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

        $scope.initTagsForEdit = function () {
            $scope.tags = options.tags;
        }

        $scope.bindProductPrice = function(price) {
            $scope.productPrice = price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }

        $scope.productPriceChange = function () {
            var val = $scope.productPrice.replace(/,/g, '');
            $scope.productPrice = val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            $('#Price').val(val);
        }

        $scope.bindDiscountPrice = function (price) {
            $scope.discountPrice = price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }

        $scope.discountPriceChange = function () {
            var val = $scope.discountPrice.replace(/,/g, '');
            $scope.discountPrice = val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            $('#Discount').val(val);
        }

        $scope.initQuestions = function () {
            for (var i = 0; i < 1; i++) {
                $scope.questions.push("");
            }

            return $scope.questions;
        }

        $scope.bindingQuestion = function () {
            $scope.questions = options.questions;
        }

        $scope.addQuestion = function () {
            $scope.questions.push("");
            if (!$scope.$$phase) $scope.$apply();
        }

        $scope.removeQuestions = function (index) {
            $scope.questions.splice(index, 1);
            if (!$scope.$$phase) $scope.$apply();
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

    app.directive("ngCkEditor", [function () {
        return {
            restrict: 'A',
            scope: {},
            link: function (scope, element, attrs) {
                CKEDITOR.replace(attrs.refId, {
                    toolbar: 'Full'
                });
            }
        }
    }]);

    app.directive("ngCkEditorMini", [function () {
        return {
            restrict: 'A',
            scope: {},
            link: function (scope, element, attrs) {
                CKEDITOR.replace(attrs.refId, {
                    toolbar: [
	                    { name: 'links', items: ['Link', 'Unlink'] },
	                    { name: 'insert', items: ['Image', 'Table', 'SpecialChar'] },
	                    { name: 'tools', items: ['Maximize'] },
	                    { name: 'document', groups: ['mode', 'document', 'doctools'] },
	                    { name: 'others', items: ['-'] },
	                    { name: 'basicstyles', groups: ['basicstyles', 'cleanup'], items: ['Bold', 'Italic', 'Strike', '-', 'RemoveFormat'] },
	                    { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'], items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote'] },
	                    { name: 'styles', items: ['Styles', 'Format'] }
                    ],
                    height: '100px'
                });
            }
        }
    }]);
})();