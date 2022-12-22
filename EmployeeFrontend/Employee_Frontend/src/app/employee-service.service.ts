import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable} from 'rxjs'
import { Employee } from './employee';
import { AddEmployee } from './add-employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeServiceService {

  constructor( private client:HttpClient) { }
 
  getAllEmployees():Observable<any>{
      return   this.client.get<any>("https://localhost:44317/api/Employee");
      
   }
   addEmployee(employee:AddEmployee):Observable<AddEmployee>{
     return this.client.post<AddEmployee>("https://localhost:44317/api/Employee",employee);
   }
   deleteEmployee(id:number):Observable<number>{
    return this.client.delete<number>("https://localhost:44317/api/Employee/"+id);
    }
  GetAllDesignation():Observable<any>{
     return this.client.get<any>("https://localhost:44317/api/Designation");
  }
  GetAllDepartment():Observable<any>{
    return this.client.get<any>("https://localhost:44317/api/Department");
  }
}
