import React, { useEffect, useState } from 'react';
import api from '../../api';

const EventList = () => {
    const [events, setEvents] = useState([]);

    useEffect(() => {
        const fetchEvents = async () => {
            const result = await api.get('/events');
            setEvents(result.data);
        };
        fetchEvents();
    }, []);

    return (
        <div>
            <h2>Events</h2>
            <ul>
                {events.map(event => (
                    <li key={event.id}>
                        <a href={`/event/${event.id}`}>{event.name}</a>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default EventList; 
