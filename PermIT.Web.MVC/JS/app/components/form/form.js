define(['ko','sammy'], function (ko,sammy) {
     app.users = function (params) {

         var self = this;
         self.defaults = new defaults(params);
         self.fields = self.defaults.fields;
         self.onSubmit = function () {

             if (self.isValid()) {
                 //console.log("valid");
                 self.defaults.onSubmit();
             } else {
                 //console.log("not valid");
             }
         }
         self.buttons = self.defaults.buttons;
         self.name = self.defaults.name;
         self.grid = self.defaults.grid;
         self.img = {
             imgs: ko.observableArray()
         }
         self.isValid = ko.pureComputed(function () {
            

             var valid = true;
             self.fields.forEach(f => {
                 if (f.value.validate != undefined) {
                     f.value.validate(f.value());
                 }
                 if (f.value.hasError != undefined && f.value.hasError() == true) {
                     valid = false;
                     console.log(f.value())
                 }
             });
             console.log("Forms",valid)
             return valid;
         },this)
    };
    var defaults = function (params) {
        var self = this;
        params = params ?? {};
        self.fields = params.fields ?? [];
        self.fields.forEach(e => {
            e.isDisabled = e.isDisabled ?? false;
            if (e.type == "date" && (e.value == undefined || e.value == null || e.value=="")) {
                e.value = app.dt(new Date())
            }
        })
        self.onSubmit = params.onSubmit ?? null;
        self.buttons = params.buttons ?? null;
        self.name = params.name + "_" + Date.now() ?? "form_" + Date.now()
        self.grid = params.grid ?? "grid1x";
        return self;
    }
    return app.users;
});