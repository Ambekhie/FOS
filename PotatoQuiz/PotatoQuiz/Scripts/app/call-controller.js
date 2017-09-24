var module = angular.module('CallApp', []);

module.controller('CallCtrl', function ($scope, $http) {
    $scope.title = "loading calls...";
    $scope.time = "";
    $scope.user = "";
    $scope.initState = true;
    $scope.id = 0;
    $scope.init = function () {
        $http.get('/api/call')
            .then(function (response) {
                console.log('getSuccess', response);
                $scope.title = response.data.restaurant;
                $scope.time = response.data.time;
                $scope.user = response.data.fosUser.email;
                $scope.id = response.data.id;
                $scope.initState = false;
            }, function (error) {
                $scope.title = "No calls started yet!";
            });
    };
    $scope.prevOrder = function () {
            $http.get('/api/call/prev/' + $scope.id)
            .then(function (response) {
                if (response !== null) {
                    $scope.title = response.data.restaurant;
                    $scope.time = response.data.time;
                    $scope.user = response.data.fosUser.email;
                    $scope.id = response.data.id;
                }
            }, function (error) {});
    };
    $scope.nextOrder = function () {
            $http.get("/api/call/next/" + $scope.id)
                .then(function (response) {
                    if (response !== null) {
                        $scope.title = response.data.restaurant;
                        $scope.time = response.data.time;
                        $scope.user = response.data.fosUser.email;
                        $scope.id = response.data.id;
                    }
                }, function (error) {});
        };
    $scope.join = function (option) {

    };
    $scope.minutes = function () {
        timeDifference = (new Date($scope.time).getTime()) - (new Date().getTime()); // deadline data minus current date
        var diffHours = Math.floor(timeDifference / (1000 * 60 * 60));
        return Math.floor((timeDifference - (diffHours * (1000 * 60 * 60))) / (1000 * 60));
    }
});


module.controller('CallForm', function ($scope, $http) {
    $http.get("/Account/GetMyID")
        .then(function (response) {
            $scope.id = response.data;
            console.log('getSuccess', response)
        }, function (error) {
            console.log('getIssue', error)
        });
    $scope.publish = function (option) {
        $http({
            method: 'POST',
            url: '/api/call/publish/' + $scope.id,
            headers: {
                'Content-Type': "application/json"
            },
            data: $scope.call
        })
        .then(function (response) {
            // success
        },
        function (error) { // optional
            // failed
        });
    };
});