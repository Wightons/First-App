export class CardDto {
    id!: number;
    name!: string;
    description!: string;
    dueDate!: Date;
    priority!: number;
    listId!: number;
}

export enum Priority{
    Low = 1,
    Medium = 2,
    High = 3
}