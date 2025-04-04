import React from 'react';
import { Link } from 'react-router-dom';

const Header = () => {
    return (
        <header>
            <nav>
                <Link to="/">Home</Link>
                <Link to="/login">Login</Link>
                <Link to="/register">Register</Link>
                <Link to="/create-event">Create Event</Link>
                <Link to="/participants">Participants</Link>
            </nav>
        </header>
    );
};

export default Header; 
