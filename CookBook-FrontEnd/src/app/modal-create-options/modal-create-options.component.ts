import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Recipe } from '../models/recipe';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-modal-create-options',
  templateUrl: './modal-create-options.component.html',
  styleUrls: ['./modal-create-options.component.scss']
})
export class ModalCreateOptionsComponent implements OnInit {
  @Input() public recipe: Recipe;
  @Output() passEntry: EventEmitter<Recipe> = new EventEmitter<Recipe>();

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit(): void {

  }

  save(): void {
    this.passEntry.emit(this.recipe);
    this.activeModal.close(this.recipe);
  }
}
