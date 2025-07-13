import React, { useEffect, useState } from "react"
import MovieTile from "./MovieTile";
import MovieInfo from "./MovieInfo";
import axios from "axios";

const MovieList = () => {
    const [movies, setMovies] = useState([]);
    const [error, setError] = useState("");
    const [selectedMovieId, setSelectedMovieId] = useState("");
    const [showModal, setShowModal] = useState(false);



    const handleModelOpen = (id) => {
        setSelectedMovieId(id);
        setShowModal(true);
    };

    const handleModelClose = () => {
        setSelectedMovieId("");
        setShowModal(false);
    };


    useEffect(() => {
        const getMovies = async () => {

            try {
                const response = await axios.get(`${import.meta.env.VITE_BLOCKBUSTER_API}/api/Blockbuster/movies`);
                //console.log(response.data.movies);
                setMovies(response.data.movies);
            }
            catch (e) {
                console.log(e, "error");
                setError("Error occured while retrieving movie info");
            }

        }

        getMovies();
    }, [])

    return (
        <>
            <div className="container">
                <div className="row g-4">
                    {movies.map((movie) => (
                        <div className="col-md-4">
                            <MovieTile key={movie.id} movie={movie} onClick={() => handleModelOpen(movie.id)} />
                        </div>
                    ))}
                </div>

                {/* {showModal && (
                    <div className="modal show d-block" tabIndex="-1" role="dialog">
                        <button onClick={handleModelClose}>Close</button>
                        <MovieInfo id={selectedMovieId} />

                    </div>

                )} */}
                {showModal && (
                    <div className="modal show d-block" tabIndex="-1" role="dialog" style={{ backgroundColor: "rgba(0,0,0,0.5)" }}>
                        <div className="modal-dialog modal-dialog-centered" role="document">
                            <div className="modal-content">
                                <div className="modal-header">

                                    <button type="button" className="btn-close" onClick={handleModelClose}></button>
                                </div>
                                <div className="modal-body">
                                    <MovieInfo id={selectedMovieId} />
                                </div>
                                <div className="modal-footer">
                                    <button className="btn btn-secondary" onClick={handleModelClose}>Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                )}
            </div>
        </>
    );
}

export default MovieList;