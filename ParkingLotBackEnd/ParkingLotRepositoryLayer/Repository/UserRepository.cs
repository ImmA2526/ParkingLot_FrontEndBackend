using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ParkingLotModelLayer;
using ParkingLotRepositoryLayer.IRepository;
using PMSMQ;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace ParkingLotRepositoryLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        public IConfiguration Configuration { get; }
        private readonly ParkingContext parkingContext;
        public UserRepository(ParkingContext parkingContext)
        {
            this.parkingContext = parkingContext;
        }

        /// <summary>
        /// PAssword Encryption
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>

        public string PasswordEncryption(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = Encoding.UTF8.GetBytes(password);
                string encodedPassword = Convert.ToBase64String(encData_byte);
                return encodedPassword;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        /// <summary>
        /// Decription of Password
        /// </summary>
        /// <param name="encryptpwd"></param>
        /// <returns></returns>

        public static string Decryptdata(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }

        /// <summary>
        /// User Registration
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public UserModel UserRegistration(UserModel model)
        {
            try
            {
                string password = model.Password;
                string encodePass = PasswordEncryption(password);
                model.Password = encodePass;
                parkingContext.UserTable.Add(model);
                var result = parkingContext.SaveChanges();
                if (result > 0)
                {
                    return model;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }

        /// <summary>
        /// Users login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns></returns>

        public UserModel UserLogin(LoginModel login)
        {
            try
            {
                var result = parkingContext.UserTable.Where<UserModel>(x => x.Email == login.Email).FirstOrDefault();
                if (result != null)
                {
                    string decryptPass = Decryptdata(result.Password);
                    if (login.Password == decryptPass)
                    {
                        return result;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("Error while Decrypting" + e.Message);
            }
        }

        /// <summary>
        /// Forgot password.
        /// </summary>
        /// <param name="forgot">The forgot.</param>
        /// <returns></returns>

        public string ForgotUserPassword(string Email)
        {
            try
            {
                var forgotPassword = "Reset Password link for Parking Model:- This is the Link of Your Forgot Password ";
                string subject = "Your Password is";
                string body;
                var result = parkingContext.UserTable.FirstOrDefault(e => e.Email == Email);
                if (result != null)
                {

                    //string decode = Decryptdata(result.Password);
                     
                    Sender send = new Sender();
                    send.MailSender(forgotPassword);

                    Recever recev = new Recever();
                    var forgotLink = recev.MailReciver();
                    body = forgotLink;
                }
                else
                {
                    return "Not Found";
                }

                using (MailMessage mailMessage = new MailMessage("imraninfo.1996@gmail.com", Email))
                {
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("imraninfo.1996@gmail.com", "9175833272");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mailMessage);
                    return "Success";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Reset password.
        /// </summary>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>

        public string ResetUserPassword(LoginModel reset)
        {
            try
            {
                var resetPwd = parkingContext.UserTable.FirstOrDefault(password => password.Email == reset.Email);
                string newPassword = reset.Password;
                string encodePass = PasswordEncryption(newPassword);
                if (resetPwd != null)
                {
                    resetPwd.Password = encodePass;
                    parkingContext.Entry(resetPwd).State = EntityState.Modified;
                    parkingContext.SaveChanges();
                    return "SUCCESS";
                }
                else
                {
                    return "NOT_FOUND";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Types Of Data Being Added //
        /// <summary>
        /// Vehicals the types.
        /// </summary>
        /// <param name="vehical">The vehical.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Error While Adding" + e.Message</exception>

        public VehicalTypeModel VehicalTypes(VehicalTypeModel vehical)
        {
            try
            {
                parkingContext.VehicalTypeTable.Add(vehical);
                var result = parkingContext.SaveChanges();
                if (result > 0)
                {
                    return vehical;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("Error While Adding" + e.Message);
            }
        }

        /// <summary>
        /// Drivers the types.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Error While Adding Driver Data" + e.Message</exception>

        public DriverTypeModel DriverTypes(DriverTypeModel driver)
        {
            try
            {
                parkingContext.DriverTypeTable.Add(driver);
                var result = parkingContext.SaveChanges();
                if (result > 0)
                {
                    return driver;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("Error While Adding Driver Data" + e.Message);
            }
        }

        /// <summary>
        /// Parking types.
        /// </summary>
        /// <param name="parking">The parking.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Error While Adding Parking Data" + e.Message</exception>
        public ParkingTypeModel ParkingTypes(ParkingTypeModel parking)
        {
            try
            {
                parkingContext.ParkingTypeTable.Add(parking);
                var result = parkingContext.SaveChanges();
                if (result > 0)
                {
                    return parking;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("Error While Adding Parking Data" + e.Message);
            }
        }
    }
}
