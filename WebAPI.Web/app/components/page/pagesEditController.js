(function (app) {
    app.controller('pagesEditController', pagesEditController);

    pagesEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService', '$stateParams'];
    //$state dùng để điều hướng
    function pagesEditController($scope, apiService, notificationService, $state, commonService, $stateParams) {
        $scope.pages = {};
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }
        $scope.Updatepages = Updatepages;
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            //tự động chuyển name thành alias
            $scope.pages.Alias = commonService.getSeoTitle($scope.pages.Name);
        }
        function Updatepages() {
            apiService.put('/api/pages/update', $scope.pages,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã cập nhập thành công');
                    $state.go('pages');
                }, function (error) {
                    notificationService.displayError('cập nhập không thành công');
                });
        }

        function loadpagesDetail() {//load giá trị lên để modify
            apiService.get('/api/pages/getbyid/' + $stateParams.id, null, function (result) {
                $scope.pages = result.data;//lấy dữ liệu
                $scope.moreImages = JSON.parse($scope.pages.MoreImages); //chuyển moreImages sang dạng mảng.
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        $scope.ChooseImage = function () {//chọn 1 ảnh
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.pages.Image = fileUrl;
                })
            }
            finder.popup();
        }

        loadpagesDetail();
    }
})(angular.module('grocery.pages'));