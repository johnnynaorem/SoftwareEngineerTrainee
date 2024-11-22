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
