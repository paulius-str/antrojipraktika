import { Component, OnInit } from '@angular/core';
import { ISubject } from '../models/subject';
import { LecturerService } from './lecturer.service';

@Component({
  selector: 'app-lecturer-view',
  templateUrl: './lecturer-view.component.html',
  styleUrls: ['./lecturer-view.component.css']
})
export class LecturerViewComponent implements OnInit {
  subjects: ISubject[];


  constructor(private lecturerService: LecturerService) { }

  ngOnInit(): void {
    this.getSubjects();
  }

  getSubjects() {
    this.lecturerService.getSubjects().subscribe(response => {
      this.subjects = response;
      console.log(this.subjects);
    })
  }
}
