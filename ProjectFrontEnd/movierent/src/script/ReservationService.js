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
