import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Input } from '@angular/core';
import { Recipe } from '../models/recipe';
import { ModalCreateOptionsComponent } from '../modal-create-options/modal-create-options.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-recipe-short-item',
  templateUrl: './recipe-short-item.component.html',
  styleUrls: ['./recipe-short-item.component.css']
})
export class RecipeShortItemComponent implements OnInit {

  @Input() recipe: Recipe;
  @Output() onCreate = new EventEmitter<Recipe>();
  @Output() onUpdate = new EventEmitter<Recipe>();
  @Output() onChosenItem = new EventEmitter<string>();
  
  constructor(public modalService: NgbModal) { }

  ngOnInit(): void {
  }

  chosenItem(id: string){
    this.onChosenItem.emit(id);
  }

  create() {
    const modalRef = this.modalService.open(ModalCreateOptionsComponent);
    modalRef.componentInstance.recipe = Object.assign({}, this.recipe);
    modalRef.result.then((result) => {
      if (result) {
        this.onCreate.emit(result);
        console.log(result);
      }
    });
  }
  
  update(){
	console.log(this.recipe);  
    const modalRef = this.modalService.open(ModalCreateOptionsComponent);
    modalRef.componentInstance.recipe = Object.assign({}, this.recipe);
    modalRef.result.then((result) => {
      if (result) {
        this.onUpdate.emit(result);
        console.log(result);
      }
    });
  }
}
