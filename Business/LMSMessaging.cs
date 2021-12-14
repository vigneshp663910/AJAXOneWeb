using Properties; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Business
{
   public class LMSMessaging
    {
       
        //public void SendDeclineMail(PAccount account)
        //{
        //    try
        //    {
        //        NotificationsVO notify = new Notification().GetNotificationById((int)MessageModuleType.DiclineAccount);
        //        string MessageBody = lmsHelper.GetMessageBody(MessageModuleType.DiclineAccount, notify.TemplatePath, notify.TemplateName);
        //        MessageBody = MessageBody.Replace("@@UserName", account.UserName);
        //        MessageBody = MessageBody.Replace("@@DeclineReason", account.DeclineReason);
        //        UserVO user = new User().GetUserDetails(account.CreatedBy);
        //        ContactDetailVO createdUserContact = new User().GetContactDetailByUser(account.CreatedBy);
        //        MessageBody = MessageBody.Replace("@@Addresse", user.ContactName);
        //        lmsMessageStatus status = MessagingService.SendEmail(new lmsEmailMessage()
        //        {
        //            Message = MessageBody,
        //            ToAddress = new string[] { createdUserContact.EmailID },
        //            Subject = notify.Subject,
        //            ModuleName = MessagingService.Application
        //        });
        //        if (!status.IsProcessed)
        //            throw new lmsException(ErrorCode.MSGERROR, new Exception(status.Errormessage));
        //    }
        //    catch (lmsException lms)
        //    {
        //        throw lms;
        //    }
        //}
        public void SendAccountCreatedMail(PAccount account)
        {
            try
            {
                PUser user = new BUser().GetUserDetails(account.CreatedBy);
                if (user.UserTypeID != (short)UserTypes.Admin)
                {
                    PNotifications notify = new BNotification().GetNotificationById((int)MessageModuleType.CreateUser);
                    string MessageBody = LMSHelper.GetMessageBody(MessageModuleType.CreateUser, notify.TemplatePath, notify.TemplateName);
                    MessageBody = MessageBody.Replace("@@UserName", account.UserName);
                    MessageBody = MessageBody.Replace("@@Date", account.CreatedOn.ToShortDateString());
                    MessageBody = MessageBody.Replace("@@User", user.ContactName);
                    //LMSMessageStatus status = MessagingService.SendEmail(new lmsEmailMessage()
                    //{
                    //    Message = MessageBody,
                    //    ToAddress = new string[] { account.EmailID },
                    //    Subject = notify.Subject,
                    //    ModuleName = MessagingService.Application
                    //});
                    //if (!status.IsProcessed)
                    //    throw new LMSException(ErrorCode.MSGERROR, new Exception(status.Errormessage));
                }
            }
            catch (LMSException lms)
            {
                throw lms;
            }
        }

    
        /// Send an email to the user on successfull authorization of the account.
        /// </summary>
        /// <param name="user">AccountVO</param>
        public void SendAuthorizationMail(PAccount account)
        {
            try
            {
                PNotifications notify = new BNotification().GetNotificationById((int)MessageModuleType.AuthorizeAccount);
                string MessageBody = LMSHelper.GetMessageBody(MessageModuleType.AuthorizeAccount, notify.TemplatePath, notify.TemplateName);
                MessageBody = MessageBody.Replace("@@Addresse", account.StaffName);
                MessageBody = MessageBody.Replace("@@UserName", account.UserName);
                MessageBody = MessageBody.Replace("@@Password", LMSHelper.DecodeString(account.LoginPassword));
               // MessageBody = MessageBody.Replace("@@UserType", new BUserType().GetUserTypeByCode(account.UserTypeID).CodeDescription);
                //lmsMessageStatus status = MessagingService.SendEmail(new lmsEmailMessage()
                //{
                //    Message = MessageBody,
                //    ToAddress = new string[] { account.EmailID },
                //    Subject = notify.Subject,
                //    ModuleName = MessagingService.Application
                //});
                //if (!status.IsProcessed)
                //    throw new lmsException(ErrorCode.MSGERROR, new Exception(status.Errormessage));
            }
            catch (LMSException lms)
            {
                throw lms;
            }
        }
        public void SendForgotPasswordMail(PUser user, string emailId)
        {
            try
            {
                PNotifications notify = new BNotification().GetNotificationById((int)MessageModuleType.ForgotPassword);
                string MessageBody = LMSHelper.GetMessageBody(MessageModuleType.ForgotPassword, notify.TemplatePath, notify.TemplateName);
                MessageBody = MessageBody.Replace("@@Addresse", user.ContactName);
                MessageBody = MessageBody.Replace("@@UserName", user.UserName);
                MessageBody = MessageBody.Replace("@@Password", LMSHelper.DecodeString(user.PassWord));
                //lmsMessageStatus status = MessagingService.SendEmail(new lmsEmailMessage()
                //{
                //    Message = MessageBody,
                //    ToAddress = new string[] { emailId },
                //    Subject = notify.Subject,
                //    ModuleName = MessagingService.Application
                //});
                //bool status = new EmailManager().SendEMail(
                //       emailId,
                //       string.Empty, "lms : Forgot password", MessageBody, true);
                //if (!status)
                //    throw new lmsException(ErrorCode.MSGERROR, new Exception());
            }
            catch (LMSException lms)
            {
                throw lms;
            }
        }
        public void SendUnLockUserMail(PUser user, string emailId)
        {
            try
            {
                PNotifications notify = new BNotification().GetNotificationById((int)MessageModuleType.UnLockUser);
                string MessageBody = LMSHelper.GetMessageBody(MessageModuleType.UnLockUser, notify.TemplatePath, notify.TemplateName);
                MessageBody = MessageBody.Replace("@@UserName", user.UserName);
                MessageBody = MessageBody.Replace("@@Password", LMSHelper.DecodeString(user.PassWord));
                //lmsMessageStatus status = MessagingService.SendEmail(new lmsEmailMessage()
                //{
                //    Message = MessageBody,
                //    ToAddress = new string[] { emailId },
                //    Subject = notify.Subject,
                //    ModuleName = MessagingService.Application
                //});
                //if (!status.IsProcessed)
                //    throw new lmsException(ErrorCode.MSGERROR, new Exception(status.Errormessage));
            }
            catch (LMSException lms)
            {
                throw lms;
            }
        }

        public void SendEnableUserMail(PUser user, string emailId)
        {
            try
            {
                PNotifications notify = new BNotification().GetNotificationById((int)MessageModuleType.EnableUser);
                string MessageBody = LMSHelper.GetMessageBody(MessageModuleType.EnableUser, notify.TemplatePath, notify.TemplateName);
                MessageBody = MessageBody.Replace("@@UserName", user.UserName);
                MessageBody = MessageBody.Replace("@@SystemAdministrator", "Administrator");
                //lmsMessageStatus status = MessagingService.SendEmail(new lmsEmailMessage()
                //{
                //    Message = MessageBody,
                //    ToAddress = new string[] { emailId },
                //    Subject = notify.Subject,
                //    ModuleName = MessagingService.Application
                //});
                //if (!status.IsProcessed)
                //    throw new lmsException(ErrorCode.MSGERROR, new Exception(status.Errormessage));
            }
            catch (LMSException lms)
            {
                throw lms;
            }
        }
        public void SendDisableUserMail(PUser user, string emailId)
        {
            try
            {
                PNotifications notify = new BNotification().GetNotificationById((int)MessageModuleType.DisableUser);
                string MessageBody = LMSHelper.GetMessageBody(MessageModuleType.DisableUser, notify.TemplatePath, notify.TemplateName);
                MessageBody = MessageBody.Replace("@@UserName", user.UserName);
                MessageBody = MessageBody.Replace("@@DisableReason", user.EnableDisableReason);
                MessageBody = MessageBody.Replace("@@SystemAdministrator", "Administrator");
                //lmsMessageStatus status = MessagingService.SendEmail(new lmsEmailMessage()
                //{
                //    Message = MessageBody,
                //    ToAddress = new string[] { emailId },
                //    Subject = notify.Subject,
                //    ModuleName = MessagingService.Application
                //});
                //if (!status.IsProcessed)
                //    throw new lmsException(ErrorCode.MSGERROR, new Exception(status.Errormessage));
            }
            catch (LMSException lms)
            {
                throw lms;
            }
        }
        
    }
}
