import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ContainerModule } from './commons/components/container/container.module';
import { SharedFormCompleteModule } from './commons/shared/shared-form-complete.module';
import { SharedComponentsModule } from './commons/shared/shared-components.module';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { AppRoutingModule } from './app-routing.module';
import { RegisterPageComponent } from './pages/register-page/register-page.component';

@NgModule({
	declarations: [AppComponent, HomePageComponent, RegisterPageComponent],
	imports: [
		BrowserModule,
		BrowserAnimationsModule,
		ContainerModule,
		SharedFormCompleteModule,
		SharedComponentsModule,
		AppRoutingModule
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule { }
