import { Injectable } from '@angular/core';
import { Recipe } from './models/recipe';
import { CollectionApiResponse } from './models/collectionApiResponse';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { ItemApiResponse } from './models/itemApiResponse';


@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  private recipeUrl = 'http://localhost:5000/api/recipe';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getParentHierarchy(recipeId: string): Observable<CollectionApiResponse<Recipe>> {
    const url = `${this.recipeUrl}/parent-hierarchy/${recipeId}`;
    return this.http.get<CollectionApiResponse<Recipe>>(url)
    .pipe(
      tap(_ => console.log('fetched recipes')),
      catchError(this.handleError<CollectionApiResponse<Recipe>>('getRecipes', null))
    );
  }


  /** GET Recipes from the server */
  getRecipes(parentId: string = null, page: number = 1, size: number = 10): Observable<CollectionApiResponse<Recipe>> {
    const url = `${this.recipeUrl}?Page=${page}&Size=${size}${(parentId != null ? `&ParentId=${parentId}` : '')}`;
    
    return this.http.get<CollectionApiResponse<Recipe>>(url)
    .pipe(
      tap(_ => console.log('fetched recipes')),
      catchError(this.handleError<CollectionApiResponse<Recipe>>('getRecipes', null))
    );
  }

  /** GET Recipe by id. Will 404 if id not found */
  getRecipe(id: string): Observable<ItemApiResponse<Recipe>> {
  const url = `${this.recipeUrl}/${id}`;
  return this.http.get<ItemApiResponse<Recipe>>(url)
      .pipe(
        tap(_ => console.log(`fetched recipe id=${id}`)),
        catchError(this.handleError<ItemApiResponse<Recipe>>(`getHero id=${id}`))
    );
  }

  /** PUT: update the hero on the server */
  updateRecipe(recipe: Recipe): Observable<any> {
    const url = `${this.recipeUrl}/${recipe.id}`;

    return this.http.put(url, recipe, this.httpOptions).pipe(
      tap(_ => console.log(`updated Recipe id=${recipe.id}`)),
      catchError(this.handleError<any>('updateRecipe'))
    );
  }

  /** POST: add a new hero to the server */
  addRecipe(recipe: any): Observable<any> {
    return this.http.post<Recipe>(this.recipeUrl, recipe, this.httpOptions).pipe(
      tap((newRecipe) => console.log(`added recipe w/ id=${newRecipe}`)),
      catchError(this.handleError<any>('addRecipe'))
    );
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error); // log to console instead
      console.error(`${operation} failed: ${error.message}`); // log to console instead
 
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
