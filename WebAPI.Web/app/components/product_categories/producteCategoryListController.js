(function (app) {
    app.controller('producteCategoryListController', producteCategoryListController);

    producteCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter']; //khởi tạo tự động các đối tượng service

    function producteCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) { //tự động nhận các thông số trên
        $scope.productCategories = [];

        $scope.page = 0;
        $scope.pagesCount = 0;

        $scope.getProductCategories = getProductCategories;
        $scope.keyword = '';

        $scope.search = search;
        $scope.deleteProductCategory = deleteProductCategory;
        $scope.selectAll = selectAll;
        

        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {//func này được byding vào trong ng-click html
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            });
            var config = {
                params: {//truyền params từ /api/productcategory/deletenulti xuống
                    checkedProductCategories: JSON.stringify(listId) //chuyển qua chuỗi JSON
                }
            }
            apiService.del('/api/productcategory/deletenulti', config, function (result) {
                notificationService.displaySuccess('Xoá thành công ' + result.data + ' bản ghi');
                search();
            }, function (error) {
                notificationService.displayError('Xoá không thành công');
            });
        }

        $scope.isAll = false; //mặc định là false
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.productCategories, function (item) { //duyệt foreach rồi gắn giá trị true nếu được checked
                    item.checked = true;
                });
                $scope.isAll = true;
            } else { //ngược lại xoá hết
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        //lắng nghe sự thay đổi của productCategories thông qua watch để tuỳ biến nút xoá hiện thị lên
        $scope.$watch("productCategories", function (n, o) { //n ,o là giá trị new và old
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteProductCategory(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xoá?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/productcategory/delete', config, function () {
                    //xoá thành công
                    notificationService.displaySuccess('Xoá thành công');
                    search(); //gọi là hàm search (cập nhập lại)
                }, function () {
                    //false
                    notificationService.displayError('Xoá không thành công');
                })
            });
        }

        //function ChangeStatus(id) {
        //    var config = {
        //        params: {
        //            id: id
        //        }
        //    }

        //    apiService.put('/api/productcategory/changestatus', config, function () {
        //        notificationService.displaySuccess('Xoá thành công');
        //        search();
        //    }, function () {
        //        notificationService.displayError('Xoá không thành công');
        //    });
        //}

        function search() {
            getProductCategories();
        }

        function getProductCategories(page) {
            page = page || 0; //nếu page nếu page ko có giá trị thì thay bằng 0

            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 10
                }
            }
            $scope.loading = true;
            apiService.get('/api/productcategory/getall', config, function (result) { //dùng apiService để gọi
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                //else {
                //    notificationService.displaySuccess('Đã tìm thấy ' + result.data.TotalCount + ' bản ghi');
                //}
                $scope.productCategories = result.data.Items; //nhận kết quả từ reponse
                $scope.page = result.data.Page;                  //nhận giá trị tự api/productcategory/getall
                $scope.pagesCount = result.data.TotalPages;        //nhận giá trị tự api/productcategory/getall
                $scope.totalCount = result.data.TotalCount;      //nhận giá trị tự api/productcategory/getall
                $scope.loading = false;
            }, function () {
                console.log('Tải danh mục sản phẩm không thành công.'); //ghi log
            })
        }
        $scope.getProductCategories(); //chạy khi controller khởi tạo
    }
})(angular.module('grocery.product_categories'));