(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);

    productCategoryAddController.$inject = ['$scope', 'apiService'];
    function productCategoryAddController($scope, apiService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
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