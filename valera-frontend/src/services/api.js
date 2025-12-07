import axios from 'axios';

const API_BASE_URL = 'https://localhost:7031/api';

const api = axios.create({
    baseURL: API_BASE_URL,
});

// Добавляем токен к каждому запросу
api.interceptors.request.use((config) => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

// Обрабатываем ошибки авторизации
api.interceptors.response.use(
    (response) => response,
    (error) => {
        if (error.response?.status === 401) {
            localStorage.removeItem('token');
            window.location.href = '/login';
        }
        return Promise.reject(error);
    }
);

export const valeraAPI = {
    login: (credentials) => api.post('/auth/login', credentials),
    register: (userData) => api.post('/auth/register', userData),

    getAll: () => api.get('/valera'),
    getUserValeras: () => api.get(`/valera/my`),
    getUserRole: () => api.get(`/valera/role`),
    getById: (id) => api.get(`/valera/${id}`),
    create: (valera) => api.post('/valera', valera),
    update: (id, valera) => api.put(`/valera/${id}`, valera),
    delete: (id) => api.delete(`/valera/${id}`),

    // Действия
    work: (id) => api.post(`/valera/${id}/work`),
    contemplateNature: (id) => api.post(`/valera/${id}/contemplate-nature`),
    drinkWineWatchSeries: (id) => api.post(`/valera/${id}/drink-wine-watch-series`),
    goToBar: (id) => api.post(`/valera/${id}/go-to-bar`),
    drinkWithFriends: (id) => api.post(`/valera/${id}/drink-with-friends`),
    singInSubway: (id) => api.post(`/valera/${id}/sing-in-subway`),
    sleep: (id) => api.post(`/valera/${id}/sleep`),
};