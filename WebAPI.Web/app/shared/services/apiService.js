/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function (app) {
    app.factory('apiService', apiService);
    apiService.$inject = ['$http']; //cần inject nếu ko thì khi chạy chữ $ đổi thành chữ e gây lỗi
    function apiService($http) {
        return {
            get: get //gọi lại func get ở dưới
        }
        function get(url, params, success, failure) {
            $http.get(url, params).then(function (result)//then là xủ lý xog sau khi gọi
            {
                success(result); //nhận thông tin trả
            }, function (error) {
                failure(error); //bắt lỗi
            });
        }
    }
})(angular.module('grocery.common'));