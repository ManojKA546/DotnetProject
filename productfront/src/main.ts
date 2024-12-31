import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { provideRouter } from '@angular/router';
import { routes } from './app/app.routes';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { RouterLink } from '@angular/router';



bootstrapApplication(AppComponent, {
  providers: [provideRouter(routes), 
    provideHttpClient(withFetch()),
  ],
}).catch((err) => console.error(err));