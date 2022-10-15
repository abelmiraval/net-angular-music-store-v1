import { NgModule } from '@angular/core';
import { FooterComponent } from './components/footer/footer.component';
import { HeaderComponent } from './components/header/header.component';
import { ContainerComponent } from './container.component';
import { MatButtonModule } from '@angular/material/button';


@NgModule({
    declarations: [ContainerComponent, HeaderComponent, FooterComponent],
    imports: [MatButtonModule],
    exports: [ContainerComponent],
})
export class ContainerModule { }
