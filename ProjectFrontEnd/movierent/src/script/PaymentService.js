import axios from "./Interceptor";

export const getPaymentByCustomer = async (customerId) => {
  try {
    const response = await axios.get(
      `https://localhost:7203/api/Payment/GetAllPaymentByCustomer?id=${customerId}`
    );
    return response;
  } catch (error) {
    alert("Error when Fetching Payment");
    return error;
  }
};

export const getAllPayments = async () => {
  try {
    const response = await axios.get(
      `https://localhost:7203/api/Payment/GetAllPayments`
    );
    return response;
  } catch (error) {
    alert("Error when Fetching Payment");
    return error;
  }
};

// export const getReservationByReservationId = async (reservationId) => {
//   try {
//     const response = await axios.get(
//       `https://localhost:7203/api/MovieReservation/GetReservationById?id=${reservationId}`
//     );
//     return response;
//   } catch (error) {
//     alert("Error when Fetching Reservation");
//     return error;
//   }
// };
