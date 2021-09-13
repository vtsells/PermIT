define(['ko', 'sammy'], function (ko, sammy) {
    app.usersList= function (params) {

        var self = this;
        self.defaults = new defaults(params);
        self.refresher = self.defaults.refresher;
        self.users = ko.observableArray([]);
        self.loading_get = ko.observable(false);
        self.jobList = ko.observableArray([]);
        self.selectedId = ko.observable(null);
        self.getUsers = function () {
            self.users.removeAll();
            self.loading_get(true);
            app.api('get', "/Users/GetAllIncludeJobs", null, function (data) {
                self.users(data)
                self.loading_get(false);
                console.log(self.users());
            })
        }
        self.getUsers();
        self.getJobsList = function () {

            app.api('post', "/Job/AsSelectList", null, function (data) {

                self.jobList(data);

            })
        }
        self.getJobsList();
        self.assignJob = {
            jobId: ko.observable(),
            userId: self.selectedId
        }
        self.onAssignJob = function () {
            console.log("Job Assigned");
            app.api('post', "/Users/AssignJob", self.assignJob, function (data) {

                self.refresher.valueHasMutated();
            })
        }

        self.enableUser = {
            userId: ko.observable(),
            status: true
        }
        self.onEnableUser = function () {

            app.api('post', "/Users/SetStatus", self.enableUser, function (data) {
                self.refresher.valueHasMutated();
                // self.usersList(data);

            })
        }

        self.disableUser = {
            userId: ko.observable(),
            status: false
        }
        self.onDisableUser = function () {
            app.api('post', "/Users/SetStatus", self.disableUser, function (data) {

                self.refresher.valueHasMutated();
            })
        }
        self.select = function (id) {
            self.assignJob.userId(id);
            self.enableUser.userId(id);
            self.disableUser.userId(id);
        }
        self.deselect = function () {
            self.assignJob.userId(null);
            self.enableUser.userId(null);
            self.disableUser.userId(null);
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
            self.getUsers();
            self.deselect();
        }
        self.refresher.subscribe(self.refresh);
    };
    var defaults = function (params) {
        var self = this;
        params = params ?? {}
        self.refresher = params.refresher ?? ko.observable();
        return self;
    }
    return app.usersList;
});