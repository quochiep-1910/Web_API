﻿(function (app) {
    app.controller('menusListController', menusListController);

    menusListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter']; //khởi tạo tự động các đối tượng service

    function menusListController($scope, apiService, notificationService, $ngBootbox, $filter) { //tự động nhận các thông số trên
        $scope.menus = [];

        $scope.page = 0;
        $scope.pagesCount = 0;

        $scope.getmenus = getmenus;
        $scope.keyword = '';

        $scope.search = search;
        $scope.deletemenus = deletemenus;
        $scope.selectAll = selectAll;

        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {//func này được byding vào trong ng-click html
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            });
            var config = {
                params: {//truyền params từ /api/menus/deletenulti xuống
                    checkedpages: JSON.stringify(listId) //chuyển qua chuỗi JSON
                }
            }
            apiService.del('/api/menu/deletenulti', config, function (result) {
                notificationService.displaySuccess('Xoá thành công ' + result.data + ' bản ghi');
                search();
            }, function (error) {
                notificationService.displayError('Xoá không thành công');
            });
        }

        $scope.isAll = false; //mặc định là false
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.menus, function (item) { //duyệt foreach rồi gắn giá trị true nếu được checked
                    item.checked = true;
                });
                $scope.isAll = true;
            } else { //ngược lại xoá hết
                angular.forEach($scope.menus, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        //lắng nghe sự thay đổi của menus thông qua watch để tuỳ biến nút xoá hiện thị lên
        $scope.$watch("menus", function (n, o) { //n ,o là giá trị new và old
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deletemenus(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xoá?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/menu/delete', config, function () {
                    //xoá thành công
                    notificationService.displaySuccess('Xoá thành công');
                    search(); //gọi là hàm search (cập nhập lại)
                }, function () {
                    //false
                    notificationService.displayError('Xoá không thành công');
                })
            });
        }

        function search() {
            getmenus();
        }

        function getmenus(page) {
            page = page || 0; //nếu page nếu page ko có giá trị thì thay bằng 0

            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 10
                }
            }
            apiService.get('/api/menu/getall', config, function (result) { //dùng apiService để gọi
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                //else {
                //    notificationService.displaySuccess('Đã tìm thấy ' + result.data.TotalCount + ' bản ghi');
                //}
                $scope.menus = result.data.Items; //nhận kết quả từ reponse
                $scope.page = result.data.Page;                  //nhận giá trị tự api/menus/getall
                $scope.pagesCount = result.data.TotalPages;        //nhận giá trị tự api/menus/getall
                $scope.totalCount = result.data.TotalCount;      //nhận giá trị tự api/menus/getall
            }, function () {
                console.log('Tải danh mục trang không thành công.'); //ghi log
            })
        }

        $scope.getmenus(); //chạy khi controller khởi tạo
    }
})(angular.module('grocery.menus'));