import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutusComponent } from './aboutus/aboutus.component';
import { EmployeeComponent } from './employee/employee.component';

const routes: Routes = [
  {path:"",redirectTo:"Employee",pathMatch:"full"},
  {path:"Employee",component:EmployeeComponent},
  {path:"Aboutus",component:AboutusComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
