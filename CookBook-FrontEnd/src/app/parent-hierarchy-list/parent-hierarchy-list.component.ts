import { Component, OnInit, Input, Output, EventEmitter, SimpleChanges } from '@angular/core';
import { RecipeService } from '../recipe.service';
import { Recipe } from '../models/recipe';
import { CollectionApiResponse } from '../models/collectionApiResponse';


@Component({
  selector: 'app-parent-hierarchy-list',
  templateUrl: './parent-hierarchy-list.component.html',
  styleUrls: ['./parent-hierarchy-list.component.css']
})
export class ParentHierarchyListComponent implements OnInit {
  page: CollectionApiResponse<Recipe>;
  recipes: Recipe[];
  @Output() onChosenItemE = new EventEmitter<string>();
  @Input() public chosenItemId: string;

  constructor(private recipeService: RecipeService) { }

  ngOnInit(): void {
    this.getParentHierarchy('');    
  }

  chosenItem(id: string){
    console.log(id);
    this.onChosenItemE.emit(id);
	this.getParentHierarchy(id);
  }

  getParentHierarchy(id: string){
    console.log(id);

  if (id){
    this.recipeService.getParentHierarchy(id)
      .subscribe(response => {
        this.page = response;
        this.recipes = [
            {
              id: '',
              title: "Home"
            } as Recipe,
            ... response.content];
          });
    }
	else
	{
        this.recipes = [
            {
              id: '',
              title: "Home"
            } as Recipe];          
	}
  }

  ngOnChanges(changes: SimpleChanges): void {
    let chng = changes["chosenItemId"];
    console.log(chng);
    this.getParentHierarchy(this.chosenItemId);
  }
}
