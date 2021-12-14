using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PFeedbackQuestion
    {
        public int FeedbackQuestionID { get; set; }
        public string FeedbackQuestion { get; set; }
        public int FeedbackQuestionType { get; set; } 
    }

    [Serializable]
    public class PFeedback
    {
        public int FeedbackID { get; set; }
        public string FeedbackName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public  PUser User { get; set; }
        public List<PFeedbackQuestion> FeedbackQuestion { get; set; }
        public List<PFeedbackFromUser> FeedbackFromUser { get; set; }
    }

     [Serializable]
    
    public class PFeedbackFromUser
    {
        public int ID { get; set; }
        public PFeedbackQuestion FeedbackQuestion { get; set; }
        public int Answer { get; set; } 
        public string Remark { get; set; } 
    }
}
