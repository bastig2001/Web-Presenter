import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AudienceComponent } from './audience/audience.component';
import { PresenterComponent } from './presenter/presenter.component';
import { PresentationMenuComponent } from './presentation-menu/presentation-menu.component';
import { PresentationViewComponent } from './presentation-view/presentation-view.component';
import { PresentationControlsComponent } from './presentation-controls/presentation-controls.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AudienceComponent,
    PresenterComponent,
    PresentationMenuComponent,
    PresentationViewComponent,
    PresentationControlsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'audience/:id', component: AudienceComponent},
      { path: 'presenter/:id', component: PresenterComponent}
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
