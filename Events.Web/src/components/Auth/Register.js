import React, { useState } from 'react';
import AuthService from './AuthService';

const Register = () => {
    const [user, setUser] = useState({ name: '', email: '', password: '' });

    const handleRegister = async (e) => {
        e.preventDefault();
        await AuthService.register(user);
        // Redirect or show message
    };

    return (
        <form onSubmit={handleRegister}>
            <h2>Register</h2>
            <input
                type="text"
                placeholder="Name"
                value={user.name}
                onChange={(e) => setUser({ ...user, name: e.target.value })}
                required
            />
            <input
                type="email"
                placeholder="Email"
                value={user.email}
                onChange={(e) => setUser({ ...user, email: e.target.value })}
                required
            />
            <input
                type="password"
                placeholder="Password"
                value={user.password}
                onChange={(e) => setUser({ ...user, password: e.target.value })}
                required
            />
            <button type="submit">Register</button>
        </form>
    );
};

export default Register; 
