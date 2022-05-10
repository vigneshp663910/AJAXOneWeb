using DataAccess;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
namespace Business
{
    public class BFeedback
    {
        private IDataAccess provider;
        public BFeedback()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public PFeedback GetFeedbackQuestionForTest()
        {
            PFeedback Feedback = null;
            try
            {
                using (DataSet ds = provider.Select("GetFeedbackQuestionForTest"))
                {
                    if (ds != null)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (Feedback == null)
                            {
                                Feedback = new PFeedback()
                                {
                                    FeedbackID = Convert.ToInt32(dr["FeedbackID"]),
                                    FeedbackName = Convert.ToString(dr["FeedbackName"]),
                                    StartDate = Convert.ToDateTime(dr["StartDate"]),
                                    EndDate = Convert.ToDateTime(dr["EndDate"]),
                                    FeedbackQuestion = new List<PFeedbackQuestion>()
                                };
                            }
                            Feedback.FeedbackQuestion.Add(
                                new PFeedbackQuestion()
                                {
                                    FeedbackQuestionID = Convert.ToInt32(dr["FeedbackQuestionID"]),
                                    FeedbackQuestion = Convert.ToString(dr["FeedbackQuestion"]),
                                    FeedbackQuestionType = Convert.ToInt32(dr["FeedbackQuestionType"]),
                                });
                        }
                    }
                }
                return Feedback;
                // This call is for track the status and loged into the trace logeer 
            }
            catch (SqlException sqlEx)
            {

            }
            catch (Exception ex)
            {

            }
            return Feedback;
        }
        public int insertFeedbackFromUser(PFeedback Feedback)
        {

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {


                    foreach (PFeedbackFromUser f in Feedback.FeedbackFromUser)
                    {
                        DbParameter UserIDP = provider.CreateParameter("UserID", Feedback.User.UserID, DbType.Int32);
                        DbParameter FeedbackIDP = provider.CreateParameter("FeedbackID", Feedback.FeedbackID, DbType.Int32);

                        DbParameter FeedbackQuestionIDP = provider.CreateParameter("FeedbackQuestionID", f.FeedbackQuestion.FeedbackQuestionID, DbType.Int32);
                        DbParameter Answerp = provider.CreateParameter("Answer", f.Answer, DbType.Int32);
                        DbParameter Remarkp = provider.CreateParameter("Remark", f.Remark, DbType.String);

                        DbParameter[] TicketTypeParams = new DbParameter[5] { UserIDP, FeedbackIDP, FeedbackQuestionIDP, Answerp, Remarkp };
                        provider.Insert("insertFeedbackFromUser", TicketTypeParams);
                    }
                    scope.Complete();
                }
                // This call is for track the status and loged into the trace logeer
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BFeedback", "insertFeedbackFromUser", sqlEx);
                return 0;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BFeedback", " insertFeedbackFromUser", ex);
                return 0;
            }
            return 1;
        }

        public Boolean CheckPendingFeedback(int UserID)
        {
           
            Boolean success = false;
            try
            {
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[1] { UserIDP };
                using (DataSet ds = provider.Select("CheckPendingFeedback", Params))
                {
                    if (ds != null)
                    {
                        success = Convert.ToBoolean(ds.Tables[0].Rows[0][0]);
                    }
                }
               
            }
            catch (SqlException sqlEx)
            {

            }
            catch (Exception ex)
            {

            }
            return success;
        }
        public long coTg_Insert_AppsFeedBack(PComment Comment)
        {
            long success = 0;
            try
            {
                DbParameter ModuleNo = provider.CreateParameter("ModuleNo", Comment.ModuleNo, DbType.Int64);
                DbParameter UserID = provider.CreateParameter("UserID", Comment.UserID, DbType.Int64);
                DbParameter Comments = provider.CreateParameter("Comments", Comment.Comments, DbType.String);
                DbParameter Stars = provider.CreateParameter("Ratings", Comment.Ratings, DbType.Int64);
                DbParameter[] Params = new DbParameter[4] { ModuleNo, UserID, Comments, Stars };


                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("coTg_Insert_AppsFeedBack", Params);
                    scope.Complete();
                }

            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BFeedback", "coTg_Insert_AppsFeedBack", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BHome", "coTg_Insert_AppsFeedBack", ex);
                throw ex;
            }
            return success;
        }
    }
}
