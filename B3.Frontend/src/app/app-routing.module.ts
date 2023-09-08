import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RendimentoCdbComponent } from './components/rendimento-cdb/rendimento-cdb.component';

const routes: Routes = [
  { path:'cdb', component: RendimentoCdbComponent },
  { path:'',  redirectTo: 'cdb', pathMatch: 'full'},
  { path:'**',  redirectTo: 'cdb', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
