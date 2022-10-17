import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PATHS_AUTH_PAGES, PATH_MAINTENANCE_PAGES } from './commons/config/path-pages';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { NotFoundPageComponent } from './pages/not-found-page/not-found-page.component';
import { RegisterPageComponent } from './pages/register-page/register-page.component';

const routes: Routes = [
    {
        path: '',
        component: HomePageComponent
    },
    {
        path: PATHS_AUTH_PAGES.loginPage.onlyPath,
        loadChildren: () => import('./pages/login-page/login-page.module').then((m) => m.LoginPageModule)
    },
    {
        path: PATHS_AUTH_PAGES.registerPage.onlyPath,
        component: RegisterPageComponent
    },
    {
        path: PATH_MAINTENANCE_PAGES.onlyPath,
        loadChildren: () => import('./pages/maintenance/maintenance.module').then((m) => m.MaintenanceModule)
    },
    // {
    //     path: '**',
    //     pathMatch: 'full',
    //     component: NotFoundPageComponent
    // }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
