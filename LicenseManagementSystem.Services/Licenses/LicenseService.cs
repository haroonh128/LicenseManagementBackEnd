using AutoMapper;
using LicenseManagementSystem.Common;
using LicenseManagementSystem.DA;
using LicenseManagementSystem.Models;
using LicenseManagementSystem.Models.Models;
using LicenseManagementSystem.Services.Email;


namespace LicenseManagementSystem.Services.Licenses
{
    public class LicenseService : ILicenseService
    {
        private readonly IMapper _mapper;
        private readonly DataBaseContext _context;
        private readonly IEmailService _emailService;

        ApiResponse response = new ApiResponse();

        public LicenseService(IMapper mapper, DataBaseContext context, IEmailService emailService)
        {
            _mapper = mapper;
            _context = context;
            _emailService = emailService;
        }

        public async Task<ApiResponse> CreateLicenseKey(LicenseModel request)
        {
            var response = new ApiResponse();

            var newLicense = _mapper.Map<License>(request);
            newLicense.Key = KeyGenerator.GenerateLicenseKey();
            newLicense.CreatedDate = DateTime.Now;
            newLicense.IsActive = request.IsActive;

            await _context.AddAsync(newLicense);
            _context.SaveChanges();
            var user = _context.Users.FirstOrDefault(x => x.Id == request.UserId);
            if (user == null)
            {
                response.Message = "User not found!";
                response.Status = 404;
                return response;
            }

            try
            {
                string subject = "Your License Key - License Management System";
                string body = $@"
            <h1>Dear {user.UserName},</h1>
            <p>Your license key is:</p>
            <p><strong>{newLicense.Key}</strong></p>
            <p><em>Kindly copy the license key and save it. You will need to activate your license.</em></p>
            <p>We hope your experience with our platform is enjoyable!</p>
            <p>Sincerely,</p>
            <p>The LMS Team</p>";

                await _emailService.SendEmailAsync(user.Email, subject, body);

                response.Message = "License created and email sent successfully!";
                response.Status = 404; // Not Found
            }
            catch (Exception ex)
            {
                response.Message = "License created successfully, but failed to send email.";
                response.Status = 200;
            }

            return response;
        }

        public List<LicenseModel> GetAllLicenses()
        {
            var licenses = _mapper.Map<List<LicenseModel>>(_context.licenses.ToList());
            return licenses;
        }

        public LicenseModel GetLicensesById(long id)
        {
            var licenses = _mapper.Map<LicenseModel>(_context.licenses.FirstOrDefault(x => x.Id == id));
            return licenses;
        }

        public async Task<string> ActiveteLicense(long userId, string key)
        {
            try
            {
                var license = _context.licenses.FirstOrDefault(x => x.UserId == userId && x.Key == key);

                if (license != null)
                {
                    license.IsActive = true;
                    _context.licenses.Update(license);
                    await _context.SaveChangesAsync();
                    return "License Activated!";
                }
                else
                {
                    return "License Activation failed!";
                }
            }

            catch (KeyNotFoundException)
            {
                return "License key is not valid!";
            }
            catch (Exception)
            {
                return "Failed to activate license";
            }

        }
    }
}
