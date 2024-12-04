import axios from "axios";
import { jwtDecode } from "jwt-decode";
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";

export const isAuthenticated = () => {
  return sessionStorage.getItem("token");
};

export const isAdminAuthenticated = () => {
  const token = sessionStorage.getItem("token");
  const decode = jwtDecode(token);
  const role =
    decode["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
  if (role == "Admin") {
    return true;
  }
};

export const registration = async (username, email, password, role) => {
  try {
    const response = await axios.post(
      "https://localhost:7203/api/User/UserRegistration",
      {
        userName: username,
        userEmail: email,
        password: password,
        role: role,
      }
    );
    return response;
  } catch (error) {
    console.log(error.response);
    if (error.response.data.errorMessage)
      toast.error(error.response.data.errorMessage);
    if (error.response.data.errors.Password) {
      toast.error("Password: " + error.response.data.errors.Password[0]);
    }
    if (error.response.data.errors.UserName)
      toast.error("Username: " + error.response.data.errors.UserName[0]);
    else if (error.response.data.errors.UserEmail)
      toast.error("Email: " + error.response.data.errors.UserEmail[0]);
  }
};

export const login = async (username, password) => {
  try {
    const response = await axios.post(
      "https://localhost:7203/api/User/UserLogin",
      {
        UserEmail: username,
        password: password,
      }
    );
    return response;
  } catch (err) {
    return err;
  }
};

export const getUser = async (email) => {
  try {
    const response = await axios.get(
      `https://localhost:7203/api/User/GetUser?email=${email}`
    );
    return response;
  } catch (err) {
    return err;
  }
};

export const getCustomer = async (id) => {
  try {
    const response = await axios.get(
      `https://localhost:7203/api/Customer/GetCustomer?id=${id}`
    );
    return response;
  } catch (err) {
    return err;
  }
};

export const profileUpdate = async (fullName, phoneNumber, Address, userId) => {
  try {
    const response = await axios.patch(
      `https://localhost:7203/api/Customer/UpdateCustomerProfile`,
      {
        fullName,
        phoneNumber,
        Address,
        userId,
      }
    );
    return response;
  } catch (error) {
    console.log(error);
    toast.error("Error when Updating Profile");
    return error;
  }
};

export const MakeCommentForMovie = async (
  comment,
  rating,
  movieId,
  customerId
) => {
  try {
    const response = await axios.post(
      `https://localhost:7203/api/Customer/MakeComment`,
      {
        comment,
        rating,
        movieId,
        customerId,
      }
    );
    return response;
  } catch (error) {
    console.log(error);
    toast.error("Error when Make Comment");
    return error;
  }
};
