import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  UserID:any
  UserFullName : any
  UserN : any
  constructor(private fb: FormBuilder, private http: HttpClient) { }
  readonly BaseURI = 'https://localhost:44381/api';

  formModel = this.fb.group({
    Email: ['', Validators.email],
    FirstName: [''],
    LastName: [''],
  });

  // comparePasswords(fb: FormGroup) {
  //   let confirmPswrdCtrl = fb.get('ConfirmPassword');
  //   //passwordMismatch
  //   //confirmPswrdCtrl.errors={passwordMismatch:true}
  //   if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
  //     if (fb.get('Password').value != confirmPswrdCtrl.value)
  //       confirmPswrdCtrl.setErrors({ passwordMismatch: true });
  //     else
  //       confirmPswrdCtrl.setErrors(null);
  //   }
  // }

  register() {
    var body = {
      UserName: this.formModel.value.Email,
      Email: this.formModel.value.Email,
      FirstName: this.formModel.value.FirstName,
      LastName: this.formModel.value.LastName
    };
    return this.http.post(this.BaseURI + '/account/Register', body);
  }

  login(formData) {
    return this.http.post(this.BaseURI + '/account/Login', formData)
  }

  getUserProfile() {
    return this.http.get(this.BaseURI + '/UserProfile');
  }
  getAll() {
    return this.http.get(this.BaseURI + '/account');
  }

  roleMatch(allowedRoles): boolean {
    var isMatch = false;
    var payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
    var userRole = payLoad.role;
    var userid = payLoad.UserID;
    this.UserID=userid
    this.UserFullName=payLoad.FullName
    this.UserN=payLoad.UserName
    allowedRoles.forEach(element => {
      if (userRole == element) {
        isMatch = true;
        return false;
      }
    });
    return isMatch;
  }
}
