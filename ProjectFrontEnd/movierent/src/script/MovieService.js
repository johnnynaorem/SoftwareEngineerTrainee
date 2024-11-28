import axios from "./Interceptor";
// export const addMovie = async () => {
//     try {

//     } catch (error) {

//     }
// }

export const getAllMovie = async () => {
  try {
    const response = await axios.get(
      "https://localhost:7203/api/Movie/GetAllMovies"
    );
    return response;
  } catch (err) {
    alert("Error when Fetching Movies");
    return err;
  }
};

export const getAllMovieCatalog = async () => {
  try {
    const response = await axios.get(
      "https://localhost:7203/api/Movie/GetAllMoviesCatalog"
    );
    return response;
  } catch (err) {
    alert("Error when Fetching Movies Catalog");
    return err;
  }
};

export const getMovieByCategory = async (category) => {
  try {
    const response = await axios.get(
      `https://localhost:7203/api/Movie/filter?genre=${category}`
    );
    return response;
  } catch (err) {
    alert("Error when Fetching Movies Catalog");
    return err;
  }
};

export const getMovieWithPagination = async (pageNumber, pageSize) => {
  try {
    const response = await axios.get(
      `https://localhost:7203/api/Movie/GetAllMoviesPagination?pageSize=${pageSize}&pageNumber=${pageNumber}`
    );
    return response;
  } catch (err) {
    alert("Error when Fetching Movies");
    return err;
  }
};

export const getMovieById = async (id) => {
  try {
    const response = await axios.get(
      `https://localhost:7203/api/Movie/GetMovieById?id=${id}`
    );
    return response;
  } catch (err) {
    alert("Error when Fetching Movie with Id");
    return err;
  }
};

export const addMovie = async (
  title,
  genre,
  rental_Price,
  coverImage,
  rating,
  description,
  availableCopies,
  releaseDate
) => {
  try {
    const response = await axios.post(
      `https://localhost:7203/api/Movie/AddMovie`,
      {
        title,
        genre,
        rental_Price,
        coverImage,
        rating,
        description,
        availableCopies,
        releaseDate,
      }
    );
    return response;
  } catch (err) {
    alert("Error when Adding Movie");
    return err;
  }
};
