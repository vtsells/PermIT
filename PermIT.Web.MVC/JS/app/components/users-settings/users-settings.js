define(['ko', 'sammy'], function (ko, sammy) {
    app.usersSettings= function (params) {

        var self = this;
        self.defaults = new defaults(params);
        self.refresher = self.defaults.refresher;
        self.domains = ko.observable();
        self.query = ko.observable();
        self.loading_getQuery = ko.observable(false);
        self.loading_getDomains = ko.observable(false);
        self.getSettings = function () {
            self.loading_getQuery(true);
            self.loading_getDomains(true);
            app.api('post', "/Settings/Get", { name: "AD Query" }, function (data) {
                self.query(data);
                self.loading_getQuery(false);

            })
            app.api('post', "/Settings/Get", {name:"Domains"}, function (data) {
                self.domains(data);
                self.loading_getDomains(false);
            })
        }
        self.onSaveChanges = function () {
            self.loading_getQuery(true);
            self.loading_getDomains(true);
            app.api('post', "/Settings/Update", { name: "AD Query", value: self.query }, function (data) {
               // self.query(data);
                self.loading_getQuery(false);
                self.refresher.valueHasMutated();
            })
            app.api('post', "/Settings/Update", { name: "Domains", value: self.domains }, function (data) {
               // self.domains(data);
                self.loading_getDomains(false);
                self.refresher.valueHasMutated();
            })
        }
        self.getSettings();
        self.refresh = function () {

        }
        self.refresher.subscribe(self.refresh);
    };
    var defaults = function (params) {
        var self = this;
        params = params ?? {}
        self.refresher = params.refresher ?? ko.observable();
        return self;
    }
    return app.usersSettings;
});