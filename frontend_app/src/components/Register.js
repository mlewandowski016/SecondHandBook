import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import api from '../Api';

function Register() {
    const [formData, setFormData] = useState({
        email: '',
        password: '',
        confirmPassword: '',
        name: '',
        lastname: '',
        phoneNumber: '',
        roleId: 0,
    });

    const handleChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value,
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await api.post('/user/register', formData);
            alert('Registration successfully completed');
            setFormData({
                email: '',
                password: '',
                confirmPassword: '',
                name: '',
                lastname: '',
                phoneNumber: '',
                roleId: 0,
            });
        } catch (error) {
            console.error("Registration error", error);
            alert('Registration error');
        }
    };

    return (
        <div className="flex items-center justify-center min-h-screen bg-gray-100">
            <div className="bg-white shadow-lg rounded-lg p-8 w-full max-w-md">
                <h2 className="text-2xl font-bold text-gray-800 text-center mb-6">Register</h2>
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
                    <div>
                        <label className="block text-gray-700 text-sm font-medium mb-1">Confirm password:</label>
                        <input
                            type="password"
                            name="confirmPassword"
                            value={formData.confirmPassword}
                            onChange={handleChange}
                            className="w-full px-4 py-2 border rounded-lg text-gray-700 bg-gray-50 focus:outline-none focus:ring-2 focus:ring-blue-500"
                            placeholder="Confirm password"
                        />
                    </div>
                    <div>
                        <label className="block text-gray-700 text-sm font-medium mb-1">Name:</label>
                        <input
                            name="name"
                            value={formData.name}
                            onChange={handleChange}
                            className="w-full px-4 py-2 border rounded-lg text-gray-700 bg-gray-50 focus:outline-none focus:ring-2 focus:ring-blue-500"
                            placeholder="Enter name"
                        />
                    </div>
                    <div>
                        <label className="block text-gray-700 text-sm font-medium mb-1">Lastname:</label>
                        <input
                            name="lastname"
                            value={formData.lastname}
                            onChange={handleChange}
                            className="w-full px-4 py-2 border rounded-lg text-gray-700 bg-gray-50 focus:outline-none focus:ring-2 focus:ring-blue-500"
                            placeholder="Enter lastname"
                        />
                    </div>
                    <div>
                        <label className="block text-gray-700 text-sm font-medium mb-1">Phone number:</label>
                        <input
                            name="phoneNumber"
                            value={formData.phoneNumber}
                            onChange={handleChange}
                            className="w-full px-4 py-2 border rounded-lg text-gray-700 bg-gray-50 focus:outline-none focus:ring-2 focus:ring-blue-500"
                            placeholder="Enter phone number"
                        />
                    </div>
                    <button
                        type="submit"
                        className="w-full py-2 px-4 bg-blue-500 text-white rounded-lg hover:bg-blue-600 transition-colors duration-200 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
                    >
                        Sign in
                    </button>
                </form>
                <p className="text-sm text-center text-gray-600 mt-4">
                    You already have an account?{" "}
                    <Link to="/login" className="text-blue-500 hover:underline">
                        Log in
                    </Link>
                </p>
            </div>
        </div>
    );
}

export default Register;
