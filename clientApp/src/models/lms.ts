import type { Entity } from ".";

export interface Course extends Entity {
    title: string;
    description: string;
    authorId: number;
    lessonsCount: number;
}

export interface Lesson extends LessonListItem {
    fields: LessonField[],
}

export interface LessonListItem extends Entity {
    description: string;
    status: LessonStatus;
    image: string;
    courseId: number;
    title: string;
    position: number;
}

export interface LessonField extends Entity {
    title: string;
    description: string | null;
    required: boolean;
    type: FieldType;
    position: number;
    lessonId: number;
}

export interface LessonPostRequest {
    description?: string;
    image: string;
    courseId: number;
    title: string;
}

export interface CourseRole extends Entity {
    userId: number;
    courseId: number;
    role: Role;
    createdAt: string | Date;
}

export interface CourseRolePostRequest {
    userId: number;
    email: string;
    courseId: number;
    role: Role;
}

export interface LessonAnswerRequest {
    lessonId: number;
    checkerId: number | null;
    data: Record<number, any>;
}

export interface LessonAnswer {
    id: number;
    status: LessonAnswerStatus;
    lessonId: number;
    createdAt: string;
    lastStatusDate: string;
    checkerId: number | null;
    data: Record<number, any>;
}

/** enums */

export enum LessonStatus {
    active = 0,
    disabled = 1,
}

export enum FieldType {
    string = 0,
    text = 1,
    number = 2,
    video = 3
}

export enum Role
{
    admin = 1,
    checker = 2,
    student = 3,
}

export enum LessonAnswerStatus {
    Draft = 0,
    Send = 1,
    Successfull = 2,
    Cancelled = 3
}
