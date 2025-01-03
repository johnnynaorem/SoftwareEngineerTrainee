import axios from "./Interceptor";

export const getReservationByCustomer = async (customerId) => {
  try {
    const response = await axios.get(
      `https://localhost:7203/api/MovieReservation/GetAllReservationByCustomer?customerId=${customerId}`
    );
    return response;
  } catch (error) {
    alert("Error when Fetching Reservation");
    return error;
  }
};

export const getReservationByReservationId = async (reservationId) => {
  try {
    const response = await axios.get(
      `https://localhost:7203/api/MovieReservation/GetReservationById?id=${reservationId}`
    );
    return response;
  } catch (error) {
    alert("Error when Fetching Reservation");
    return error;
  }
};

export const reservationSumbit = async (
  CustomerId,
  MovieId,
  ReservationDate
) => {
  try {
    const response = await axios.post(
      `https://localhost:7203/api/MovieReservation/ReserveMovie`,
      {
        CustomerId,
        MovieId,
        ReservationDate,
      }
    );
    return response;
  } catch (error) {
    alert("Error when Movie Reservation");
    return error;
  }
};

export const getAllReservation = async () => {
  try {
    const response = await axios.get(
      "https://localhost:7203/api/MovieReservation/GetAllReservation"
    );
    return response;
  } catch (error) {
    console.log(error);
    return error;
  }
};

export const updateReservationStatus = async (reservationId, status) => {
  try {
    const response = await axios.patch(
      `https://localhost:7203/api/MovieReservation/UpdateMovieReserveStatus`,
      {
        reservationId,
        status,
      }
    );
    return response;
  } catch (error) {
    alert("Error in updating Status");
    console.log(error);
    return error;
  }
};

export const getReservationByCustomerIdAndMovieId = async (
  movieId,
  customerId
) => {
  try {
    const response = await axios.get(
      `https://localhost:7203/api/MovieReservation/GetReservationByMovieIdAndCustomerId?movieId=${movieId}&customerId=${customerId}`
    );
    return response;
  } catch (err) {
    console.log(err);
    alert("Error when Fetching Reservation using customer and movie ids");
    return err;
  }
};
