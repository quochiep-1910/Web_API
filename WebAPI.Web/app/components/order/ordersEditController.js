(function (app) {
    app.controller('ordersEditController', ordersEditController);

    ordersEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService', '$stateParams'];
    //$state dùng để điều hướng
    function ordersEditController($scope, apiService, notificationService, $state, commonService, $stateParams) {
        $scope.orders = {};
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }
        $scope.Updateorders = Updateorders;

        function Updateorders() {
            apiService.put('/api/order/update', $scope.orders,
                function (result) {
                    notificationService.displaySuccess(result.data.CustomerName + ' đã cập nhập thành công');
                    $state.go('orders');
                }, function (error) {
                    notificationService.displayError('cập nhập không thành công');
                });
        }

        function loadordersDetail() {//load giá trị lên để modify
            apiService.get('/api/order/getbyid/' + $stateParams.id, null, function (result) {
                $scope.orders = result.data;//lấy dữ liệu
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        loadordersDetail();
    }
})(angular.module('grocery.orders'));