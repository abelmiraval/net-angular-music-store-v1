import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SharedComponentsModule } from './../../commons/shared/shared-components.module';
import { SharedFormCompleteModule } from './../../commons/shared/shared-form-complete.module';
import { BuyPageComponent } from './buy-page.component';

export const routes: Routes = [{ path: '', component: BuyPageComponent }];

@NgModule({
	declarations: [BuyPageComponent],
	imports: [RouterModule.forChild(routes), SharedFormCompleteModule, SharedComponentsModule]
})
export class BuyPageModule {}
