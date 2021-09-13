define(['ko','sammy'], function (ko,sammy) {
     app.dashboard_model = function (params) {

        var self = this;
        self.title = "Trolled";
         self.info = ko.observable("");
         var links = [{ text: "Manage" },
             { i: "fas fa-tachometer-alt", hash: "#home", text: "Dashboard" },
             { i: "fas fa-users", hash: "#users/manage", text: "Users" },
             { i: "fas fa-tasks", hash: "#users/manage", text: "Checklists" },
             { text: "Permissions" },
             { i: "fas fa-compact-disc", hash: "#campaigns", text: "Applications" },
             { i: "fas fa-user-friends", hash: "#templates", text: "Groups" },
             { i: "fas fa-user-tag", hash: "#templates", text: "Exceptions" },
             { text: "File System" },
             { i: "fas fa-folder", hash: "#targetgroups", text: "Folders" },
             { i: "fas fa-user-friends", hash: "#campaigns", text: "Groups" },
             { i: "fas fa-user-tag", hash: "#templates", text: "Exceptions" }         ]
         self.links = ko.observableArray(links);
         self.isOpen = ko.observable(false);
         self.toggleOpen = function () {
             var isOpen = self.isOpen();
             self.isOpen(!isOpen);
         }
         self.page = app.page;
         self.nav = {
             links: ko.observableArray(links)
         }
    };
    var defaults = function (params) {
        var self = this;
        return self;
    }
    return app.dashboard_model;
});