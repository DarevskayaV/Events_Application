import React, { useEffect, useState } from 'react';
import api from '../../api';

const ParticipantList = ({ eventId }) => {
    const [participants, setParticipants] = useState([]);

    useEffect(() => {
        const fetchParticipants = async () => {
            const result = await api.get(`/events/${eventId}/participants`);
            setParticipants(result.data);
        };
        fetchParticipants();
    }, [eventId]);

    return (
        <div>
            <h2>Participants</h2>
            <ul>
                {participants.map(participant => (
                    <li key={participant.id}>
                        {participant.firstName} {participant.lastName}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default ParticipantList; 
