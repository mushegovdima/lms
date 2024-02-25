import type { LessonAnswer, LessonAnswerRequest } from "@/models";
import lmsInstance from "./lms-instance";

const controllerName = 'lessonAnswer';
export const lessonAnswerService = {
    create: (data: LessonAnswerRequest) : Promise<number> =>
        lmsInstance.post(controllerName, data).then(x => x.data),

    update: (id: number, data: LessonAnswerRequest) : Promise<LessonAnswer> =>
        lmsInstance.put(`${controllerName}/${id}`, data).then(x => x.data),

    sendToCheck: (id: number) : Promise<void> =>
        lmsInstance.post(`${controllerName}/sendToCheck/${id}`).then(x => x.data),

    getByLessonId: (id: number, author: number) : Promise<LessonAnswer> =>
        lmsInstance.get(`${controllerName}/getByLessonId/${id}/${author}`).then(x => x.data),

    get: (id: number) : Promise<LessonAnswer> =>
        lmsInstance.get(`${controllerName}/${id}`).then(x => x.data),

    getByCheckerId: (id: number) : Promise<LessonAnswer[]> =>
        lmsInstance.get(`${controllerName}/getByCheckerId/${id}`).then(x => x.data),
}
