import React, { useState, useEffect } from 'react';
import Header from './components/Header';
import CarCreator_classComp from './components/CarCreator_classComp';
import CarTable from './components/CarTable';
import './index.css';

function App() {
    const [cars, setCars] = useState([]);

    useEffect(() => {
        const storedCars = JSON.parse(localStorage.getItem('cars')) || [];
        console.log("Stored cars:", storedCars);
        setCars(storedCars);
    }, []);

    const handleAddCar = (newCar) => {
        const updatedCars = [...cars, newCar];
        setCars(updatedCars);
        localStorage.setItem('cars', JSON.stringify(updatedCars));
    };

    return (
        <div>
            <Header/>
            <div className="container">
                <div className="left"> 
                    <CarCreator_classComp onAddCar={handleAddCar}/>
                </div>
                <div className="right"> 
                    <CarTable data={cars}/>
                </div>
            </div>
        </div>
    );    
}

export default App;
