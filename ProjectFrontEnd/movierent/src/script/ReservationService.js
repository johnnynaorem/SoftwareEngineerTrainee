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
