import React, { createContext, useContext, useState } from 'react';
import AuthService from '../components/Auth/AuthService';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(AuthService.isAuthenticated());

    const login = async (credentials) => {
        await AuthService.login(credentials);
        setIsAuthenticated(true);
    };

    const logout = () => {
        AuthService.logout();
        setIsAuthenticated(false);
    };

    return (
        <AuthContext.Provider value={{ isAuthenticated, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    return useContext(AuthContext);
}; 
