(function (app) {
    app.controller('revenueStatisticController', revenueStatisticController);

    revenueStatisticController.$inject = ['$scope', 'apiService', 'notificationService', '$filter'];

    function revenueStatisticController($scope, apiService, notificationService, $filter) {
        $scope.tabledata = [];
        $scope.labels = [];
        $scope.series = ['Doanh số', 'Lợi nhuận'];

        $scope.from = {
            fromDate: [],
            toDate: []
        };
        $scope.loadData = loadData;
        //ẩn nút
        $('#btnfilter').off('click').on('click', function (e) {
            e.preventDefault();
            $('#divfilter').toggle();
        });
        $scope.chartdata = [];
        function getStatistic() {
            var config = {
                param: {
                    fromDate: '01/01/2019',
                    toDate: '01/01/2022'
                }
            }
            apiService.get('/api/statistic/getrevenue?fromDate=' + config.param.fromDate + '&toDate=' + config.param.toDate, null, function (response) {
                $scope.tabledata = response.data;
                var labels = [];
                var chartData = [];
                var revenues = [];
                var benefits = [];
                $.each(response.data, function (i, item) {
                    //lấy giá trị
                    labels.push($filter('date')(item.Date, 'dd/MM/yyyy'));
                    revenues.push(item.Revenues);
                    benefits.push(item.Benefit);
                });

                chartData.push(revenues);
                chartData.push(benefits);

                $scope.chartdata = chartData;
                $scope.labels = labels;
            }, function (response) {
                notificationService.displayError('Không thể tải dữ liệu.');
            });
        }
        function loadfrom() {
            $('#btnLoadform').off('click').on('click', function (e) {
                loadData();
            });
        }

        //lọc
        function loadData() {
            var config = {
                param: {
                    fromDate: $scope.from.fromDate,

                    toDate: $scope.from.toDate
                }
            }
            apiService.get('/api/statistic/getrevenue?fromDate=' + config.param.fromDate + '&toDate=' + config.param.toDate, null, function (response) {
                $scope.tabledata = response.data;
                var labels = [];
                var chartData = [];
                var revenues = [];
                var benefits = [];
                $.each(response.data, function (i, item) {
                    //lấy giá trị
                    labels.push($filter('date')(item.Date, 'dd/MM/yyyy'));
                    revenues.push(item.Revenues);
                    benefits.push(item.Benefit);
                });

                chartData.push(revenues);
                chartData.push(benefits);

                $scope.chartdata = chartData;
                $scope.labels = labels;
            }, function (response) {
                notificationService.displayError('Không thể tải dữ liệu.');
            });
        }
        getStatistic();

        
    }
})(angular.module('grocery.statistic'));