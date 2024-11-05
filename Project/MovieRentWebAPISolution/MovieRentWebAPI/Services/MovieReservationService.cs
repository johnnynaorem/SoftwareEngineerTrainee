using MovieRentWebAPI.EmailInterface;
using MovieRentWebAPI.EmailModels;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Services
{
    public class MovieReservationService : IReservationService
    {
        private readonly IRepository<int, Movie> _movieRepo;
        private readonly IRepository<int, Customer> _customerRepo;
        private readonly IRepository<int, Reservation> _reservationRepo;
        private readonly ILogger<MovieReservationService> _logger;
        private readonly IEmailSender _emailSender;

        public MovieReservationService(IRepository<int, Reservation> reservationRepository, IRepository<int, Customer> customerRepo, IRepository<int, Movie> movieRepo, ILogger<MovieReservationService> logger, IEmailSender emailSender)
        {
            _movieRepo = movieRepo;
            _customerRepo = customerRepo;
            _reservationRepo = reservationRepository;
            _logger = logger;
            _emailSender = emailSender;

        }

        private void SendMail(string title, string body)
        {
            var rng = new Random();
            var message = new Message(new string[] {
                        "johnnynaorem7@gmail.com" },
                    title,
                    body);
            _emailSender.SendEmail(message);
        }
        public async Task<int> ReserverMovie(ReserveMovieDTO reserveMovie)
        {
            var reservation = new Reservation
            {
                CustomerId = reserveMovie.CustomerId,
                MovieId = reserveMovie.MovieId,
                ReservationDate = reserveMovie.ReservationDate
            };

            var addReservation = await _reservationRepo.Add(reservation);
            if (addReservation != null) 
            {

                var customer = await _customerRepo.Get(addReservation.CustomerId);

                var movie = await _movieRepo.Get(addReservation.MovieId);

                string emailBody = $"Dear {customer.FullName},\n\n" +
                                    "Thank you for your reservation with the Video Disc Rental App!\n\n" +
                                    $"Your reservation for the movie **{movie.Title}** has been successfully created on {DateTime.Now}.\n\n" +
                                    "Here are the details of your reservation:\n\n" +
                                    $"**Reservation ID:** {reservation.ReservationId}\n" +
                                    $"**Movie Title:** {movie.Title}\n" +
                                    $"**Genre:** {movie.Genre}\n" +
                                    $"**Reservation Date:** {reservation.ReservationDate}\n" +
                                    $"**Status:** {reservation.Status}\n\n" +
                                    "You can pick up your reserved movie at your convenience. If you have any questions or need to modify your reservation, feel free to contact our support team.\n\n" +
                                    "Happy watching!\n\n" +
                                    "Best regards,\n" +
                                    "The Video Disc Rental App Team";
                SendMail("Your Reservation Has Been Created", emailBody);
            }
            return addReservation.ReservationId;
        }

        public async Task<ReservedStatusUpdateResponseDTO> UpdateMovieReservationStatus(ReservedMovieStatusUpdateRequestDTO entity)
        {
            var reservation = new Reservation
            {
                Status = entity.Status
            };

            var movieReservationUpdateStatus = await _reservationRepo.Update(reservation, entity.ReservationId);

            if (movieReservationUpdateStatus != null)
            {

                var customer = await _customerRepo.Get(movieReservationUpdateStatus.CustomerId);

                var movie = await _movieRepo.Get(movieReservationUpdateStatus.MovieId);

                string mailBody = string.Empty;

                switch (movieReservationUpdateStatus.Status)
                {
                    case ReservationStatus.Active:
                        mailBody = $"Dear {customer.FullName},\n\n" +
                            "We are thrilled to announce that your movie reservation is now officially active.\n\n" +
                            $"**Reservation Status:** Active\n" +
                            $"**Details:** Your reservation is valid and ready for pick-up.\n" +
                            $"**Movie Title:** \"{movie.Title}\"\n" +
                            $"**Reservation ID:** {movieReservationUpdateStatus.ReservationId}\n" +
                            $"**Active Until:** {movieReservationUpdateStatus.ReservationDate.AddDays(7).ToString("MMMM dd, yyyy")}\n\n" +
                            "Your movie awaits you! Feel free to pick it up at your convenience.\n" +
                            "Should you have any questions or require assistance, our support team is always here to help.\n\n" +
                            "Thank you for choosing the Video Disc Rental App.\n\n" +
                            "Warm regards,\n" +
                            "The Video Disc Rental App Team";
                        SendMail("Update: Your Reservation Status", mailBody);
                        break;

                    //case ReservationStatus.Pending:
                    //    mailBody = $"Dear {customer.FullName},\n\n" +
                    //        "We would like to keep you informed that your movie reservation is currently in a pending state.\n\n" +
                    //        $"**Reservation Status:** Pending\n" +
                    //        $"**Details:** Awaiting confirmation\n" +
                    //        $"**Movie Title:** \"{movie.Title}\"\n" +
                    //        $"**Reservation ID:** {reservation.ReservationId}\n\n" +
                    //        "We appreciate your patience during this time and will update you as soon as your reservation is confirmed.\n" +
                    //        "If you have any inquiries, please do not hesitate to reach out to our support team.\n\n" +
                    //        "Best wishes,\n" +
                    //        "The Video Disc Rental App Team";
                    //    SendMail("Update: Your Reservation Status", mailBody);
                    //    break;

                    case ReservationStatus.Completed:
                        mailBody = $"Dear {customer.FullName},\n\n" +
                            "We are delighted to inform you that your movie reservation has been successfully completed.\n\n" +
                            $"**Reservation Status:** Completed\n" +
                            $"**Details:** The movie has been picked up\n" +
                            $"**Movie Title:** \"{movie.Title}\"\n" +
                            $"**Reservation ID:** {movieReservationUpdateStatus.ReservationId}\n\n" +
                            "We hope you enjoy your cinematic experience! Should you have any feedback or questions, feel free to contact us.\n\n" +
                            "Thank you for being a valued member of the Video Disc Rental App community.\n\n" +
                            "Sincerely,\n" +
                            "The Video Disc Rental App Team";
                        SendMail("Update: Your Reservation Status", mailBody);
                        break;

                    case ReservationStatus.Cancelled:
                        mailBody = $"Dear {customer.FullName},\n\n" +
                            "We regret to inform you that your movie reservation has been cancelled.\n\n" +
                            $"**Reservation Status:** Cancelled\n" +
                            $"**Details:** Cancelled by customer or system\n" +
                            $"**Movie Title:** \"{movie.Title}\"\n" +
                            $"**Reservation ID:** {movieReservationUpdateStatus.ReservationId}\n\n" +
                            "If you have any questions or need further assistance, please feel free to reach out to our support team.\n\n" +
                            "Thank you for your understanding.\n\n" +
                            "Kind regards,\n" +
                            "The Video Disc Rental App Team";
                        SendMail("Update: Your Reservation Status", mailBody);
                        break;

                    case ReservationStatus.Expired:
                        mailBody = $"Dear {customer.FullName},\n\n" +
                            "We would like to inform you that your movie reservation has unfortunately expired.\n\n" +
                            $"**Reservation Status:** Expired\n" +
                            $"**Details:** Reservation period lapsed without action\n" +
                            $"**Movie Title:** \"{movie.Title}\"\n" +
                            $"**Reservation ID:** {movieReservationUpdateStatus.ReservationId}\n\n" +
                            "Should you wish to rent this movie, please feel free to create a new reservation. Our support team is available for any questions you may have.\n\n" +
                            "Thank you for being part of the Video Disc Rental App family.\n\n" +
                            "Best wishes,\n" +
                            "The Video Disc Rental App Team";
                        SendMail("Update: Your Reservation Status", mailBody);
                        break;

                    case ReservationStatus.Not_Available:
                        mailBody = $"Dear {customer.FullName},\n\n" +
                            "We regret to inform you that the movie you reserved is no longer available.\n\n" +
                            $"**Reservation Status:** Not Available\n" +
                            $"**Details:** The reserved movie is no longer available\n" +
                            $"**Movie Title:** \"{movie.Title}\"\n" +
                            $"**Reservation ID:** {movieReservationUpdateStatus.ReservationId}\n\n" +
                            "We apologize for any inconvenience this may cause. Please feel free to explore our extensive catalog for other available films.\n" +
                            "Our support team is here to assist you with any questions you might have.\n\n" +
                            "Thank you for your understanding and continued support.\n\n" +
                            "Warm regards,\n" +
                            "The Video Disc Rental App Team";
                        SendMail("Update: Your Reservation Status", mailBody);
                        break;

                }
            }
            return new ReservedStatusUpdateResponseDTO { Status = movieReservationUpdateStatus.Status.ToString(), ReservationId = movieReservationUpdateStatus.ReservationId };
        }

        public async Task<IEnumerable<Reservation>> GetAll()
        {
            var reservation = await _reservationRepo.GetAll();
            return reservation;
        }
    }
}
