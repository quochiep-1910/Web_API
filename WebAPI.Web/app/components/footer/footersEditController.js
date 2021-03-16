(function (app) {
    app.controller('footersEditController', footersEditController);

    footersEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService', '$stateParams'];
    //$state dùng để điều hướng
    function footersEditController($scope, apiService, notificationService, $state, commonService, $stateParams) {
        $scope.footers = {};
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }
        $scope.Updatefooters = Updatefooters;

        function Updatefooters() {
            apiService.put('/api/footer/update', $scope.footers,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã cập nhập thành công');
                    $state.go('footers');
                }, function (error) {
                    notificationService.displayError('cập nhập không thành công');
                });
        }

        function loadfootersDetail() {//load giá trị lên để modify
            apiService.get('/api/footer/getbyid/' + $stateParams.id, null, function (result) {
                $scope.footers = result.data;//lấy dữ liệu
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        loadfootersDetail();
    }
})(angular.module('grocery.footers'));