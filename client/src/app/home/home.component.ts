import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FilterSectionComponent } from './components/filter-section/filter-section.component';
import { RouterModule } from '@angular/router';
import { RecipeListComponent } from './components/recipe-list/recipe-list.component';
import { IRecipe } from '../shared/types/recipe';
import { RecipeService } from '../shared/services/recipe.service';
import { HttpErrorResponse } from '@angular/common/http';
import { RecipeParams } from '../shared/types/recipe-params';
import { RecipeDetailsDialogComponent } from './components/recipe-details-dialog/recipe-details-dialog.component';
import { map } from 'rxjs';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, FilterSectionComponent, RecipeListComponent, RouterModule, RecipeDetailsDialogComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {

  recipes = signal<IRecipe[]>([]);

  recipeService: RecipeService = inject(RecipeService);

  ngOnInit(): void {
    this.loadRecipes();
  }

  loadRecipes(): void {
    this.recipeService.getRecipes().pipe(
      map((recipes: IRecipe[]) => {
        recipes.forEach(recipe => {
          recipe.steps.sort((a, b) => a.number - b.number);
        });
        this.setRecipes(recipes);
      })
    )
    .subscribe();
  }

  setRecipes(recipes: IRecipe[]): void {
    this.recipes.set(recipes);
  }

  resetRecipeParams(): void {
    this.recipeService.setRecipeParams(new RecipeParams());
  }

}
