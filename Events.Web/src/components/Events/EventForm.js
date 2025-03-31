import React, { useState } from 'react';
import api from '../../api';

const EventForm = () => {
    const [event, setEvent] = useState({
        name: '',
        description: '',
        date: '',
        location: '',
        category: '',
        maxParticipants: 0,
        imageUrl: '',
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setEvent({ ...event, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        await api.post('/events', event);
        // Redirect or show success message
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>Create Event</h2>
            <input type="text" name="name" placeholder="Event Name" onChange={handleChange} required />
            <textarea name="description" placeholder="Description" onChange={handleChange} required />
            <input type="datetime-local" name="date" onChange={handleChange} required />
            <input type="text" name="location" placeholder="Location" onChange={handleChange} required />
            <input type="text" name="category" placeholder="Category" onChange={handleChange} required />
            <input type="number" name="maxParticipants" placeholder="Max Participants" onChange={handleChange} required />
            <input type="text" name="imageUrl" placeholder="Image URL" onChange={handleChange} />
            <button type="submit">Create Event</button>
        </form>
    );
};

export default EventForm; 
