// Mock API service for demonstration when backend is not available
const mockStudents = [
  {
    id: 1,
    name: "John Doe",
    email: "john@example.com",
    age: 20,
    course: "Computer Science",
    createdDate: "2024-01-15T10:30:00Z"
  },
  {
    id: 2,
    name: "Jane Smith",
    email: "jane@example.com",
    age: 22,
    course: "Mathematics",
    createdDate: "2024-01-16T14:20:00Z"
  },
  {
    id: 3,
    name: "Mike Johnson",
    email: "mike@example.com",
    age: 21,
    course: "Physics",
    createdDate: "2024-01-17T09:15:00Z"
  }
];

let students = [...mockStudents];

export const mockAuthAPI = {
  login: async (credentials) => {
    // Simulate API delay
    await new Promise(resolve => setTimeout(resolve, 1000));
    
    if (credentials.username === 'admin' && credentials.password === 'password123') {
      return {
        token: 'mock-jwt-token-12345',
        expiresIn: 3600
      };
    }
    throw new Error('Invalid credentials');
  },
};

export const mockStudentsAPI = {
  getAll: async () => {
    await new Promise(resolve => setTimeout(resolve, 500));
    return students;
  },
  getById: async (id) => {
    await new Promise(resolve => setTimeout(resolve, 300));
    const student = students.find(s => s.id === parseInt(id));
    if (!student) throw new Error('Student not found');
    return student;
  },
  create: async (studentData) => {
    await new Promise(resolve => setTimeout(resolve, 500));
    const newStudent = {
      id: Math.max(...students.map(s => s.id)) + 1,
      ...studentData,
      createdDate: new Date().toISOString()
    };
    students.push(newStudent);
    return newStudent;
  },
  update: async (id, studentData) => {
    await new Promise(resolve => setTimeout(resolve, 500));
    const index = students.findIndex(s => s.id === parseInt(id));
    if (index === -1) throw new Error('Student not found');
    
    students[index] = { ...students[index], ...studentData };
    return students[index];
  },
  delete: async (id) => {
    await new Promise(resolve => setTimeout(resolve, 300));
    const index = students.findIndex(s => s.id === parseInt(id));
    if (index === -1) throw new Error('Student not found');
    
    students.splice(index, 1);
  },
};
