import axios from './AxiosInterceptor'

export function GetBuses(from, to, dateTime) {
    return axios.get('https://localhost:7176/api/Booking/SeeAllBuses', {
        params: {
            from,
            to,
            dateTime,
            pagenum: 1,
            pagesize: 10
        }
    });

}

export function GetSeats(id) {
    return axios.get(`https://localhost:7176/api/Booking/busAndSeatsDetails`, {
        params: {
            busId: id
        }
    });
}