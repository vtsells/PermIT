require(['ko', 'sammy', 'moment','kochart'], function (ko, sammy, moment,kochart) {
    function parseQueryString(queryString) {
        var data = {},
            pairs, pair, separatorIndex, escapedKey, escapedValue, key, value;

        if (queryString === null) {
            return data;
        }

        pairs = queryString.split("&");

        for (var i = 0; i < pairs.length; i++) {
            pair = pairs[i];
            separatorIndex = pair.indexOf("=");

            if (separatorIndex === -1) {
                escapedKey = pair;
                escapedValue = null;
            } else {
                escapedKey = pair.substr(0, separatorIndex);
                escapedValue = pair.substr(separatorIndex + 1);
            }

            key = decodeURIComponent(escapedKey);
            value = decodeURIComponent(escapedValue);

            data[key] = value;
        }

        return data;
    }
    function getFragment() {
        if (window.location.hash.indexOf("#") === 0) {
            return parseQueryString(window.location.hash.substr(1));
        } else {
            return {};
        }
    };
    function App() {
        var self = this;
        self.title = "Perm IT";

        self.setAccessToken = function (accessToken) {
            sessionStorage.setItem("accessToken", accessToken);
        };
        self.hash = ko.observable(window.location.hash);
        self.getAccessToken = function () {
            return sessionStorage.getItem("accessToken");
        };
        if (!self.getAccessToken()) {
            // The following code looks for a fragment in the URL to get the access token which will be
            // used to call the protected Web API resource
            var fragment = getFragment();

            if (fragment.access_token) {
                // returning with access token, restore old hash, or at least hide token
                window.location.hash = fragment.state || '';
                self.setAccessToken(fragment.access_token);
            } else {
                // no token - so bounce to Authorize endpoint in AccountController to sign in or register
                window.location = "/Account/Authorize?client_id=web&response_type=token&state=" + encodeURIComponent(window.location.hash);
            }
        }
        self.dt = function (date) {
            var valid = moment(date).isValid();
            return (valid) ? moment(date).format('yyyy-MM-DD') : "No date yet";
        }
        self.api = function (method, url, data, success) {
            $.ajax({
                method: method,
                url: url,
                dataType: "json",
                data: JSON.stringify(ko.toJS(data)),
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.getAccessToken()
                },
                success: function (data) {
                    success(data)
                    // self.dashboard_model.info('Your Hometown is : ' + data.hometown);
                }
            });
        }
        self.toParams = function (obj) {
            var isObj = function (a) {
                if ((!!a) && (a.constructor === Object)) {
                    return true;
                }
                return false;
            };
            var _st = function (z, g) {
                return "" + (g != "" ? "[" : "") + z + (g != "" ? "]" : "");
            };
            var fromObject = function (params, skipobjects, prefix) {
                if (skipobjects === void 0) {
                    skipobjects = false;
                }
                if (prefix === void 0) {
                    prefix = "";
                }
                var result = "";
                if (typeof (params) != "object") {
                    return prefix + "=" + encodeURIComponent(params) + "&";
                }
                for (var param in params) {
                    var c = "" + prefix + _st(param, prefix);
                    if (isObj(params[param]) && !skipobjects) {
                        result += fromObject(params[param], false, "" + c);
                    } else if (Array.isArray(params[param]) && !skipobjects) {
                        params[param].forEach(function (item, ind) {
                            result += fromObject(item, false, c + "[" + ind + "]");
                        });
                    } else {
                        result += c + "=" + encodeURIComponent(params[param]) + "&";
                    }
                }
                return result;
            };
            return fromObject(obj);
        }
        self.page = ko.observable();
        self.inventoryParams = {
            viewingId: ko.observable()
        }
        self.reportParams = {
            viewingId: ko.observable(),
            controllerName: ko.observable()
        }
        self.campaignsParams = {
            viewingId: ko.observable()
        }
        self.templatesParams = {
            viewingId: ko.observable()
        }
    };
    app = new App();


    // ko.components.register('app', {viewModel:app});
    var comps = ['dashboard',
        'targetGroups',
        'campaigns',
        'campaigns-manage',
        'campaigns-list',
        'campaigns-view',
        'form',
        'templates',
        'templates-manage',
        'templates-list',
        'templates-view',
        'html-editor',
        'console',
        'nav',
        'users',
        'users-list',
        'users-settings',
        'users-manage',
        'loader'];
    var base = 'components';
    comps.forEach(c => {
        ko.components.register(c, {
            viewModel: { require: base+"/"+c+"/"+c },
            template: { require: "text!" + base + "/" + c + "/" + c +".html" }
        });
    })
    ko.bindingHandlers.fileSrc = {
        init: function (element, valueAccessor) {
            var value = valueAccessor();
           
            ko.utils.registerEventHandler(element, "change", function () {
                valueAccessor().removeAll();
                for (var i = 0; i < element.files.length; i++) {
                    let file = { file: element.files[i], durl: ko.observable() }
                    let reader = new FileReader();
                    reader.onload = function (e) {
                        var value = valueAccessor();
                        file.durl(e.target.result);
                        //value.push(e.target.result);
                    }
                    reader.readAsDataURL(element.files[i]);
                    valueAccessor().push(file);
                }
            });
        }
    };
    ko.extenders.required = function (target, overrideMessage) {
        //add some sub-observables to our observable
        target.hasError = ko.observable();
        target.validationMessage = ko.observable();

        //define a function to do validation
        target.validate = function(newValue) {
            target.hasError(newValue ? false : true);
            target.validationMessage(newValue ? "" : overrideMessage || "This field is required");
        }

        //initial validation
       // validate(target());

        //validate whenever the value changes
        target.subscribe(target.validate);

        //return the original observable
        return target;
    };
    ko.extenders.rvalidate = function (target, options) {
        console.log(options)
        var url = options.url;
        var overrideMessage = options.overrideMessage
        //add some sub-observables to our observable
        target.hasError = ko.observable();
        target.validationMessage = ko.observable();

        //define a function to do validation
        target.validate = function (newValue) {
           // target.hasError(true);
            app.api("post", url, { tag: newValue }, function (data) {
                target.hasError(!data);
                target.validationMessage(data ? "" : overrideMessage || "This field is required");
                console.log(target.hasError());
            })
           // target.hasError(newValue ? false : true);
            //target.validationMessage(newValue ? "" : overrideMessage || "This field is required");
        }

        //initial validation
        // validate(target());

        //validate whenever the value changes
        target.subscribe(target.validate);

        //return the original observable
        return target;
    };

    ko.applyBindings(app);
    sammy(function () {
        this._checkFormSubmission = function (form) {

            return true;
        };
        this.get("#users/manage", function () {
            window.location.hash = "#users/manage";
            app.hash(window.location.hash);
            app.page("Users");
        })
        this.get("#users/settings", function () {
            window.location.hash = "#users/settings";
            app.hash(window.location.hash);
            app.page("Users");
        })
        this.get("#users/list", function () {
            //window.location.hash = "#users/list";
            app.hash(window.location.hash);
            app.page("Users");
        })
        this.get("#users", function () {
            this.app.runRoute('get', '#users/list');
          //  app.hash(window.location.hash);
            app.page("Users");
        })


        this.get("#home", function () {
           // window.location.hash = "#home";
            app.hash(window.location.hash);
            app.page("Dashboard");
        })

        this.get("/", function () {
            this.app.runRoute('get', '#home');
           // app.hash(window.location.hash);
        })

    }).run();
});
