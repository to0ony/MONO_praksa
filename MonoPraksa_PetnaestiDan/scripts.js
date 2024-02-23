class Car {
    constructor(brand, model, color, mileage, year) {
        this.brand = brand;
        this.model = model;
        this.color = color;
        this.mileage = mileage;
        this.year = year;
    }
}

window.onload = function() {
    displayCars();
};

function addCar() {
    var brand = document.getElementById("brand").value;
    var model = document.getElementById("model").value;
    var color = document.getElementById("color").value;
    var mileage = document.getElementById("mileage").value;
    var year = document.getElementById("year").value;

    var car = new Car(brand, model, color, mileage, year);

    if (localStorage.getItem('cars') === null) {
        var cars = [];
        cars.push(car);
        localStorage.setItem('cars', JSON.stringify(cars));
    } else {
        var cars = JSON.parse(localStorage.getItem('cars'));
        cars.push(car);
        localStorage.setItem('cars', JSON.stringify(cars));
    }
    alert('Automobil je uspješno dodan!');
}

function displayCars() {
    var cars = JSON.parse(localStorage.getItem('cars'));

    if (cars) {
        var tbody = document.querySelector('.car-table tbody');
        tbody.innerHTML = '';

        cars.forEach(function(car, index) {
            var row = document.createElement('tr');
            row.innerHTML = `
                <td>${car.brand}</td>
                <td>${car.model}</td>
                <td>${car.color}</td>
                <td>${car.mileage}</td>
                <td>${car.year}</td>
                <td>
                    <button class="edit-btn" data-index="${index}">Edit</button>
                    <button class="delete-btn" data-index="${index}">Delete</button>
                </td>
            `;
            tbody.appendChild(row);
        });

        var editButtons = document.querySelectorAll('.edit-btn');
        editButtons.forEach(function(button) {
            button.addEventListener('click', function() {
                var index = this.getAttribute('data-index');
                window.location.href = 'updateCar.html?index=' + index;
            });
        });

        var deleteButtons = document.querySelectorAll('.delete-btn');
        deleteButtons.forEach(function(button) {
            button.addEventListener('click', function() {
                var index = this.getAttribute('data-index');
                deleteCar(index);
            });
        });
    }
}

function loadAndUpdateCarDetails() {
    var urlParams = new URLSearchParams(window.location.search);
    var index = urlParams.get('index');

    if (index !== null) {
        var cars = JSON.parse(localStorage.getItem('cars'));

        if (cars && cars[index]) {
            var car = cars[index];
            document.getElementById('brand').value = car.brand;
            document.getElementById('model').value = car.model;
            document.getElementById('color').value = car.color;
            document.getElementById('mileage').value = car.mileage;
            document.getElementById('year').value = car.year;

            document.getElementById('update-car-form').onsubmit = function(event) {
                event.preventDefault();

                var updatedBrand = document.getElementById('brand').value.trim();
                var updatedModel = document.getElementById('model').value.trim();
                var updatedColor = document.getElementById('color').value.trim();
                var updatedMileage = document.getElementById('mileage').value.trim();
                var updatedYear = document.getElementById('year').value.trim();

                if (updatedBrand !== '') {
                    cars[index].brand = updatedBrand;
                }
                if (updatedModel !== '') {
                    cars[index].model = updatedModel;
                }
                if (updatedColor !== '') {
                    cars[index].color = updatedColor;
                }
                if (updatedMileage !== '') {
                    cars[index].mileage = updatedMileage;
                }
                if (updatedYear !== '') {
                    cars[index].year = updatedYear;
                }

                localStorage.setItem('cars', JSON.stringify(cars));

                window.location.href = 'main.html';
            };
        }
    }
}

function deleteCar(index) {
    var cars = JSON.parse(localStorage.getItem('cars'));

    if (cars && cars[index]) {
        cars.splice(index, 1);
        localStorage.setItem('cars', JSON.stringify(cars));
        displayCars();
    }
}
