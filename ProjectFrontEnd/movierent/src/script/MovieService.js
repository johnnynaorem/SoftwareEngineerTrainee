import axios from './Interceptor';
// export const addMovie = async () => {
//     try {


        
//     } catch (error) {
        
//     }
// }

export const getAllMovie = async () => {

    try {
        const response = await axios.get('https://localhost:7203/api/Movie/GetAllMovies');
        return response;
    } catch (err) { 
        alert("Error when Fetching Movies");
        return err; 
    }
    
}

export const getAllMovieCatalog = async () => {

    try {
        const response = await axios.get('https://localhost:7203/api/Movie/GetAllMoviesCatalog');
        return response;
    } catch (err) { 
        alert("Error when Fetching Movies Catalog");
        return err; 
    }
}

export const getMovieByCategory = async (category) => {

    try {
        const response = await axios.get(`https://localhost:7203/api/Movie/filter?genre=${category}`);
        return response;
    } catch (err) { 
        alert("Error when Fetching Movies Catalog");
        return err; 
    }
}