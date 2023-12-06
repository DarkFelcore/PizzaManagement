import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CreateRecipeFormComponent } from './components/create-recipe-form/create-recipe-form.component';

@Component({
  selector: 'app-create-recipe',
  standalone: true,
  imports: [CommonModule, RouterModule, CreateRecipeFormComponent],
  templateUrl: './create-recipe.component.html',
  styleUrl: './create-recipe.component.scss'
})
export class CreateRecipeComponent implements OnInit {

  ngOnInit(): void {
  }

}
