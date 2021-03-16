/// <reference path="../../../assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('grocery.footers', ['grocery.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'] //tiêm thuộc tính có sẵn trong angular
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('footers', { //khai báo cho đường dẫn admin và dùng controller
            url: "/footers",
            parent: 'base',
            templateUrl: "/app/components/footer/footersListView.html",
            controller: "footersListController"
        }).state('footers_add', { //khai báo cho đường dẫn admin và dùng controller
            url: "/footers_add", //trỏ tới url này thì nó chuyển vào view html bên dưới
            templateUrl: "/app/components/footer/footersAddView.html",
            parent: 'base',
            controller: "footersAddController" //nó tự chạy js controller này
        }).state('footers_edit', { //khai báo cho đường dẫn admin và dùng controller
            url: "/footers_edit/:id", //trỏ tới url này thì nó chuyển vào view html bên dưới
            parent: 'base',
            templateUrl: "/app/components/footer/footersEditView.html",
            controller: "footersEditController" //nó tự chạy js controller này
        });
    }
})(); 