import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable} from 'rxjs'
import { Employee } from './employee';
import { AddEmployee } from './add-employee';
import { EditEmployee } from './edit-employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeServiceService {

  constructor( private client:HttpClient) { }
 
  getAllEmployees():Observable<any>{
      return   this.client.get<any>("https://localhost:44317/api/Employee");
      
   }
   addEmployee(employee:AddEmployee):Observable<any>{
     return this.client.post<any>("https://localhost:44317/api/Employee",employee);
   }
   deleteEmployee(id:number):Observable<number>{
    return this.client.delete<number>("https://localhost:44317/api/Employee/"+id);
    }
    getEmployeeById(id:number):Observable<any>{
   return this.client.get<any>("https://localhost:44317/api/Employee/"+id)
  }
    updateEmployee(employee:EditEmployee):Observable<any>{
       return this.client.put<any>("https://localhost:44317/api/Employee",employee)
    }
   getAllDesignation():Observable<any>{
    return this.client.get<any>("https://localhost:44317/api/Designation");
 }
   getAllDepartment():Observable<any>{
   return this.client.get<any>("https://localhost:44317/api/Department");
 }
}
