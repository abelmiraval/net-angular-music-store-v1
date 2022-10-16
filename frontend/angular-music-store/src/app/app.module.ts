import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ContainerModule } from './commons/components/container/container.module';
import { SharedFormCompleteModule } from './commons/shared/shared-form-complete.module';
import { SharedComponentsModule } from './commons/shared/shared.components.module';

@NgModule({
	declarations: [AppComponent],
	imports: [
		BrowserModule,
		BrowserAnimationsModule,
		ContainerModule,
		SharedFormCompleteModule,
		SharedComponentsModule
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule { }
