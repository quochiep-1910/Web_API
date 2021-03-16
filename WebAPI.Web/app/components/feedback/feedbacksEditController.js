(function (app) {
    app.controller('feedbacksEditController', feedbacksEditController);

    feedbacksEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService', '$stateParams'];
    //$state dùng để điều hướng
    function feedbacksEditController($scope, apiService, notificationService, $state, commonService, $stateParams) {
        $scope.feedbacks = {};
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }
        $scope.Updatefeedbacks = Updatefeedbacks;

        //vô hiệu hoá message
        //var message = document.getElementById("mess");
        function Updatefeedbacks() {
            apiService.put('/api/feedback/update', $scope.feedbacks,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã cập nhập thành công');
                    $state.go('feedbacks');
                }, function (error) {
                    notificationService.displayError('cập nhập không thành công');
                });
        }

        function loadfeedbacksDetail() {//load giá trị lên để modify
            apiService.get('/api/feedback/getbyid/' + $stateParams.id, null, function (result) {
                $scope.feedbacks = result.data;//lấy dữ liệu
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        loadfeedbacksDetail();
    }
})(angular.module('grocery.feedbacks'));