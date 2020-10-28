import { Component, OnInit, ComponentFactoryResolver, ComponentRef, ViewChild, ViewContainerRef, ChangeDetectorRef, Input, TemplateRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray, FormControl } from '@angular/forms';
import { LeaveService } from "../../../core/services/leave.service";
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzUploadChangeParam } from 'ng-zorro-antd/upload';
import { NzModalService, NzModalRef } from 'ng-zorro-antd/modal';
import { NzCalendarMode } from 'ng-zorro-antd/calendar';

@Component({
  selector: 'app-apply-leave',
  templateUrl: './apply-leave.component.html',
  styleUrls: ['./apply-leave.component.css']
})
export class ApplyLeaveComponent implements OnInit {
  leaveRequestForm !: FormGroup;
  leaveTypeList: any;
  leaveDurationList: any;
  attachmentList = [];
  leaveDateList: FormArray = new FormArray([]);

  constructor(
    private fb: FormBuilder,
    private leaveService: LeaveService,
    private msg: NzMessageService,
  ) { }

  ngOnInit(): void {
    this.leaveRequestForm = this.fb.group({
      leaveType: ['', [Validators.required]],
      description: [''],
      attachmentList: this.fb.array([

      ]),
      leaveDateList: this.leaveDateList
    });

    this.leaveTypeList = this.leaveService.getLeaveTypes();
    this.leaveDurationList = this.leaveService.getLeaveDuration();
  }

  submitForm(): void {
    console.log(this.leaveRequestForm.value);
  }

  handleChange(info: NzUploadChangeParam): void {
    if (info.file.status !== 'uploading') {
      console.log(info.file, info.fileList);
    }
    if (info.file.status === 'done') {
      this.msg.success(`${info.file.name} file uploaded successfully`);
      console.log(`${info.file.name} file uploaded successfully`);
      let array = this.leaveRequestForm.get('attachmentList') as FormArray;
      array.push(this.fb.group({
        name: info.file.name,
        url: info.file.response.url
      }))
    } else if (info.file.status === 'error') {
      this.msg.error(`${info.file.name} file upload failed.`);
      console.log(`${info.file.name} file upload failed.`);
    }
  }

  addField(e?: MouseEvent): void {
    if (e) {
      e.preventDefault();
    }

    this.leaveDateList.push(this.fb.group({
      date: new FormControl(''),
      dayType: new FormControl(''),
    }));

  }

  removeField(i: number, e: MouseEvent): void {
    e.preventDefault();
    if(this.leaveDateList.length > 0){
      this.leaveDateList.removeAt(i);
    }
  }
}

