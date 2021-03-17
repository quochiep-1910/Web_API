(function (app) {
    app.controller('slidesEditController', slidesEditController);

    slidesEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService', '$stateParams'];
    //$state dùng để điều hướng
    function slidesEditController($scope, apiService, notificationService, $state, commonService, $stateParams) {
        $scope.slides = {};
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }
        $scope.Updateslides = Updateslides;

        function Updateslides() {
            apiService.put('/api/slide/update', $scope.slides,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã cập nhập thành công');
                    $state.go('slides');
                }, function (error) {
                    notificationService.displayError('cập nhập không thành công');
                });
        }

        function loadslidesDetail() {//load giá trị lên để modify
            apiService.get('/api/slide/getbyid/' + $stateParams.id, null, function (result) {
                $scope.slides = result.data;//lấy dữ liệu
                $scope.moreImages = JSON.parse($scope.slides.MoreImages); //chuyển moreImages sang dạng mảng.
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        $scope.ChooseImage = function () {//chọn 1 ảnh
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.slides.Image = fileUrl;
                })
            }
            finder.popup();
        }

        loadslidesDetail();
    }
})(angular.module('grocery.slides'));