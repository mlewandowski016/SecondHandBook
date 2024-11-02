import React, { useState } from 'react';
import { Link } from 'react-router-dom'; // Importujemy Link z react-router-dom
import api from '../Api';
import './register.css';

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
            alert('Rejestracja zakończona sukcesem');
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
            console.error("Błąd rejestracji", error);
            alert('Błąd podczas rejestracji');
        }
    };

    return (
        <div className="register-container">
            <form onSubmit={handleSubmit} className="register-form">
                <label>Email:
                    <input name="email" value={formData.email} onChange={handleChange} />
                </label>
                <label>Hasło:
                    <input type="password" name="password" value={formData.password} onChange={handleChange} />
                </label>
                <label>Potwierdź hasło:
                    <input type="password" name="confirmPassword" value={formData.confirmPassword} onChange={handleChange} />
                </label>
                <label>Imię:
                    <input name="name" value={formData.name} onChange={handleChange} />
                </label>
                <label>Nazwisko:
                    <input name="lastname" value={formData.lastname} onChange={handleChange} />
                </label>
                <label>Numer telefonu:
                    <input name="phoneNumber" value={formData.phoneNumber} onChange={handleChange} />
                </label>
                <label>Rola:
                    <input type="number" name="roleId" value={formData.roleId} onChange={handleChange} />
                </label>
                <button type="submit">Zarejestruj</button>
                <p>Masz już konto? <Link to="/login">Zaloguj się</Link></p> {/* Link do logowania */}
            </form>
        </div>
    );
}

export default Register;
