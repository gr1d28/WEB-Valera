import React, { useState } from 'react';
import { valeraAPI } from '../services/api';
import { Link } from 'react-router-dom';

const Login = ({ onLogin }) => {
    const [formData, setFormData] = useState({
        email: '',
        password: ''
    });
    const [loading, setLoading] = useState(false);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);

        try {
            const response = await valeraAPI.login(formData);
            const token = response.data.token;

            // Сохраняем токен
            localStorage.setItem('token', token);

            // Уведомляем родительский компонент
            onLogin(token);

            alert('Успешный вход!');
        } catch (error) {
            console.error('Ошибка входа:', error);
            alert('Ошибка входа: ' + (error.response?.data?.message || error.message));
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="min-h-screen flex items-center justify-center bg-gray-100">
            <div className="max-w-3xl w-full mx-auto p-8 bg-white rounded-lg shadow-md">
                <h2 className="text-2xl font-bold mb-10 text-center">Вход в систему</h2>
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

                    {/* Пароль */}
                    <div className="flex items-center mb-8">
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

                    {/* Кнопки */}
                    <div className="flex justify-center">
                        <div className="w-2/3">
                            <button
                                type="submit"
                                disabled={loading}
                                className="w-full bg-blue-500 text-white p-3 rounded hover:bg-blue-600 disabled:bg-gray-400 mb-6"
                            >
                                {loading ? 'Вход...' : 'Войти'}
                            </button>

                            <div className="text-center">
                                <p className="text-gray-600 mb-4">Нет аккаунта?</p>
                                <Link
                                    to="/register"
                                    className="inline-block w-full bg-gray-200 text-gray-800 p-3 rounded hover:bg-gray-300 transition-colors"
                                >
                                    Зарегистрироваться
                                </Link>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default Login;