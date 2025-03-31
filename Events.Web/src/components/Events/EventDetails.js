import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import api from '../../api';

const EventDetails = () => {
    const { id } = useParams();
    const [event, setEvent] = useState(null);

    useEffect(() => {
        const fetchEvent = async () => {
            const result = await api.get(`/events/${id}`);
            setEvent(result.data);
        };
        fetchEvent();
    }, [id]);

    if (!event) return <div>Loading...</div>;

    return (
        <div>
            <h2>{event.name}</h2>
            <p>{event.description}</p>
            <img src={event.imageUrl} alt={event.name} />
            <p>Date: {new Date(event.date).toLocaleString()}</p>
            <p>Location: {event.location}</p>
            <p>Category: {event.category}</p>
            <p>Participants: {event.participantCount}/{event.maxParticipants}</p>
        </div>
    );
};

export default EventDetails; 
