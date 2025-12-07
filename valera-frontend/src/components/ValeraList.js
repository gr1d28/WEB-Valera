import React, { useState, useEffect } from 'react';
import { valeraAPI } from '../services/api';
import { useNavigate } from 'react-router-dom';

const ValeraList = () => {
    const [valeras, setValeras] = useState([]);
    const [searchTerm, setSearchTerm] = useState('');
    const [showForm, setShowForm] = useState(false);
    const [newValera, setNewValera] = useState({
        health: 100,
        mana: 0,
        cheerfulness: 0,
        fatigue: 0,
        money: 512
    });
    const [loading, setLoading] = useState(false);
    const [userRole, setUserRole] = useState(null); // Добавляем состояние для роли
    const navigate = useNavigate();

    // Получение информации о пользователе
    const getUserRole = async () => {
        try {
            const response = await valeraAPI.getUserRole(); // Предполагаемый метод API
            setUserRole(response.data);
            return response.data;
        } catch (error) {
            console.error('Ошибка получения информации о пользователе:', error);
            return 'User'; // По умолчанию считаем User
        }
    };

    // Загрузка списка Валер в зависимости от роли
    const loadValeras = async () => {
        try {
            setLoading(true);
            let response;

            if (userRole === 'Admin') {
                response = await valeraAPI.getAll(); // Метод для получения всех Валер
            } else {
                response = await valeraAPI.getUserValeras(); // Метод для получения Валер текущего пользователя
            }

            setValeras(response.data);
        } catch (error) {
            console.error('Ошибка загрузки Валер:', error);
            alert('Ошибка загрузки списка Валер');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        const initialize = async () => {
            await getUserRole();
            await loadValeras();
        };
        initialize();
    }, [userRole]); // Зависимость от userRole

    // Фильтрация по ID
    const filteredValeras = valeras.filter(valera =>
        valera.id.toString().includes(searchTerm)
    );

    // Создание нового Валеры
    const handleCreateValera = async (e) => {
        e.preventDefault();
        try {
            await valeraAPI.create(newValera);
            setShowForm(false);
            setNewValera({
                health: 100,
                mana: 0,
                cheerfulness: 0,
                fatigue: 0,
                money: 512
            });
            loadValeras(); // Обновляем список
            alert('Валера успешно создан!');
        } catch (error) {
            console.error('Ошибка создания Валеры:', error);
            alert('Ошибка создания Валеры');
        }
    };

    // Удаление Валеры
    const handleDelete = async (id) => {
        if (window.confirm('Удалить эту Валеру?')) {
            try {
                await valeraAPI.delete(id);
                loadValeras();
            } catch (error) {
                console.error('Ошибка удаления:', error);
                alert('Ошибка удаления Валеры');
            }
        }
    };

    // Переход к статистике
    const handleValeraClick = (id) => {
        navigate(`/valera/${id}`);
    };

    return (
        <div className="container mx-auto p-4">
            <h1 className="text-3xl font-bold mb-6">Управление Валерой</h1>

            {/* Поиск и кнопка создания */}
            <div className="mb-6 flex gap-4">
                <input
                    type="text"
                    placeholder="Поиск по ID..."
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                    className="flex-1 p-2 border border-gray-300 rounded"
                />
                <button
                    onClick={() => setShowForm(!showForm)}
                    className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
                >
                    {showForm ? 'Отмена' : 'Создать Валеру'}
                </button>
            </div>

            {/* Форма создания */}
            {showForm && (
                <div className="mb-6 p-4 border border-gray-300 rounded bg-gray-50">
                    <h2 className="text-xl font-semibold mb-4">Создать нового Валеру</h2>
                    <form onSubmit={handleCreateValera} className="grid grid-cols-2 gap-4">
                        <div>
                            <label className="block mb-2">Здоровье:</label>
                            <input
                                type="number"
                                value={newValera.health}
                                onChange={(e) => setNewValera({ ...newValera, health: parseInt(e.target.value) })}
                                className="w-full p-2 border border-gray-300 rounded"
                                min="0"
                                max="100"
                            />
                        </div>
                        <div>
                            <label className="block mb-2">Алкоголь:</label>
                            <input
                                type="number"
                                value={newValera.mana}
                                onChange={(e) => setNewValera({ ...newValera, mana: parseInt(e.target.value) })}
                                className="w-full p-2 border border-gray-300 rounded"
                                min="0"
                                max="100"
                            />
                        </div>
                        <div>
                            <label className="block mb-2">Жизнерадостность:</label>
                            <input
                                type="number"
                                value={newValera.cheerfulness}
                                onChange={(e) => setNewValera({ ...newValera, cheerfulness: parseInt(e.target.value) })}
                                className="w-full p-2 border border-gray-300 rounded"
                                min="-10"
                                max="10"
                            />
                        </div>
                        <div>
                            <label className="block mb-2">Усталость:</label>
                            <input
                                type="number"
                                value={newValera.fatigue}
                                onChange={(e) => setNewValera({ ...newValera, fatigue: parseInt(e.target.value) })}
                                className="w-full p-2 border border-gray-300 rounded"
                                min="0"
                                max="100"
                            />
                        </div>
                        <div>
                            <label className="block mb-2">Деньги:</label>
                            <input
                                type="number"
                                value={newValera.money}
                                onChange={(e) => setNewValera({ ...newValera, money: parseInt(e.target.value) })}
                                className="w-full p-2 border border-gray-300 rounded"
                            />
                        </div>
                        <div className="flex items-end">
                            <button
                                type="submit"
                                className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600"
                            >
                                Создать
                            </button>
                        </div>
                    </form>
                </div>
            )}

            {/* Список Валер */}
            {loading ? (
                <div className="text-center">Загрузка...</div>
            ) : (
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                    {filteredValeras.map(valera => (
                        <div
                            key={valera.id}
                            className="border border-gray-300 rounded p-4 hover:shadow-md transition-shadow cursor-pointer"
                            onClick={() => handleValeraClick(valera.id)}
                        >
                            <div className="flex justify-between items-start mb-2">
                                <h3 className="text-xl font-semibold">Валера #{valera.id}</h3>
                                <button
                                    onClick={(e) => {
                                        e.stopPropagation();
                                        handleDelete(valera.id);
                                    }}
                                    className="bg-red-500 text-white px-2 py-1 rounded text-sm hover:bg-red-600"
                                >
                                    Удалить
                                </button>
                            </div>

                            <div className="space-y-1 text-sm">
                                <div>Здоровье: {valera.health}/100</div>
                                <div>Алкоголь: {valera.mana}/100</div>
                                <div>Жизнерадостность: {valera.cheerfulness}</div>
                                <div>Усталость: {valera.fatigue}/100</div>
                                <div>Деньги: {valera.money}</div>
                            </div>

                            <div className="mt-3 text-center text-blue-500 font-semibold">
                                Кликните для управления
                            </div>
                        </div>
                    ))}
                </div>
            )}

            {filteredValeras.length === 0 && !loading && (
                <div className="text-center text-gray-500 mt-8">
                    {valeras.length === 0 ? 'Нет созданных Валер' : 'Ничего не найдено'}
                </div>
            )}
        </div>
    );
};

export default ValeraList;