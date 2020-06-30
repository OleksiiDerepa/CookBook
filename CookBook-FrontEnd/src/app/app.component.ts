import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalCreateOptionsComponent } from './modal-create-options/modal-create-options.component';
import { RecipeService } from './recipe.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'CookBook-FrontEnd';
  chosenItemId: string;
  constructor(
  public modalService: NgbModal, 
  private recipeService: RecipeService) { }

  onChosenItem(id: string){
    console.log(id);
    this.chosenItemId = id;
   }

  openModal() {
    const modalRef = this.modalService.open(ModalCreateOptionsComponent);
    modalRef.componentInstance.recipe = {
      id: '',
      title: 'your title',
      description: 'your description'
    };

    modalRef.result.then((result) => {
      if (result) {
        this.recipeService.addRecipe(result)
        .subscribe(response => {
        console.log(response);
		this.chosenItemId = '';
        });
      }
    });
  }
}
