import React, { useState, useEffect } from 'react';
import CarCreator from '../components/CarCreator';
import CarTable from '../components/CarTable';
import { getAllCars } from '../services/api';
import './cardashboard.css';

function App() {
    const [cars, setCars] = useState([]);

    useEffect(() => {
        fetchCars(); 
    }, []);

    const fetchCars = async () => {
        try {
            const fetchedCars = await getAllCars(); 
            setCars(fetchedCars);
        } catch (error) {
            console.error('Error fetching cars:', error);
        }
    };

    const handleCarCreated = async () => {
        try{
            const fetchedCars = await getAllCars();
            setCars(fetchedCars);
        }catch(error){
            console.error('ERROR:',error);
        }
    };

    return (
        <div>
            <div className="container">
                <div className="left">
                    <CarCreator onCarCreated={handleCarCreated} />
                </div>
                <div className="right">
                    <CarTable cars={cars} onCarDeleted={fetchCars} onCarUpdated={fetchCars}/>
                </div>
            </div>
        </div>
    );
}

export default App;
