import { IRating } from "../types/recipe";

export const EMAIL_REGEX = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
export const PASSWORD_REGEX = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/

export const EMPTY_RATING : IRating = {
    firstName: "",
    lastName: "",
    email: "",
    stars: 0,
    comment: ""
}