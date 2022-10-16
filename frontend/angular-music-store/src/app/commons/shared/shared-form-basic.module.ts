import { NgModule } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
    exports: [ReactiveFormsModule, MatButtonModule, MatInputModule, MatFormFieldModule]
})
export class SharedFormBasicModule { }
