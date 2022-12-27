import { Component, OnInit } from '@angular/core';
import { IDropdownSettings, } from 'ng-multiselect-dropdown';
import { FormBuilder,FormGroup, Validators } from '@angular/forms';
import { Departments } from '../departments';
import { Employee } from '../employee';
import { EmployeeServiceService } from '../employee-service.service';
import { Designation } from '../designation';
import { AddEmployee } from '../add-employee';
import { EditEmployee } from '../edit-employee';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.scss']
})
export class EmployeeComponent implements OnInit {
  employees: Employee[] = [];
  departmentList:Departments[]=[];
  designationList:Designation[]=[];
  dropdownSettings:IDropdownSettings={};
  addEmployees:AddEmployee=new AddEmployee();
  editEmployees:EditEmployee= new EditEmployee();
  employeeForm!:FormGroup
  updateEmployeeForm!:FormGroup
  list:any[]=[];
  dep:any[]=[];
  departments=[{
        departmentId:[],
        departmentName:[]
  }]

  constructor(private service:EmployeeServiceService,private formBuilder:FormBuilder)
  { 
      this.employeeForm=this.formBuilder.group({
        name:['',Validators.required],
        address:['',Validators.required],
        designation:['',Validators.required],
        departments:['',Validators.required]
      })
      this.updateEmployeeForm= this.formBuilder.group({
        EmployeeId:[],
        name:['',Validators.required],
        address:['',Validators.required],
        designation:['',Validators.required],
        departments:['',Validators.required]
      })
  }
  ngOnInit(): void {
     this.GetAll();
     this.GetAllDepartment();
     this.DropDownSettings();
     this.GetAllDesignation();
    
    }
      GetAll(){
        this.service.getAllEmployees().subscribe(
    (response)=>{
          this.employees=response;
    },
     (error)=>{
          console.log(error);
     }
   )   
}
  GetAllDepartment(){
    this.service.getAllDepartment().subscribe(
      (response)=>{
          this.departmentList=response;
      },
      (error)=>{
            console.log(error);
      }
    )
  }
  GetAllDesignation(){
     this.service.getAllDesignation().subscribe(
      (response)=>{
            this.designationList=response;
      },
      (error)=>{
                 console.log(error);
      }
     )
  }
  DropDownSettings(){
  this.dropdownSettings = {
    idField: 'departmentId',
    textField: 'departmentName',
    enableCheckAll: true,
    selectAllText: "Select All Departments",
    unSelectAllText: "UnSelect All Departments",
    allowSearchFilter: true
  }}
    onSave(){
         console.log(this.employeeForm.value);
        if(this.employeeForm.invalid){
         this.employeeForm.markAllAsTouched();
          return ;
       }
      
      this.addEmployees.EmployeeName = this.employeeForm.value.name 
      this.addEmployees.EmployeeAddress=this.employeeForm.value.address
      this.addEmployees.DesignationId = this.employeeForm.value.designation
        this.departments= this.employeeForm.value.departments
          for(let i=0;i<this.departments.length;i++){
               
                    this.list.push(this.departments[i].departmentId);
               }
        this.addEmployees.DepartmentsIds= this.list
         this.service.addEmployee(this.addEmployees).subscribe(
         (response)=>{
            this.list=[];
            this.GetAll();
       },
          (error)=>{
            this.emptyModal();
              console.log(error);
          }
        )
        } 
    deleteEmployee(id:number){
      var ans = confirm('Want to Delete Employee')
      if(!ans)  return;
      this.service.deleteEmployee(id).subscribe(
       (response)=>{
         this.GetAll();
       },
       (error)=>{
           console.log(error);
       }
      )
}
  editEmployee(id:number){
         this.service.getEmployeeById(id).subscribe(
          (response)=>{
               console.log(response);
              this.editEmployees.EmployeeId = response.employeeId
              this.editEmployees.EmployeeName= response.employeeName
              this.editEmployees.EmployeeAddress= response.employeeAddress
              this.editEmployees.DesignationId = response.designationId
              this.editEmployees.DepartmentsIds=response.departments
             // console.log(response.departments[0].departmentName)
            
          },
          (error)=>{
             console.log(error);
          }
         )  
  }
    emptyModal()
    {
        this.addEmployees.EmployeeName="";
        this.addEmployees.EmployeeAddress="",
        this.addEmployees.DesignationId="",
        this.list=[];
    }
    Deplist(){
      debugger
      this.service.getAllDepartment().subscribe(
        (response)=>{
            this.departmentList=response;
        },
        (error)=>{
              console.log(error);
        }
      )
    }
    updateEmployee(){
      if(this.updateEmployeeForm.invalid){
        this.updateEmployeeForm.markAllAsTouched();
         return ;
      }
        
      for(let i=0;i<this.editEmployees.DepartmentsIds.length;i++){
               
             this.list.push(this.editEmployees.DepartmentsIds[i].departmentId);
            
   }         
           this.editEmployees.DepartmentsIds= this.list
        this.service.updateEmployee(this.editEmployees).subscribe(
          (response)=>{
                this.GetAll();
                this.list=[]
                this.editEmployees.EmployeeId = 0
                this.editEmployees.EmployeeName= ""
                this.editEmployees.EmployeeAddress= ""
                this.editEmployees.DesignationId = 0
                 this.editEmployees.DepartmentName= [];
                 this.editEmployees.DepartmentsIds=[];
               alert('user updated')
          },
          (error)=>{
                    console.log(error);
          }
        )
      }




}