import { LoginComponent } from './login/login.component';
export const AppRoutes = [
	{ path: 'login', component: LoginComponent, data: { title: 'Login' } },
	{
		path: '',
		redirectTo: '/login',
		pathMatch: 'full'
	},
	{ path: '**', component: LoginComponent, data: { title: 'Login' } },
];