define(['ko', 'sammy'], function (ko, sammy) {
    app.usersManage= function (params) {

        var self = this;
        self.defaults = new defaults(params);
        self.refresher = self.defaults.refresher;
        self.loading_get = ko.observable(false);
        self.loading_syncing = ko.observable(false);
        self.unSyncedUsers = ko.observableArray([]);
        self.usersList = ko.observableArray([]);
        self.usersDisabledList = ko.observableArray([]);
        self.jobList = ko.observableArray([]);
        self.getUnSyncedUsers = function () {
            self.unSyncedUsers.removeAll();
            self.loading_get(true);
            app.api('get', "/Users/GetUnsyncedUsers", null, function (data) {
                console.log(data)
                self.unSyncedUsers(data)
                self.loading_get(false);

            })
        }
        self.syncUsers = function () {
            self.loading_syncing(true);
            app.api('post', "/Users/SyncUsers", null, function (data) {
                self.loading_syncing(false);
                self.refresher.valueHasMutated();
                self.getUnSyncedUsers();
            })
        }
        self.getUsersList = function () {

            app.api('post', "/Users/AsSelectList", { enabled: true }, function (data) {

                self.usersList(data);
               
            })
        }
        self.getJobsList = function () {

            app.api('post', "/Job/AsSelectList", null, function (data) {

                self.jobList(data);

            })
        }
        self.getUsersDisabledList = function () {

            app.api('post', "/Users/AsSelectList", { enabled: false }, function (data) {

                self.usersDisabledList(data);

            })
        }
        self.getUnSyncedUsers();
        self.getUsersList();
        self.getUsersDisabledList();
        self.getJobsList();
        self.disableUser = {
            userId: ko.observable(),
            status: false
        }
        self.assignJob = {
            jobId: ko.observable(),
            userId: ko.observable()
        }
        self.enableUser = {
            userId: ko.observable(),
            status: true
        }
        self.onDisableUser = function () {
            app.api('post', "/Users/SetStatus", self.disableUser, function (data) {

                self.usersList(data);
                self.refresher.valueHasMutated();
            })
        }
        self.disableForm = {
            fields: [{ type: "select", text: "User", value: self.disableUser.userId, list: self.usersList }],
            onSubmit: self.onDisableUser,
            buttons: [{ css: "btn btn-negative", type: "submit", text: "Disable User" }],
            grid:"flexRow"
        }


        self.onAssignJob = function () {
            console.log("Job Assigned");
            app.api('post', "/Users/AssignJob", self.assignJob, function (data) {

                self.refresher.valueHasMutated();
            })
        }
        self.assignForm = {
            fields: [{ type: "select", text: "User", value: self.assignJob.userId, list: self.usersList },
                { type: "select", text: "Job", value: self.assignJob.jobId, list: self.jobList }            ],
            onSubmit: self.onAssignJob,
            buttons: [{ css: "btn btn-neutral", type: "submit", text: "Assign Job" }],
            grid: "flexRow"
        }
        self.onEnableUser = function () {
            
            app.api('post', "/Users/SetStatus", self.enableUser, function (data) {
                self.refresher.valueHasMutated();
               // self.usersList(data);
               
            })
        }
        self.enableForm = {
            fields: [{ type: "select", text: "User", value: self.enableUser.userId, list: self.usersDisabledList }],
            onSubmit: self.onEnableUser,
            buttons: [{ css: "btn btn-positive", type: "submit", text: "Enable User" }],
            grid: "flexRow"
        }

        //self.campaigns = ko.observableArray([]);
        //self.getCampaigns = function () {
        //    self.campaigns.removeAll();
        //    app.api('get', "/Campaign/List", null, function (data) {
        //        self.campaigns(data)
        //    })
        //}
        //self.getCampaigns();
        //self.select = function (id) {
        //    window.location.hash = '#campaigns/view/' + id;
        ////}
        //self.startCampaign = function (id) {

        //    var data = { id: id };
        //    app.api('POST', "/Campaign/Start", data, function (data) {
        //        //self.refresh();
        //        console.log(data)
        //        app.campaignsRefresher.valueHasMutated();
        //    });
        //}
        self.refresh = function () {
            self.getUnSyncedUsers();
            self.getUsersList();
            self.getUsersDisabledList();
            self.getJobsList();
            self.disableUser.userId(null);
            self.assignJob.jobId(null);
            self.assignJob.userId(null);
            
            self.enableUser.userId(null);
        }
        self.refresher.subscribe(self.refresh);
    };
    var defaults = function (params) {
        var self = this;
        params = params ?? {}
        self.refresher = params.refresher ?? ko.observable();
        return self;
    }
    return app.usersManage;
});