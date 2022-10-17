import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PATH_MAINTENANCE_PAGES } from './../../commons/config/path-pages';
import { MaintenanceComponent } from './maintenance.component';

export const routes: Routes = [
	{
		path: '',
		component: MaintenanceComponent,
		children: [
			{
				path: PATH_MAINTENANCE_PAGES.buy.onlyPath,
				loadChildren: () =>
					import('./maintenance-buy-page/maintenance-buy-page.module').then((m) => m.MaintenanceBuyPageModule)
			},
			{
				path: PATH_MAINTENANCE_PAGES.events.onlyPath,
				loadChildren: () =>
					import('./maintenance-events-page/maintenance-events-page.module').then((m) => m.MaintenanceEventsPageModule)
			},
			{
				path: PATH_MAINTENANCE_PAGES.reports.onlyPath,
				loadChildren: () =>
					import('./maintenance-reports/maintenance-reports.module').then((m) => m.MaintenanceReportsModule)
			},
			{
				path: '',
				pathMatch: 'full',
				redirectTo: PATH_MAINTENANCE_PAGES.buy.onlyPath
			}
		]
	}
];
@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class MaintenanceRoutingModule { }
