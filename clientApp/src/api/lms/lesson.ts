import type { Lesson, LessonListItem, LessonPostRequest } from "@/models";
import lmsInstance from "./lms-instance";

const controllerName = 'lesson';
export const lessonService = {
    create: (data: LessonPostRequest) : Promise<string> =>
        lmsInstance.post(controllerName, data).then(x => x.data),

    update: (id: number, data: Lesson) : Promise<Lesson> =>
        lmsInstance.put(`${controllerName}/${id}`, data).then(x => x.data),

    getById: (id: number) : Promise<Lesson> =>
        lmsInstance.get(`${controllerName}/${id}`).then(x => x.data),

    getByCourseId: (id: number) : Promise<LessonListItem[]> =>
        lmsInstance.get(`${controllerName}/getByCourse/${id}`).then(x => x.data),
}
