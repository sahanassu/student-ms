export const StudentTypes = {
  student: {
    id: 'number',
    name: 'string',
    email: 'string',
    age: 'number',
    course: 'string',
    createdDate: 'string'
  },
  createStudentRequest: {
    name: 'string',
    email: 'string',
    age: 'number',
    course: 'string'
  },
  updateStudentRequest: {
    name: 'string',
    email: 'string',
    age: 'number',
    course: 'string'
  },
  loginRequest: {
    username: 'string',
    password: 'string'
  },
  loginResponse: {
    token: 'string',
    expiresIn: 'number'
  }
};
