export interface ICreateOrUpdateUserRecipeRatingRequest {
    recipeId: string;
    stars: number;
    comment?: string;
}