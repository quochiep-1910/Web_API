/// <reference path="../../../assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('grocery.slides', ['grocery.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'] //tiêm thuộc tính có sẵn trong angular
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('slides', { //khai báo cho đường dẫn admin và dùng controller
            url: "/slides",
            parent: 'base',
            templateUrl: "/app/components/slide/slidesListView.html",
            controller: "slidesListController"
        }).state('slides_add', { //khai báo cho đường dẫn admin và dùng controller
            url: "/slides_add", //trỏ tới url này thì nó chuyển vào view html bên dưới
            templateUrl: "/app/components/slide/slidesAddView.html",
            parent: 'base',
            controller: "slidesAddController" //nó tự chạy js controller này
        }).state('slides_edit', { //khai báo cho đường dẫn admin và dùng controller
            url: "/slides_edit/:id", //trỏ tới url này thì nó chuyển vào view html bên dưới
            parent: 'base',
            templateUrl: "/app/components/slide/slidesEditView.html",
            controller: "slidesEditController" //nó tự chạy js controller này
        });
    }
})(); 