import axios from 'axios';

const API_BASE_URL = 'https://localhost:7031/api';

const api = axios.create({
    baseURL: API_BASE_URL,
});

export const valeraAPI = {
    getAll: () => api.get('/valera'),
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