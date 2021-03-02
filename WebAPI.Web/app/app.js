/// <reference path="../assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('grocery',
        ['grocery.products',
            'grocery.application_groups',
            'grocery.product_categories',
            'grocery.application_roles',
            'grocery.pages',
            'grocery.application_users',
            'grocery.statistic',
            'grocery.common'])
        .config(config)
        .config(configAuthentication); //check xem mỗi request lên có kèm theo token hay không

    config.$inject = ['$stateProvider', '$urlRouterProvider'] //tiêm thuộc tính có sẵn trong angular

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('base', {//khai báo cho đường dẫn admin và dùng controller
                url: '',
                templateUrl: '/app/shared/views/baseView.html',
                abstract: true
            }).state('login', { //khai báo cho đường dẫn admin và dùng controller
                url: "/login",
                templateUrl: "/app/components/login/loginView.html",
                controller: "loginController"
            }).state('home', { //khai báo cho đường dẫn admin và dùng controller
                url: "/admin",
                parent: 'base',
                templateUrl: "/app/components/home/homeView.html",
                controller: "homeController"
            });
        $urlRouterProvider.otherwise('/login'); //otherwise là nếu ko thì trả về admin (đây coi như là trang đầu tiên)
    }
    function configAuthentication($httpProvider) {
        //quản trị phần tương tác giữa client với service
        //kiểm tra việc tương tác
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {
                    return config;
                },
                requestError: function (rejection) {
                    return $q.reject(rejection);
                },
                response: function (response) { //chưa đăng nhập sẽ trả về trang login
                    if (response.status == "401") {
                        $location.path('/login');
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {
                    if (rejection.status == "401") {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }
})();