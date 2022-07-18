using ProjectManagement.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using static ProjectManagement.Constants.Enum.ProjectEnum;

namespace ProjectManagement.APIs.Public.Dto
{
    public class UserInfo
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string BranchName { get; set; }
        public string BranchDisplayName { get; set; }
        public string AvatarPath{ get; set; }
        
        public UserType UserType { get; set; }

        public string FullAvatarPath => FileUtils.FullFilePath(AvatarPath);
        public string UserTypeName => CommonUtil.UserTypeName(UserType);
    }
}
