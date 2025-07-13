import React, { useEffect, useState } from "react";
import axios from "axios";
import logo from './../assets/images.jpeg';

const MovieInfo = ({ id }) => {
    const [movieInfo, setMovieInfo] = useState(null);
    const [error, setError] = useState("");
    // const getMovieInfo = async () => {
    //     await axios.get(`${import.meta.env.VITE_BLOCKBUSTER_API}/api/Blockbuster/movie/${id}`).then((response) => {
    //         setMovieInfo(response.data)
    //     }).catch((e) => {
    //         console.log(e, "error");
    //         setError("Error occured while retrieving movie info");
    //     });
    // }

    useEffect(() => {


        const getMovieInfo = async () => {

            try {
                const response = await axios.get(`${import.meta.env.VITE_BLOCKBUSTER_API}/api/Blockbuster/movie/${id}`);
                console.log(response.data);
                setMovieInfo(response.data);
            }
            catch (e) {
                console.log(e, "error");
                setError("Error occured while retrieving movie info");
            }

        }

        getMovieInfo();
    }, [id])

    if (!movieInfo) return <div>Loading...</div>

    return (
        <div className="text-center">

            <img
                src={movieInfo.poster || logo}
                className="card-img-top"
                style={{ objectFit: "cover", height: "300px" }}
                onError={(e) => {
                    e.target.onerror = null;
                    e.target.src = logo
                }}
            />
            <h5 className="mb-3">{movieInfo.title}</h5>
            <p><strong>Title:</strong>{" "}{movieInfo.title}</p>
            <p><strong>Actors:</strong>{" "}{movieInfo.actors}</p>
            <p><strong>Country:</strong>{" "}{movieInfo.country}</p>
            <p><strong>Director:</strong>{" "}{movieInfo.director}</p>
            <p><strong>Genre:</strong>{" "}{movieInfo.genre}</p>
            <p><strong>Id:</strong>{" "}{movieInfo.id}</p>
            <p><strong>Language:</strong>{" "}{movieInfo.language}</p>
            <p><strong>Metascore:</strong>{" "}{movieInfo.metascore}</p>
            <p><strong>Plot:</strong>{" "}{movieInfo.plot}</p>
            <p><strong>Price:</strong>{" "}{movieInfo.price}</p>
            <p><strong>Rated:</strong>{" "}{movieInfo.rated}</p>
            <p><strong>Rating:</strong>{" "}{movieInfo.rating}</p>
            <p><strong>Released:</strong>{" "}{movieInfo.released}</p>
            <p><strong>Runtime:</strong>{" "}{movieInfo.runtime}</p>
            <p><strong>Votes:</strong>{" "}{movieInfo.votes}</p>
            <p><strong>Writer:</strong>{" "}{movieInfo.writer}</p>
            <p><strong>Year:</strong>{" "}{movieInfo.year}</p>


        </div>

    );
};

export default MovieInfo;