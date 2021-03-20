(function (app) {
    app.controller('menusAddController', menusAddController);

    menusAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService'];
    //$state dùng để điều hướng
    function menusAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.menus = {}
        $scope.target = ["_self", "_blank"];

        $scope.Addmenus = Addmenus;
        $scope.GetUrl = GetUrl;
        $scope.flatFolders = [];

        $scope.GetTarget = GetTarget;
        function GetTarget() {
            //dropdownlist Target
            $scope.menus.Target = $scope.target;
        }
        function GetUrl() {
            //tự động chuyển name thành URL
            $scope.menus.URL = commonService.getSeoTitle($scope.menus.Name);
        }
        function Addmenus() {
            apiService.post('/api/menu/create', $scope.menus,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + 'đã thêm mới');
                    $state.go('menus');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
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
    }
})(angular.module('grocery.menus'));