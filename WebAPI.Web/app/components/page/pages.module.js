/// <reference path="../../../assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('grocery.pages', ['grocery.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'] //tiêm thuộc tính có sẵn trong angular
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('pages', { //khai báo cho đường dẫn admin và dùng controller
            url: "/pages",
            parent: 'base',
            templateUrl: "/app/components/page/pagesListView.html",
            controller: "pagesListController"
        }).state('pages_add', { //khai báo cho đường dẫn admin và dùng controller
            url: "/pages_add", //trỏ tới url này thì nó chuyển vào view html bên dưới
            templateUrl: "/app/components/page/pagesAddView.html",
            parent: 'base',
            controller: "pagesAddController" //nó tự chạy js controller này
        }).state('pages_edit', { //khai báo cho đường dẫn admin và dùng controller
            url: "/pages_edit/:id", //trỏ tới url này thì nó chuyển vào view html bên dưới
            parent: 'base',
            templateUrl: "/app/components/page/pagesEditView.html",
            controller: "pagesEditController" //nó tự chạy js controller này
        });
    }
})(); 