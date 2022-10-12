import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { HttpService } from 'src/app/services/http.service';
import { IDepartment } from './department-model';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit, OnDestroy {

  departments = [] as IDepartment[];
  subscription!: Subscription;

  constructor(private http: HttpService) { }

  ngOnInit(): void {
    this.loadDepartments();
  }

  loadDepartments() {
    this.subscription = this.http.getData("Department").subscribe({
      next: (data: any) => {
        this.departments = data.data as IDepartment[];
      },
      error: reason => console.log(reason)
    });
  }

  ngOnDestroy(): void {
    if (this.subscription)
      this.subscription.unsubscribe();
  }

}