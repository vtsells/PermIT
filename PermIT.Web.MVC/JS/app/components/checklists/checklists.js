define(['ko', 'sammy'], function (ko, sammy) {
    app.users= function (params) {

        var self = this;
        self.defaults = new defaults(params);
        self.refresher = ko.observable();
        // app.campaignsRefresher = ko.observable();
        var links = [{ i: "fas fa-pencil-alt", hash: "#users/manage", text: "Manage Users", tabPage: "users-manage", tabPageParams: { refresher: self.refresher } },
            { i: "far fa-eye", hash: "#users/list", text: "All Users", tabPage: "users-list", tabPageParams: { refresher: self.refresher} },
            { i: "fas fa-cog", hash: "#users/settings", text: "Settings", tabPage: "users-settings", tabPageParams: { refresher: self.refresher} },
        ]
        self.links = ko.observableArray(links);
        //app.campaignsRefresher.subscribe(function () {
        //    self.links(null);
        //    self.links(links);
        //});

        return self;
    };
    var defaults = function (params) {
        var self = this;

        return self;
    }
    return app.users;
});