import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from "../hooks/useAuth";
import api from '../Api';

function Login() {
    const [formData, setFormData] = useState({
        email: '',
        password: '',
    });
    const [errorMessage, setErrorMessage] = useState("");

    const handleChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value,
        });
    };

    const { login } = useAuth();

    const handleSubmit = async (e) => {
        e.preventDefault();
        setErrorMessage("");
        try {
            const response = await api.post('/user/login', formData);
            var token = response.data.token.replace(/[""]+/g, '')
            await login(response.data, token);
            setFormData({ email: '', password: '' });
        } catch (error) {
            if (error.response && error.response.data === "Invalid email or password") {
                setErrorMessage("Invalid email or password.");
            } else {
                setErrorMessage("An error occurred while logging in. Please try again.");
            }
            console.error("Login error", error);
        }
    };

    return (
        <div className="flex items-center justify-center min-h-screen bg-gray-100">
            <div className="bg-white shadow-lg rounded-lg p-8 w-full max-w-md">
                <h2 className="text-2xl font-bold text-gray-800 text-center mb-6">Logging</h2>
                <form onSubmit={handleSubmit} className="space-y-4">
                    <div>
                        <label className="block text-gray-700 text-sm font-medium mb-1">Email:</label>
                        <input
                            type="email"
                            name="email"
                            value={formData.email}
                            onChange={handleChange}
                            className="w-full px-4 py-2 border rounded-lg text-gray-700 bg-gray-50 focus:outline-none focus:ring-2 focus:ring-blue-500"
                            placeholder="Enter email"
                        />
                    </div>
                    <div>
                        <label className="block text-gray-700 text-sm font-medium mb-1">Password:</label>
                        <input
                            type="password"
                            name="password"
                            value={formData.password}
                            onChange={handleChange}
                            className="w-full px-4 py-2 border rounded-lg text-gray-700 bg-gray-50 focus:outline-none focus:ring-2 focus:ring-blue-500"
                            placeholder="Enter password"
                        />
                    </div>
                    <button
                        type="submit"
                        className="w-full py-2 px-4 bg-blue-500 text-white rounded-lg hover:bg-blue-600 transition-colors duration-200 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
                    >
                        Logi in
                    </button>
                    {errorMessage && (
                        <p className="text-red-500 text-sm mt-2 text-center">{errorMessage}</p>
                    )}
                </form>
                <p className="text-sm text-center text-gray-600 mt-4">
                    You don't have an account?{" "}
                    <Link to="/register" className="text-blue-500 hover:underline">
                        Sign up
                    </Link>
                </p>
            </div>
        </div>
    );
}

export default Login;
