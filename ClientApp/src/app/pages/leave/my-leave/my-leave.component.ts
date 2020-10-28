import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-my-leave',
  templateUrl: './my-leave.component.html',
  styleUrls: ['./my-leave.component.css']
})
export class MyLeaveComponent implements OnInit {
  listOfData: ItemData[] = [];
  constructor() { }

  ngOnInit(): void {
    for (let i = 0; i < 100; i++) {
      let date = new Date(Date.now());
      this.listOfData.push({
        date: date.toISOString().substring(0, 10),
        leaveType: 'Annual',
        noOfDay: i,
        comment: `comment-${i}`,
        status: `status-${i}`
      });
    }
  }

}



interface ItemData {
  date: string;
  leaveType: string;
  noOfDay: number;
  status: string;
  comment: string;
}