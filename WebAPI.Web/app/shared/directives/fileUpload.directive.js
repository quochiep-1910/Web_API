(function (app) {
    'use strict';
    app.directive('fileUpload', function () {
        return {
            scope: true,        //create a new scope
            link: function (scope, el, attrs) {
                el.bind('change', function (event) {
                    var files = event.target.files;
                    // lặp lại các tệp vì 'multiple' có thể được chỉ định trên phần tử
                    for (var i = 0; i < files.length; i++) {
                        //emit event upward
                        scope.$emit("fileSelected", { file: files[i] });
                    }
                });
            }
        };
    });
})(angular.module('grocery.common'));