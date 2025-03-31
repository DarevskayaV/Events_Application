import api from '../../api';

const AuthService = {
    login: async (credentials) => {
        const response = await api.post('/auth/login', credentials);
        localStorage.setItem('token', response.data.token); // Сохранение токена
    },
    register: async (user) => {
        await api.post('/auth/register', user);
    },
    logout: () => {
        localStorage.removeItem('token'); // Удаление токена
    },
    isAuthenticated: () => {
        return !!localStorage.getItem('token'); // Проверка аутентификации
    },
};

export default AuthService; 
