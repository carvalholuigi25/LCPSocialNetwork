import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public connected: boolean = false;
  public hubConnection: signalR.HubConnection = this.buildConnection();

  constructor() {
  }

  public buildConnection() {
    return new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5001/chathub', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Information)
      .build();
  }

  public startConnection = async () => {
    try {
      await this.hubConnection.start();
      console.log("SignalR Connected.");
    } catch (err) {
      console.log(err);
      // setTimeout(this.startConnection, 5000);
    }
  }

  public addMessageListener = () => {
    this.hubConnection.on('ReceiveMessage', (user: string, message: string) => {
      console.log(`Received message from ${user}: ${message}`);
    });
  }

  public sendMessage = (methodname: string, user: string, message: string) => {
    this.hubConnection.send(methodname, user, message);
  }

  public getState = () => {
    console.log(this.hubConnection.state);
  }

  public closeConnection = () => {
    this.hubConnection.onclose(async () => {
      await this.startConnection();
    });
  }
}
