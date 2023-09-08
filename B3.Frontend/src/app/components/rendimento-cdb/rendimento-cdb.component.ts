import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { CalculoRendimento } from 'src/app/models/CalculoRendimento';
import { RentabilidadeCdb } from 'src/app/models/RentabilidadeCdb';
import { CdbService } from 'src/app/services/cdb.service';
import { ModalComponent } from 'src/app/shared/modal/modal.component';

@Component({
  selector: 'app-rendimento-cdb',
  templateUrl: './rendimento-cdb.component.html',
  styleUrls: ['./rendimento-cdb.component.scss']
})
export class RendimentoCdbComponent implements OnInit {

  public form: FormGroup = new FormGroup({});
  public calculoRendimento: CalculoRendimento | null = null;
  public rentabilidadeCdb: RentabilidadeCdb | null = null;

  constructor(
    private fb: FormBuilder,
    private cdbService: CdbService,
    private modalService: BsModalService
  ) { }

  ngOnInit(): void {
    this.validation();
  }

  private validation() : void {
    this.form = this.fb.group({
      valorInicial: ['', [Validators.required, Validators.min(0.01)]],
      numeroMeses: ['', [Validators.required, Validators.min(2), Validators.pattern("^[0-9]*$")]]
    });
  }

  public cssValidator(campoForm: FormControl | AbstractControl) : any {
    return {'is-invalid' : campoForm.errors && campoForm.touched};
  }

  public calcularRendimento() : void {
    if (this.form.valid) {
      this.calculoRendimento = {...this.form.value};
      this.cdbService.rendimentoCdb(this.calculoRendimento!)
        .subscribe(
          (rentabilidade: RentabilidadeCdb) => this.rentabilidadeCdb = {...rentabilidade},
          (error: any) => {
            const initialState: ModalOptions = {
              initialState: {
                mensagem: 'Erro ao calcular rendimento. \n Por favor, verifique se a API está online!'
              }
            }
            this.modalService.show(ModalComponent, initialState);
          }
        );
    }
  }
}
