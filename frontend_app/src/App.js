import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Login from './components/Login';
import Register from './components/Register';
import HomePage from './components/HomePage';
import { ProtectedRoute } from "./components/ProtectedRoute";
import { AuthProvider } from "./hooks/useAuth";
import { RedirectIfLoggedRoute } from './components/RedirectIfLoggedRoute';

function App() {
    return (
    <AuthProvider>
      <Routes>
        <Route path="/login" element={
            <RedirectIfLoggedRoute>
                <Login/>
            </RedirectIfLoggedRoute>
            } />
        <Route path="/register" element={
            <RedirectIfLoggedRoute>
                <Register/>
            </RedirectIfLoggedRoute>
        } />
        <Route 
            path="/" 
            element={
                <ProtectedRoute>
                    <HomePage/>
                </ProtectedRoute>
            } />
      </Routes>
    </AuthProvider>
    );
}

export default App;
