﻿(function () {
    'use strict';

    var controllerId = 'userResourcePoolController';
    angular.module('main')
        .controller(controllerId, ['userService', 'userResourcePoolService', 'mainService', '$location', 'logger', userResourcePoolController]);

    function userResourcePoolController(userService, userResourcePoolService, mainService, $location, logger) {
        logger = logger.forSource(controllerId);

        var vm = this;
        vm.userResourcePool = null;
        vm.decreaseNumberOfSales = decreaseNumberOfSales;
        vm.increaseNumberOfSales = increaseNumberOfSales;
        vm.resetNumberOfSales = resetNumberOfSales;

        initialize();

        function initialize() {
            getUserResourcePool();
        };

        function getUserResourcePool() {
            var resourcePoolId = 0;

            switch ($location.path()) {
                case '/SectorIndex': resourcePoolId = 1; break;
                case '/KnowledgeIndex': resourcePoolId = 2; break;
                case '/TotalCostIndex': resourcePoolId = 3; break;
                case '/QualityIndex': resourcePoolId = 4; break;
                case '/EmployeeSatisfactionIndex': resourcePoolId = 5; break;
                case '/CustomerSatisfactionIndex': resourcePoolId = 6; break;
                case '/AllInOne': resourcePoolId = 7; break;
            }

            userResourcePoolService.getUserResourcePoolCustomByResourcePoolId(resourcePoolId)
                .success(function (userResourcePool) {
                    vm.userResourcePool = userResourcePool;
                });
        }

        function decreaseNumberOfSales() {
            userResourcePoolService.decreaseNumberOfSales(vm.userResourcePool.Id)
                .success(function () {
                    getUserResourcePool();
                });
        }

        function increaseNumberOfSales() {
            userResourcePoolService.increaseNumberOfSales(vm.userResourcePool.Id)
                .success(function () {
                    getUserResourcePool();
                });
        }

        function resetNumberOfSales() {
            userResourcePoolService.resetNumberOfSales(vm.userResourcePool.Id)
                .success(function () {
                    getUserResourcePool();
                });
        }
    };
})();