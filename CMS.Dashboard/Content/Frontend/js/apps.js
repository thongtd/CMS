var app = angular.module('cows', []);

app.controller('cowsController', function ($scope) {
    $scope.cartItems = [];

    $scope.addToCart = function (productId) {
        $.ajax({
            type: 'POST',
            url: '/add-to-cart',
            data: { id: productId },
            success: function (data) {
                $("#CartAmount").html(data.Items.Count);
                $scope.cartItems = data.Items;
                if (!$scope.$$phase) $scope.$apply();
            }
        });
    }

    $scope.removeFromCart = function (productId) {
        $.ajax({
            type: 'POST',
            url: '/remove-from-cart',
            data: { id: productId },
            success: function (data) {
                $("#CartAmount").html(data.Items.Count);
                $scope.cartItems = data.Items;
                if (!$scope.$$phase) $scope.$apply();
            }
        });
    }

    $scope.getCartItems = function() {
        $.ajax({
            type: 'POST',
            url: '/get-cart-items',
            success: function (data) {
                $scope.cartItems = data;
                if (!$scope.$$phase) $scope.$apply();
            }
        });
    }
});