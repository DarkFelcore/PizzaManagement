import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IRating } from '../../../shared/types/recipe';
import { UserRecipeRatingComponent } from '../user-recipe-rating/user-recipe-rating.component';

@Component({
  selector: 'app-recipe-rating-item',
  standalone: true,
  imports: [CommonModule, UserRecipeRatingComponent],
  templateUrl: './recipe-rating-item.component.html',
  styleUrl: './recipe-rating-item.component.scss'
})
export class RecipeRatingItemComponent {
  @Input() rating!: IRating;
}
