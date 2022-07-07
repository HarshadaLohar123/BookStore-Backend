using DatabaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IFeedbackRL
    {
        public string AddFeedback(FeedbackModel feedbackModel, int userId);
        public List<ViewFeedbackModel> GetFeedback(int BookId);


    }
}
