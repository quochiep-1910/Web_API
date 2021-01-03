/// <reference path="../assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('grocery.products', ['grocery.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'] //tiêm thuộc tính có sẵn trong angular
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('products', { //khai báo cho đường dẫn admin và dùng controller
            url: "/products",
            templateUrl: "/app/components/products/productListView.html",
            controller: "productListController"
        }).state('product_add', { //khai báo cho đường dẫn admin và dùng controller
            url: "/product_add",
            templateUrl: "/app/components/products/productAddView.html",
            controller: "productAddController"
        });
    }
})();