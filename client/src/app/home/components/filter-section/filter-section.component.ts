import { Component, ElementRef, EventEmitter, OnInit, Output, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RecipeParams } from '../../../shared/types/recipe-params';
import { RecipeService } from '../../../shared/services/recipe.service';
import { IRecipe } from '../../../shared/types/recipe';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-filter-section',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './filter-section.component.html',
  styleUrl: './filter-section.component.scss'
})
export class FilterSectionComponent implements OnInit {

  filterForm!: FormGroup;

  @ViewChild('search') searchTerm!: ElementRef;

  fb: FormBuilder = inject(FormBuilder);
  recipeService: RecipeService = inject(RecipeService);

  @Output() recipeEmitter: EventEmitter<IRecipe[]> = new EventEmitter<IRecipe[]>();

  ngOnInit(): void {
    this.filterForm = this.fb.group({
      name: ['', Validators.required]
    });
  }

  onSubmit(): void {
    const params = this.recipeService.getRecipeParams();
    params.search = this.searchTerm.nativeElement.value;
    this.recipeService.setRecipeParams(params);
    this.getRecipes();
  }

  getRecipes(): void {
    this.recipeService.getRecipes().subscribe({
      next: (recipes: IRecipe[]) => this.recipeEmitter.emit(recipes),
      error: (err: HttpErrorResponse) => console.log(err)
    })
  }
}
