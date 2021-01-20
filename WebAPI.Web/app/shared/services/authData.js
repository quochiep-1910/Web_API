//chứa thông tin đăng nhập, xem user đăng nhập hay chưa

//cách viết dưới đây là cách viết mới được bao bởi dấu []
(function (app) {
    'use strict';
    app.factory('authData', [
        function () {
            var authDataFactory = {};

            var authentication = {
                IsAuthenticated: false, //mặc định chưa có thông tin đăng nhập
                userName: ""
            };
            authDataFactory.authenticationData = authentication; //gán giá trị

            return authDataFactory;
        }
    ]);
})(angular.module('grocery.common'));