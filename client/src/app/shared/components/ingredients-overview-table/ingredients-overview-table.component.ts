import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IIngredient } from '../../types/recipe';

@Component({
  selector: 'app-ingredients-overview-table',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './ingredients-overview-table.component.html',
  styleUrl: './ingredients-overview-table.component.scss'
})
export class IngredientsOverviewTableComponent {
  @Input() ingredients!: IIngredient[];
  @Input() isEditable: boolean = false;

  removeIngredient(index: number): void {
    this.ingredients.splice(index, 1);
  }
}
