using MovieRentWebAPI.EmailInterface;
using MovieRentWebAPI.EmailModels;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<int, Customer> _customerRepo;
        private readonly ILogger<CustomerService> _logger;
        private readonly IEmailSender _emailSender;

        public CustomerService(IRepository<int, Customer> customerRepository, ILogger<CustomerService> logger, IEmailSender emailSender)
        {
            _customerRepo = customerRepository;
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
        public async Task<int> CreateCustomer(CreateCustomerDTO entity)
        {
            var customer = new Customer
            {
                FullName = entity.FullName,
                Address = entity.Address,
                PhoneNumber = entity.PhoneNumber,
                UserId = entity.UserId,
            };

            var addCustomer = await _customerRepo.Add(customer);
            if (addCustomer != null)
            {
                string emailBody =
                                $"Dear {addCustomer.FullName},\n\n" +
                                "Thank you for registering with the Video Disc Rental App!\n\n" +
                                $"Your customer account has been successfully created at {DateTime.Now}.\n\n" +
                                "Here are your account details:\n\n" +
                                $"**Customer ID:** {addCustomer.CustomerId}\n" +
                                $"**Registered Contact Number:** {addCustomer.PhoneNumber}\n\n" +
                                "You can now start browsing our extensive catalog of movies, reserve discs, and manage your rentals. To log in to your account, please click the link below:\n\n" +
                                $"[Login to Your Account](https://localhost:7203/swagger/index.html)\n\n" +
                                "For your convenience, here are some tips to enhance your experience:\n" +
                                "- Explore different genres to find your favorites.\n" +
                                "- Use our wishlist feature to save movies for later.\n" +
                                "- Don’t hesitate to reach out if you need assistance with your account or rentals.\n\n" +
                                "Thank you for choosing us! If you have any questions, feel free to contact our support team.\n\n" +
                                "Best regards,\n" +
                                "The Video Disc Rental App Team";

                SendMail("Your Customer Account Has Been Created", emailBody);
            }
            return addCustomer.CustomerId;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomer()
        {
            var customers = await _customerRepo.GetAll();
            return customers;
        }

        public Task<Customer> GetCustomerById(int id)
        {
            var customer = _customerRepo.Get(id);
            return customer;
        }

        public async Task<int> UpdateCustomer(CreateCustomerDTO customer, int key)
        {
            var updateCustomer = new Customer
            {
                FullName = customer.FullName,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address
            };

            var updatedCustomer = await _customerRepo.Update(updateCustomer, key);
            if (updateCustomer != null)
            {
                string emailBody =
                                $"Dear {updatedCustomer.FullName},\n\n" +
                                "Your profile has been successfully updated in the Video Disc Rental App!\n\n" +
                                $"The changes were made on {DateTime.Now}.\n\n" +
                                "Here are your updated account details:\n\n" +
                                $"**Customer ID:** {updatedCustomer.CustomerId}\n" +
                                $"**Updated Contact Number:** {updatedCustomer.PhoneNumber}\n\n" +
                                "You can continue to browse our extensive catalog of movies, reserve discs, and manage your rentals. If you need to log in to your account again, please click the link below:\n\n" +
                                $"[Log in to Your Account](www.MovieRentalApp.com)\n\n" +
                                "To enhance your experience, here are some tips:\n" +
                                "- Explore different genres to discover new favorites.\n" +
                                "- Use our wishlist feature to save movies for later viewing.\n" +
                                "- Reach out to us if you need assistance with your account or rentals.\n\n" +
                                "Thank you for being a valued customer! If you have any questions or need further assistance, feel free to contact our support team.\n\n" +
                                "Best regards,\n" +
                                "The Video Disc Rental App Team";

                SendMail("Your Customer Account Profile Has Been Updated", emailBody);
            }
            return updatedCustomer.CustomerId;
        }
    }
}
