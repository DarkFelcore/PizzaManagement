import { Component, ElementRef, OnInit, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { IIngredient, IRecipe, IStep } from '../../../shared/types/recipe';
import { RecipeService } from '../../../shared/services/recipe.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { IngredientsOverviewTableComponent } from '../../../shared/components/ingredients-overview-table/ingredients-overview-table.component';
import { StepsOverviewComponent } from '../../../shared/components/steps-overview/steps-overview.component';

@Component({
  selector: 'app-create-recipe-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, IngredientsOverviewTableComponent, StepsOverviewComponent],
  templateUrl: './create-recipe-form.component.html',
  styleUrl: './create-recipe-form.component.scss'
})
export class CreateRecipeFormComponent implements OnInit {
  createRecipeForm!: FormGroup;
  selectedFile: File | null = null;
  
  fb: FormBuilder = inject(FormBuilder);
  recipeService: RecipeService = inject(RecipeService);
  router: Router = inject(Router);

  ingredients: IIngredient[] = [];
  steps: IStep[] = [];

  @ViewChild('ingredientName', { static: true }) ingredientName!: ElementRef;
  @ViewChild('ingredientQuantity', { static: true }) ingredientQuantity!: ElementRef;
  @ViewChild('ingredientMeasurement', { static: true }) ingredientMeasurement!: ElementRef;
  @ViewChild('stepDescription', { static: true }) stepDescription!: ElementRef;
  
  ngOnInit(): void {
    this.createRecipeForm = this.fb.group({
      title: [null, Validators.required],
      description: [null, Validators.required],
      preparationTime_duration: [null, Validators.required],
      preparationTime_timeMeasurement: [null, Validators.required],
    })
  }

  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0] as File;
  }

  addIngredientElement(): void {
    if(this.ingredientName.nativeElement.value !== "") {
      const ingredient: IIngredient = {
        name: this.ingredientName.nativeElement.value,
        quantity: this.ingredientQuantity.nativeElement.value === "" ? null : this.ingredientQuantity.nativeElement.value,
        measurement: this.ingredientMeasurement.nativeElement.value === "" ? null : this.ingredientMeasurement.nativeElement.value
      };
  
      this.ingredients.push(ingredient);
      this.clearIngredientFields();
    }
  }

  addStep(): void {
    if(this.stepDescription.nativeElement.value !== "") {
      const step: IStep = {
        number: this.steps.length + 1,
        description: this.stepDescription.nativeElement.value
       };
   
       this.steps.push(step);
       this.clearStepFields();
    }
  }
  
  onSubmit(): void {
    const formData: FormData = new FormData();
    formData.append('title', this.createRecipeForm.get('title')?.value);
    formData.append('description', this.createRecipeForm.get('description')?.value);
    formData.append('preparationTime.duration', this.createRecipeForm.get('preparationTime_duration')?.value);
    formData.append('preparationTime.timeMeasurement', this.createRecipeForm.get('preparationTime_timeMeasurement')?.value);
    formData.append('steps', JSON.stringify(this.steps));
    formData.append('ingredients', JSON.stringify(this.ingredients));
    if(this.selectedFile) {
      formData.append('imageFile', this.selectedFile)
    }

    this.recipeService.createRecipe(formData).subscribe({
      next: (recipe: IRecipe) => {
        this,this.createRecipeForm.reset();
        this.router.navigateByUrl('/')
      },
      error: (err: HttpErrorResponse) => console.log(err)
    })
  }
  
  private clearIngredientFields(): void {
    this.ingredientName.nativeElement.value = "";
    this.ingredientQuantity.nativeElement.value = "";
    this.ingredientMeasurement.nativeElement.value = "";
  }

  private clearStepFields(): void {
    this.stepDescription.nativeElement.value = "";
  }
}
