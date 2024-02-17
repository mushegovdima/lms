export const rules = {
    required: (value: any) => typeof(value) === 'number' && value === 0 || !!value || 'Field is required',
    email: (value: string) => emailPattern.test(value) || 'Invalid e-mail.',
}

const emailPattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;