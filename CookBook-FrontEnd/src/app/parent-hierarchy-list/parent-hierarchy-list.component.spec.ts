import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ParentHierarchyListComponent } from './parent-hierarchy-list.component';

describe('ParentHierarchyListComponent', () => {
  let component: ParentHierarchyListComponent;
  let fixture: ComponentFixture<ParentHierarchyListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ParentHierarchyListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ParentHierarchyListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
