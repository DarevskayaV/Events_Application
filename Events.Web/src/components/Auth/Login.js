import React, { useState } from 'react';
import AuthService from './AuthService';

const Login = () => {
    const [credentials, setCredentials] = useState({ username: '', password: '' });

    const handleLogin = async (e) => {
        e.preventDefault();
        await AuthService.login(credentials);
        // Redirect or show message
    };

    return (
        <form onSubmit={handleLogin}>
            <h2>Login</h2>
            <input
                type="text"
                placeholder="Username"
                value={credentials.username}
                onChange={(e) => setCredentials({ ...credentials, username: e.target.value })}
                required
            />
            <input
                type="password"
                placeholder="Password"
                value={credentials.password}
                onChange={(e) => setCredentials({ ...credentials, password: e.target.value })}
                required
            />
            <button type="submit">Login</button>
        </form>
    );
};

export default Login; 
