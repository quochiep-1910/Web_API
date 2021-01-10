(function (app) {
    app.controller('producteCategoryListController', producteCategoryListController);

    producteCategoryListController.$inject = ['$scope', 'apiService', 'notificationService']; //khởi tạo tự động các đối tượng service

    function producteCategoryListController($scope, apiService, notificationService) { //tự động nhận các thông số trên
        $scope.productCategories = [];

        $scope.page = 0;
        $scope.pagesCount = 0;

        $scope.getProductCategories = getProductCategories;
        $scope.keyword = '';

        $scope.search = search;

        function search() {
            getProductCategories();
        }

        function getProductCategories(page) {
            page = page || 0; //nếu page nếu page ko có giá trị thì thay bằng 0

            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 3
                }
            }
            apiService.get('/api/productcategory/getall', config, function (result) { //dùng apiService để gọi
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                else {
                    notificationService.displaySuccess('Đã tìm thấy ' + result.data.TotalCount + ' bản ghi');
                }
                $scope.productCategories = result.data.Items; //nhận kết quả từ reponse
                $scope.page = result.data.Page;                  //nhận giá trị tự api/productcategory/getall
                $scope.pagesCount = result.data.TotalPages;        //nhận giá trị tự api/productcategory/getall
                $scope.totalCount = result.data.TotalCount;      //nhận giá trị tự api/productcategory/getall
            }, function () {
                console.log('Load productcategory failed.'); //ghi log
            })
        }
        $scope.getProductCategories(); //chạy khi controller khởi tạo
    }
})(angular.module('grocery.product_categories'));