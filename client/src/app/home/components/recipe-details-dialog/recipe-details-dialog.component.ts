import { AfterViewInit, Component, ElementRef, Input, OnChanges, OnInit, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IRating, IRecipe } from '../../../shared/types/recipe';
import { RecipeService } from '../../../shared/services/recipe.service';
import { RatingComponent } from '../rating/rating.component';
import { IngredientsOverviewTableComponent } from '../../../shared/components/ingredients-overview-table/ingredients-overview-table.component';
import { StepsOverviewComponent } from '../../../shared/components/steps-overview/steps-overview.component';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-recipe-details-dialog',
  standalone: true,
  imports: [CommonModule, RatingComponent, IngredientsOverviewTableComponent, StepsOverviewComponent],
  templateUrl: './recipe-details-dialog.component.html',
  styleUrl: './recipe-details-dialog.component.scss'
})
export class RecipeDetailsDialogComponent {
  recipeService: RecipeService = inject(RecipeService);
}
