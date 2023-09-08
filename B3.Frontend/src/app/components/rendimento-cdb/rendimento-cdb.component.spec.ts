import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RendimentoCdbComponent } from './rendimento-cdb.component';

describe('RendimentoCdbComponent', () => {
  let component: RendimentoCdbComponent;
  let fixture: ComponentFixture<RendimentoCdbComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RendimentoCdbComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RendimentoCdbComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
