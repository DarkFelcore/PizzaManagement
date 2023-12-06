import { ChangeDetectorRef, Component, Input, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IReadOnlyStar, IStar } from '../../../shared/types/star';

@Component({
  selector: 'app-user-recipe-rating',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-recipe-rating.component.html',
  styleUrl: './user-recipe-rating.component.scss'
})
export class UserRecipeRatingComponent {
  @Input() starCount!: number;

  stars = signal<IReadOnlyStar[]>([
    { selected: false },
    { selected: false },
    { selected: false },
    { selected: false },
    { selected: false },
  ]);

  ngAfterViewInit(): void {
    this.loadActiveStars();
  }

  private loadActiveStars(): void {
    for(let i = 0; i < this.starCount; i++) {
      this.stars()[i].selected = true;
    }
  }
}
