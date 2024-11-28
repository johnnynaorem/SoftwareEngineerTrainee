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

export const makePayment = async (rentalId, customerId, paymentType) => {
  try {
    const response = await axios.post(
      `https://localhost:7203/api/Payment/MakePayment`,
      {
        rentalId,
        customerId,
        paymentType,
      }
    );
    return response;
  } catch (error) {
    alert("Error when Payment");
    return error;
  }
};
