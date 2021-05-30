(function (app) {
    app.controller('menugroupsAddController', menugroupsAddController);

    menugroupsAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService'];
    //$state dùng để điều hướng
    function menugroupsAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.menugroups = {}

        $scope.Addmenugroups = Addmenugroups;

        function Addmenugroups() {
            apiService.post('/api/menugroup/create', $scope.menugroups,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + 'đã thêm mới');
                    $state.go('menugroups');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        }
    }
})(angular.module('grocery.menugroups'));