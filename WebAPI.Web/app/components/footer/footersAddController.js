(function (app) {
    app.controller('footersAddController', footersAddController);

    footersAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService'];
    //$state dùng để điều hướng
    function footersAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.footers = {}

        $scope.Addfooters = Addfooters;

        function Addfooters() {
            apiService.post('/api/footer/create', $scope.footers,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + 'đã thêm mới');
                    $state.go('footers');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        }
    }
})(angular.module('grocery.footers'));