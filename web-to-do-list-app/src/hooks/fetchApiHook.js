import { useState, useEffect } from 'react';

export const useFetchData = (BASE_URL = '', error_msg = 'An error has ocurred') => {
    const [fetch_data, setFetchData] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');

    const getData = async () => {
        const fetchData = async () => {
            setLoading(true);            
            try {
                const response = await window.fetch(BASE_URL);
                if (!response.ok) {
                    throw new Error(`Http status ${response.status}`);
                }
                const data = await response.json();                
                setFetchData(data);
            } catch (error) {                
                setError(error_msg)
            }
            setLoading(false);
        }
        fetchData();
    }; 
    return { data: fetch_data, loading, error };
}