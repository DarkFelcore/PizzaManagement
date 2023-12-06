import { Component, Input, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IStar } from '../../../shared/types/star';
import { HttpErrorResponse } from '@angular/common/http';
import { IRating } from '../../../shared/types/recipe';
import { EMPTY_RATING } from '../../../shared/constants/constants';
import { RecipeService } from '../../../shared/services/recipe.service';
import { ICreateOrUpdateUserRecipeRatingRequest } from '../../../shared/types/requests/create-or-update-user-recipe-rating-request';

@Component({
  selector: 'app-rating',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './rating.component.html',
  styleUrl: './rating.component.scss'
})
export class RatingComponent {
  recipeService: RecipeService = inject(RecipeService);

  rating = signal<IRating | null>(EMPTY_RATING);
  stars = signal<IStar[]>([
    { value: 1 },
    { value: 2 },
    { value: 3 },
    { value: 4 },
    { value: 5 },
  ]);

  @Input() recipeId!: string;

  ngOnChanges(): void {
    // Need to use this lifecycle because we should fetch whenever the dialog opens
    this.loadUserRecipeRating();
  }

  onRatingChanged(value: number) : void {
    if(this.rating()?.stars !== value) {
      var request: ICreateOrUpdateUserRecipeRatingRequest = {
        recipeId: this.recipeId,
        stars: value,
      }

      this.recipeService.createOrUpdateUserRecipeRating(request).subscribe({
        next: (rating: IRating | null) => {
          this.rating.set(rating);
          this.loadActiveStars();
        },
        error: (err: HttpErrorResponse) => {
          console.log(err)
        }
      })
    }
  }
  
  private loadUserRecipeRating(): void {
    this.recipeService.getUserRecipeRating(this.recipeId).subscribe({
      next: (rating : IRating | null) => {
        this.rating.set(rating);
        this.loadActiveStars();
      },
      error: (err: HttpErrorResponse) => {
        this.rating.set(null)
        this.loadActiveStars();
        console.log(err)
      }
    })
  }

  private loadActiveStars(): void {
    let starElements = document.querySelectorAll('.stars');
    this.clearActiveStars();
    if(this.rating() !== null) {
      for(let i = 0; i < this.stars().length; i++) {
        if(this.stars()[i].value <= (this.rating()?.stars as number)) {
          starElements[i].classList.add('selected');
        }
      }
    } 
  }
  
  private clearActiveStars() {
    let starElements = document.querySelectorAll('.stars');
    for(let i = 0; i < this.stars().length; i++) {
      starElements[i].classList.remove('selected');
    }
  }

}
