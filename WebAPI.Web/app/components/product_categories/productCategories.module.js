/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('grocery.product_categories', ['grocery.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'] //tiêm thuộc tính có sẵn trong angular
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('product_categories', { //khai báo cho đường dẫn admin và dùng controller
                url: "/product_categories",//trỏ tới url này thì nó chuyển vào view html bên dưới
                templateUrl: "/app/components/product_categories/productCategoryListView.html",
                parent: 'base',
                controller: "producteCategoryListController" //nó tự chạy js controller này
            })
            .state('add_product_category', { //khai báo cho đường dẫn admin và dùng controller
                url: "/add_product_category",//trỏ tới url này thì nó chuyển vào view html bên dưới
                templateUrl: "/app/components/product_categories/productCategoryAddView.html",
                parent: 'base',
                controller: "productCategoryAddController" //nó tự chạy js controller này
            })
            .state('edit_product_category', { //khai báo cho đường dẫn admin và dùng controller
                url: "/edit_product_category/:id",//trỏ tới url này thì nó chuyển vào view html bên dưới
                templateUrl: "/app/components/product_categories/productCategoryEditView.html",
                parent: 'base',
                controller: "productCategoryEditController" //nó tự chạy js controller này
            });
    }
})();