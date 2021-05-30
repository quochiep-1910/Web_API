/// <reference path="../../../assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('grocery.menugroups', ['grocery.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'] //tiêm thuộc tính có sẵn trong angular
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('menugroups', { //khai báo cho đường dẫn admin và dùng controller
            url: "/menugroups",
            parent: 'base',
            templateUrl: "/app/components/menugroup/menugroupsListView.html",
            controller: "menugroupsListController"
        }).state('menugroups_add', { //khai báo cho đường dẫn admin và dùng controller
            url: "/menugroups_add", //trỏ tới url này thì nó chuyển vào view html bên dưới
            templateUrl: "/app/components/menugroup/menugroupsAddView.html",
            parent: 'base',
            controller: "menugroupsAddController" //nó tự chạy js controller này
        }).state('menugroups_edit', { //khai báo cho đường dẫn admin và dùng controller
            url: "/menugroups_edit/:id", //trỏ tới url này thì nó chuyển vào view html bên dưới
            parent: 'base',
            templateUrl: "/app/components/menugroup/menugroupsEditView.html",
            controller: "menugroupsEditController" //nó tự chạy js controller này
        });
    }
})(); 