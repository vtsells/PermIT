define(['ko', 'sammy'], function (ko, sammy) {
    app.users = function (params) {

        var self = this;
        self.defaults = new defaults(params);
        self.waitingFor = self.defaults.waitingFor;
        self.isLoading = ko.computed(function () {
            let stillLoading = self.waitingFor.filter(m => m() == true)
            return (stillLoading.length == 0) ? false : true;
        });
    };
    var defaults = function (params) {
        var self = this;
        params = params ?? {};
        self.waitingFor = params ?? ko.observableArray([]);
        return self;
    }
    return app.users;
});