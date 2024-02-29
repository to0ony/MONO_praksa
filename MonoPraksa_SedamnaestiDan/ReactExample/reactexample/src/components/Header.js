import React from 'react';
import { Link } from 'react-router-dom';

function Header() {
  return (
    <div className="navbar">
      <div className="car-container">
        <img src="https://static.vecteezy.com/system/resources/previews/014/070/026/original/car-icon-3d-design-for-application-and-website-presentation-png.png" alt="Car icon" />
      </div>
      <div className="button-container">
        <Link to="/"><button>Main</button></Link>
        <Link to="/car-dashboard"><button>Car Dashboard</button></Link>
      </div>
    </div>
  );
}

export default Header;