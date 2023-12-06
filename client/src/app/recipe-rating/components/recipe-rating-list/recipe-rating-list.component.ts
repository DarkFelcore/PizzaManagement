import { Component, OnInit, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IRating } from '../../../shared/types/recipe';
import { RecipeRatingItemComponent } from '../recipe-rating-item/recipe-rating-item.component';

@Component({
  selector: 'app-recipe-rating-list',
  standalone: true,
  imports: [CommonModule, RecipeRatingItemComponent],
  templateUrl: './recipe-rating-list.component.html',
  styleUrl: './recipe-rating-list.component.scss'
})
export class RecipeRatingListComponent implements OnInit {

  @Input() ratings!: IRating[];

  ngOnInit(): void {

  }

}
