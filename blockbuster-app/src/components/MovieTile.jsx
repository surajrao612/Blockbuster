import React from "react";
import logo from './../assets/images.jpeg';

const MovieTile = ({ movie, onClick }) => {
    return (

        <div
            className="card h-100 shadow-sm border-0"
            onClick={onClick}
            style={{ cursor: "pointer" }}
        >
            <img
                src={movie.poster || logo}
                className="card-img-top"
                style={{ objectFit: "cover", height: "300px" }}
                onError={(e)=>{
                    e.target.onerror=null;
                    e.target.src = logo
                }}
            />
            

            <div className="card-body text-center">
                <h5 className="card-title mb-2">{movie.title}</h5>
                <p className="card-text text-muted">{movie.year}</p>

            </div>
        </div>



    );
};

export default MovieTile;