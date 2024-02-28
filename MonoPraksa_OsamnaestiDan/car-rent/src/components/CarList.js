import React, { useEffect, useState } from 'react';
import axios from 'axios';
import CarUpdate from './CarUpdate';


const CarList = () => {
    const [cars, setCars] = useState([]);
    const [selectedCar, setSelectedCar] = useState(null);
    const [updateSuccess, setUpdateSuccess] = useState(false);

    useEffect(() => {
        fetchData();
    }, [updateSuccess]);

    const fetchData = async () => {
        try {
            const response = await axios.get('https://localhost:44338/api/Car');
            setCars(response.data);
        } catch (error) {
            console.error('Error fetching car data:', error);
        }
    };

    const handleDelete = async id => {
        try {
            await axios.delete(`https://localhost:44338/api/Car/${id}`);
            fetchData();
        } catch (error) {
            console.error('Error deleting car:', error);
        }
    };

    const handleUpdate = car => {
        setSelectedCar(car);
    };

    const handleUpdateSuccess = () => {
        setUpdateSuccess(true);
    };

    return (
        <div>
            <h2>Car List</h2>
            <table>
                <thead>
                    <tr>
                        <th>Brand</th>
                        <th>Model</th>
                        <th>Manufacture Date</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {cars.map(car => (
                        <tr key={car.Id}>
                            <td>{car.Brand}</td>
                            <td>{car.Model}</td>
                            <td>{car.ManafactureDate}</td>
                            <td>
                                <button onClick={() => handleDelete(car.Id)}>Delete</button>
                                <button onClick={() => handleUpdate(car)}>Update</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
            {selectedCar && !updateSuccess && <CarUpdate car={selectedCar} onUpdateSuccess={handleUpdateSuccess} />}
        </div>
    );
};

export default CarList;
