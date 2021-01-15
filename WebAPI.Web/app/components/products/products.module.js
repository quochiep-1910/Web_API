/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('grocery.products', ['grocery.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'] //tiêm thuộc tính có sẵn trong angular
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('products', { //khai báo cho đường dẫn admin và dùng controller
            url: "/products",
            parent: 'base',
            templateUrl: "/app/components/products/productListView.html",
            controller: "productListController"
        }).state('product_add', { //khai báo cho đường dẫn admin và dùng controller
            parent: 'base',
            url: "/product_add", //trỏ tới url này thì nó chuyển vào view html bên dưới
            templateUrl: "/app/components/products/productAddView.html",
            controller: "productAddController" //nó tự chạy js controller này
        }).state('product_edit', { //khai báo cho đường dẫn admin và dùng controller
            parent: 'base',
            url: "/product_edit/:id", //trỏ tới url này thì nó chuyển vào view html bên dưới
            templateUrl: "/app/components/products/productEditView.html",
            controller: "productEditController" //nó tự chạy js controller này
        });
    }
})(); 