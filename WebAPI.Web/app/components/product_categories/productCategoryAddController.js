(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);

    productCategoryAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService'];
    //$state dùng để điều hướng
    function productCategoryAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }

        $scope.AddProductCategory = AddProductCategory;
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            //tự động chuyển name thành alias
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }
        function AddProductCategory() {
            apiService.post('/api/productcategory/create', $scope.productCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + 'đã thêm mới');
                    $state.go('product_categories');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        }
        function loadParentCategory() {
            //gọi api getallparents ko truyền tham số
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data; // nhận kết quả trả về từ api
            }, function () {
                //false
                console.log('Cannot get list parent');
            });
        }
        loadParentCategory();
    }
})(angular.module('grocery.product_categories'));