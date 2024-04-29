import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { combineLatest } from 'rxjs';
import { AuthorizationGuard } from './guards/guards/authorization.guard';
import { AuthenticationGuard } from './guards/authentication.guard';
import { LibraryComponent } from './library/library.component';
import { LoginComponent } from './login/login.component';
import { ManageBooksComponent } from './manage-books/manage-books.component';
import { ProfileComponent } from './profile/profile.component';
import { RegisterComponent } from './register/register.component';

import { UsersListComponent } from './users-list/users-list.component';

const routes: Routes = [
  {
    path: 'books/library',
    component: LibraryComponent,
    canActivate: [AuthenticationGuard],
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
 
  {
    path: 'users/list',
    component: UsersListComponent,
    canActivate: [AuthorizationGuard] ,
  },
  {
    path: 'books/maintenance',
    component: ManageBooksComponent,
    canActivate: [AuthorizationGuard],
  },
  {
    path: 'users/profile',
    component: ProfileComponent,
    canActivate: [AuthenticationGuard],
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
