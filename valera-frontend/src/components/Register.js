import React, { useState } from 'react';
import { valeraAPI } from '../services/api';
import { Link } from 'react-router-dom';

const Register = ({ onRegister }) => {
    const [formData, setFormData] = useState({
        email: '',
        username: '',
        password: '',
        confirmPassword: ''
    });
    const [loading, setLoading] = useState(false);

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (formData.password !== formData.confirmPassword) {
            alert('Пароли не совпадают!');
            return;
        }

        setLoading(true);

        try {
            const response = await valeraAPI.register({
                email: formData.email,
                username: formData.username,
                password: formData.password
            });

            const token = response.data.token;
            localStorage.setItem('token', token);
            onRegister(token);

            alert('Регистрация успешна!');
        } catch (error) {
            console.error('Ошибка регистрации:', error);
            alert('Ошибка регистрации: ' + (error.response?.data?.message || error.message));
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="min-h-screen flex items-center justify-center bg-gray-100">
            <div className="max-w-3xl w-full mx-auto p-8 bg-white rounded-lg shadow-md">
                <h2 className="text-2xl font-bold mb-10 text-center">Регистрация</h2>
                <form onSubmit={handleSubmit}>
                    {/* Email */}
                    <div className="flex items-center mb-6">
                        <div className="w-1/3 flex justify-end pr-8">
                            <label className="text-gray-700 whitespace-nowrap">Email:</label>
                        </div>
                        <div className="w-2/3">
                            <input
                                type="email"
                                value={formData.email}
                                onChange={(e) => setFormData({ ...formData, email: e.target.value })}
                                className="w-full p-3 border border-gray-300 rounded"
                                required
                            />
                        </div>
                    </div>

                    {/* Имя пользователя */}
                    <div className="flex items-center mb-6">
                        <div className="w-1/3 flex justify-end pr-8">
                            <label className="text-gray-700 whitespace-nowrap">Имя пользователя:</label>
                        </div>
                        <div className="w-2/3">
                            <input
                                type="text"
                                value={formData.username}
                                onChange={(e) => setFormData({ ...formData, username: e.target.value })}
                                className="w-full p-3 border border-gray-300 rounded"
                                required
                            />
                        </div>
                    </div>

                    {/* Пароль */}
                    <div className="flex items-center mb-6">
                        <div className="w-1/3 flex justify-end pr-8">
                            <label className="text-gray-700 whitespace-nowrap">Пароль:</label>
                        </div>
                        <div className="w-2/3">
                            <input
                                type="password"
                                value={formData.password}
                                onChange={(e) => setFormData({ ...formData, password: e.target.value })}
                                className="w-full p-3 border border-gray-300 rounded"
                                required
                            />
                        </div>
                    </div>

                    {/* Подтверждение пароля */}
                    <div className="flex items-center mb-8">
                        <div className="w-1/3 flex justify-end pr-8">
                            <label className="text-gray-700 whitespace-nowrap">Подтвердите пароль:</label>
                        </div>
                        <div className="w-2/3">
                            <input
                                type="password"
                                value={formData.confirmPassword}
                                onChange={(e) => setFormData({ ...formData, confirmPassword: e.target.value })}
                                className="w-full p-3 border border-gray-300 rounded"
                                required
                            />
                        </div>
                    </div>

                    {/* Кнопки */}
                    <div className="flex justify-center">
                        <div className="w-2/3">
                            <button
                                type="submit"
                                disabled={loading}
                                className="w-full bg-green-500 text-white p-3 rounded hover:bg-green-600 disabled:bg-gray-400 mb-6"
                            >
                                {loading ? 'Регистрация...' : 'Зарегистрироваться'}
                            </button>

                            <div className="text-center">
                                <p className="text-gray-600 mb-4">Уже есть аккаунт?</p>
                                <Link
                                    to="/login"
                                    className="inline-block w-full bg-gray-200 text-gray-800 p-3 rounded hover:bg-gray-300 transition-colors"
                                >
                                    Перейти к входу
                                </Link>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default Register;