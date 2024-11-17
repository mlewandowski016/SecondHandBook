import React from "react";
import { useAuth } from "../hooks/useAuth";
import { Link } from "react-router-dom";

export const Navbar = () => {
    const { logout } = useAuth();
    const { user } = useAuth();

    const handleLogout = () => {
        logout();
    };

    const isUser = () => {
        if (user) {
            return (
                <div className="space-x-4">
                    <button onClick={handleLogout} className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">
                        Logout
                    </button>
                </div>
            );
        };
    };

    return (
        <header className="py-6 container mx-auto px-4">
            <nav className="flex justify-between items-center">
                <Link to="/" className="text-3xl font-bold">
                    SecondHandBook
                </Link>
                {isUser()}
            </nav>
        </header>
    );
}