/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function (app) {
    app.factory('apiService', apiService);

    apiService.$inject = ['$http', 'notificationService', 'authenticationService']; //cần inject nếu ko thì khi chạy chữ $ đổi thành chữ e gây lỗi

    function apiService($http, notificationService, authenticationService) {
        return {
            get: get, //gọi lại func get ở dưới
            post: post,
            put: put,
            del: del
        }
        function post(url, data, success, failure) {
            authenticationService.setHeader();
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

        function del(url, data, success, failure) {
            authenticationService.setHeader();
            $http.delete(url, data).then(function (result) {
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
            authenticationService.setHeader();
            $http.get(url, params).then(function (result)//then là xủ lý xog sau khi gọi
            {
                success(result); //nhận thông tin trả
            }, function (error) {
                console.log(error.status)
                failure(error); //bắt lỗi
            });
        }
        function put(url, data, success, failure) {
            authenticationService.setHeader();
            $http.put(url, data).then(function (result) {
                success(result);
            }, function (error) {
                console.log(error.status)
                if (error.status === 401) {
                    notificationService.displayError('Xác thực là bắt buộc.');
                }
                else if (failure != null) {
                    failure(error);
                }
            });
        }
    }
})(angular.module('grocery.common'));