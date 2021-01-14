(function (app) {
    app.controller('productEditController', productEditController);

    productEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService', '$stateParams'];
    //$state dùng để điều hướng
    function productEditController($scope, apiService, notificationService, $state, commonService, $stateParams) {
        $scope.product = {};
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }
        $scope.UpdateProduct = UpdateProduct;
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            //tự động chuyển name thành alias
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }
        function UpdateProduct() {
            apiService.put('/api/product/update', $scope.product,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + 'đã cập nhập thành công');
                    $state.go('products');
                }, function (error) {
                    notificationService.displayError('cập nhập không thành công');
                });
        }

        function loadProductDetail() {//load giá trị lên để modify
            apiService.get('api/product/getbyid/' + $stateParams.id, null, function (result) {
                $scope.product = result.data;//lấy dữ liệu
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }
        function loadProductCategory() {
            //gọi api getallparents ko truyền tham số
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.productCategories = result.data; // nhận kết quả trả về từ api
            }, function () {
                //false
                console.log('Cannot get list parent');
            });
        }
        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.product.Image = fileUrl;
            }
            finder.popup();
        }
        loadProductCategory();
        loadProductDetail();
    }
})(angular.module('grocery.products'));