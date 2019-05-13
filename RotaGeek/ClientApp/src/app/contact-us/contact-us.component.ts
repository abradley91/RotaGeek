import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
  selector: 'app-home',
  templateUrl: './contact-us.component.html',
})
export class ContactUsComponent {
  url: string = "";
  public contactMessage = {
    name: "",
    email: "",
    message: ""
  }

  constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl;
  }

  onSubmit() {
    console.log('submitting: ' + JSON.stringify(this.contactMessage) + " " + this.url + 'api/ContactUs/Save');

    return this._http.post(this.url + 'api/ContactUs/Save/', this.contactMessage)
      .subscribe(result => {
        this.clearForm();
        alert('Message created.');
      });
  }

  clearForm() {
    this.contactMessage.name = "";
    this.contactMessage.email = "";
    this.contactMessage.message = "";
  }
}
