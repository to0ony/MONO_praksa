import React, { useState } from 'react';

function CarUpdate({ car, onUpdateCar }) {
    const [updatedCar, setUpdatedCar] = useState({ ...car });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setUpdatedCar(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        onUpdateCar(updatedCar);
    };

    return (
        <div className="update-form-container">
            <h2>Update Car</h2>
            <form onSubmit={handleSubmit}>
                <label>Brand:</label>
                <input type="text" name="brand" value={updatedCar.brand} onChange={handleChange} required />
                <label>Model:</label>
                <input type="text" name="model" value={updatedCar.model} onChange={handleChange} required />
                <label>Color:</label>
                <input type="text" name="color" value={updatedCar.color} onChange={handleChange} required />
                <label>Mileage:</label>
                <input type="number" name="mileage" value={updatedCar.mileage} onChange={handleChange} required />
                <label>Year:</label>
                <input type="number" name="year" value={updatedCar.year} onChange={handleChange} required />
                <button type="submit">Apply</button>
            </form>
        </div>
    );
}

export default CarUpdate;