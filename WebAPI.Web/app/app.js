/// <reference path="../assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('grocery', ['grocery.products', 'grocery.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'] //tiêm thuộc tính có sẵn trong angular
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('home', { //khai báo cho đường dẫn admin và dùng controller
            url: "/admin",
            templateUrl: "/app/components/home/homeView.html",
            controller: "homeController"
        });
        $urlRouterProvider.otherwise('/admin'); //otherwise là nếu ko thì trả về admin
    }
})();