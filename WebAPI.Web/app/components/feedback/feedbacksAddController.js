(function (app) {
    app.controller('feedbacksAddController', feedbacksAddController);

    feedbacksAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService'];
    //$state dùng để điều hướng
    function feedbacksAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.feedbacks = {
            CreatedDate: new Date(),
            Status: true
        }

        $scope.Addfeedbacks = Addfeedbacks;

        function Addfeedbacks() {
            apiService.post('/api/feedback/create', $scope.feedbacks,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + 'đã thêm mới');
                    $state.go('feedbacks');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        }
    }
})(angular.module('grocery.feedbacks'));