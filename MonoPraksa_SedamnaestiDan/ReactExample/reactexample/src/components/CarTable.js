import React, { useState } from 'react';
import CarUpdate from './CarUpdate'
import { deleteCar, updateCar } from '../services/api';

function CarTable({ cars, onCarDeleted, onCarUpdated }) {
    const [showUpdateForm, setShowUpdateForm] = useState(false);
    const [selectedCar, setSelectedCar] = useState(null);

    const handleDeleteClick = async (id) => {
        try {
            const deleted = await deleteCar(id); 
            if (deleted) {
                onCarDeleted();
            }
        } catch (error) {
            console.error('Error deleting car:', error);
        }
    };

    const handleUpdateClick = (car) => {
        setSelectedCar(car); 
        setShowUpdateForm(true); 
    };

    const handleUpdateSubmit = async (id, updatedCarData) => {
        try {
            const updated = await updateCar(id, updatedCarData); 
            if (updated) {
                onCarUpdated();
                setShowUpdateForm(false); 
            }
        } catch (error) {
            console.error('Error updating car:', error);
        }
    };

    return (
        <div className="table-container">
            <table>
                <thead>
                    <tr>
                        <th>Brand</th>
                        <th>Model</th>
                        <th>Manufacture Date</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {cars.map((car, index) => (
                        <tr key={index}>
                            <td>{car.brand}</td>
                            <td>{car.model}</td>
                            <td>{car.manafactureDate}</td>
                            <td>
                                <button onClick={() => handleDeleteClick(car.id)}>Delete</button>
                                <button onClick={() => handleUpdateClick(car)}>Edit</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
            {showUpdateForm && (
                <CarUpdate car={selectedCar} onUpdate={(updatedCarData) => handleUpdateSubmit(selectedCar.id, updatedCarData)} />
            )}
        </div>
    );
}

export default CarTable;
