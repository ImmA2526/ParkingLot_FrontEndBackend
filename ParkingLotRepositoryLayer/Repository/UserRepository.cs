using Microsoft.EntityFrameworkCore;
using ParkingLotModelLayer;
using ParkingLotRepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ParkingLotRepositoryLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ParkingContext parkingContext;
        public UserRepository(ParkingContext parkingContext)
        {
            this.parkingContext = parkingContext;
        }

        public UserModel UserRegistration(UserModel model)
        {
            //try
            //{
            parkingContext.UserTable.Add(model);
            var result = parkingContext.SaveChanges();
            if (result > 0)
            {
                return model;
            }
            return null;
            //}
            //catch (Exception e)
            //{
            //    return this.(new { Status = false, Message = e.Message });
            //}
        }


        /// <summary>
        /// Users login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns></returns>

        public LoginModel UserLogin(LoginModel login)
        {
            var result = parkingContext.UserTable.Where<UserModel>(x => x.Email == login.Email && x.Password == login.Password).FirstOrDefault();
            if (result != null)
            {
                return login;
            }
            return null;
        }

        /// <summary>
        /// Forgot password.
        /// </summary>
        /// <param name="forgot">The forgot.</param>
        /// <returns></returns>

        public string ForgotUserPassword(ForgotModel forgot)
        {
            string subject = "Your Password is";
            string body;
            var result = parkingContext.UserTable.FirstOrDefault(e => e.Email == forgot.Email);
            if (result != null)
            {
                body = result.Password;
                //return true;        
            }
            else
            {
                return "Not Found";
            }

            using (MailMessage mailMessage = new MailMessage("imraninfo.1996@gmail.com", forgot.Email))
            {
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("imraninfo.1996@gmail.com", "9175833272*");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mailMessage);
                return "Success";
            }
        }

        /// <summary>
        /// Reset password.
        /// </summary>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        
        public string ResetUserPassword(string oldPassword, string newPassword)
        {
            var resetPwd = parkingContext.UserTable.FirstOrDefault(password => password.Password == oldPassword);
            if (resetPwd != null)
            {
                resetPwd.Password = newPassword;
                parkingContext.Entry(resetPwd).State = EntityState.Modified;
                parkingContext.SaveChanges();
                return "SUCCESS";
            }
            else
            {
                return "NOT_FOUND";
            }
        }

    }
}
