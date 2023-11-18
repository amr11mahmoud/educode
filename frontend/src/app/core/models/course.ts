import { courseLevel } from "../enums/courseLevel";
import { courseTag } from "../enums/courseTag";

export interface Course {
  id:string;
  title: string;
  rating: number;
  ratingCount:number;
  level: courseLevel;
  lessons: number;
  durationHours: number;
  durationMinutes: number;
  teacher: string;
  price: number;
  categoryId: number;
  discount:number;
  tags:courseTag[]
}
