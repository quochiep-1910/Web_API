/// <reference path="../../../assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('grocery.orders', ['grocery.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'] //tiêm thuộc tính có sẵn trong angular
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('orders', { //khai báo cho đường dẫn admin và dùng controller
            url: "/orders",
            parent: 'base',
            templateUrl: "/app/components/order/ordersListView.html",
            controller: "ordersListController"
        }).state('orders_add', { //khai báo cho đường dẫn admin và dùng controller
            url: "/orders_add", //trỏ tới url này thì nó chuyển vào view html bên dưới
            templateUrl: "/app/components/order/ordersAddView.html",
            parent: 'base',
            controller: "ordersAddController" //nó tự chạy js controller này
        }).state('orders_edit', { //khai báo cho đường dẫn admin và dùng controller
            url: "/orders_edit/:id", //trỏ tới url này thì nó chuyển vào view html bên dưới
            parent: 'base',
            templateUrl: "/app/components/order/ordersEditView.html",
            controller: "ordersEditController" //nó tự chạy js controller này
        });
    }
})(); 