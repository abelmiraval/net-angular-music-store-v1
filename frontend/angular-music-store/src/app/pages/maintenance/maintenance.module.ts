import { NgModule } from '@angular/core';
import { SharedComponentsModule } from 'src/app/commons/shared/shared-components.module';
import { MaintenanceRoutingModule } from './maintenance-routing.module';
import { MaintenanceComponent } from './maintenance.component';

@NgModule({
	declarations: [MaintenanceComponent],
	imports: [MaintenanceRoutingModule, SharedComponentsModule]
})
export class MaintenanceModule {}
