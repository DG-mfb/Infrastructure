import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent }  from './app.component';

@NgModule({
	imports: [BrowserModule, FormsModule ],
	declarations: [AppComponent, LoginComponent ],
	bootstrap: [LoginComponent ]
})
export class AppModule { }
