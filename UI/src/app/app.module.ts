import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PageFooterComponent } from './page-footer/page-footer.component';
import { PageHeaderComponent } from './page-header/page-header.component';
import { MaterialModule } from './material/material.module';
import { SideNavComponent } from './side-nav/side-nav.component';
import { LibraryComponent } from './library/library.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { UsersListComponent } from './users-list/users-list.component';
import { ManageBooksComponent } from './manage-books/manage-books.component';
import { ProfileComponent } from './profile/profile.component';

@NgModule({
  declarations: [
    AppComponent,
    PageFooterComponent,
    PageHeaderComponent,
    SideNavComponent,
    LibraryComponent,
    RegisterComponent,
    LoginComponent,
    UsersListComponent,
    ManageBooksComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    ReactiveFormsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config:{
        tokenGetter:() => {
          return localStorage.getItem('access_token');
        },
        allowedDomains: ['localhost:7182',]
        },
      }),

    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
