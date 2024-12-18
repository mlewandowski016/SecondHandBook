import axios from 'axios';
import config from './config';

const api = axios.create({
    baseURL: config.API_URL,
    headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem('token').replace(/[""]+/g, '')
    }
});

export default api;