import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './login/login.component';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { StudentViewComponent } from './student-view/student-view.component';
import { LecturerViewComponent } from './lecturer-view/lecturer-view.component';
import { AdminViewComponent } from './admin-view/admin-view.component';
import { SubjectViewComponent } from './admin-view/subject-view/subject-view.component';
import { GroupViewComponent } from './admin-view/group-view/group-view.component';
import { JwtInterceptor } from './util/jwt-interceptor';
import { LecturerSubjectViewComponent } from './lecturer-view/lecturer-subject-view/lecturer-subject-view.component';
import { LecturerStudentViewComponent } from './lecturer-view/lecturer-subject-view/lecturer-student-view/lecturer-student-view.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    StudentViewComponent,
    LecturerViewComponent,
    AdminViewComponent,
    SubjectViewComponent,
    GroupViewComponent,
    LecturerSubjectViewComponent,
    LecturerStudentViewComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    TooltipModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'student', component: StudentViewComponent },
      { path: 'lecturer', component: LecturerViewComponent },
      { path: 'admin-view', component: AdminViewComponent },
      { path: 'lecturer-view', component: LecturerViewComponent },
      { path: 'student-view', component: StudentViewComponent }
    ])
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
