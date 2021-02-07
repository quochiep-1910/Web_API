(function (app) {
    app.controller('pagesAddController', pagesAddController);

    pagesAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService'];
    //$state dùng để điều hướng
    function pagesAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.pages = {
            CreatedDate: new Date(),
            Status: true
        }

        $scope.Addpages = Addpages;
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            //tự động chuyển name thành alias
            $scope.pages.Alias = commonService.getSeoTitle($scope.pages.Name);
        }
        function Addpages() {
            apiService.post('/api/pages/create', $scope.pages,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + 'đã thêm mới');
                    $state.go('pages');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        }
        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () { //apply là load lại ngay lập tức nếu có hình
                    $scope.pages.Image = fileUrl;
                })
            }
            finder.popup(); //lệnh bật của sổ của CKfinder
        }
    }
})(angular.module('grocery.pages'));