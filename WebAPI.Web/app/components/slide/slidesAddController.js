(function (app) {
    app.controller('slidesAddController', slidesAddController);

    slidesAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService'];
    //$state dùng để điều hướng
    function slidesAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.slides = {
            Status: false
        }

        $scope.Addslides = Addslides;

        function Addslides() {
            apiService.post('/api/slide/create', $scope.slides,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + 'đã thêm mới');
                    $state.go('slides');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        }
        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () { //apply là load lại ngay lập tức nếu có hình
                    $scope.slides.Image = fileUrl;
                })
            }
            finder.popup(); //lệnh bật của sổ của CKfinder
        }
    }
})(angular.module('grocery.slides'));