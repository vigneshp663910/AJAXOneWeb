using DataAccess;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Business
{
    public class BNotification
    {
        private IDataAccess provider;
        public BNotification()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public PNotifications GetNotificationById(int Id)
        {
            PNotifications notifyVo = new PNotifications();
            try
            {
                using (DataTable notifications = GetNotificationById1(Id))
                {
                    if (notifications != null && notifications.Rows.Count > 0)
                        notifyVo = ConvertToNotificationsVO(notifications.Rows[0]);
                }
                return notifyVo;
            }
            catch (LMSException lmsEx)
            {
                throw lmsEx;
            }
            catch (LMSFunctionalException lmsfExe)
            {
                throw lmsfExe;
            }
            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }
        public DataTable GetNotificationById1(int Id)
        {
            DateTime traceStartTime = DateTime.Now;
            DataTable NotificationDataTable = new DataTable();
            try
            {
                DbParameter Param = provider.CreateParameter("NotificationId", Id, DbType.Int16);
                DbParameter[] Params = new DbParameter[1] { Param };

                using (DataSet plantsDataSet = provider.Select("GetNotificationById", Params))
                {
                    if (plantsDataSet != null)
                        NotificationDataTable = plantsDataSet.Tables[0];
                }
                // This call is for track the status and loged into the trace logeer
                TraceLogger.Log(traceStartTime);
                return NotificationDataTable;
            }
            catch (SqlException sqlEx)
            {
                throw new LMSException(ErrorCode.SQLDBE, sqlEx);
            }
            
            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
        }
        private PNotifications ConvertToNotificationsVO(DataRow notification)
        {
            return new PNotifications()
            {
                AlertCode = Convert.ToString(notification["AlertCode"]),
                NotificationCode = Convert.ToString(notification["NotificationCode"]),
                NotificationId = Convert.ToInt16(notification["NotificationId"]),
                TemplateName = Convert.ToString(notification["TemplateName"]),
                TemplatePath = Convert.ToString(notification["TemplatePath"]),
                Subject = Convert.ToString(notification["Subject"])
            };
        }

    }
}