import React, { Component } from 'react';

class CarCreator_classComp extends Component {
    constructor(props) {
        super(props);
        this.state = {
            brand: '',
            model: '',
            color: '',
            mileage: '',
            year: ''
        };
    }

    componentDidMount() {
        const storedCars = JSON.parse(localStorage.getItem('cars')) || [];
        if (storedCars.length === 0) {
            this.setState({
                brand: '',
                model: '',
                color: '',
                mileage: '',
                year: ''
            });
        }
    }

    handleChange = (e) => {
        const { name, value } = e.target;
        this.setState(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    handleSubmit = (e) => {
        e.preventDefault();
        const { onAddCar } = this.props;
        const { brand, model, color, mileage, year } = this.state;
        const newCar = { brand, model, color, mileage, year };
        onAddCar(newCar);
        this.setState({
            brand: '',
            model: '',
            color: '',
            mileage: '',
            year: ''
        });

        const storedCars = JSON.parse(localStorage.getItem('cars')) || [];
        const updatedCars = [...storedCars, newCar];
        localStorage.setItem('cars', JSON.stringify(updatedCars));
    };

    render() {
        const { brand, model, color, mileage, year } = this.state;
        return (
            <form className="form-container" onSubmit={this.handleSubmit}>
                <h2>Add Car</h2>
                <label>Brand:</label>
                <input type="text" name="brand" value={brand} onChange={this.handleChange} required />
                <label>Model:</label>
                <input type="text" name="model" value={model} onChange={this.handleChange} required />
                <label>Color:</label>
                <input type="text" name="color" value={color} onChange={this.handleChange} required />
                <label>Mileage:</label>
                <input type="number" name="mileage" value={mileage} onChange={this.handleChange} required />
                <label>Year:</label>
                <input type="number" name="year" value={year} onChange={this.handleChange} required />
                <button type="submit">Add Car</button>
            </form>
        );
    }
}

export default CarCreator_classComp;