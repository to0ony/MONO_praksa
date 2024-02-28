import React, { useState } from 'react';
import axios from 'axios';
import { v4 as uuidv4 } from 'uuid';


const CarAdd = ({ carList, setCarList }) => {
    const [formData, setFormData] = useState({
        Brand: '',
        Model: '',
        Mileage: '',
        InsuranceStatus: false,
        Available: false,
        ManafactureDate: ''
    });

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
        const id = uuidv4(); // Generiramo GUID
        try {
            const response = await axios.post('https://localhost:44338/api/Car', { ...formData, Id: id });
            if (response.status === 201) {
                fetchData();
                setFormData({
                    Brand: '',
                    Model: '',
                    Mileage: '',
                    InsuranceStatus: false,
                    Available: false,
                    ManafactureDate: ''
                });
            }
        } catch (error) {
            console.error('Error adding car:', error);
        }
    };

    const fetchData = async () => {
        try {
            const response = await axios.get('https://localhost:44338/api/Car');
            setCarList(response.data);
        } catch (error) {
            console.error('Error fetching car data:', error);
        }
    };

    return (
        <div>
            <h2>Add Car</h2>
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
                <button type="submit">Add Car</button>
            </form>
        </div>
    );
};

export default CarAdd;
