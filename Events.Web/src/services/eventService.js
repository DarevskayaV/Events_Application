import axios from 'axios';

const API_URL_EVENTS = 'http://backend:8080/api/events'; // Измените на имя сервиса

export const getEvents = async () => {
    const response = await axios.get(API_URL_EVENTS);
    return response.data;
};

export const createEvent = async (eventData) => {
    const response = await axios.post(API_URL_EVENTS, eventData);
    return response.data;
};