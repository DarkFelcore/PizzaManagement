import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { IRating, IRecipe } from '../types/recipe';
import { BehaviorSubject, Observable, ReplaySubject, map, of } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import { RecipeParams } from '../types/recipe-params';
import { EMPTY_RATING } from '../constants/constants';
import { ICreateOrUpdateUserRecipeRatingRequest } from '../types/requests/create-or-update-user-recipe-rating-request';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {
  recipes: IRecipe[] = [];
  rating: IRating | null = EMPTY_RATING;
  recipeForDialog!: IRecipe;

  http: HttpClient = inject(HttpClient);

  recipeParams: RecipeParams = new RecipeParams();

  recipeCache = new Map();

  ignoreCache: boolean = false;

  getRecipes() : Observable<IRecipe[]> {
    const params = this.createHttpParams();

    if(this.recipeCache.size > 0 && !this.ignoreCache) {
      if(this.recipeCache.has(Object.values(this.recipeParams).join("-"))) {
        this.recipes = this.recipeCache.get(Object.values(this.recipeParams).join("-"));
        return of(this.recipes);
      }
    }

    return this.http.get<IRecipe[]>(environment.baseUrl + "recipes", { params }).pipe(
      map((recipes: IRecipe[]) => {
        this.ignoreCache = false;
        this.recipeCache.set(Object.values(this.recipeParams).join("-"), recipes);
        return recipes;
      })
    );
  }

  getRecipeById(recipeId: string): Observable<IRecipe | null> {
    this.recipeCache.forEach((recipes: IRecipe[]) => {
      const recipeFound = recipes.find(recipe => recipe.id === recipeId);
      return of(recipeFound);
    })
    return this.http.get<IRecipe | null>(environment.baseUrl + 'recipes/' + recipeId);
  }

  createRecipe(data: FormData) : Observable<IRecipe> {
    // This is done in order the refetch the backend recipe items because a new item was added.
    // If we dont do this, the app will use the previous cache and dont have the latest added item.
    this.ignoreCache = true;
    return this.http.post<IRecipe>(environment.baseUrl + 'recipes', data);
  }

  getUserRecipeRating(recipeId: string) : Observable<IRating | null> {
    return this.http.get<IRating | null>(environment.baseUrl + 'recipes/rating/' + recipeId).pipe(
      map((rating: IRating | null) => {
        this.rating = rating;
        return rating;
      })
    )
  }

  createOrUpdateUserRecipeRating(request: ICreateOrUpdateUserRecipeRatingRequest) : Observable<IRating | null> {
    return this.http.put<IRating | null>(environment.baseUrl + 'recipes/rating', request).pipe(
      map((rating: IRating | null) => {
        this.rating = rating;
        return rating;
      })
    )
  }

  getAllRecipeRatings(recipeId: string): Observable<IRating[]> {
    return this.http.get<IRating[]>(environment.baseUrl + 'recipes/ratings/' + recipeId);
  }

  getRecipeParams(): RecipeParams {
    return this.recipeParams;
  }

  setRecipeParams(params: RecipeParams): void {
    this.recipeParams = params;
  }

  getRecipeForDialog(): IRecipe {
    return this.recipeForDialog;
  }

  setRecipeForDialog(recipe: IRecipe): void {
    this.recipeForDialog = recipe;
  }

  private createHttpParams(): HttpParams {
    let params= new HttpParams();

    if(this.recipeParams.search) {
      params = params.append('search', this.recipeParams.search);
    }

    return params;
  }
}
