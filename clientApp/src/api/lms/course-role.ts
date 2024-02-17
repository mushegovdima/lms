import type { CourseRole, CourseRolePostRequest } from "@/models";
import lmsInstance from "./lms-instance";

const controllerName = 'courseRole';
export const courseRoleService = {
    create: (data: CourseRolePostRequest) : Promise<string> =>
        lmsInstance.post(controllerName, data).then(x => x.data),

    getByCourseId: (id: number) : Promise<CourseRole[]> =>
        lmsInstance.get(`${controllerName}/getByCourse/${id}`).then(x => x.data),

    remove: (id: number) : Promise<void> =>
        lmsInstance.delete(`${controllerName}/${id}`).then(x => x.data),
}
