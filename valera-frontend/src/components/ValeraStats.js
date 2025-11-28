import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { valeraAPI } from '../services/api';

const ValeraStats = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [valera, setValera] = useState(null);
    const [loading, setLoading] = useState(true);
    const [actionLoading, setActionLoading] = useState(false);

    // Загрузка данных Валеры
    const loadValera = async () => {
        try {
            setLoading(true);
            const response = await valeraAPI.getById(id);
            setValera(response.data);
        } catch (error) {
            console.error('Ошибка загрузки Валеры:', error);
            alert('Ошибка загрузки данных Валеры');
            navigate('/');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        if (id) {
            loadValera();
        }
    }, [id]);

    // Выполнение действия
    const handleAction = async (action) => {
        try {
            setActionLoading(true);
            await action(id);
            await loadValera(); // Обновляем данные после действия
        } catch (error) {
            console.error('Ошибка выполнения действия:', error);
            alert('Ошибка выполнения действия');
        } finally {
            setActionLoading(false);
        }
    };

    // Валидация кнопок
    const canWork = valera && valera.mana < 50 && valera.fatigue < 10;

    if (loading) {
        return <div className="container mx-auto p-4 text-center">Загрузка...</div>;
    }

    if (!valera) {
        return <div className="container mx-auto p-4 text-center">Валера не найден</div>;
    }

    return (
        <div className="container mx-auto p-4">
            {/* Заголовок и кнопка назад */}
            <div className="flex justify-between items-center mb-6">
                <h1 className="text-3xl font-bold">Валера #{valera.id}</h1>
                <button
                    onClick={() => navigate('/')}
                    className="bg-gray-500 text-white px-4 py-2 rounded hover:bg-gray-600"
                >
                    Назад к списку
                </button>
            </div>

            {/* Статистика */}
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6 mb-8">
                {/* Прогресс-бары */}
                <div className="space-y-4">
                    <h2 className="text-xl font-semibold">Состояние Валеры</h2>

                    <div>
                        <div className="flex justify-between mb-1">
                            <span>❤️ Здоровье</span>
                            <span>{valera.health}/100</span>
                        </div>
                        <div className="w-full bg-gray-200 rounded-full h-4">
                            <div
                                className="bg-red-500 h-4 rounded-full transition-all"
                                style={{ width: `${valera.health}%` }}
                            ></div>
                        </div>
                    </div>

                    <div>
                        <div className="flex justify-between mb-1">
                            <span>🍺 Алкоголь в крови</span>
                            <span>{valera.mana}/100</span>
                        </div>
                        <div className="w-full bg-gray-200 rounded-full h-4">
                            <div
                                className="bg-yellow-500 h-4 rounded-full transition-all"
                                style={{ width: `${valera.mana}%` }}
                            ></div>
                        </div>
                    </div>

                    <div>
                        <div className="flex justify-between mb-1">
                            <span>😊 Жизнерадостность</span>
                            <span>{valera.cheerfulness}/10</span>
                        </div>
                        <div className="w-full bg-gray-200 rounded-full h-4">
                            <div
                                className={`h-4 rounded-full transition-all ${valera.cheerfulness >= 0 ? 'bg-green-500' : 'bg-red-500'
                                    }`}
                                style={{ width: `${Math.abs(valera.cheerfulness) * 10}%` }}
                            ></div>
                        </div>
                    </div>

                    <div>
                        <div className="flex justify-between mb-1">
                            <span>😫 Усталость</span>
                            <span>{valera.fatigue}/100</span>
                        </div>
                        <div className="w-full bg-gray-200 rounded-full h-4">
                            <div
                                className="bg-blue-500 h-4 rounded-full transition-all"
                                style={{ width: `${valera.fatigue}%` }}
                            ></div>
                        </div>
                    </div>

                    <div className="text-center p-4 bg-green-100 rounded-lg">
                        <span className="text-2xl font-bold">💰 {valera.money} ₽</span>
                    </div>
                </div>

                {/* Действия */}
                <div>
                    <h2 className="text-xl font-semibold mb-4">Действия</h2>
                    <div className="space-y-3">
                        <button
                            onClick={() => handleAction(valeraAPI.work)}
                            disabled={!canWork || actionLoading}
                            className={`w-full p-3 rounded text-white font-semibold ${canWork ? 'bg-blue-500 hover:bg-blue-600' : 'bg-gray-400 cursor-not-allowed'
                                }`}
                        >
                            {actionLoading ? 'Выполняется...' : '💼 Пойти на работу'}
                            {!canWork && <div className="text-xs mt-1">(Требуется: алкоголь меньше 50 и усталость меньше 10)</div>}
                        </button>

                        <button
                            onClick={() => handleAction(valeraAPI.contemplateNature)}
                            disabled={actionLoading}
                            className="w-full p-3 bg-green-500 text-white rounded font-semibold hover:bg-green-600 disabled:bg-gray-400"
                        >
                            {actionLoading ? 'Выполняется...' : '🌳 Созерцать природу'}
                        </button>

                        <button
                            onClick={() => handleAction(valeraAPI.drinkWineWatchSeries)}
                            disabled={actionLoading}
                            className="w-full p-3 bg-purple-500 text-white rounded font-semibold hover:bg-purple-600 disabled:bg-gray-400"
                        >
                            {actionLoading ? 'Выполняется...' : '🍷 Пить вино и смотреть сериал'}
                        </button>

                        <button
                            onClick={() => handleAction(valeraAPI.goToBar)}
                            disabled={actionLoading}
                            className="w-full p-3 bg-yellow-500 text-white rounded font-semibold hover:bg-yellow-600 disabled:bg-gray-400"
                        >
                            {actionLoading ? 'Выполняется...' : '🍻 Сходить в бар'}
                        </button>

                        <button
                            onClick={() => handleAction(valeraAPI.drinkWithFriends)}
                            disabled={actionLoading}
                            className="w-full p-3 bg-orange-500 text-white rounded font-semibold hover:bg-orange-600 disabled:bg-gray-400"
                        >
                            {actionLoading ? 'Выполняется...' : '🥃 Выпить с маргинальными личностями'}
                        </button>

                        <button
                            onClick={() => handleAction(valeraAPI.singInSubway)}
                            disabled={actionLoading}
                            className="w-full p-3 bg-pink-500 text-white rounded font-semibold hover:bg-pink-600 disabled:bg-gray-400"
                        >
                            {actionLoading ? 'Выполняется...' : '🎤 Петь в метро'}
                        </button>

                        <button
                            onClick={() => handleAction(valeraAPI.sleep)}
                            disabled={actionLoading}
                            className="w-full p-3 bg-indigo-500 text-white rounded font-semibold hover:bg-indigo-600 disabled:bg-gray-400"
                        >
                            {actionLoading ? 'Выполняется...' : '😴 Спать'}
                        </button>
                    </div>
                </div>
            </div>

            {/* Текущие ограничения */}
            <div className="bg-gray-100 p-4 rounded-lg">
                <h3 className="font-semibold mb-2">Текущие ограничения:</h3>
                <ul className="list-disc list-inside space-y-1 text-sm">
                    <li>Работа доступна: {canWork ? '✅' : '❌'} (алкоголь меньше 50 и усталость меньше 10)</li>
                    <li>Здоровье: {valera.health <= 0 ? '❌ Валера умер!' : '✅ Норма'}</li>
                    <li>Деньги: {valera.money < 0 ? '❌ Долг!' : '✅ Есть деньги'}</li>
                </ul>
            </div>
        </div>
    );
};

export default ValeraStats;