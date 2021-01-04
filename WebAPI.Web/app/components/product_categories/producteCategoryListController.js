(function (app) {
    app.controller('producteCategoryListController', producteCategoryListController);

    producteCategoryListController.$inject = ['$scope', 'apiService'];

    function producteCategoryListController($scope, apiService) {
        $scope.productCategories = [];

        $scope.getProductCategories = getProductCategories;

        function getProductCategories() {
            apiService.get('/api/productcategory/getall', null, function (result) { //dùng apiService để gọi
                $scope.productCategories = result.data; //nhận kết quả từ reponse
            }, function () {
                console.log('Load productcategory failed.'); //ghi log
            })
        }
        $scope.getProductCategories(); //chạy khi controller khởi tạo
    }
})(angular.module('grocery.product_categories'));