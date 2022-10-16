import { NgModule } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { CardEventComponent } from '../components/card-event/card-event.component';
import { CardMenusComponent } from '../components/card-menus/card-menus.component';


@NgModule({
    declarations: [CardEventComponent, CardMenusComponent],
    imports: [MatCardModule],
    exports: [CardEventComponent, CardMenusComponent],
})
export class SharedComponentsModule { }
