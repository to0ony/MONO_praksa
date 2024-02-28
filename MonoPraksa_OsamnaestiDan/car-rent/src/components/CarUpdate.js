import React, { useState, useEffect } from 'react';
import axios from 'axios';


const CarUpdate = ({ car, onUpdateSuccess }) => {
    const [formData, setFormData] = useState({
        Brand: '',
        Model: '',
        Mileage: '',
        InsuranceStatus: false,
        Available: false,
        ManafactureDate: ''
    });

    useEffect(() => {
        if (car) {
            setFormData({
                Brand: car.Brand || '',
                Model: car.Model || '',
                Mileage: car.Mileage || '',
                InsuranceStatus: car.InsuranceStatus || false,
                Available: car.Available || false,
                ManafactureDate: car.ManafactureDate || ''
            });
        }
    }, [car]);

    const handleChange = e => {
        const { name, value, type, checked } = e.target;
        const inputValue = type === 'checkbox' ? checked : value;
        setFormData(prevState => ({
            ...prevState,
            [name]: inputValue
        }));
    };

    const handleSubmit = async e => {
        e.preventDefault();
        try {
            const response = await axios.put(`https://localhost:44338/api/Car/${car.Id}`, formData);
            if (response.status === 200) {
                onUpdateSuccess({ ...formData, Id: car.Id });
            }
        } catch (error) {
            console.error('Error updating car:', error);
        }
    };

    return (
        <div>
            <h2>Update Car</h2>
            <form onSubmit={handleSubmit}>
                <label>
                    Brand:
                    <input type="text" name="Brand" value={formData.Brand} onChange={handleChange} required />
                </label>
                <label>
                    Model:
                    <input type="text" name="Model" value={formData.Model} onChange={handleChange} required />
                </label>
                <label>
                    Mileage:
                    <input type="number" name="Mileage" value={formData.Mileage} onChange={handleChange} />
                </label>
                <label>
                    Insurance Status:
                    <input type="checkbox" name="InsuranceStatus" checked={formData.InsuranceStatus} onChange={handleChange} />
                </label>
                <label>
                    Available:
                    <input type="checkbox" name="Available" checked={formData.Available} onChange={handleChange} />
                </label>
                <label>
                    Manufacture Date:
                    <input type="number" name="ManafactureDate" value={formData.ManafactureDate} onChange={handleChange} />
                </label>
                <button type="submit">Apply</button>
            </form>
        </div>
    );
};

export default CarUpdate;
