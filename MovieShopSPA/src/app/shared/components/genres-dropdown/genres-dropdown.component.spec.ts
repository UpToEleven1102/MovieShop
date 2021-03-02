import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenresDropdownComponent } from './genres-dropdown.component';

describe('GenresDropdownComponent', () => {
  let component: GenresDropdownComponent;
  let fixture: ComponentFixture<GenresDropdownComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GenresDropdownComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GenresDropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
