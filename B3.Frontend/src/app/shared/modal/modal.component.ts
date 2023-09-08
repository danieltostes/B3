import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss']
})
export class ModalComponent implements OnInit {
  
  public mensagem?: string;
  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit(): void {
  }

  public closeModal() : void {
    this.bsModalRef.hide();
  }

}
