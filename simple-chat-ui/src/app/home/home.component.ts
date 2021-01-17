import { UserService } from '../service/user.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { environment } from 'src/environments/environment';
import { MessageService } from '../service/message.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  userDetails=JSON.parse(localStorage.getItem("login-user"))
  users;
  chatUser;

  messages : any[]=[];
  displayMessages : any[]=[]
message : string
hubConnection: HubConnection;

UserID=this.service.UserID;
UserConnectionID
UsersFullName
UsersConnectionId
selectedUser:number =0
usersss
sender
connectedUsers : any[] = []
  constructor(private router: Router, private service: UserService,private messageService:MessageService) { }

  ngOnInit() {
       this.messageService.getUserReceivedMessages(this.userDetails.id).subscribe((item:any)=>{
         if(item){
           this.messages=item;
           this.messages.forEach(x=>{
            x.type=x.receiver===this.userDetails.id?'recieved':'sent';
           })
           console.log(this.messages);
         }
       })
        this.service.getAll().subscribe(
          (user:any) => {
            if(user){
            this.users=user.filter(x=>x.email!==this.userDetails.email);
            this.users.forEach(item=>{
              item['isActive']=false;
            })
            this.makeItOnline();
            }
          },
          err => {
            console.log(err);
          },
        );
   
   
    

    this.message=''
    this.hubConnection = new HubConnectionBuilder().withUrl(environment.chatHubUrl).build();
    const self = this
    this.hubConnection.start()
    .then(()=>{
      self.hubConnection.invoke("OnConnect",this.userDetails.id,this.userDetails.firstName,this.userDetails.userName)
      .then(()=>console.log('User Sent Successfully'))
      .catch(err => console.error(err));

      this.hubConnection.on("OnConnect",Usrs=>
      {
        this.connectedUsers = Usrs;
        this.makeItOnline();
      })
      this.hubConnection.on("OnDisconnect",Usrs=>{
        this.connectedUsers = Usrs;
       this.users.forEach(item => {
         item.isOnline=false;
       });
        this.makeItOnline();
      })
    })
    .catch(err => console.log(err));
    
    this.hubConnection.on("UserConnected", (connectionId) =>this.UserConnectionID=connectionId);

    this.hubConnection.on('receiveMessageAll', (connectionId ,message) => {
      message.type='recieved';
      this.messages.push(message)
    })
    
    this.hubConnection.on('ReceiveDM', (connectionId,message) => {
      message.type='recieved';
      this.messages.push(message);
      let curentUser=this.users.find(x=>x.id===message.sender);
      this.chatUser=curentUser;
      this.users.forEach(item=>{
        item['isActive']=false;
      });
      var user=this.users.find(x=>x.id==this.chatUser.id);
      user['isActive']=true;
      this.displayMessages=this.messages.filter(x=>(x.type==='sent' && x.receiver===this.chatUser.id) || (x.type==='recieved' && x.sender===this.chatUser.id));
    })
  }

  SendDirectMessage(){
    if(this.message!='' && this.message.trim()!='')
    {
    var connectedUser =this.connectedUsers.find(x=>x.username===this.chatUser.userName);
     var msg={
       sender:this.userDetails.id,
       receiver:this.chatUser.id,
       messageDate:new Date(),
       type:'sent',
       content:this.message
     };
     this.messages.push(msg);
     this.displayMessages=this.messages.filter(x=>(x.type==='sent' && x.receiver===this.chatUser.id) || (x.type==='recieved' && x.sender===this.chatUser.id));

    this.hubConnection.invoke('SendMessageToUser',msg)
    .then(()=>console.log('Message to user Sent Successfully'))
    .catch(err => console.error(err));
    this.message='';
    }
  }

  openChat(user){
    this.users.forEach(item=>{
      item['isActive']=false;
    });
    user['isActive']=true;
    this.chatUser=user;
    this.displayMessages=this.messages.filter(x=>(x.type==='sent' && x.receiver===this.chatUser.id) || (x.type==='recieved' && x.sender===this.chatUser.id));;
  }

  makeItOnline(){
    if(this.connectedUsers && this.users){
    this.connectedUsers.forEach(item=>{
      var u=this.users.find(x=>x.userName==item.username);
      if(u){
        u.isOnline=true;
      }
    })
  }
  }

  onLogout() {
    this.hubConnection.invoke("RemoveOnlineUser",this.userDetails.id)
      .then(()=>{
        this.messages.push('User Disconnected Successfully')
      })
      .catch(err => console.error(err));
    localStorage.removeItem('token');
    this.router.navigate(['/user/login']);
  }
}
