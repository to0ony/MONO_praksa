import React, { useState, useEffect } from 'react';

function CarCreator({ onAddCar }) {
    const [formData, setFormData] = useState({
        brand: '',
        model: '',
        color: '',
        mileage: '',
        year: ''
    });

    useEffect(() => {
        const storedCars = JSON.parse(localStorage.getItem('cars')) || [];
        setFormData({
            brand: '',
            model: '',
            color: '',
            mileage: '',
            year: ''
        });
    }, []);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        onAddCar(formData);
        setFormData({
            brand: '',
            model: '',
            color: '',
            mileage: '',
            year: ''
        });

        // Spremanje podataka u localStorage
        const storedCars = JSON.parse(localStorage.getItem('cars')) || [];
        const updatedCars = [...storedCars, formData];
        localStorage.setItem('cars', JSON.stringify(updatedCars));
    };

    return (
        <form className="form-container" onSubmit={handleSubmit}>
            <h2>Add Car</h2>
            <label>Brand:</label>
            <input type="text" name="brand" value={formData.brand} onChange={handleChange} required />
            <label>Model:</label>
            <input type="text" name="model" value={formData.model} onChange={handleChange} required />
            <label>Color:</label>
            <input type="text" name="color" value={formData.color} onChange={handleChange} required />
            <label>Mileage:</label>
            <input type="number" name="mileage" value={formData.mileage} onChange={handleChange} required />
            <label>Year:</label>
            <input type="number" name="year" value={formData.year} onChange={handleChange} required />
            <button type="submit">Add Car</button>
        </form>
    );
}

export default CarCreator;