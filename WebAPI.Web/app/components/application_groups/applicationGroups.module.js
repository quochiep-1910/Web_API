/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('grocery.application_groups', ['grocery.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'] //tiêm thuộc tính có sẵn trong angular
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('application_groups', { //khai báo cho đường dẫn admin và dùng controller
                url: "/application_groups",//trỏ tới url này thì nó chuyển vào view html bên dưới
                templateUrl: "/app/components/application_groups/applicationGroupListView.html",
                parent: 'base',
                controller: "applicationGroupListController" //nó tự chạy js controller này
            })
            .state('add_application_group', { //khai báo cho đường dẫn admin và dùng controller
                url: "/add_product_category",//trỏ tới url này thì nó chuyển vào view html bên dưới
                templateUrl: "/app/components/application_groups/applicationGroupAddView.html",
                parent: 'base',
                controller: "applicationGroupAddController" //nó tự chạy js controller này
            })
            .state('edit_application_group', { //khai báo cho đường dẫn admin và dùng controller
                url: "/edit_product_category/:id",//trỏ tới url này thì nó chuyển vào view html bên dưới
                templateUrl: "/app/components/application_groups/applicationGroupEditView.html",
                parent: 'base',
                controller: "applicationGroupEditController" //nó tự chạy js controller này
            });
    }
})();