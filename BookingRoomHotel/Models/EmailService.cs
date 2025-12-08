using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using BookingRoomHotel.Models.ModelsInterface;

namespace BookingRoomHotel.Models
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendRegisterMail(string recip, string name, string id, string MatKhau)
        {
            string NoiDung = string.Format("<!DOCLoai html>\r\n<html lang=\"en\">\r\n<head>\r\n    " +
                    "<meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" NoiDung=\"width=device-width, initial-scale=1.0\">\r\n    " +
                    "<TieuDe>Registor Successful! Booking Phong Hotel, KIOT!</TieuDe>\r\n</head>\r\n<body>\r\n    <h2>Registor Successful!!</h2>\r\n    " +
                    "<p>Hi <strong>{0},</strong></p>\r\n    " +
                    "<p>Congratulations on your successful registration!</p>\r\n    " +
                    "<p>Your ID:<strong> {1}</strong></p>\r\n    " +
                    "<p>Your Password:<strong> {2}</strong></p>\r\n    " +
                    "<p>Thank you for using our service!</p>\r\n    <p>Best Regards,</p>\r\n    " +
                    "<p style=\"font-style: italic; font-weight: bold;\">KIOT Team</p>\r\n</body>\r\n</html>\r\n", name, id, MatKhau);
            try
            {
                checkEmailValid(recip);
                SendMail("Registor Successful! Booking Phong Hotel, KIOT!", recip, NoiDung);
            }
            catch (Exception ex)
            {
                throw new Exception("Email could not be sent. " + ex.Message);
            }
        }
        public void SendConfirmQ(string recip, string name, string TieuDe)
        {
            string NoiDung = string.Format("<!DOCLoai html>\r\n<html lang=\"en\">\r\n<head>\r\n    " +
                    "<meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" NoiDung=\"width=device-width, initial-scale=1.0\">\r\n    " +
                    "<TieuDe>Send Request Successful! Booking Phong Hotel, KIOT!</TieuDe>\r\n</head>\r\n<body>\r\n    <h2>Send Request Successful!!</h2>\r\n    " +
                    "<p>Hi <strong>{0},</strong></p>\r\n    " +
                    "<p>Your request: <span style=\"color:red; font-style: italic;\">{1}</span> is being processed. We will respond to you within 24 hours!</p>\r\n    " +
                    "<p>Thank you for using our service!</p>\r\n    <p>Best Regards,</p>\r\n    " +
                    "<p style=\"font-style: italic; font-weight: bold;\">KIOT Team</p>\r\n</body>\r\n</html>\r\n", name, TieuDe);
            try
            {
                checkEmailValid(recip);
                SendMail("Send Request Successful! Booking Phong Hotel, KIOT!", recip, NoiDung);
            }
            catch (Exception ex)
            {
                throw new Exception("Email could not be sent. " + ex.Message);
            }
        }
        
        public void SendPhanHoiQ(string recip, string name, string TieuDe, string NoiDungQ, string PhanHoi)
        {
            string NoiDung = string.Format("<!DOCLoai html>\r\n<html lang=\"en\">\r\n<head>\r\n    " +
                    "<meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" NoiDung=\"width=device-width, initial-scale=1.0\">\r\n    " +
                    "<TieuDe>PhanHoi your problem from Booking Phong Hotel, KIOT!</TieuDe>\r\n</head>\r\n<body>\r\n    <h2>PhanHoi for your problem,</h2>\r\n    " +
                    "<p>Hi <strong>{0},</strong></p>\r\n    " +
                    "<p>We have received your request. Your request: <span style=\"color:red; font-style: italic;\">{1}</span></p>\r\n    " +
                    "<p>NoiDung: <span style=\"color:red; font-style: italic;\">{2}</span> </p>\r\n    " +
                    "<p>PhanHoi: <span style=\"color:red; font-style: italic;\">{3}</span> </p>\r\n    " +
                    "<p>Thank you for using our service!</p>\r\n    <p>Best Regards,</p>\r\n    " +
                    "<p style=\"font-style: italic; font-weight: bold;\">KIOT Team</p>\r\n</body>\r\n</html>\r\n", name, TieuDe, NoiDungQ, PhanHoi);
            try
            {
                checkEmailValid(recip);
                SendMail("Send Request Successful! Booking Phong Hotel, KIOT!", recip, NoiDung);
            }
            catch (Exception ex)
            {
                throw new Exception("Email could not be sent. " + ex.Message);
            }
        }

        public void SendChangePasswordMail(string recip, string name, string MatKhau)
        {
            string NoiDung = string.Format("<!DOCLoai html>\r\n<html lang=\"en\">\r\n<head>\r\n    " +
                    "<meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" NoiDung=\"width=device-width, initial-scale=1.0\">\r\n    " +
                    "<TieuDe>Change Password Success! Booking Phong Hotel, KIOT!</TieuDe>\r\n</head>\r\n<body>\r\n    <h2>Change Password Successful!!</h2>\r\n    " +
                    "<p>Hi <strong>{0},</strong></p>\r\n    " +
                    "<p>Your New Password: <strong>{1}</strong></p>\r\n    " +
                    "<p>Thank you for using our service!</p>\r\n    <p>Best Regards,</p>\r\n    " +
                    "<p style=\"font-style: italic; font-weight: bold;\">KIOT Team</p>\r\n</body>\r\n</html>\r\n", name, MatKhau);
            try
            {
                SendMail("Change Password Success! Booking Phong Hotel, KIOT!", recip, NoiDung);
            }
            catch (Exception ex)
            {
                throw new Exception("Email could not be sent. " + ex.Message);
            }
        }

        public void SendForgotPasswordMail(string recip, string name, string MatKhau)
        {
            string NoiDung = string.Format("<!DOCLoai html>\r\n<html lang=\"en\">\r\n<head>\r\n    " +
                    "<meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" NoiDung=\"width=device-width, initial-scale=1.0\">\r\n    " +
                    "<TieuDe>Forgot Password! Booking Phong Hotel, KIOT!</TieuDe>\r\n</head>\r\n<body>\r\n    <h2>Get Your Password Successful!!</h2>\r\n    " +
                    "<p>Hi <strong>{0},</strong></p>\r\n    " +
                    "<p>Your Old Password:<strong> {1}</strong></p>\r\n    " +
                    "<p>Please change your password when you receive this email</p>\r\n    " +
                    "<p>Thank you for using our service!</p>\r\n    <p>Best Regards,</p>\r\n    " +
                    "<p style=\"font-style: italic; font-weight: bold;\">KIOT Team</p>\r\n</body>\r\n</html>\r\n", name, MatKhau);
            try
            {
                SendMail("Forgot Password! Booking Phong Hotel, KIOT!", recip, NoiDung);
            }
            catch (Exception ex)
            {
                throw new Exception("Email could not be sent. " + ex.Message);
            }
        }
        public async void SendMail(string TieuDe, string recip, string s)
        {
            try
            {
                string fromMail = _configuration["EmailSetting:EmailID"];
                string fromPassword = _configuration["EmailSetting:AppPassword"];
                MailMessage NoiDung = new MailMessage();
                NoiDung.From = new MailAddress(fromMail);
                NoiDung.Subject = TieuDe;
                NoiDung.To.Add(new MailAddress(recip));
                NoiDung.Body = s;
                NoiDung.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true,
                };

                await smtpClient.SendMailAsync(NoiDung);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void checkEmailValid(string email)
        {
            try
            {
                string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
                if (!Regex.IsMatch(email, pattern))
                {
                    throw new Exception("Email invalid!");
                }
                string[] domain = email.Split('@');
                if (domain.Length >= 2)
                {
                    IPHostEntry emailEntry = Dns.GetHostEntry(domain[domain.Length - 1]);
                    if (emailEntry == null || emailEntry.AddressList.Length == 0)
                    {
                        throw new Exception("Email invalid!");
                    }
                }
                else
                {
                    throw new Exception("Email invalid!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
