import { Component, Input, WritableSignal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IRecipe } from '../../../shared/types/recipe';
import { RecipeItemComponent } from '../recipe-item/recipe-item.component';

@Component({
  selector: 'app-pizza-list',
  standalone: true,
  imports: [CommonModule, RecipeItemComponent],
  templateUrl: './recipe-list.component.html',
  styleUrl: './recipe-list.component.scss'
})
export class RecipeListComponent {

  @Input() recipes!: WritableSignal<IRecipe[]>;


}
