(function (app) {
    app.controller('menusEditController', menusEditController);

    menusEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService', '$stateParams'];
    //$state dùng để điều hướng
    function menusEditController($scope, apiService, notificationService, $state, commonService, $stateParams) {
        $scope.menus = {};
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }
        $scope.Updatemenus = Updatemenus;
        $scope.GetUrl = GetUrl;
        $scope.flatFolders = [];

        $scope.target = ["_self", "_blank"];
        $scope.GetTarget = GetTarget;
        function GetTarget() {
            //dropdownlist Target
            $scope.menus.Target = $scope.target;
        }

        function GetUrl() {
            //tự động chuyển name thành URL
            $scope.menus.URL = commonService.getSeoTitle($scope.menus.Name);
        }
        function Updatemenus() {
            apiService.put('/api/menu/update', $scope.menus,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã cập nhập thành công');
                    $state.go('menus');
                }, function (error) {
                    notificationService.displayError('cập nhập không thành công');
                });
        }

        function loadmenusDetail() {//load giá trị lên để modify
            apiService.get('/api/menu/getbyid/' + $stateParams.id, null, function (result) {
                $scope.menus = result.data;//lấy dữ liệu
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }
        function loadParentId() {
            //gọi api getallparents ko truyền tham số
            apiService.get('/api/menu/getallparents', null, function (result) {
                $scope.parentId = commonService.getTree(result.data, "ID", "ParentID"); // nhận kết quả trả về từ api
                $scope.parentId.forEach(function (item) {
                    recur(item, 0, $scope.flatFolders);
                });
            }, function () {
                //false
                console.log('Cannot get list parent');
            });
        }
        function times(n, str) {
            var result = '';
            for (var i = 0; i < n; i++) {
                result += str;
            }
            return result;
        };
        function recur(item, level, arr) {
            arr.push({
                Name: times(level, '-') + ' ' + item.Name,
                ID: item.ID,
                Level: level,
                Indent: times(level, '-')
            });
            if (item.children) {
                item.children.forEach(function (item) {
                    recur(item, level + 1, arr);
                });
            }
        };
        loadParentId();
        loadmenusDetail();
    }
})(angular.module('grocery.menus'));