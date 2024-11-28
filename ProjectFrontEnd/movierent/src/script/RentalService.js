import axios from "./Interceptor";

export const getRentalByCustomer = async (customerId) => {
  try {
    const response = await axios.get(
      `https://localhost:7203/api/Rental/GetRentalsByCustomer?customerId=${customerId}`
    );
    return response;
  } catch (error) {
    alert("Error when Fetching Rental");
    return error;
  }
};

export const getRentalByRentalId = async (rentalId) => {
  try {
    const response = await axios.get(
      `https://localhost:7203/api/MovieReservation/GetReservationById?id=${rentalId}`
    );
    return response;
  } catch (error) {
    alert("Error when Fetching Reservation");
    return error;
  }
};

export const getAllRetals = async () => {
  try {
    const response = await axios.get(
      `https://localhost:7203/api/Rental/GetRentals`
    );
    return response;
  } catch (error) {
    alert("Error when Fetching Rentals");
    return error;
  }
};

export const rentMovie = async (customerId, movieId) => {
  try {
    const response = await axios.post(
      "https://localhost:7203/api/Rental/RentMovie",
      { customerId, movieId }
    );
    return response;
  } catch (error) {
    console.log(error);
    return error;
  }
};

//pickup Movie

export const pickupMovieByCustomer = async (rentId, movieId, customerId) => {
  try {
    const response = await axios.patch(
      "https://localhost:7203/api/Customer/PickUpMovie",
      { rentId, movieId, customerId }
    );
    return response;
  } catch (err) {
    console.log(err);
    alert("Error when Pickup movie");
  }
};

//return Movie
export const returnMovieByCustomer = async (rentId, customerId) => {
  try {
    const response = await axios.patch(
      "https://localhost:7203/api/Customer/returnMovie",
      { rentId, customerId }
    );
    return response;
  } catch (err) {
    console.log(err);
    alert("Error when returning movie");
  }
};
