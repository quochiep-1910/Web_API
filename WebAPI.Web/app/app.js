/// <reference path="../assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('grocery', ['grocery.products', 'grocery.common',
        'grocery.product_categories',]).config(config);

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
})();