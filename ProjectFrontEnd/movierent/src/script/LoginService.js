import axios from "axios"

export const login = async (username, password) => {
    try{
        const response = await axios.post('https://localhost:7203/api/User/UserLogin',
            {
                "UserEmail": username,
                "password": password
            });
        return response;
        }
        catch(err){
            return err;
        }
}