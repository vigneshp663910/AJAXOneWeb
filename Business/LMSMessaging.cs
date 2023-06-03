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
        
        /// Send an email to the user on successfull authorization of the account.
        /// </summary>
        /// <param name="user">AccountVO</param>
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
