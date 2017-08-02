"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var login_component_1 = require("./login/login.component");
exports.AppRoutes = [
    { path: 'login', component: login_component_1.LoginComponent, data: { title: 'Login' } },
    {
        path: '',
        redirectTo: '/login',
        pathMatch: 'full'
    },
    { path: '**', component: login_component_1.LoginComponent, data: { title: 'Login' } },
];
//# sourceMappingURL=app.routing.js.map