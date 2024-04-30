using System.Net;
using System.Net.Mail;
using FluentValidation.Results;
using Parking.WebAPI.CoreHelper.CryptoHelper;
using Parking.WebAPI.CoreHelper.Email;
using Parking.WebAPI.CoreHelper.Extensions;
using Parking.WebAPI.HourSettingType.Validators;
using Parking.WebAPI.UserService.BusinessLayer.Interfaces;
using Parking.WebAPI.UserService.DataLayer;
using Parking.WebAPI.UserService.DataLayer.Interfaces;
using Parking.WebAPI.UserService.DTOs;
using Parking.WebAPI.UserService.Models;
using Parking.WebAPI.UserService.Validators;

namespace Parking.WebAPI.UserService.BusinessLayer
{
    public class AuthBL : IAuthBL
    {
        private readonly IAuthDAL _authDAL;
        private readonly IConfiguration _config;
        public AuthBL(IAuthDAL authDAL, IConfiguration config)
        {
            _authDAL = authDAL;
            _config = config;

        }

        public async Task<User> AuthenticateUserAsync(UserLoginRequest request)
        {
            var validator = new UserLoginRequestValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
                throw validationResult.Errors.CustomException(HttpStatusCode.BadRequest);

            var user = await _authDAL.GetUserAsync(request.Phone) ?? throw new List<ValidationFailure>
                {
                   new ValidationFailure { ErrorCode = nameof(Resources.UserService.Auth.US_AUTH_001), ErrorMessage = Resources.UserService.Auth.US_AUTH_001 }
                }.CustomException(HttpStatusCode.BadRequest);
            Random rand = new Random();
            int randomNumber = rand.Next(100000, 999999);

            await SendEmail(user.Email, "One Time Password", randomNumber.ToString());

            SaveOTPRequest saveRequest = new SaveOTPRequest();
            saveRequest.OTP = randomNumber.ToString();
            saveRequest.Phone = request.Phone;
            await SaveOTPAsync(saveRequest);

            return user;
        }


        public async Task<bool> ConfirmLoginAsync(UserConfirmLoginRequest request)
        {
            var validator = new UserConfirmLoginRequestValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
                throw validationResult.Errors.CustomException(HttpStatusCode.BadRequest);

            var result = await _authDAL.CheckOTPAsync(request.Phone, request.OTP);
            if (result == false)
            {
                throw new List<ValidationFailure>
                {
                    new ValidationFailure { ErrorCode = nameof(Resources.UserService.Common.US_COMMON_001), ErrorMessage = Resources.UserService.Common.US_COMMON_001 }
                }.CustomException(HttpStatusCode.BadRequest);
            }
            return result;
        }

        public async Task<Transaction> SaveOTPAsync(SaveOTPRequest OTP)
        {
            var validator = new SaveOTPRequestValidator();
            var validationResult = validator.Validate(OTP);
            if (!validationResult.IsValid)
                throw validationResult.Errors.CustomException(HttpStatusCode.BadRequest);


            return await _authDAL.SaveOTPAsync(OTP);
        }
        public async Task SendEmail(string toAddress, string subject, string body)
        {
            try
            {
                using (var smtpClient = new SmtpClient(_config["MailSettings:Host"], Convert.ToInt16(_config["MailSettings:Port"])))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(_config["MailSettings:UserName"], _config["MailSettings:Password"]);
                    smtpClient.EnableSsl = true;

                    var mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(_config["MailSettings:UserName"]);
                    mailMessage.To.Add(toAddress);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
        public async Task<User> RegisterUserAsync(UserRegistrationRequest user)
        {
            user.Validate<UserRegistrationRequest, UserRegistrationRequestValidator>();
            var isPhonenumberExists = await _authDAL.IsPhoneNumberExistsAsync(user.Phone);
            if (isPhonenumberExists)
            {
                throw new List<ValidationFailure>{
                    new ValidationFailure {
                        ErrorCode = nameof(Resources.UserService.User.US_User_001),
                        ErrorMessage = Resources.UserService.User.US_User_001
                    }
                }.CustomException(HttpStatusCode.BadRequest);
            }
            return await _authDAL.RegisterUserAsync(user);
        }        
    }
}

