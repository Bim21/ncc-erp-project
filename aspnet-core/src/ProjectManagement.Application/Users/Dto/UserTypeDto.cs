using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.Users.Dto
{
    public class UserTypeDto
    {
        public long Id { get; set; }

        public UserType? UserType { get; set; }
    }
}
