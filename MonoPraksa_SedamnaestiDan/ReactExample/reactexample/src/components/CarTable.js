import React, { useState, useEffect } from 'react';
import CarUpdate from './CarUpdate';

function CarTable({ data }) {
    const [cars, setCars] = useState(data);
    const [editingIndex, setEditingIndex] = useState(null);

    useEffect(() => {
        setCars(data);
    }, [data]);

    const handleEditCar = (index) => {
        setEditingIndex(index);
    };

    const handleUpdateCar = (updatedCar) => {
        const updatedCars = cars.map((car, index) => {
            if (index === editingIndex) {
                return updatedCar;
            }
            return car;
        });
        setCars(updatedCars);
        setEditingIndex(null);
        localStorage.setItem('cars', JSON.stringify(updatedCars));
    };

    const handleCancelUpdate = () => {
        setEditingIndex(null);
    };

    const handleDeleteCar = (index) => {
        const updatedCars = cars.filter((car, carIndex) => carIndex !== index);
        setCars(updatedCars);
        localStorage.setItem('cars', JSON.stringify(updatedCars));
    };

    return (
        <div className="table-container">
            <table>
                <thead>
                    <tr>
                        <th>Brand</th>
                        <th>Model</th>
                        <th>Color</th>
                        <th>Mileage</th>
                        <th>Year</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {cars.map((item, index) => (
                        <tr key={index}>
                            <td>{item.brand}</td>
                            <td>{item.model}</td>
                            <td>{item.color}</td>
                            <td>{item.mileage}</td>
                            <td>{item.year}</td>
                            <td>
                                {editingIndex === index ? (
                                    <button onClick={handleCancelUpdate}>Cancel</button>
                                ) : (
                                    <div>
                                        <button onClick={() => handleEditCar(index)}>Edit</button>
                                        <button onClick={() => handleDeleteCar(index)}>Delete</button>
                                    </div>
                                )}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
            {editingIndex !== null && (
                <CarUpdate
                    car={cars[editingIndex]}
                    onUpdateCar={handleUpdateCar}
                />
            )}
        </div>
    );
}

export default CarTable;
