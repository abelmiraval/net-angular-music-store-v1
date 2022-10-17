import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule } from '@angular/router';
import { FooterComponent } from './components/footer/footer.component';
import { HeaderComponent } from './components/header/header.component';
import { ContainerComponent } from './container.component';

@NgModule({
	declarations: [ContainerComponent, HeaderComponent, FooterComponent],
	imports: [MatButtonModule, RouterModule],
	exports: [ContainerComponent]
})
export class ContainerModule {}
