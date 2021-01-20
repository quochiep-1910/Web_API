﻿(function (app) {
    'use strict';
    app.service('loginService', ['$http', '$q', 'authenticationService', 'authData', 'apiService',
        function ($http, $q, authenticationService, authData, apiService) {
            var userInfo;
            var deferred;

            this.login = function (userName, password) {
                deferred = $q.defer();
                var data = "grant_type=password&username=" + userName + "&password=" + password;
                $http.post('/oauth/token', data, {
                    headers:
                        { 'Content-Type': 'application/x-www-form-urlencoded' }
                }).then(function (response) {
                    userInfo = {
                        accessToken: response.data.access_token, //trả về token
                        userName: userName
                    };
                    //gán token vào user
                    authenticationService.setTokenInfo(userInfo);
                    authData.authenticationData.IsAuthenticated = true;
                    authData.authenticationData.userName = userName;
                    //authData.authenticationData.accessToken = userInfo.accessToken;

                    deferred.resolve(null);
                }, function (err, status) { //trả về lỗi
                    authData.authenticationData.IsAuthenticated = false;
                    authData.authenticationData.userName = "";
                    deferred.resolve(err);
                })
                return deferred.promise;
            }

            this.logOut = function () { //xoá token
                authenticationService.removeToken();
                authData.authenticationData.IsAuthenticated = false;
                authData.authenticationData.userName = "";
                authData.authenticationData.accessToken = "";
            }
        }]);
})(angular.module('grocery.common'));