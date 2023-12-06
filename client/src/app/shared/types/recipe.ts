export interface IRecipe {
    id: string;
    title: string;
    description: string;
    image?: string;
    preparationTime: IPreparationTime;
    steps: IStep[];
    ingredients: IIngredient[];
    ratings: IRating[];
}

export interface IPreparationTime {
    duration: number;
    timeMeasurement: string;
}

export interface IStep {
    number: number;
    description: string;
}

export interface IIngredient {
    name: string;
    quantity?: number;
    measurement?: string;
}

export interface IRating {
    firstName: string;
    lastName: string;
    email: string;
    stars: number;
    comment?: string;
}