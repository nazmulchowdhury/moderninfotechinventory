function ManageViewModel(app, dataModel) {
    var self = this,
        startedLoad = false;

    // UI state used by private state
    self.logins = ko.observableArray();

    // my code
    self.companies = ko.observableArray();

    // Private state
    self.hasLocalPassword = ko.computed(function () {
        var logins = self.logins();

        for (var i = 0; i < logins.length; i++) {
            if (logins[i].loginProvider() === self.localLoginProvider()) {
                return true;
            }
        }

        return false;
    });

    // Data
    self.userName = ko.observable();
    self.localLoginProvider = ko.observable();

    // UI state
    self.loading = ko.observable(true);
    self.message = ko.observable();
    self.errors = ko.observableArray();

    self.changePassword = ko.computed(function () {
        if (!self.hasLocalPassword()) {
            return null;
        }

        return new ChangePasswordViewModel(app, self, self.userName(), dataModel);
    });

    self.setPassword = ko.computed(function () {
        if (self.hasLocalPassword()) {
            return null;
        }

        return new SetPasswordViewModel(app, self, dataModel);
    });

    self.canRemoveLogin = ko.computed(function () {
        return self.logins().length > 1;
    });

    // Operations
    self.load = function () { // Load user management data
        if (!startedLoad) {
            startedLoad = true;

            dataModel.getManageInfo(dataModel.returnUrl, true /* generateState */)
                .done(function (data) {
                    if (typeof (data.localLoginProvider) !== "undefined" &&
                        typeof (data.userName) !== "undefined" &&
                        typeof (data.logins) !== "undefined") {
                        self.userName(data.userName);
                        self.localLoginProvider(data.localLoginProvider);

                        // Loading all companies by me
                        dataModel.getAllCompanies().done(function (data) {
                            self.companies(data);
                        });

                        for (var i = 0; i < data.logins.length; i++) {
                            self.logins.push(new RemoveLoginViewModel(data.logins[i], self, dataModel));
                        }
                    } else {
                        app.errors.push("Error retrieving user information.");
                    }

                    self.loading(false);
                }).failJSON(function (data) {
                    var errors;

                    self.loading(false);
                    errors = dataModel.toErrorsArray(data);

                    if (errors) {
                        app.errors(errors);
                    } else {
                        app.errors.push("Error retrieving user information.");
                    }
                });
        }
    }

    self.home = function () {
        app.navigateToHome();
    };
}

function ChangePasswordViewModel(app, parent, name, dataModel) {
    var self = this;

    // Private operations
    function reset() {
        self.errors.removeAll();
        self.oldPassword(null);
        self.newPassword(null);
        self.confirmPassword(null);
        self.changing(false);
        self.validationErrors.showAllMessages(false);
    }

    // Data
    self.name = ko.observable(name);
    self.oldPassword = ko.observable("").extend({ required: true });
    self.newPassword = ko.observable("").extend({ required: true });
    self.confirmPassword = ko.observable("").extend({ required: true, equal: self.newPassword });

    // Other UI state
    self.changing = ko.observable(false);
    self.errors = ko.observableArray();
    self.validationErrors = ko.validation.group([self.oldPassword, self.newPassword, self.confirmPassword]);

    // Operations
    self.change = function () {
        self.errors.removeAll();
        if (self.validationErrors().length > 0) {
            self.validationErrors.showAllMessages();
            return;
        }
        self.changing(true);

        dataModel.changePassword({
            oldPassword: self.oldPassword(),
            newPassword: self.newPassword(),
            confirmPassword: self.confirmPassword()
        }).done(function (data) {
            self.changing(false);
            reset();
            parent.message("Your password has been changed.");
        }).failJSON(function (data) {
            var errors;

            self.changing(false);
            errors = dataModel.toErrorsArray(data);

            if (errors) {
                self.errors(errors);
            } else {
                self.errors.push("An unknown error occurred.");
            }
        });
    };
}

function RemoveLoginViewModel(data, parent, dataModel) {
    // Private state
    var self = this,
        providerKey = ko.observable(data.providerKey);

    // Data
    self.loginProvider = ko.observable(data.loginProvider);

    // Other UI state
    self.removing = ko.observable(false);

    // Operations
    self.remove = function () {
        parent.errors.removeAll();
        self.removing(true);
        dataModel.removeLogin({
            loginProvider: self.loginProvider(),
            providerKey: providerKey()
        }).done(function (data) {
            self.removing(false);
            parent.logins.remove(self);
            parent.message("The login was removed.");
        }).failJSON(function (data) {
            var errors;

            self.removing(false);
            errors = dataModel.toErrorsArray(data);

            if (errors) {
                parent.errors(errors);
            } else {
                parent.errors.push("An unknown error occurred.");
            }
        });
    };
}

function SetPasswordViewModel(app, parent, dataModel) {
    var self = this;

    // Data
    self.newPassword = ko.observable("").extend({ required: true });
    self.confirmPassword = ko.observable("").extend({ required: true, equal: self.newPassword });

    // Other UI state
    self.setting = ko.observable(false);
    self.errors = ko.observableArray();
    self.validationErrors = ko.validation.group([self.newPassword, self.confirmPassword]);

    // Operations
    self.set = function () {
        self.errors.removeAll();
        if (self.validationErrors().length > 0) {
            self.validationErrors.showAllMessages();
            return;
        }
        self.setting(true);

        dataModel.setPassword({
            newPassword: self.newPassword(),
            confirmPassword: self.confirmPassword()
        }).done(function (data) {
            self.setting(false);
            parent.logins.push(new RemoveLoginViewModel({
                loginProvider: parent.localLoginProvider(),
                providerKey: parent.userName()
            }, parent, dataModel));
            parent.message("Your password has been set.");
        }).failJSON(function (data) {
            var errors;

            self.setting(false);
            errors = dataModel.toErrorsArray(data);

            if (errors) {
                self.errors(errors);
            } else {
                self.errors.push("An unknown error occurred.");
            }
        });
    };
}

app.addViewModel({
    name: "Manage",
    bindingMemberName: "manage",
    factory: ManageViewModel,
    navigatorFactory: function (app) {
        return function () {
            app.errors.removeAll();
            app.view(app.Views.Manage);
            app.manage().load();
        }
    }
});