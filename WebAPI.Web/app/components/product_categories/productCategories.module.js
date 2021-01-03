/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('grocery.product_categories', ['grocery.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'] //tiêm thuộc tính có sẵn trong angular
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('product_categories', { //khai báo cho đường dẫn admin và dùng controller
            url: "/product_categories",//trỏ tới url này thì nó chuyển vào view html bên dưới
            templateUrl: "/app/components/product_categories/productCategoryListView.html",
            controller: "producteCategoryListController" //nó tự chạy js controller này
        });
    }
})();