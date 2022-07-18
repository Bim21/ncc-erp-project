using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.APIs.Projects.Dto
{
    public class GetTempOfUserInProjectDto
    {
        public string EmailAddress { get; set; }
        public List<TimeJoinOut> ListTimeJoinOut { get; set; }
    }

    public class TimeJoinOut
    {
        public DateTime StartTime { get; set; }
        public bool IsJoin { get; set; }
    }


    public class GetUserTempInProject
    {
        public string EmailAddress { get; set; }
    }

    public class ResultGetUserTempInProject
    {
        public string Code { get; set; }
        public List<GetUserTempInProject> ListUserTempInProject { get; set; }
    }
}
