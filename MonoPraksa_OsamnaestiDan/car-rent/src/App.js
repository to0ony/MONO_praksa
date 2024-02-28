import React, { useState, useEffect } from 'react';
import axios from 'axios';
import CarAdd from './components/CarAdd';
import CarList from './components/CarList';


const App = () => {
    const [carList, setCarList] = useState([]);

    useEffect(() => {
        fetchData();
    }, []);

    const fetchData = async () => {
        try {
            const response = await axios.get('https://localhost:44338/api/Car');
            setCarList(response.data);
        } catch (error) {
            console.error('Error fetching car data:', error);
        }
    };

    const handleCarAdded = async newCar => {
        setCarList(prevCarList => [...prevCarList, newCar]);
    };

    return (
        <div>
            <h1>Car Rental System</h1>
            <CarAdd onCarAdded={handleCarAdded} />
            <CarList carList={carList} />
        </div>
    );
};

export default App;
