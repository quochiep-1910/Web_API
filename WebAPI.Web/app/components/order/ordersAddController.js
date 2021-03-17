(function (app) {
    app.controller('ordersAddController', ordersAddController);

    ordersAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService'];
    //$state dùng để điều hướng
    function ordersAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.orders = {
            CreatedDate: new Date()
        }

        $scope.Addorders = Addorders;

        function Addorders() {
            apiService.post('/api/order/create', $scope.orders,
                function (result) {
                    notificationService.displaySuccess(result.data.CustomerName + 'đã thêm mới');
                    $state.go('orders');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        }
    }
})(angular.module('grocery.orders'));