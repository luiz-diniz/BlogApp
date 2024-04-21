import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../services/users.service';
import { ActivatedRoute } from '@angular/router';
import { UserProfileModel } from '../../models/user.profile.model';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss'
})

export class UserComponent implements OnInit{

  user: UserProfileModel = new UserProfileModel;

  constructor(private usersService: UsersService, private route: ActivatedRoute, private sanitizer: DomSanitizer){
  }

  ngOnInit() : void{
    const routeUsername = this.route.snapshot.paramMap.get('username');

    if(routeUsername !== null){
      this.usersService.getUserProfile(routeUsername).subscribe({
        next: (user) => {
          this.user = user;
          this.user.profileImageContentSafe = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + user.profileImageContent);
        },
        error: (e) => console.log(e)
      })
    }
  }
}