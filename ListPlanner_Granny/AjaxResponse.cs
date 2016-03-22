using System.Collections.Generic;

namespace ListPlanner_Granny
{
    public class AjaxResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
        public AjaxResponse()
        {
            Errors = new List<string>();
        }
    }
}