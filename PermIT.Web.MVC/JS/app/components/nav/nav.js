define(['ko','sammy'], function (ko,sammy) {
     app.nav= function (params) {

         var self = this;
         self.defaults = new defaults(params);
         self.links = self.defaults.links;
         self.title = "Trolled";
         self.info = ko.observable("");
        // self.links = ko.observableArray(links);
         self.isOpen = ko.observable(false);
         self.toggleOpen = function () {
             var isOpen = self.isOpen();
             self.isOpen(!isOpen);
         }
         self.page = app.page;

    };
    var defaults = function (params) {
        var self = this;
        params = params ?? {};
        self.links = params.links ?? ko.observableArray();
        return self;
    }
    return app.nav;
});