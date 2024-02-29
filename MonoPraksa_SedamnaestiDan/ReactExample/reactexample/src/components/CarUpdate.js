import React, { useState } from 'react';

function CarUpdate({ car, onUpdate }) {
    const [updatedCar, setUpdatedCar] = useState({
        brand: car.brand,
        model: car.model,
        mileage: car.Mileage || 0,
        insuranceStatus: false,
        available: false,
        manafactureDate: car.manafactureDate
    });

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        const newValue = type === 'checkbox' ? checked : value;
        setUpdatedCar({ ...updatedCar, [name]: newValue });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        const updatedData = {
            Id: car.id,
            Brand: updatedCar.brand,
            Model: updatedCar.model,
            Mileage: updatedCar.mileage,
            InsuranceStatus: updatedCar.insuranceStatus,
            Available: updatedCar.available,
            ManafactureDate: updatedCar.manafactureDate
        };
        onUpdate(updatedData);
    };

    return (
        <div className="update-form-container">
            <h2>Update Car</h2>
            <form onSubmit={handleSubmit}>
                <label>Brand:</label>
                <input type="text" name="brand" value={updatedCar.brand} onChange={handleChange} required />
                <label>Model:</label>
                <input type="text" name="model" value={updatedCar.model} onChange={handleChange} required />
                <label>Mileage:</label>
                <input type="number" name="mileage" value={updatedCar.mileage} onChange={handleChange} required />
                <label>Manufacture Date:</label>
                <input type="text" name="manafactureDate" value={updatedCar.manafactureDate} onChange={handleChange} required />
                <label>Insurance Status:</label>
                <select name="insuranceStatus" value={updatedCar.insuranceStatus} onChange={handleChange}>
                    <option value={true}>Yes</option>
                    <option value={false}>No</option>
                </select>
                <label>Available:</label>
                <select name="available" value={updatedCar.available} onChange={handleChange}>
                    <option value={true}>Yes</option>
                    <option value={false}>No</option>
                </select>
                <button type="submit">Apply</button>
            </form>
        </div>
    );
}

export default CarUpdate;
