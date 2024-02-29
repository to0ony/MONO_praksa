import React, { useState } from 'react';
import { createCar } from '../services/api';
import { v4 as uuidv4 } from 'uuid';

function CarCreator({ onCarCreated }) {
    const [formData, setFormData] = useState({
        Id: '', // Dodano polje za ID
        Brand: '',
        Model: '',
        Mileage: 0,
        ManafactureDate: '',
        InsuranceStatus: false,
        Available: true
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const carDataWithId = { ...formData, Id: uuidv4() };
            const created = await createCar(carDataWithId);
            console.log(created);
            if (created) {
                onCarCreated();
                setFormData({
                    Id: '',
                    Brand: '',
                    Model: '',
                    Mileage: 0,
                    ManafactureDate: '',
                    InsuranceStatus: false,
                    Available: true
                });
            }
        } catch (error) {
            console.error('Error creating car:', error);
        }
    };

    return (
    <form className="form-container" onSubmit={handleSubmit}>
        <h2>Add Car</h2>
        <label>Brand:</label>
        <input type="text" name="Brand" value={formData.Brand} onChange={handleChange} required />
        <label>Model:</label>
        <input type="text" name="Model" value={formData.Model} onChange={handleChange} required />
        <label>Mileage:</label>
        <input type="number" name="Mileage" value={formData.Mileage} onChange={handleChange} required />
        <label>Manufacture Date:</label>
        <input type="text" name="ManafactureDate" value={formData.ManafactureDate} onChange={handleChange} />
        <label>Insurance Status:</label>
        <select name="InsuranceStatus" value={formData.InsuranceStatus} onChange={handleChange} className="select-input">
            <option value={true}>Yes</option>
            <option value={false}>No</option>
        </select>
        <label>Available:</label>
        <select name="Available" value={formData.Available} onChange={handleChange} className="select-input">
            <option value={true}>Yes</option>
            <option value={false}>No</option>
        </select>
        <button type="submit">Add Car</button>
</form>

    );
}

export default CarCreator;