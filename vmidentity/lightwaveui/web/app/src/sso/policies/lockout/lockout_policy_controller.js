/*
 * Copyright © 2012-2016 VMware, Inc.  All Rights Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the “License”); you may not
 * use this file except in compliance with the License.  You may obtain a copy
 * of the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an “AS IS” BASIS, without
 * warranties or conditions of any kind, EITHER EXPRESS OR IMPLIED.  See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */

'use strict';

var module = angular.module('lightwave.ui.sso');
module.controller('LockoutPolicyCntrl', [ '$scope', '$rootScope', 'TenantService',
        function($scope, $rootScope, TenantService) {

            $scope.updateLockoutPolicy = updateLockoutPolicy;

            init();

            function init(){
                $rootScope.globals.errors = null;
                $rootScope.globals.popup_errors = null;
            }

            function updateLockoutPolicy(lockoutPolicy) {
                $rootScope.globals.errors = null;
                var policy = {
                    lockoutPolicy: lockoutPolicy
                };

                TenantService
                    .UpdateDirConfiguration($rootScope.globals.currentUser, policy)
                    .then(function (res) {
                        if (res.status == 200) {
                            $rootScope.globals.errors = {details: 'Lockout policy updated successfully', success:true};
                            $scope.getConfig();
                            $scope.closeThisDialog('save');
                        }
                        else {
                            $rootScope.globals.popup_errors = res.data;
                        }
                    });
            }
        }]);