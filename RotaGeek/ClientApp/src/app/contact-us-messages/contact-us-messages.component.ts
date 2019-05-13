import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-contact-us-messages',
  templateUrl: './contact-us-messages.component.html'
})
export class ContactUsMessagesComponent {
  public messages: ContactMessage[];

  constructor(private _http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    _http.get<ContactMessage[]>(baseUrl + 'api/ContactUs/ContactMessages')
      .subscribe(result => { this.messages = result; }, error => console.error(error));
  }

  deleteMessage(message) {
    return this._http.delete(this.baseUrl + 'api/ContactUs/Delete/' + message.id,)
      .subscribe(result =>
      {
        var index = this.messages.indexOf(message);
        this.messages.splice(index, 1);
      });
  }
}

interface ContactMessage {
  id: number
  name: string;
  email: string;
  message: string;
  date: Date;
}
