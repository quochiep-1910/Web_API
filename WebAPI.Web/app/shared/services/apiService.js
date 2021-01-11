/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function (app) {
    app.factory('apiService', apiService);
    apiService.$inject = ['$http', 'notificationService']; //cần inject nếu ko thì khi chạy chữ $ đổi thành chữ e gây lỗi
    function apiService($http, notificationService) {
        return {
            get: get, //gọi lại func get ở dưới
            post: post,
            put: put
        }
        function post(url, data, success, failure) {
            $http.post(url, data).then(function (result) {
                success(result);
            }, function (error) {//bắt lỗi 401
                if (error.status === 401) {
                    notificationService.displayError('Xác thực là bắt buộc.');
                } //bắt lỗi từ api trả về
                else if (failure != null) {
                    failure(error);
                }
            });
        }
        function get(url, params, success, failure) {
            $http.get(url, params).then(function (result)//then là xủ lý xog sau khi gọi
            {
                success(result); //nhận thông tin trả
            }, function (error) {
                failure(error); //bắt lỗi
            });
        }
        function put(url, params, success, failure) {
            $http.put(url, params).then(function (result)//then là xủ lý xog sau khi gọi
            {
                success(result); //nhận thông tin trả
            }, function (error) {
                failure(error); //bắt lỗi
            });
        }
    }
})(angular.module('grocery.common'));