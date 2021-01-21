import { Injectable } from '@angular/core';
import { FormBuilder, Validators} from '@angular/forms';
import { HttpClient} from "@angular/common/http";
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  readonly BaseURI = environment.apiBaseUrl;
  UserID:any
  UserFullName : any
  UserN : any
  constructor(private fb: FormBuilder, private http: HttpClient) { }
  

  formModel = this.fb.group({
    Email: ['', Validators.email],
    FirstName: [''],
    LastName: [''],
  });
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

  getToken(){
    return {"token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySUQiOiI1NmVmMmU2MS00NzI0LTRlYWUtODkzNS01YjBkM2YxOGJhODMiLCJVc2VyTmFtZSI6ImVoYXNhbkBvdXRsb29rLmNvbSIsIkZ1bGxOYW1lIjoiRWhhc2FudWwgIEhvcXVlIiwibmJmIjoxNjExMjA5MDEwLCJleHAiOjE2MTEyOTU0MTAsImlhdCI6MTYxMTIwOTAxMH0.jZ7zKn6wBKZxLK45yc_15zSXCCErJ2bMr3i78lKiqIU","user":{"firstName":"Ehasanul ","lastName":"Hoque","isOnline":true,"id":"56ef2e61-4724-4eae-8935-5b0d3f18ba83","userName":"ehasan@outlook.com","normalizedUserName":"EHASAN@OUTLOOK.COM","email":"ehasan@outlook.com","normalizedEmail":"EHASAN@OUTLOOK.COM","emailConfirmed":false,"passwordHash":"AQAAAAEAACcQAAAAEK9+jOlmYETk6Xc1dPovNI6iw43IAkqSR76IoJ1xHMWMfTynrOi99e0HDxk+syGsPw==","securityStamp":"KZKMM465CN4QOOYSIXN5QDJ437EGDNCH","concurrencyStamp":"11f46505-4a2f-4332-af5b-f65658874239","phoneNumber":null,"phoneNumberConfirmed":false,"twoFactorEnabled":false,"lockoutEnd":null,"lockoutEnabled":true,"accessFailedCount":0}};
  }

}
