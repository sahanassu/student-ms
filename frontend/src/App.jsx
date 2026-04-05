import React, { useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Login from './components/Login';
import StudentList from './components/StudentList';
import StudentForm from './components/StudentForm';
import Layout from './components/Layout';
import './App.css';

function App() {
  const [isAuthenticated, setIsAuthenticated] = React.useState(false);

  useEffect(() => {
    const token = localStorage.getItem('token');
    setIsAuthenticated(!!token);
  }, []);

  return (
    <Router>
      <div className="App">
        <Routes>
          <Route 
            path="/login" 
            element={isAuthenticated ? <Navigate to="/students" /> : <Login />} 
          />
          <Route
            path="/students"
            element={
              isAuthenticated ? (
                <Layout>
                  <StudentList />
                </Layout>
              ) : (
                <Navigate to="/login" />
              )
            }
          />
          <Route
            path="/students/add"
            element={
              isAuthenticated ? (
                <Layout>
                  <StudentForm />
                </Layout>
              ) : (
                <Navigate to="/login" />
              )
            }
          />
          <Route
            path="/students/edit/:id"
            element={
              isAuthenticated ? (
                <Layout>
                  <StudentForm />
                </Layout>
              ) : (
                <Navigate to="/login" />
              )
            }
          />
          <Route path="/" element={<Navigate to="/students" />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
