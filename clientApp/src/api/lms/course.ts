import type { Course } from "@/models";
import lmsInstance from "./lms-instance";

const controllerName = 'course';
export const courseService = {
    create: (data: Course) : Promise<string> =>
        lmsInstance.post(controllerName, data).then(x => x.data),

    update: (id: number, data: Course) : Promise<Course> =>
        lmsInstance.put(`${controllerName}/${id}`, data).then(x => x.data),

    getById: (id: number) : Promise<Course> =>
        lmsInstance.get(`${controllerName}/${id}`).then(x => x.data),

    getByAuthorId: (id: number) : Promise<Course[]> =>
        lmsInstance.get(`${controllerName}/getByAuthor/${id}`).then(x => x.data),
}
