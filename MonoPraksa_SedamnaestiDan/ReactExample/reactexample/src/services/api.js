import axios from 'axios';

const apiUrl = 'https://localhost:44338/api/Car';

async function getAllCars(pageNum = 1, pageSize = 10, sortBy = 'Model', sortOrder = 'ASC', searchQuery = null, brand = null, model = null, mileage = null, insuranceStatus = null, available = null, manufactureDate = null) {
    try {
        const response = await axios.get(apiUrl, {
            params: {
                pageNum,
                pageSize,
                sortBy,
                sortOrder,
                searchQuery,
                brand,
                model,
                mileage,
                insuranceStatus,
                available,
                manufactureDate
            }
        });
        return response.data;
    } catch (error) {
        console.error('Error fetching cars:', error);
        throw error;
    }
}

async function getCarById(id) {
    try {
        const response = await axios.get(`${apiUrl}/${id}`);
        return response.data;
    } catch (error) {
        console.error('Error fetching car by ID:', error);
        throw error;
    }
}

async function createCar(car) {
    try {
        const response = await axios.post(apiUrl, car);
        return response.status === 200; // Check if status is Created (201)
    } catch (error) {
        console.error('Error creating car:', error);
        throw error;
    }
}

async function updateCar(id, updatedCar) {
    try {
        const response = await axios.put(`${apiUrl}/${id}`, updatedCar);
        return response.status === 200; // Check if status is No Content (204)
    } catch (error) {
        console.error('Error updating car:', error);
        throw error;
    }
}

async function deleteCar(id) {
    try {
        const response = await axios.delete(`${apiUrl}/${id}`);
        return response.status === 200; // Check if status is No Content (204)
    } catch (error) {
        console.error('Error deleting car:', error);
        throw error;
    }
}

export { getAllCars, getCarById, createCar, updateCar, deleteCar };
