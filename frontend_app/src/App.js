import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Login from './components/Login';
import Register from './components/Register';
import HomePage from './components/HomePage';
import CreateOffer from './components/NewOffer';
import OfferDetails from './components/OfferDetails';
import Footer from './components/Footer';
import { Navbar } from './components/Navbar';
import { ProtectedRoute } from "./components/ProtectedRoute";
import { AuthProvider } from "./hooks/useAuth";
import { RedirectIfLoggedRoute } from './components/RedirectIfLoggedRoute';
import EditOffer from './components/EditOffer';

function App() {
    return (
        <AuthProvider>
            <Navbar />
            <Routes>
                <Route path="/login" element={
                    <RedirectIfLoggedRoute>
                        <Login />
                    </RedirectIfLoggedRoute>
                } />
                <Route path="/register" element={
                    <RedirectIfLoggedRoute>
                        <Register />
                    </RedirectIfLoggedRoute>
                } />
                <Route
                    path="/" element={
                        <ProtectedRoute>
                            <HomePage />
                        </ProtectedRoute>
                    } />
                <Route path="/offer/:id" element={
                    <ProtectedRoute>
                        <OfferDetails />
                    </ProtectedRoute>
                } />
                <Route path="/new" element={
                    <ProtectedRoute>
                        <CreateOffer />
                    </ProtectedRoute>
                } />
                <Route path="/edit-offer/:id" element={
                    <ProtectedRoute>
                        <EditOffer />
                    </ProtectedRoute>
                } />
            </Routes>
            <Footer />
        </AuthProvider>
    );
}

export default App;
