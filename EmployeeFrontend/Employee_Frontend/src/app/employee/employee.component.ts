import { Component, OnInit } from '@angular/core';
import { IDropdownSettings, } from 'ng-multiselect-dropdown';
import { FormBuilder,FormGroup, Validators } from '@angular/forms';
import { Departments } from '../departments';
import { Employee } from '../employee';
import { EmployeeServiceService } from '../employee-service.service';
import { Designation } from '../designation';
import { AddEmployee } from '../add-employee';

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
  addEmployee:AddEmployee=new AddEmployee();
  employeeForm!:FormGroup
  data:[]=[];
  list:[]=[];

  constructor(private service:EmployeeServiceService,private formBuilder:FormBuilder)
  { 
      this.employeeForm=this.formBuilder.group({
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
    this.service.GetAllDepartment().subscribe(
      (response)=>{
          this.departmentList=response;
      },
      (error)=>{
            console.log(error);
      }
    )
  }
  GetAllDesignation(){
     this.service.GetAllDesignation().subscribe(
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
    
  selectAllItems(item:any){
    for(let i=0;i<=item.length;i++){

      this.data=item[i].departmentId;
      this.list=this.data;
      console.log(this.list);
    }
       console.log(item);
    }
    selectSingleItem(item:any){


        this.data=item.departmentId;
        this.list=this.data;
        console.log(this.list);
 
         console.log(item);
    }
    Allselect(item:any){
        // console.log(item.departmentId);
    }
    onSave(){
      this.addEmployee.EmployeeName = this.employeeForm.value.name 
      this.addEmployee.EmployeeAddress=this.employeeForm.value.address
      this.addEmployee.DesignationId = this.employeeForm.value.designation
        console.log(this.employeeForm.value.deparment.departmentId)
      // console.log(this.employeeForm.value['departmentId'])
          //this.addEmployee.Department
          //console.log(this.obj.departmentsIds)
       // this.addEmployee.DepartmentsIds=this.obj.departmentsIds
         console.log(this.employeeForm.value);
      //  this.addEmployee.DepartmentsIds= this.departmentsIds
       // console.log(this.addEmployee);
      // console.log(this.addEmployee);
      //    this.service.addEmployee(this.addEmployee).subscribe(
      //    (response)=>{
      //  }
      //   );
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
updateEmployee(){

}

}