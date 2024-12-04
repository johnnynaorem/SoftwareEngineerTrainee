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

export const updateMovie = async (
  movieId,
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
    const response = await axios.patch(
      `https://localhost:7203/api/Movie/UpdateMovieDetails?movieId=${movieId}`,
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
    alert("Error when Updating Movie Details");
    return err;
  }
};

export const movieFilter = async (title, genre) => {
  try {
    if (title != "" && genre != "") {
      const response = await axios.get(
        `https://localhost:7203/api/Movie/filter?title=${title}&genre=${genre}`
      );
      return response;
    }
    if (title == "" && genre != "") {
      const response = await axios.get(
        `https://localhost:7203/api/Movie/filter?genre=${genre}`
      );
      return response;
    }

    if (title != "" && genre == "") {
      const response = await axios.get(
        `https://localhost:7203/api/Movie/filter?title=${title}`
      );
      return response;
    }
  } catch (error) {
    console.log(error);
  }
};

export const addMovieToWishlist = async (movieId, customerId) => {
  try {
    const response = await axios.post(
      `https://localhost:7203/api/Wishlist/AddWishlist`,
      {
        movieId,
        customerId,
      }
    );
    return response;
  } catch (error) {
    console.log(error);
    alert("Error when add to wishlist process");
  }
};

export const removeMovieFromWishlist = async (movieId, customerId) => {
  try {
    const response = await axios.delete(
      `https://localhost:7203/api/Wishlist/RemoveWishlist`,
      {
        headers: {
          "Content-Type": "application/json",
        },
        data: {
          movieId,
          customerId,
        },
      }
    );
    return response;
  } catch (error) {
    console.log(error);
    alert("Error when remove from wishlist process");
  }
};

export const getWishlistByMovieIdAndCustomerId = async (
  movieId,
  customerId
) => {
  try {
    const response = await axios.get(
      `https://localhost:7203/api/Wishlist/GetWishlistByMovieIdAndCustomerId?movieId=${movieId}&customerId=${customerId}`
    );
    return response;
  } catch (error) {
    return error;
  }
};

export const getWishlistsCustomerId = async (customerId) => {
  try {
    const response = await axios.get(
      `https://localhost:7203/api/Wishlist/GetWishlistsByCustomer?customerId=${customerId}`
    );
    return response;
  } catch (error) {
    return error;
  }
};
