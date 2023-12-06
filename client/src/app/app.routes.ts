import { Routes } from '@angular/router';
import { AuthGuard } from './shared/guards/auth.guard';

export const routes: Routes = [
    { path: 'login', loadComponent: () => import('./auth/login/login.component').then((mod) => mod.LoginComponent), canActivate: [AuthGuard] },
    { path: 'register', loadComponent: () => import('./auth/register/register.component').then((mod) => mod.RegisterComponent), canActivate: [AuthGuard] },
    { path: 'recipes', loadComponent: () => import('./home/home.component').then((mod) => mod.HomeComponent) },
    { path: 'recipes/create', loadComponent: () => import('./create-recipe/create-recipe.component').then((mod) => mod.CreateRecipeComponent) },
    { path: 'recipes/rating/:recipeId', loadComponent: () => import('./recipe-rating/recipe-rating.component').then((mod) => mod.RecipeRatingComponent) },
    { path: '**', redirectTo: '/recipes', pathMatch: 'full' }
];
