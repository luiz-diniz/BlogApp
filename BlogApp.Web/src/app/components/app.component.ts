import { Component, OnInit, inject } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';

@Component({
    selector: 'app-root',
    standalone: false,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
})

export class AppComponent implements OnInit {
    authenticated: boolean;
    username: string;
    authService = inject(AuthenticationService);

    ngOnInit(): void {
        this.authService.setTokenSignal();
    }

    logout(){
        this.authService.logout();
    }
}