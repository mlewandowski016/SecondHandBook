import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from "../hooks/useAuth";
import api from '../Api';
import './login.css';

function Login() {
    const [formData, setFormData] = useState({
        email: '',
        password: '',
    });

    const handleChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value,
        });
    };

    const { login } = useAuth();

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await api.post('/user/login', formData);
            const token = response.data;
            await login(token);
            alert('Zalogowano pomyślnie');
            localStorage.setItem('token', token);
            setFormData({ email: '', password: '' });
        } catch (error) {
            console.error("Błąd logowania", error);
            alert('Błąd podczas logowania');
        }
    };

    return (
        <div className="login-container">
            <form onSubmit={handleSubmit} className="login-form">
                <label>Email:
                    <input name="email" value={formData.email} onChange={handleChange} />
                </label>
                <label>Hasło:
                    <input type="password" name="password" value={formData.password} onChange={handleChange} />
                </label>
                <button type="submit">Zaloguj się</button>
                <p>Nie masz konta? <Link to="/register">Zarejestruj się</Link></p> {/* Link do rejestracji */}
            </form>
        </div>
    );
}

export default Login;
