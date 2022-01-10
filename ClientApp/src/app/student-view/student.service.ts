import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IGradeWithSubject } from '../models/gradewithsubject';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  constructor(private http: HttpClient) { }

  getGrades(){
    return this.http.get<IGradeWithSubject[]>(environment.baseUrl + 'student/grades');
  }
}
