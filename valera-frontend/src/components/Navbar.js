import React from 'react';

const Navbar = ({ onLogout }) => {
    return (
        <nav className="bg-blue-600 text-white p-4">
            <div className="container mx-auto flex justify-between items-center">
                <h1 className="text-xl font-bold">Valera Game</h1>
                <button
                    onClick={onLogout}
                    className="bg-red-500 hover:bg-red-600 px-4 py-2 rounded"
                >
                    Выйти
                </button>
            </div>
        </nav>
    );
};

export default Navbar;