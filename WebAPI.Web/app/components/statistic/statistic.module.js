/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('grocery.statistic', ['grocery.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'] //tiêm thuộc tính có sẵn trong angular
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('statistic_revenue', { //khai báo cho đường dẫn admin và dùng controller
            url: "/statistic_revenue",
            parent: 'base',
            templateUrl: "/app/components/statistic/revenueStatisticView.html",
            controller: "revenueStatisticController"
        });
    }
})(); 