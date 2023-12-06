import { Component, ElementRef, Input, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IRecipe } from '../../../shared/types/recipe';
import { RecipeService } from '../../../shared/services/recipe.service';
import { AuthService } from '../../../auth/auth.service';
import { IUser } from '../../../shared/types/user';
import { HttpErrorResponse } from '@angular/common/http';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-recipe-item',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './recipe-item.component.html',
  styleUrl: './recipe-item.component.scss'
})
export class RecipeItemComponent {

  currentUser!: IUser | null;

  recipeService: RecipeService = inject(RecipeService);
  authService: AuthService = inject(AuthService);
  router: Router = inject(Router);

  @Input() recipe! : IRecipe;

  @ViewChild('modalButton') modalButton!: ElementRef;

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser(): void {
    this.authService.currentUser$.subscribe({
      next: (user: IUser | null) => this.currentUser = user,
      error: (err: HttpErrorResponse) => console.log(err),
    });
  }

  onViewModalClicked = (recipe: IRecipe) => {
    if(this.currentUser === null) {
      this.modalButton.nativeElement.removeAttribute('recipe-datails')
    } else {
      this.modalButton.nativeElement.setAttribute('data-target', '.recipe-datails')
    }
    this.recipeService.setRecipeForDialog(recipe)
  }

}
