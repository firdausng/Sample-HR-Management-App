import { Component, OnInit } from '@angular/core';
import { LeaveService, LeaveEntitlement } from 'src/app/core/services/leave.service';

@Component({
  selector: 'app-leave-overview',
  templateUrl: './leave-overview.component.html',
  styleUrls: ['./leave-overview.component.css']
})
export class LeaveOverviewComponent implements OnInit {

  entitlementList: LeaveEntitlement[];

  constructor(private leaveService: LeaveService) { }

  ngOnInit(): void {
    this.entitlementList =this.leaveService.getLeaveEntitlementList();
  }

}



