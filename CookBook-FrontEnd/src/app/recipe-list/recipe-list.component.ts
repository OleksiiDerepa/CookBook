import { Component, OnInit, Output, EventEmitter, SimpleChanges, Input } from '@angular/core';
import { RecipeService } from '../recipe.service';
import { Recipe } from '../models/recipe';
import { CollectionApiResponse } from '../models/collectionApiResponse';


@Component({
  selector: 'app-recipe-list',
  templateUrl: './recipe-list.component.html',
  styleUrls: ['./recipe-list.component.css']
})
export class RecipeListComponent implements OnInit {
  page: CollectionApiResponse<Recipe>;
  recipes: Recipe[];
  @Output() onChosenItemE = new EventEmitter<string>();
  @Input() public chosenItemId: string;

  constructor(private recipeService: RecipeService) { }

  ngOnInit(): void {
    this.getRecipes('');
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.getRecipes(this.chosenItemId);
  }

  getRecipes(id: string): void {
    console.log(id);
    
    this.recipeService.getRecipes(id)
        .subscribe(response => {
          this.page = response;
          this.recipes = response.content;
        });
  }

  onChosenItem(id: string){
    this.onChosenItemE.emit(id);

    this.getRecipes(id);
  }
  
  onCreate(item: Recipe){
    console.log(item);

    var createItem = {
		title: item.title,
		description: item.description,
		parentId: item.id
    };

    this.recipeService.addRecipe(createItem)
        .subscribe(response => {
          console.log(response);
        });
  }

  onUpdate(item: Recipe){
    console.log(item);

    this.recipeService.updateRecipe(item)
        .subscribe(response => {
          console.log(response);
		  this.getRecipes(this.chosenItemId);
        });
  }
}
