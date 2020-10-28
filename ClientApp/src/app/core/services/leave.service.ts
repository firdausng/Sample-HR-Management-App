import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LeaveService {

  constructor() { }

  getLeaveTypes(){
    return ['Annual', 'Sick', 'Maternity']
  }

  getLeaveDuration(){
    return ['Full Day', 'Morning', 'Afternoon']
  }

  getLeaveEntitlementList(): LeaveEntitlement[]{
    return [
      {leaveType: 'Annual', entitlementType: '', validFrom: '1/1/2020', validTo: '31/12/2020', totalDays: 20},
      {leaveType: 'Sick', entitlementType: '', validFrom: '1/1/2020', validTo: '31/12/2020', totalDays: 12},
      {leaveType: 'Paternity', entitlementType: '', validFrom: '1/1/2020', validTo: '31/12/2020', totalDays: 30}
    ]
  }
}


export interface LeaveEntitlement {
  leaveType: string;
  entitlementType: string;
  validFrom: string;
  validTo: string;
  totalDays: number;
}