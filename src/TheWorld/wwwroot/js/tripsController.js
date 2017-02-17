(function () {
    "use strict";
    //Getting the module
    angular.module("app-trips").controller("tripsController", tripsController);

    function tripsController($http) {

        var vm = this;

        vm.trips = [];

        vm.newTrip = {};

        vm.errorMessage = '';

        vm.isBusy = true;

        $http.get('/api/trips')
            .then(
                    function (response) {
                        //Success
                        angular.copy(response.data, vm.trips);
                    },
                    function (error) {
                        //Failure
                        vm.errorMessage = 'Failed to get trips : ' + error;
                    }
            ).finally(function () {
                vm.isBusy = false;
            });

        vm.addTrip = function () {

            vm.isBusy = true;

            vm.errorMessage = '';

            $http.post('/api/trips',vm.newTrip)
            .then(
                    function (response) {
                        //Success
                        vm.trips.push(response.data);
                        vm.newTrip = {};
                    },
                    function (error) {
                        //Failure
                        vm.errorMessage = 'Failed to post trips : ' + error;
                    }
            ).finally(function () {
                vm.isBusy = false;
            });

        }
    }
})();