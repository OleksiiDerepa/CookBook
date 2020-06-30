import { Component, OnInit, Input, Output, EventEmitter, SimpleChanges } from '@angular/core';
import { RecipeService } from '../recipe.service';
import { Recipe } from '../models/recipe';
import { ItemApiResponse } from '../models/itemApiResponse';
import { ModalCreateOptionsComponent } from '../modal-create-options/modal-create-options.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';


@Component({
  selector: 'app-recipe-full-item',
  templateUrl: './recipe-full-item.component.html',
  styleUrls: ['./recipe-full-item.component.css']
})
export class RecipeFullItemComponent implements OnInit {

  recipe: Recipe;
  @Input() public chosenItemId: string;

  constructor(private recipeService: RecipeService,
  public modalService: NgbModal) { }

  ngOnInit(): void {
    this.getRecipe('');    
  }

  getRecipe(id: string){
    console.log(id);

  if (id){
		this.recipeService.getRecipe(id)
		  .subscribe(response => this.recipe = response.content);
    }
	else
	{
        this.recipe = {
              id: "",
              title: '',
			  description: '',
			  parentId: ''
            } as Recipe;          
	}
  }


  ngOnChanges(changes: SimpleChanges): void {
    let chng = changes["chosenItemId"];
    console.log(chng);
    this.getRecipe(this.chosenItemId);
  }


	update(){
		console.log(this.recipe);  
		const modalRef = this.modalService.open(ModalCreateOptionsComponent);
		modalRef.componentInstance.recipe = Object.assign({}, this.recipe);
		modalRef.result.then((result) => {
		  if (result) {
			console.log(result);
			this.recipeService.updateRecipe(result)
		  .subscribe(response => console.log(response));
		  this.recipe = result;
		  }
		});
		
	  }
}
