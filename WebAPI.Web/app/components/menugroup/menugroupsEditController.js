(function (app) {
    app.controller('menugroupsEditController', menugroupsEditController);

    menugroupsEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService', '$stateParams'];
    //$state dùng để điều hướng
    function menugroupsEditController($scope, apiService, notificationService, $state, commonService, $stateParams) {
        $scope.menugroups = {};
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }
        $scope.Updatemenugroups = Updatemenugroups;

        function Updatemenugroups() {
            apiService.put('/api/menugroup/update', $scope.menugroups,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã cập nhập thành công');
                    $state.go('menugroups');
                }, function (error) {
                    notificationService.displayError(' cập nhập không thành công');
                });
        }

        function loadmenugroupsDetail() {//load giá trị lên để modify
            apiService.get('/api/menugroup/getbyid/' + $stateParams.id, null, function (result) {
                $scope.menugroups = result.data;//lấy dữ liệu
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        loadmenugroupsDetail();
    }
})(angular.module('grocery.menugroups'));