/// <reference path="../../../assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('grocery.feedbacks', ['grocery.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'] //tiêm thuộc tính có sẵn trong angular
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('feedbacks', { //khai báo cho đường dẫn admin và dùng controller
            url: "/feedbacks",
            parent: 'base',
            templateUrl: "/app/components/feedback/feedbacksListView.html",
            controller: "feedbacksListController"
        }).state('feedbacks_add', { //khai báo cho đường dẫn admin và dùng controller
            url: "/feedbacks_add", //trỏ tới url này thì nó chuyển vào view html bên dưới
            templateUrl: "/app/components/feedback/feedbacksAddView.html",
            parent: 'base',
            controller: "feedbacksAddController" //nó tự chạy js controller này
        }).state('feedbacks_edit', { //khai báo cho đường dẫn admin và dùng controller
            url: "/feedbacks_edit/:id", //trỏ tới url này thì nó chuyển vào view html bên dưới
            parent: 'base',
            templateUrl: "/app/components/feedback/feedbacksEditView.html",
            controller: "feedbacksEditController" //nó tự chạy js controller này
        });
    }
})(); 