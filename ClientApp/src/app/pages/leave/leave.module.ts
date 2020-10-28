import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApplyLeaveComponent } from './apply-leave/apply-leave.component';
import { LeaveOverviewComponent } from './leave-overview/leave-overview.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MyLeaveComponent } from './my-leave/my-leave.component';


const routes: Routes = [
  { path: '', component: LeaveOverviewComponent, data: { pageName: 'Leave' } },
  { path: 'leave-request', component: ApplyLeaveComponent, data: { pageName: 'Request Leave' } },
  { path: 'my-leave', component: MyLeaveComponent, data: { pageName: 'Request Leave' } },
  { path: '**', component: LeaveOverviewComponent }
];

@NgModule({
  declarations: [ApplyLeaveComponent, LeaveOverviewComponent, MyLeaveComponent],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule.forChild(routes),
  ]
})
export class LeaveModule { }
