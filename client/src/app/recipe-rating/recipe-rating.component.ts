import { ChangeDetectorRef, Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RecipeRatingListComponent } from './components/recipe-rating-list/recipe-rating-list.component';
import { RecipeService } from '../shared/services/recipe.service';
import { IRating, IRecipe } from '../shared/types/recipe';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RatingComponent } from '../home/components/rating/rating.component';
import { ICreateOrUpdateUserRecipeRatingRequest } from '../shared/types/requests/create-or-update-user-recipe-rating-request';

@Component({
  selector: 'app-recipe-rating',
  standalone: true,
  imports: [CommonModule, RecipeRatingListComponent, RouterModule, ReactiveFormsModule, RatingComponent],
  templateUrl: './recipe-rating.component.html',
  styleUrl: './recipe-rating.component.scss'
})
export class RecipeRatingComponent implements OnInit {

  recipeId!: string;
  ratingForm!: FormGroup;

  ratings =  signal<IRating[]>([]);
  userRating = signal<IRating | null>(null);
  recipe = signal<IRecipe | null>(null);

  recipeService: RecipeService = inject(RecipeService);
  activatedRoute: ActivatedRoute = inject(ActivatedRoute);
  router: Router = inject(Router);
  fb: FormBuilder = inject(FormBuilder);
  
  ngOnInit(): void {
    this.recipeId = String(this.activatedRoute.snapshot.paramMap.get('recipeId'));

    this.createRatingForm();
    this.loadUserRating();
    this.loadRecipeDetails();
    this.loadRecipeRatings();
  }

  createRatingForm(): void {
    this.ratingForm = this.fb.group({
      stars: ["0", Validators.required],
      comment: [""]
    })
  }

  loadUserRating(): void {
    this.recipeService.getUserRecipeRating(this.recipeId).subscribe({
      next: (rating: IRating | null) => {
        this.userRating.set(rating);
        this.ratingForm.get('comment')?.setValue(rating?.comment ?? '')
        this.ratingForm.get('stars')?.setValue(rating?.stars.toString() ?? '')
      },
      error: (err: HttpErrorResponse) => this.userRating.set(null)
    })
  }

  loadRecipeDetails(): void {
    this.recipeService.getRecipeById(this.recipeId).subscribe({
      next: (recipe: IRecipe | null) => this.recipe.set(recipe),
      error: (err: HttpErrorResponse) => console.log(err)
    })
  }

  loadRecipeRatings(): void {
    this.recipeService.getAllRecipeRatings(this.recipeId).subscribe({
      next: (ratings: IRating[]) => {
        this.ratings.set(ratings);
      },
      error: (err: HttpErrorResponse) => console.log(err)
    });
  }

  onRatingSubmit(): void {
    if(this.ratingForm.valid && this.ratingForm.get('stars')?.value !== "0") {
      var request : ICreateOrUpdateUserRecipeRatingRequest = {
        recipeId: this.recipeId,
        stars: Number(this.ratingForm.get('stars')?.value),
        comment: this.ratingForm.get('comment')?.value,
      }
      this.recipeService.createOrUpdateUserRecipeRating(request).subscribe({
        next: (rating: IRating | null) => {
          this.ratingForm.reset(rating);
          this.updateRatingList(rating);
        },
        error: (err: HttpErrorResponse) => console.log(err)
      });
    }
  }

  private updateRatingList(newRating: IRating | null): void {
    if(newRating) {
      const index = this.ratings().findIndex(rating => rating.email === newRating.email);

      if(index !== -1) {
        this.ratings()[index] = { ...this.ratings()[index], ...newRating };
      } else {
        this.ratings().push(newRating);
      }

      window.location.reload();
    }
  }
}
