import { Component, OnInit, inject } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-root',
    standalone: false,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
})

export class AppComponent implements OnInit {
    authService = inject(AuthenticationService);
    router = inject(Router);

    ngOnInit(): void {
        this.authService.setSignals();   
    }

    logout(){
        this.authService.logout();        
        this.router.navigateByUrl('');
    }
}