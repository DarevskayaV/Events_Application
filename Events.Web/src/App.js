import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Login from './components/Auth/Login';
import Register from './components/Auth/Register';
import EventForm from './components/Events/EventForm';
import EventsList from './components/Events/EventList';
import EventDetails from './components/Events/EventDetails'; // Импортируйте EventDetails
import Header from './components/Shared/Header';
import Footer from './components/Shared/Footer';
import ParticipantList from './components/Participants/ParticipantList'; // Импортируйте ParticipantList

const App = () => {
    return (
        <Router>
            <Header />
            <Routes>
                <Route path="/" element={<EventsList />} />
                <Route path="/event/:id" element={<EventDetails />} /> {/* Измените на EventDetails */}
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
                <Route path="/create-event" element={<EventForm />} />
                <Route path="/participants" element={<ParticipantList />} /> {/* Измените на ParticipantList */}
            </Routes>
            <Footer />
        </Router>
    );
};

export default App;