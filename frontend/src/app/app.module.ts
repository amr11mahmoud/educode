import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { RouterModule, Routes, RouterOutlet } from '@angular/router';
import { FooterComponent } from './components/footer/footer.component';
import { SharedModule } from './modules/shared/shared.module';
import { HomeComponent } from './modules/home/home.component';
import { HomeModule } from './modules/home/home.module';

const routes:Routes = [
  {
    path:"",
    component:HomeComponent
  },
  {
    path:"footer",
    component:FooterComponent
  }
];

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    RouterModule.forRoot(routes),
    BrowserModule,
    RouterOutlet,
    HomeModule,
    SharedModule
  ],
  exports:[
    SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
  
}
