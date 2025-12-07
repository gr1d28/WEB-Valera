import React, { useState, useEffect }  from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import ValeraList from './components/ValeraList';
import ValeraStats from './components/ValeraStats';
import Login from './components/Login';
import Register from './components/Register';
import Navbar from './components/Navbar';
import './App.css';

function App() {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        // Проверяем наличие токена при загрузке
        const token = localStorage.getItem('token');
        if (token) {
            setIsAuthenticated(true);
        }
        setLoading(false);
    }, []);

    const handleLogin = (token) => {
        localStorage.setItem('token', token);
        setIsAuthenticated(true);
    };

    const handleLogout = () => {
        localStorage.removeItem('token');
        setIsAuthenticated(false);
    };

    if (loading) {
        return <div className="text-center p-8">Загрузка...</div>;
    }

    return (
        <Router>
            <div className="App">
                {isAuthenticated && <Navbar onLogout={handleLogout} />}

                <Routes>
                    {/* Публичные маршруты */}
                    {!isAuthenticated && (
                        <>
                            <Route path="/login" element={<Login onLogin={handleLogin} />} />
                            <Route path="/register" element={<Register onRegister={handleLogin} />} />
                            <Route path="*" element={<Navigate to="/login" />} />
                        </>
                    )}

                    {/* Защищенные маршруты */}
                    {isAuthenticated && (
                        <>
                            <Route path="/" element={<ValeraList />} />
                            <Route path="/valera/:id" element={<ValeraStats />} />
                            <Route path="*" element={<Navigate to="/" />} />
                        </>
                    )}
                </Routes>
            </div>
        </Router>
    );
}

export default App;