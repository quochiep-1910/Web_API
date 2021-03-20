/// <reference path="../../../assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('grocery.menus', ['grocery.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'] //tiêm thuộc tính có sẵn trong angular
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('menus', { //khai báo cho đường dẫn admin và dùng controller
            url: "/menus",
            parent: 'base',
            templateUrl: "/app/components/menu/menusListView.html",
            controller: "menusListController"
        }).state('menus_add', { //khai báo cho đường dẫn admin và dùng controller
            url: "/menus_add", //trỏ tới url này thì nó chuyển vào view html bên dưới
            templateUrl: "/app/components/menu/menusAddView.html",
            parent: 'base',
            controller: "menusAddController" //nó tự chạy js controller này
        }).state('menus_edit', { //khai báo cho đường dẫn admin và dùng controller
            url: "/menus_edit/:id", //trỏ tới url này thì nó chuyển vào view html bên dưới
            parent: 'base',
            templateUrl: "/app/components/menu/menusEditView.html",
            controller: "menusEditController" //nó tự chạy js controller này
        });
    }
})(); 