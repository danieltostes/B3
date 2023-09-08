import { NgModule, LOCALE_ID } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RendimentoCdbComponent } from './components/rendimento-cdb/rendimento-cdb.component';
import { TituloComponent } from './shared/titulo/titulo.component';
import { NgxCurrencyModule } from 'ngx-currency';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CdbService } from './services/cdb.service';
import ptBr from '@angular/common/locales/pt';
import { registerLocaleData } from '@angular/common';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ModalComponent } from './shared/modal/modal.component';

registerLocaleData(ptBr);

@NgModule({
  declarations: [
    AppComponent,
    RendimentoCdbComponent,
    TituloComponent,
    ModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NgxCurrencyModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ModalModule.forRoot()
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'pt'},
    CdbService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
