/// <reference path="../plugins/angular/angular.js" />
var MyApp = angular.module('myModule', []);

MyApp.controller("myController", myController);
MyApp.controller("myController1", myController);

//myController.$inject = ['$scope'];
function myController($rootScope, $scope) {
	$rootScope.message = "This is ";
}
function myController1($scope) {
	$scope.message = "This is abc ";
}