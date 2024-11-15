import axios from "./Interceptor";

export const getReservationByCustomer = async () => {
  try {
    const response = await axios.get(
      `https://localhost:7203/api/MovieReservation/GetAllReservationByCustomer?customerId=3`
    );
    console.log(response);
    return response;
  } catch (error) {
    alert("Error when Fetching Reservation");
    return error;
  }
};
