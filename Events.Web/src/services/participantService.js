import axios from 'axios';

const API_URL_PARTICIPANTS = 'http://backend:8080/api/participants'; // Измените на имя сервиса

export const registerParticipant = (eventId, participantData) => {
  return axios.post(`${API_URL_PARTICIPANTS}`, { ...participantData, eventId });
};