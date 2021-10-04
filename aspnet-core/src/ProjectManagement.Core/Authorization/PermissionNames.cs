using Abp.MultiTenancy;
using System.Collections.Generic;
using static ProjectManagement.Authorization.Roles.StaticRoleNames;

namespace ProjectManagement.Authorization
{
    public static class PermissionNames
    {     
        //Admin
        public const string Admin = "Admin";
            public const string Admin_CanViewMenu = "Admin.CanViewMenu";
            public const string Pages_Tenants = "Pages.Tenants";
            public const string Pages_Roles = "Pages.Roles";
            //User
            public const string Pages_Users = "Pages.Users";
                public const string Pages_Users_ViewAll = "Pages.Users.ViewAll";
                public const string Pages_Users_Create = "Pages.Users.Create";
                public const string Pages_Users_Update = "Pages.Users.Update";
                public const string Pages_Users_Delete = "Pages.Users.Delete";
                public const string Pages_Users_ImportUserFromFile = "Pages.Users.ImportUserFromFile";
                public const string Pages_Users_UpdateAvatar = "Pages.Users.UpdateAvatar";
                public const string Pages_Users_AutoUpdateUserFromHRM = "Pages.Users.AutoUpdateUserFromHRM";

            //Config
            public const string Admin_Configuration = "Admin.Configuration";
            public const string Admin_Configuration_ViewAll = "Admin.Configuration.ViewAll";
            public const string Admin_Configuration_Edit = "Admin.Configuration.Edit";

            //Client
            public const string Admin_Client = "Admin.Client";
            public const string Admin_Client_ViewAll = "Admin.Client.ViewAll";
            public const string Admin_Client_Create = "Admin.Client.Create";
            public const string Admin_Client_Edit = "Admin.Client.Edit";
            public const string Admin_Client_Delete = "Admin.Client.Delete";

            //Skill
            public const string Admin_Skill = "Admin.Skill";
            public const string Admin_Skill_ViewAll = "Admin.Skill.ViewAll";
            public const string Admin_Skill_Create = "Admin.Skill.Create";
            public const string Admin_Skill_Update = "Admin.Skill.Update";
            public const string Admin_Skill_Delete = "Admin.Skill.Delete";

        //Pm Manager
        public const string PmManager = "PmManager";
            // view menu
            public const string PmManager_CanViewMenu = "PmManager.CanViewMenu";
            public const string PmManager_CanViewMenu_ResourceManagement = "PmManager.CanViewMenu.ResourceManagement";
            public const string PmManager_CanViewMenu_Milestone = "PmManager.CanViewMenu.Milestone";
            public const string PmManager_CanViewMenu_WeeklyReport = "PmManager.CanViewMenu.WeeklyReport";
            public const string PmManager_CanViewMenu_ProjectChecklist = "PmManager.CanViewMenu.ProjectChecklist";
            public const string PmManager_CanViewMenu_Timesheet = "PmManager.CanViewMenu.Timesheet";

            public const string PmManager_CanViewMenu_PMCanCreate = "PmManager.CanViewMenu.PMCanCreateProjectUser";
            public const string PmManager_CanViewMenu_PMCanUpdate = "PmManager.CanViewMenu.PMCanUpdateProjectUser";
            public const string PmManager_CanViewMenu_PMCanDelete = "PmManager.CanViewMenu.PMCanDeleteProjectUser";

            //Project
            public const string PmManager_Project = "PmManager.Project";
            public const string PmManager_Project_ViewAll = "PmManager.Project.ViewAll";
            public const string PmManager_Project_ViewonlyMe = "PmManager.Project.ViewOnlyMe";
            public const string PmManager_Project_ViewDetail = "PmManager.Project.ViewDetail";
            public const string PmManager_Project_Create = "PmManager.Project.Create";
            public const string PmManager_Project_Update = "PmManager.Project.Update";
            public const string PmManager_Project_Delete = "PmManager.Project.Delete";

            //Project User Bill
            public const string PmManager_ProjectUserBill = "PmManager.ProjectUserBill";
            public const string PmManager_ProjectUserBill_GetAllPaging = "PmManager.ProjectUserBill.GetAllPaging";
            public const string PmManager_ProjectUserBill_GetAllByproject = "PmManager.ProjectUserBill.GetAllbyProject";
            public const string PmManager_ProjectUserBill_Create = "PmManager.ProjectUserBill.Create";
            public const string PmManager_ProjectUserBill_Update = "PmManager.ProjectUserBill.Update";
            public const string PmManager_ProjectUserBill_Delete = "PmManager.ProjectUserBill.Delete";

            //ProjectUser
            public const string PmManager_ProjectUser = "PmManager.ProjectUser";
            public const string PmManager_ProjectUser_ViewAllByProject = "PmManager.ProjectUser.ViewAllByProject";
            public const string PmManager_ProjectUser_ViewDetailProjectUser = "PmManager.ProjectUser.ViewDetailProjectUser";
            public const string PmManager_ProjectUser_Create = "PmManager.ProjectUser.Create";
            public const string PmManager_ProjectUser_Update = "PmManager.ProjectUser.Update";
            public const string PmManager_ProjectUser_Delete = "PmManager.ProjectUser.Delete";

            //Project Milestone
            public const string PmManager_ProjectMilestone = "PmManager.ProjectMilestone";
            public const string PmManager_ProjectMilestone_Create = "PmManager.ProjectMilestone.Create";
            public const string PmManager_ProjectMilestone_Delete = "PmManager.ProjectMilestone.Delete";
            public const string PmManager_ProjectMilestone_Update = "PmManager.ProjectMilestone.Update";
            public const string PmManager_ProjectMilestone_ViewAll = "PmManager.ProjectMilestone.ViewAll";

            //PmReportProject
            public const string PmManager_PMReportProject = "PmManager.PMReportProject";
            public const string PmManager_PMReportProject_GetAllByPmReport = "PmManager.PMReportProject.GetAllByPmProject";
            public const string PmManager_PMReportProject_ResourceChangesDuringTheWeek = "PmManager.PMReportProject.ResourceChangesDuringTheWeek";
            public const string PmManager_PMReportProject_ResourceChangesInTheFuture = "PmManager.PMReportProject.ResourceChangesInTheFuture";
            public const string PmManager_PMReportProject_SendReport = "PmManager.PMReportProject.SendReport";
            public const string PmManager_PMReportProject_UpdatePmReportProjectHealth = "PmManager.PMReportProject.UpdatePmReportProjectHealth";
            public const string PmManager_PMReportProject_Create = "PmManager.PMReportProject.Create";
            public const string PmManager_PMReportProject_Update = "PmManager.PMReportProject.Update";
            public const string PmManager_PMReportProject_Delete = "PmManager.PMReportProject.Delete";
            public const string PmManager_PMReportProject_UpdateNote = "PmManager.PMReportProject.UpdateNote";
            public const string PmManager_PMReportProject_GetWorkingTimeFromTimesheet = "PmManager.PMReportProject.GetWorkingTimeFromTimesheet";


            //Resource Request
            public const string PmManager_ResourceRequest = "PmManager.ResourceRequest";
            public const string PmManager_ResourceRequest_ViewAllByProject = "PmManager.ResourceRequest.ViewAllByProject";
            public const string PmManager_ResourceRequest_ViewAllResourceRequest = "PmManager.ResourceRequest.ViewAllResourceRequest";
            public const string PmManager_ResourceRequest_ViewDetailResourceRequest = "PmManager.ResourceRequest.ViewDetailResourceRequest";
            public const string PmManager_ResourceRequest_AddUserToRequest = "PmManager.ResourceRequest.AddUserToRequest";
            public const string PmManager_ResourceRequest_SearchAvailableUserForRequest = "PmManager.ResourceRequest.SearchAvailableUserForRequest";
            public const string PmManager_ResourceRequest_AvailableResource = "PmManager.ResourceRequest.AvailableResource";
            public const string PmManager_ResourceRequest_AvailableResourceFuture = "PmManager.ResourceRequest.AvailableResourceFuture";
            public const string PmManager_ResourceRequest_PlanUser = "PmManager.ResourceRequest.PlanUser";
            public const string PmManager_ResourceRequest_ApproveUser = "PmManager.ResourceRequest.ApproveUser";
            public const string PmManager_ResourceRequest_RejectUser = "PmManager.ResourceRequest.RejectUser";
            public const string PmManager_ResourceRequest_Create = "PmManager.ResourceRequest.Create";
            public const string PmManager_ResourceRequest_Update = "PmManager.ResourceRequest.Update";
            public const string PmManager_ResourceRequest_Delete = "PmManager.ResourceRequest.Delete";

            //PMReportProjectIssues
            public const string PmManager_PMReportProjectIssue = "PmManager.PMReportProjectIssue";
            public const string PmManager_PMReportProjectIssue_ProblemsOfTheWeek = "PmManager.PMReportProjectIssue.ProblemsOfTheWeek";
            public const string PmManager_PMReportProjectIssue_Create = "PmManager.PMReportProjectIssue.Create";
            public const string PmManager_PMReportProjectIssue_Update = "PmManager.PMReportProjectIssue.Update";
            public const string PmManager_PMReportProjectIssue_Delete = "PmManager.PMReportProjectIssue.Delete";

        //Timesheet
        public const string Timesheet = "Timesheet";
            public const string Timesheet_CanViewMenu = "Timesheet.CanViewMenu";
            //Timesheet
            public const string Timesheet_Timesheet = "Timesheet.Timesheet";
            public const string Timesheet_Timesheet_ViewAll = "Timesheet.Timesheet.ViewAll";
            public const string Timesheet_Timesheet_Get = "Timesheet.Timesheet.Get";
            public const string Timesheet_Timesheet_Create = "Timesheet.Timesheet.Create";
            public const string Timesheet_Timesheet_Update = "Timesheet.Timesheet.Update";
            public const string Timesheet_Timesheet_Delete = "Timesheet.Timesheet.Delete";
            public const string Timesheet_Timesheet_DoneTimesheetById = "Timesheet.Timesheet.DoneTimesheetById";
            public const string Timesheet_Timesheet_ReverseActive = "Timesheet.Timesheet.ReverseActive";

            //TimesheetProject
            public const string Timesheet_TimesheetProject = "Timesheet.TimesheetProject";
            public const string Timesheet_TimesheetProject_GetAllByproject = "Timesheet.TimesheetProject.GetAllByProject";
            public const string Timesheet_TimesheetProject_GetAllProjectTimesheetByTimesheet = "Timesheet.Timesheet.GetAllProjectTimesheetByTimesheet";
            public const string Timesheet_TimesheetProject_ViewOnlyme = "Timesheet.Timesheet.ViewOnlyMe";
            public const string Timesheet_TimesheetProject_ViewOnlyActiveProject = "Timesheet.Timesheet.ViewOnlyActiveProject";
            public const string Timesheet_TimesheetProject_GetAllRemainProjectInTimesheet = "Timesheet.TimesheetProject.GetAllRemainProjectInTimesheet";
            public const string Timesheet_TimesheetProject_ViewProjectBillInfomation = "Timesheet.Timesheet.ViewProjectBillInfomation";
            public const string Timesheet_TimesheetProject_Create = "Timesheet.TimesheetProject.Create";
            public const string Timesheet_TimesheetProject_Update = "Timesheet.TimesheetProject.Update";
            public const string Timesheet_TimesheetProject_Delete = "Timesheet.TimesheetProject.Delete";
            public const string Timesheet_TimesheetProject_UploadFileTimesheetProject = "Timesheet.TimesheetProject.UploadFileTimesheetProject";
            public const string Timesheet_TimesheetProject_DownloadFileTimesheetProject = "Timesheet.TimesheetProject.DownloadFileTimesheetProject";
            public const string Timesheet_TimesheetProject_ViewInvoice = "Timesheet.TimesheetProject.ViewInvoice";
            public const string Timesheet_TimesheetProject_CreateInvoice = "Timesheet.TimesheetProject.CreateInvoice";

        //Checklist
        public const string CheckList = "CheckList";
            public const string CheckList_CanViewMenu = "CheckList.CanviewMenu";
            // Check List Category
            public const string CheckList_CheckListCategory = "CheckList.CheckListCategory";
            public const string CheckList_CheckListCategory_ViewAll = "CheckList.CheckListCategory.ViewAll";
            public const string CheckList_CheckListCategory_Create = "CheckList.CheckListCategory.Create";
            public const string CheckList_CheckListCategory_Update = "CheckList.CheckListCategory.Update";
            public const string CheckList_CheckListCategory_Delete = "CheckList.CheckListCategory.Delete";

            // Check List Item
            public const string CheckList_CheckListItem = "CheckList.CheckListItem";
            public const string CheckList_CheckListItem_ViewAll = "CheckList.CheckListItem.ViewAll";
            public const string CheckList_CheckListItem_Create = "CheckList.CheckListItem.Create";
            public const string CheckList_CheckListItem_Update = "CheckList.CheckListItem.Update";
            public const string CheckList_CheckListItem_Delete = "CheckList.CheckListItem.Delete";

            // Project Checklist
            public const string CheckList_ProjectChecklist = "CheckList.ProjectChecklist";
            public const string CheckList_ProjectChecklist_Create = "CheckList.ProjectChecklist.Create";
            public const string CheckList_ProjectChecklist_Delete = "CheckList.ProjectChecklist.Delete";
            public const string CheckList_ProjectChecklist_ReverseActive = "CheckList.ProjectChecklist.ReverseActive";
            public const string CheckList_ProjectChecklist_AddByProjectType = "CheckList.ProjectChecklist.AddByProjectType";
            public const string CheckList_ProjectChecklist_GetCheckListItemByProject = "CheckList.ProjectChecklist.GetCheckListItemByProject";
            public const string CheckList_ProjectChecklist_AddCheckListItemByProject = "CheckList.ProjectChecklist.AddCheckListItemByProject";

        //Sao Do
        public const string SaoDo = "SaoDo";   
            public const string SaoDo_CanViewMenu = "SaoDo.CanViewMenu";
            // Audit Result People
            public const string SaoDo_AuditResultPeople = "SaoDo.AuditResultPeople";
            public const string SaoDo_AuditResultPeople_Create = "SaoDo.AuditResultPeople.Create";
            public const string SaoDo_AuditResultPeople_Delete = "SaoDo.AuditResultPeople.Delete";
            public const string SaoDo_AuditResultPeople_Update = "SaoDo.AuditResultPeople.Update";

            // Audit Result
            public const string SaoDo_AuditResult = "SaoDo.AuditResult";
            public const string SaoDo_AuditResult_Create = "SaoDo.AuditResult.Create";
            public const string SaoDo_AuditResult_Delete = "SaoDo.AuditResult.Delete";
            public const string SaoDo_AuditResult_Update = "SaoDo.AuditResult.Update";
            public const string SaoDo_AuditResult_GetNote = "SaoDo.AuditResult.GetNote";
            public const string SaoDo_AuditResult_UpdateNote = "SaoDo.AuditResult.UpdateNote";

            // Audit Session
            public const string SaoDo_AuditSession = "SaoDo.AuditSession";
            public const string SaoDo_AuditSession_Create = "SaoDo.AuditSession.Create";
            public const string SaoDo_AuditSession_Delete = "SaoDo.AuditSession.Delete";
            public const string SaoDo_AuditSession_Update = "SaoDo.AuditSession.Update";
            public const string SaoDo_AuditSession_View = "SaoDo.AuditSession.View";
            public const string SaoDo_AuditSession_ViewAll = "SaoDo.AuditSession.ViewAll";
            public const string SaoDo_AuditSession_AddAuditResult = "SaoDo.AuditSession.AddAuditResult";

        //Delivery Management
        public const string DeliveryManagement = "DeliveryManagement";
            public const string DeliveryManagement_CanViewMenu = "DeliveryManagement.CanViewMenu";
            public const string Deliverymanagement_CanViewMenu_ResourceManagement = "Deliverymanagement.CanViewMenu.ResourceManagement";
            public const string DeliveryManagement_CanViewMenu_WeeklyReport = "Deliverymanagement.CanViewMenu.WeeklyReport";
            //PmReport
            public const string DeliveryManagement_PMReport = "DeliveryManagement.PMReport";
            public const string DeliveryManagement_PMReport_ViewAll = "DeliveryManagement.PMReport.ViewAll";
            public const string DeliveryManagement_PMReport_Create = "DeliveryManagement.PMReport.Create";
            public const string DeliveryManagement_PMReport_Update = "DeliveryManagement.PMReport.Update";
            public const string DeliveryManagement_PMReport_Delete = "DeliveryManagement.PMReport.Delete";
            public const string DeliveryManagement_PMReport_Get = "DeliveryManagement.PMReport.Get";
            public const string DeliveryManagement_PMReport_CloseReport = "DeliveryManagement.PMReport.CloseReport";
            public const string DeliveryManagement_PMReport_StatisticsReport = "DeliveryManagement.PMReport.StatisticsReport";

            //PmReportProject
            public const string DeliveryManagement_PMReportProject = "DeliveryManagement.PMReportProject";
            public const string DeliveryManagement_PMReportProject_GetAllByPmReport = "DeliveryManagement.PMReportProject.GetAllByPmProject";
            public const string DeliveryManagement_PMReportProject_ResourceChangesDuringTheWeek = "DeliveryManagement.PMReportProject.ResourceChangesDuringTheWeek";
            public const string DeliveryManagement_PMReportProject_ResourceChangesInTheFuture = "DeliveryManagement.PMReportProject.ResourceChangesInTheFuture";
            public const string DeliveryManagement_PMReportProject_SendReport = "DeliveryManagement.PMReportProject.SendReport";
            public const string DeliveryManagement_PMReportProject_UpdatePmReportProjectHealth = "DeliveryManagement.PMReportProject.UpdatePmReportProjectHealth";
            public const string DeliveryManagement_PMReportProject_Create = "DeliveryManagement.PMReportProject.Create";
            public const string DeliveryManagement_PMReportProject_Update = "DeliveryManagement.PMReportProject.Update";
            public const string DeliveryManagement_PMReportProject_Delete = "DeliveryManagement.PMReportProject.Delete";
            public const string DeliveryManagement_PMReportProject_UpdateNote = "DeliveryManagement.PMReportProject.UpdateNote";
            public const string DeliveryManagement_PMReportProject_GetWorkingTimeFromTimesheet = "DeliveryManagement.PMReportProject.GetWorkingTimeFromTimesheet";

            //Resource Request
            public const string DeliveryManagement_ResourceRequest = "DeliveryManagement.ResourceRequest";
            public const string DeliveryManagement_ResourceRequest_ViewAllByProject = "DeliveryManagement.ResourceRequest.ViewAllByProject";
            public const string DeliveryManagement_ResourceRequest_ViewAllResourceRequest = "DeliveryManagement.ResourceRequest.ViewAllResourceRequest";
            public const string DeliveryManagement_ResourceRequest_ViewDetailResourceRequest = "DeliveryManagement.ResourceRequest.ViewDetailResourceRequest";
            public const string DeliveryManagement_ResourceRequest_AddUserToRequest = "DeliveryManagement.ResourceRequest.AddUserToRequest";
            public const string DeliveryManagement_ResourceRequest_SearchAvailableUserForRequest = "DeliveryManagement.ResourceRequest.SearchAvailableUserForRequest";
            public const string DeliveryManagement_ResourceRequest_AvailableResource = "DeliveryManagement.ResourceRequest.AvailableResource";
            public const string DeliveryManagement_ResourceRequest_AvailableResourceFuture = "DeliveryManagement.ResourceRequest.AvailableResourceFuture";
            public const string DeliveryManagement_ResourceRequest_PlanUser = "DeliveryManagement.ResourceRequest.PlanUser";
            public const string DeliveryManagement_ResourceRequest_ApproveUser = "DeliveryManagement.ResourceRequest.ApproveUser";
            public const string DeliveryManagement_ResourceRequest_RejectUser = "DeliveryManagement.ResourceRequest.RejectUser";
            public const string DeliveryManagement_ResourceRequest_Create = "DeliveryManagement.ResourceRequest.Create";
            public const string DeliveryManagement_ResourceRequest_Update = "DeliveryManagement.ResourceRequest.Update";
            public const string DeliveryManagement_ResourceRequest_Delete = "DeliveryManagement.ResourceRequest.Delete";
            public const string DeliveryManagement_ResourceRequest_GetProjectForDM = "DeliveryManagement.ResourceRequest.GetProjectForDM";

            //PMReportProjectIssues
            public const string DeliveryManagement_PMReportProjectIssue = "DeliveryManagement.PMReportProjectIssue";
            public const string DeliveryManagement_PMReportProjectIssue_ProblemsOfTheWeek = "DeliveryManagement.PMReportProjectIssue.ProblemsOfTheWeek";
            public const string DeliveryManagement_PMReportProjectIssue_Create = "DeliveryManagement.PMReportProjectIssue.Create";
            public const string DeliveryManagement_PMReportProjectIssue_Update = "DeliveryManagement.PMReportProjectIssue.Update";
            public const string DeliveryManagement_PMReportProjectIssue_Delete = "DeliveryManagement.PMReportProjectIssue.Delete";

            //ProjectUser
            public const string DeliveryManagement_ProjectUser = "DeliveryManagement.ProjectUser";
            public const string DeliveryManagement_ProjectUser_ViewAllByProject = "DeliveryManagement.ProjectUser.ViewAllByProject";
            public const string DeliveryManagement_ProjectUser_ViewDetailProjectUser = "DeliveryManagement.ProjectUser.ViewDetailProjectUser";
            public const string DeliveryManagement_ProjectUser_Create = "DeliveryManagement.ProjectUser.Create";
            public const string DeliveryManagement_ProjectUser_Update = "DeliveryManagement.ProjectUser.Update";
            public const string DeliveryManagement_ProjectUser_Delete = "DeliveryManagement.ProjectUser.Delete";
    }

    public class GrantPermissionRoles
    {
        public static Dictionary<string, List<string>> PermissionRoles = new Dictionary<string, List<string>>()
        {
            {
                Host.Admin,
                new List<string>()
                {
                    // permission root
                    PermissionNames.Admin,
                    PermissionNames.PmManager,
                    PermissionNames.SaoDo,
                    PermissionNames.CheckList,
                    PermissionNames.DeliveryManagement,
                    PermissionNames.Timesheet,

                    PermissionNames.Admin_CanViewMenu,
                    PermissionNames.SaoDo_CanViewMenu,
                    PermissionNames.CheckList_CanViewMenu,
                    PermissionNames.Timesheet_CanViewMenu,

                    PermissionNames.PmManager_CanViewMenu,
                    PermissionNames.PmManager_CanViewMenu_Milestone,
                    PermissionNames.PmManager_CanViewMenu_ProjectChecklist,
                    PermissionNames.PmManager_CanViewMenu_ResourceManagement,
                    PermissionNames.PmManager_CanViewMenu_Timesheet,
                    PermissionNames.PmManager_CanViewMenu_WeeklyReport,

                    PermissionNames.PmManager_CanViewMenu_PMCanCreate,
                    PermissionNames.PmManager_CanViewMenu_PMCanUpdate,
                    PermissionNames.PmManager_CanViewMenu_PMCanDelete,

                    PermissionNames.DeliveryManagement_CanViewMenu,
                    PermissionNames.Deliverymanagement_CanViewMenu_ResourceManagement,
                    PermissionNames.DeliveryManagement_CanViewMenu_WeeklyReport,

                    //Admin
                        //User
                        PermissionNames.Pages_Users,
                            PermissionNames.Pages_Users_ViewAll,
                            PermissionNames.Pages_Users_Create,
                            PermissionNames.Pages_Users_Update,
                            PermissionNames.Pages_Users_Delete,
                            PermissionNames.Pages_Users_ImportUserFromFile,
                            PermissionNames.Pages_Users_UpdateAvatar,
                            PermissionNames.Pages_Users_AutoUpdateUserFromHRM,

                    PermissionNames.Pages_Tenants,
                    PermissionNames.Pages_Roles,

                    //Configuration
                    PermissionNames.Admin_Configuration,
                    PermissionNames.Admin_Configuration_ViewAll,
                    PermissionNames.Admin_Configuration_Edit,

                    // Client
                    PermissionNames.Admin_Client,
                    PermissionNames.Admin_Client_ViewAll,
                    PermissionNames.Admin_Client_Create,
                    PermissionNames.Admin_Client_Edit,
                    PermissionNames.Admin_Client_Delete,

                    //Skill
                    PermissionNames.Admin_Skill,
                    PermissionNames.Admin_Skill_ViewAll,
                    PermissionNames.Admin_Skill_Create,
                    PermissionNames.Admin_Skill_Update,
                    PermissionNames.Admin_Skill_Delete,

                    // Project
                    PermissionNames.PmManager_Project,
                    PermissionNames.PmManager_Project_ViewAll,
                    PermissionNames.PmManager_Project_ViewonlyMe,
                    PermissionNames.PmManager_Project_ViewDetail,
                    PermissionNames.PmManager_Project_Create,
                    PermissionNames.PmManager_Project_Update,
                    PermissionNames.PmManager_Project_Delete,

                    //TimeSheet
                    PermissionNames.Timesheet_Timesheet,
                    PermissionNames.Timesheet_Timesheet_ViewAll,
                    PermissionNames.Timesheet_Timesheet_Get,
                    PermissionNames.Timesheet_Timesheet_Create,
                    PermissionNames.Timesheet_Timesheet_Update,
                    PermissionNames.Timesheet_Timesheet_Delete,
                    PermissionNames.Timesheet_Timesheet_DoneTimesheetById,
                    PermissionNames.Timesheet_Timesheet_ReverseActive,

                    //Timesheet Project
                    PermissionNames.Timesheet_TimesheetProject,
                    PermissionNames.Timesheet_TimesheetProject_GetAllByproject,
                    PermissionNames.Timesheet_TimesheetProject_GetAllProjectTimesheetByTimesheet,
                    PermissionNames.Timesheet_TimesheetProject_GetAllRemainProjectInTimesheet,
                    PermissionNames.Timesheet_TimesheetProject_ViewProjectBillInfomation,
                    PermissionNames.Timesheet_TimesheetProject_Create,
                    PermissionNames.Timesheet_TimesheetProject_Update,
                    PermissionNames.Timesheet_TimesheetProject_Delete,
                    PermissionNames.Timesheet_TimesheetProject_UploadFileTimesheetProject,
                    PermissionNames.Timesheet_TimesheetProject_DownloadFileTimesheetProject,
                    PermissionNames.Timesheet_TimesheetProject_ViewInvoice,
                    PermissionNames.Timesheet_TimesheetProject_CreateInvoice,

                    //Project User Bill
                    PermissionNames.PmManager_ProjectUserBill,
                    PermissionNames.PmManager_ProjectUserBill_GetAllPaging,
                    PermissionNames.PmManager_ProjectUserBill_GetAllByproject,
                    PermissionNames.PmManager_ProjectUserBill_Create,
                    PermissionNames.PmManager_ProjectUserBill_Update,
                    PermissionNames.PmManager_ProjectUserBill_Delete,

                    //ProjectUser
                    PermissionNames.PmManager_ProjectUser,
                    PermissionNames.PmManager_ProjectUser_ViewAllByProject,
                    PermissionNames.PmManager_ProjectUser_ViewDetailProjectUser,
                    PermissionNames.PmManager_ProjectUser_Create,
                    PermissionNames.PmManager_ProjectUser_Update,
                    PermissionNames.PmManager_ProjectUser_Delete,

                     //PMReportProject
                    PermissionNames.PmManager_PMReportProject,
                    PermissionNames.PmManager_PMReportProject_GetAllByPmReport,
                    PermissionNames.PmManager_PMReportProject_ResourceChangesDuringTheWeek,
                    PermissionNames.PmManager_PMReportProject_ResourceChangesInTheFuture,
                    PermissionNames.PmManager_PMReportProject_SendReport,
                    PermissionNames.PmManager_PMReportProject_UpdatePmReportProjectHealth,
                    PermissionNames.PmManager_PMReportProject_Create,
                    PermissionNames.PmManager_PMReportProject_Update,
                    PermissionNames.PmManager_PMReportProject_Delete,
                    PermissionNames.PmManager_PMReportProject_UpdateNote,
                    PermissionNames.PmManager_PMReportProject_GetWorkingTimeFromTimesheet,

                    //ResourceRequest
                    PermissionNames.PmManager_ResourceRequest,
                    PermissionNames.PmManager_ResourceRequest_ViewAllByProject,
                    PermissionNames.PmManager_ResourceRequest_ViewAllResourceRequest,
                    PermissionNames.PmManager_ResourceRequest_ViewDetailResourceRequest,
                    PermissionNames.PmManager_ResourceRequest_AddUserToRequest,
                    PermissionNames.PmManager_ResourceRequest_SearchAvailableUserForRequest,
                    PermissionNames.PmManager_ResourceRequest_AvailableResource,
                    PermissionNames.PmManager_ResourceRequest_AvailableResourceFuture,
                    PermissionNames.PmManager_ResourceRequest_PlanUser,
                    PermissionNames.PmManager_ResourceRequest_ApproveUser,
                    PermissionNames.PmManager_ResourceRequest_RejectUser,
                    PermissionNames.PmManager_ResourceRequest_Create,
                    PermissionNames.PmManager_ResourceRequest_Update,
                    PermissionNames.PmManager_ResourceRequest_Delete,

                    //PMReportProjectIssues
                    PermissionNames.PmManager_PMReportProjectIssue,
                    PermissionNames.PmManager_PMReportProjectIssue_ProblemsOfTheWeek,
                    PermissionNames.PmManager_PMReportProjectIssue_Create,
                    PermissionNames.PmManager_PMReportProjectIssue_Update,
                    PermissionNames.PmManager_PMReportProjectIssue_Delete,

                    //Check List Category
                    PermissionNames.CheckList_CheckListCategory,
                    PermissionNames.CheckList_CheckListCategory_Create,
                    PermissionNames.CheckList_CheckListCategory_Delete,
                    PermissionNames.CheckList_CheckListCategory_Update,
                    PermissionNames.CheckList_CheckListCategory_ViewAll,

                    //Check List Item
                    PermissionNames.CheckList_CheckListItem,
                    PermissionNames.CheckList_CheckListItem_Create,
                    PermissionNames.CheckList_CheckListItem_Delete,
                    PermissionNames.CheckList_CheckListItem_Update,
                    PermissionNames.CheckList_CheckListItem_ViewAll,

                    // Audit Result People
                    PermissionNames.SaoDo_AuditResultPeople,
                    PermissionNames.SaoDo_AuditResultPeople_Create,
                    PermissionNames.SaoDo_AuditResultPeople_Delete,
                    PermissionNames.SaoDo_AuditResultPeople_Update,

                     // Audit Result 
                    PermissionNames.SaoDo_AuditResult,
                    PermissionNames.SaoDo_AuditResult_Create,
                    PermissionNames.SaoDo_AuditResult_Delete,
                    PermissionNames.SaoDo_AuditResult_Update,
                    PermissionNames.SaoDo_AuditResult_GetNote,
                    PermissionNames.SaoDo_AuditResult_UpdateNote,

                    // Audit Session
                    PermissionNames.SaoDo_AuditSession,
                    PermissionNames.SaoDo_AuditSession_Create,
                    PermissionNames.SaoDo_AuditSession_Delete,
                    PermissionNames.SaoDo_AuditSession_Update,
                    PermissionNames.SaoDo_AuditSession_View,
                    PermissionNames.SaoDo_AuditSession_ViewAll,
                    PermissionNames.SaoDo_AuditSession_AddAuditResult,

                    // Project Checklist
                    PermissionNames.CheckList_ProjectChecklist,
                    PermissionNames.CheckList_ProjectChecklist_AddByProjectType,
                    PermissionNames.CheckList_ProjectChecklist_GetCheckListItemByProject,
                    PermissionNames.CheckList_ProjectChecklist_AddCheckListItemByProject,
                    PermissionNames.CheckList_ProjectChecklist_Create,
                    PermissionNames.CheckList_ProjectChecklist_Delete,
                    PermissionNames.CheckList_ProjectChecklist_ReverseActive,

                    //PMReport
                    PermissionNames.DeliveryManagement_PMReport,
                    PermissionNames.DeliveryManagement_PMReport_ViewAll,
                    PermissionNames.DeliveryManagement_PMReport_Create,
                    PermissionNames.DeliveryManagement_PMReport_Update,
                    PermissionNames.DeliveryManagement_PMReport_Delete,
                    PermissionNames.DeliveryManagement_PMReport_Get,
                    PermissionNames.DeliveryManagement_PMReport_CloseReport,
                    PermissionNames.DeliveryManagement_PMReport_StatisticsReport,

                    //PMReportProject
                    PermissionNames.DeliveryManagement_PMReportProject,
                    PermissionNames.DeliveryManagement_PMReportProject_GetAllByPmReport,
                    PermissionNames.DeliveryManagement_PMReportProject_ResourceChangesDuringTheWeek,
                    PermissionNames.DeliveryManagement_PMReportProject_ResourceChangesInTheFuture,
                    PermissionNames.DeliveryManagement_PMReportProject_SendReport,
                    PermissionNames.DeliveryManagement_PMReportProject_UpdatePmReportProjectHealth,
                    PermissionNames.DeliveryManagement_PMReportProject_Create,
                    PermissionNames.DeliveryManagement_PMReportProject_Update,
                    PermissionNames.DeliveryManagement_PMReportProject_Delete,
                    PermissionNames.DeliveryManagement_PMReportProject_UpdateNote,
                    PermissionNames.DeliveryManagement_PMReportProject_GetWorkingTimeFromTimesheet,

                    //ResourceRequest
                    PermissionNames.DeliveryManagement_ResourceRequest,
                    PermissionNames.DeliveryManagement_ResourceRequest_ViewAllByProject,
                    PermissionNames.DeliveryManagement_ResourceRequest_ViewAllResourceRequest,
                    PermissionNames.DeliveryManagement_ResourceRequest_ViewDetailResourceRequest,
                    PermissionNames.DeliveryManagement_ResourceRequest_AddUserToRequest,
                    PermissionNames.DeliveryManagement_ResourceRequest_SearchAvailableUserForRequest,
                    PermissionNames.DeliveryManagement_ResourceRequest_AvailableResource,
                    PermissionNames.DeliveryManagement_ResourceRequest_AvailableResourceFuture,
                    PermissionNames.DeliveryManagement_ResourceRequest_PlanUser,
                    PermissionNames.DeliveryManagement_ResourceRequest_ApproveUser,
                    PermissionNames.DeliveryManagement_ResourceRequest_RejectUser,
                    PermissionNames.DeliveryManagement_ResourceRequest_Create,
                    PermissionNames.DeliveryManagement_ResourceRequest_Update,
                    PermissionNames.DeliveryManagement_ResourceRequest_Delete,
                    PermissionNames.DeliveryManagement_ResourceRequest_GetProjectForDM,

                    //PMReportProjectIssues
                    PermissionNames.DeliveryManagement_PMReportProjectIssue,
                    PermissionNames.DeliveryManagement_PMReportProjectIssue_ProblemsOfTheWeek,
                    PermissionNames.DeliveryManagement_PMReportProjectIssue_Create,
                    PermissionNames.DeliveryManagement_PMReportProjectIssue_Update,
                    PermissionNames.DeliveryManagement_PMReportProjectIssue_Delete,

                     //ProjectUser
                    PermissionNames.DeliveryManagement_ProjectUser,
                    PermissionNames.DeliveryManagement_ProjectUser_ViewAllByProject,
                    PermissionNames.DeliveryManagement_ProjectUser_ViewDetailProjectUser,
                    PermissionNames.DeliveryManagement_ProjectUser_Create,
                    PermissionNames.DeliveryManagement_ProjectUser_Update,
                    PermissionNames.DeliveryManagement_ProjectUser_Delete,

                    //Project Milestone
                    PermissionNames.PmManager_ProjectMilestone,
                    PermissionNames.PmManager_ProjectMilestone_Create,
                    PermissionNames.PmManager_ProjectMilestone_Delete,
                    PermissionNames.PmManager_ProjectMilestone_Update,
                    PermissionNames.PmManager_ProjectMilestone_ViewAll,
                }
            }
        };
        public class SystemPermission
        {
            public string Name { get; set; }
            public MultiTenancySides MultiTenancySides { get; set; }
            public string DisplayName { get; set; }
            public bool IsConfiguration { get; set; }
            public List<SystemPermission> Childrens { get; set; }
            public static List<SystemPermission> ListPermissions = new List<SystemPermission>()
            {
                // root
                new SystemPermission{ Name =  PermissionNames.Admin, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Admin" },
                new SystemPermission{ Name =  PermissionNames.PmManager, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "PM Manager" },
                new SystemPermission{ Name =  PermissionNames.SaoDo, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Sao Do" },
                new SystemPermission{ Name =  PermissionNames.CheckList, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Check List" },
                new SystemPermission{ Name =  PermissionNames.DeliveryManagement, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delivery Management" },
                new SystemPermission{ Name =  PermissionNames.Timesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Timesheet" },

                new SystemPermission{ Name =  PermissionNames.Admin_CanViewMenu, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Admin Menu" },
                new SystemPermission{ Name =  PermissionNames.SaoDo_CanViewMenu, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Sao Do Menu" },
                new SystemPermission{ Name =  PermissionNames.CheckList_CanViewMenu, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Check List Menu" },
                new SystemPermission{ Name =  PermissionNames.Timesheet_CanViewMenu, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Timesheet Menu" },

                // dm can view
                new SystemPermission{ Name =  PermissionNames.DeliveryManagement_CanViewMenu, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Delivery Management Menu" },
                new SystemPermission{ Name =  PermissionNames.Deliverymanagement_CanViewMenu_ResourceManagement, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Delivery Management Menu Resource Management" },
                new SystemPermission{ Name =  PermissionNames.DeliveryManagement_CanViewMenu_WeeklyReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Delivery Management Menu Weekly Report" },

                // pm manager can view
                new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View PM Manager Menu" },
                new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu_WeeklyReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View PM Manager Menu Weekly Report" },
                new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu_Timesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View PM Manager Menu Timesheet" },
                new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu_ResourceManagement, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View PM Manager Menu Resource Management" },
                new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu_ProjectChecklist, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View PM Manager Menu Project Checklist" },
                new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu_Milestone, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View PM Manager Menu Milestone" },
                new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu_PMCanCreate, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "PM Can Create ProjectUser" },
                new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu_PMCanUpdate, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "PM Can Update ProjectUser" },
                new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu_PMCanDelete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "PM Can Delete ProjectUser" },

                 //Admin
                 new SystemPermission{ Name =  PermissionNames.Pages_Roles, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Roles" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Tenants, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tenants" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Users, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Users" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Users_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All User" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Users_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create new User" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Users_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update User" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Users_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete User" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Users_ImportUserFromFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Import User From File" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Users_UpdateAvatar, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Avatar" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Users_AutoUpdateUserFromHRM, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update All User From HRM" },

                 //Configuration
                 new SystemPermission{ Name =  PermissionNames.Admin_Configuration, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Configuration" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Configuration_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Configuration" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Configuration_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit Configuration" },

                 //Client
                 new SystemPermission{ Name =  PermissionNames.Admin_Client, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Client" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Client_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Client" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Client_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Client" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Client_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit Client" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Client_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Client" },

                 //Skill
                 new SystemPermission{ Name =  PermissionNames.Admin_Skill, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Skill" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Skill_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Skill" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Skill_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Skill" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Skill_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Skill" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Skill_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Skill" },

                 //Project
                 new SystemPermission{ Name =  PermissionNames.PmManager_Project, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_Project_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Project" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_Project_ViewonlyMe, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Only Me" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_Project_ViewDetail, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Detail Project" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_Project_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Project" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_Project_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Project" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_Project_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Project" },

                 // TimeSheet
                 new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Timesheet" },
                 new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Timesheet" },
                 new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_Get, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Detail Timesheet" },
                 new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Timesheet" },
                 new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Timesheet" },
                 new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Timesheet" },
                 new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_ReverseActive, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Reverse Active Timesheet" },
                 new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_DoneTimesheetById, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Done Timesheet" },

                 //Timesheet Project
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Timesheet Project" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_GetAllByproject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get All By Project" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_GetAllProjectTimesheetByTimesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Timesheet Project By TimeSheet" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_ViewOnlyme, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Only Me" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_ViewOnlyActiveProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Only Active Project" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_GetAllRemainProjectInTimesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get All Remain Project In Timesheet" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_ViewProjectBillInfomation, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Project Bill Infomation" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Timesheet Project" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Timesheet Project" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Timesheet Project" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_UploadFileTimesheetProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Upload File TimeSheet Project" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_DownloadFileTimesheetProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Download File TimeSheet Project" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_ViewInvoice, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View info Invoice" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_CreateInvoice, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Invoice To Finance" },

                  //Project User Bill
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUserBill, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project User Bill" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUserBill_GetAllPaging, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUserBill_GetAllByproject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get All By Project" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUserBill_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Project User Bill" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUserBill_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit Project User Bill" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUserBill_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Project User Bill" },

                  //ProjectUser
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project User" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUser_ViewAllByProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get All By Project" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUser_ViewDetailProjectUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Detail Project User" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUser_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Project User" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUser_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Project User" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUser_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Project User" },
                 
                  //ProjectUser by DM
                  new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project User" },
                  new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_ViewAllByProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get All By Project" },
                  new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_ViewDetailProjectUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Detail Project User" },
                  new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Project User" },
                  new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Project User" },
                  new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Project User" },

                  //Check List Category
                  new SystemPermission{ Name =  PermissionNames.CheckList_CheckListCategory, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Check List Category" },
                  new SystemPermission{ Name =  PermissionNames.CheckList_CheckListCategory_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Check List Category" },
                  new SystemPermission{ Name =  PermissionNames.CheckList_CheckListCategory_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Check List Category" },
                  new SystemPermission{ Name =  PermissionNames.CheckList_CheckListCategory_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Check List Category" },
                  new SystemPermission{ Name =  PermissionNames.CheckList_CheckListCategory_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Check List Category" },
                  
                 //Check List Item
                 new SystemPermission{ Name =  PermissionNames.CheckList_CheckListItem, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Check List Item" },
                 new SystemPermission{ Name =  PermissionNames.CheckList_CheckListItem_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Check List Item" },
                 new SystemPermission{ Name =  PermissionNames.CheckList_CheckListItem_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Check List Item" },
                 new SystemPermission{ Name =  PermissionNames.CheckList_CheckListItem_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Check List Item" },                 
                 new SystemPermission{ Name =  PermissionNames.CheckList_CheckListItem_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Check List Item" },

                 //Audit Result People
                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditResultPeople, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Audit Result People" },
                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditResultPeople_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Audit Result People" },
                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditResultPeople_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Audit Result People" },
                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditResultPeople_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Audit Result People" },
                
                 //Audit Result 
                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditResult, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Audit Result " },
                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditResult_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Audit Result " },
                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditResult_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Audit Result " },
                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditResult_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Audit Result " },
                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditResult_UpdateNote, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update note Audit Result " },
                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditResult_GetNote, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get note Audit Result " },

                 //Audit Session
                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditSession, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Audit Session" },
                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditSession_AddAuditResult, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add Audit Result" },
                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditSession_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Audit Session" },
                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditSession_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Audit Session" },
                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditSession_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Audit Session" },
                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditSession_View, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Audit Session" },
                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditSession_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Audit Session" },

                 //Project Checklist
                 new SystemPermission{ Name =  PermissionNames.CheckList_ProjectChecklist, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project Checklist" },
                 new SystemPermission{ Name =  PermissionNames.CheckList_ProjectChecklist_AddByProjectType, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add Project Checklist by Project Type" },
                 new SystemPermission{ Name =  PermissionNames.CheckList_ProjectChecklist_GetCheckListItemByProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get Checklist Item By Project" },
                 new SystemPermission{ Name =  PermissionNames.CheckList_ProjectChecklist_AddCheckListItemByProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add Checklist Item By Project" },
                 new SystemPermission{ Name =  PermissionNames.CheckList_ProjectChecklist_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Project Checklist" },
                 new SystemPermission{ Name =  PermissionNames.CheckList_ProjectChecklist_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Project Checklist" },
                 new SystemPermission{ Name =  PermissionNames.CheckList_ProjectChecklist_ReverseActive, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Reverse Active Project Checklist" },

                 //PmReport
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "PM Report" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReport_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All PMReport" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReport_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New PMReport" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReport_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update PMReport" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReport_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete PMReport" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReport_Get, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get PMReport" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReport_CloseReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Close PMReport" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReport_StatisticsReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Statistics PMReport" },

                 //PmReportProject
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "PM Report Project" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProject_GetAllByPmReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All By PmReport" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProject_ResourceChangesDuringTheWeek, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Resource Changes During The Week" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProject_ResourceChangesInTheFuture, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Resource Changes In The Future" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProject_SendReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Send Report" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProject_UpdatePmReportProjectHealth, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update PmReportProject Health" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProject_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New PMReport Project" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProject_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update PMReport Project" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProject_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete PMReport Project" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProject_UpdateNote, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update note PM Report Project" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProject_GetWorkingTimeFromTimesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get Working Time From Timesheet" },

                 //Resource Request
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_ViewAllByProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Resource Request By Project" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_ViewAllResourceRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_ViewDetailResourceRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Detail Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_AddUserToRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add User To Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_SearchAvailableUserForRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Available User For Request" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_AvailableResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Available Resource" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_AvailableResourceFuture, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Available Resource Future" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_PlanUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Plan For User" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_ApproveUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Approve User" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_RejectUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Reject User" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_GetProjectForDM, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get Project For Delivery Management" },

                 //PMReportProjectIssues
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProjectIssue, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "PMReport Project Issues" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProjectIssue_ProblemsOfTheWeek, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Problems Of The Week" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProjectIssue_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New PMReport Project Issues" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProjectIssue_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update PMReport Project Issues" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProjectIssue_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete PMReport Project Issues" },

                 //PmReportProject by PM
                 new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "PM Report Project" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProject_GetAllByPmReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All By PmReport" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProject_ResourceChangesDuringTheWeek, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Resource Changes During The Week" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProject_ResourceChangesInTheFuture, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Resource Changes In The Future" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProject_SendReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Send Report" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProject_UpdatePmReportProjectHealth, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update PmReportProject Health" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProject_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New PMReport Project" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProject_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update PMReport Project" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProject_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete PMReport Project" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProject_UpdateNote, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update note PM Report Project" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProject_GetWorkingTimeFromTimesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get Working Time From Timesheet" },

                 //Resource Request by PM
                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_ViewAllByProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Resource Request By Project" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_ViewAllResourceRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_ViewDetailResourceRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Detail Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_AddUserToRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add User To Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_SearchAvailableUserForRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Available User For Request" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_AvailableResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Available Resource" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_AvailableResourceFuture, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Available Resource Future" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_PlanUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Plan For User" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_ApproveUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Approve User" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_RejectUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Reject User" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Resource Request" },

                 //PMReportProjectIssues by PM
                 new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProjectIssue, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "PMReport Project Issues" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProjectIssue_ProblemsOfTheWeek, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Problems Of The Week" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProjectIssue_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New PMReport Project Issues" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProjectIssue_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update PMReport Project Issues" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProjectIssue_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete PMReport Project Issues" },

                 //Project Milestone
                 new SystemPermission{ Name =  PermissionNames.PmManager_ProjectMilestone, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project Milestone" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_ProjectMilestone_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Project Milestone" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_ProjectMilestone_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Project Milestone" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_ProjectMilestone_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Project Milestone" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_ProjectMilestone_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Project Milestone" },
            };
            public static List<SystemPermission> TreePermissions = new List<SystemPermission>()
            {
                //Admin
                new SystemPermission { Name =  PermissionNames.Admin, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Admin",
                    Childrens = new List<SystemPermission>() {
                        new SystemPermission{ Name =  PermissionNames.Admin_CanViewMenu, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Admin Menu" },
                        new SystemPermission { Name = PermissionNames.Pages_Tenants, MultiTenancySides = MultiTenancySides.Host, DisplayName = "Tenants" },
                        new SystemPermission { Name = PermissionNames.Pages_Roles, MultiTenancySides = MultiTenancySides.Host, DisplayName = "Roles" },
                         //User
                         new SystemPermission { Name =  PermissionNames.Pages_Users, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "User",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.Pages_Users_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All User" },
                                 new SystemPermission{ Name =  PermissionNames.Pages_Users_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create new User" },
                                 new SystemPermission{ Name =  PermissionNames.Pages_Users_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update User" },
                                 new SystemPermission{ Name =  PermissionNames.Pages_Users_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete User" },
                                 new SystemPermission{ Name =  PermissionNames.Pages_Users_ImportUserFromFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Import User From File" },
                                 new SystemPermission{ Name =  PermissionNames.Pages_Users_UpdateAvatar, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Avatar" },
                                 new SystemPermission{ Name =  PermissionNames.Pages_Users_AutoUpdateUserFromHRM, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update All User From HRM" },
                            }
                        },
                        //Configuration
                         new SystemPermission { Name =  PermissionNames.Admin_Configuration, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Configuration",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.Admin_Configuration_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Configuration" },
                                 new SystemPermission{ Name =  PermissionNames.Admin_Configuration_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit Configuration" },
                            }
                        },
                        //Client
                        new SystemPermission { Name =  PermissionNames.Admin_Client, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Client",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.Admin_Client_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Client" },
                                 new SystemPermission{ Name =  PermissionNames.Admin_Client_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Client" },
                                 new SystemPermission{ Name =  PermissionNames.Admin_Client_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit Client" },
                                 new SystemPermission{ Name =  PermissionNames.Admin_Client_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Client" },
                            }
                        },
                        //Skill
                        new SystemPermission { Name =  PermissionNames.Admin_Skill, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Skill",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.Admin_Skill_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Skill" },
                                 new SystemPermission{ Name =  PermissionNames.Admin_Skill_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Skill" },
                                 new SystemPermission{ Name =  PermissionNames.Admin_Skill_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Skill" },
                                 new SystemPermission{ Name =  PermissionNames.Admin_Skill_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Skill" },
                            }
                        },
                    }
                },
                //PM Manager
                new SystemPermission { Name =  PermissionNames.PmManager, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "PM Manager",
                    Childrens = new List<SystemPermission>() {
                        // pm manager can view menu
                        new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View PM Manager Menu",
                        Childrens = new List<SystemPermission>()
                            {
                                    new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu_Milestone, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View PM Manager Menu Milestone" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu_ProjectChecklist, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View PM Manager Menu Project Checklist" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu_ResourceManagement, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View PM Manager Menu Resource Management" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu_Timesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View PM Manager Menu Timesheet" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu_WeeklyReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View PM Manager Menu Weekly Report" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu_PMCanCreate, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "PM Can Create ProjectUser" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu_PMCanUpdate, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "PM Can Update ProjectUser" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu_PMCanDelete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "PM Can Delete ProjectUser" },
                            }
                        },
                        //Project
                       new SystemPermission { Name =  PermissionNames.PmManager_Project, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project",
                            Childrens = new List<SystemPermission>()
                            {
                                    new SystemPermission{ Name =  PermissionNames.PmManager_Project_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Project" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_Project_ViewonlyMe, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Only Me" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_Project_ViewDetail, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Detail Project" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_Project_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Project" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_Project_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Project" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_Project_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Project" },
                            }
                        },                   
                       
                       //ProjectUser
                        new SystemPermission { Name =  PermissionNames.PmManager_ProjectUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project User",
                            Childrens = new List<SystemPermission>()
                            {
                                   new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUser_ViewAllByProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get All By Project" },
                                   new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUser_ViewDetailProjectUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Detail Project User" },
                                   new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUser_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Project User" },
                                   new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUser_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Project User" },
                                   new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUser_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Project User" },
                            }
                        },
                       //Project User Bill
                        new SystemPermission { Name =  PermissionNames.PmManager_ProjectUserBill, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project User Bill",
                            Childrens = new List<SystemPermission>()
                            {
                                    new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUserBill_GetAllPaging, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUserBill_GetAllByproject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get All By Project" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUserBill_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Project User Bill" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUserBill_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit Project User Bill" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUserBill_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Project User Bill" },
                            }
                        },

                        //Project Milestone
                        new SystemPermission { Name =  PermissionNames.PmManager_ProjectMilestone, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project Milestone",
                            Childrens = new List<SystemPermission>()
                            {
                                    new SystemPermission{ Name =  PermissionNames.PmManager_ProjectMilestone_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Project Milestone" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_ProjectMilestone_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Project Milestone" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_ProjectMilestone_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Project Milestone" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_ProjectMilestone_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Project Milestone" },
                            }
                        },
                        //Pm Report Project
                        new SystemPermission { Name =  PermissionNames.PmManager_PMReportProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "PM Report Project",
                            Childrens = new List<SystemPermission>()
                            {
                                new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProject_GetAllByPmReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All By PmReport" },
                                new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProject_ResourceChangesDuringTheWeek, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Resource Changes During The Week" },
                                new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProject_ResourceChangesInTheFuture, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Resource Changes In The Future" },
                                new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProject_SendReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Send Report" },
                                new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProject_UpdatePmReportProjectHealth, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update PmReportProject Health" },
                                new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProject_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New PMReport Project" },
                                new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProject_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update PMReport Project" },
                                new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProject_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete PMReport Project" },
                                new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProject_UpdateNote, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update note PM Report Project" },
                                new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProject_GetWorkingTimeFromTimesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get Working Time From Timesheet" },
                            }
                        },

                        // Resource Request
                        new SystemPermission { Name =  PermissionNames.PmManager_ResourceRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Resource Request",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_ViewAllByProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Resource Request By Project" },
                                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_ViewAllResourceRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Resource Request" },
                                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_ViewDetailResourceRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Detail Resource Request" },
                                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_AddUserToRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add User To Resource Request" },
                                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_SearchAvailableUserForRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Available User For Request" },
                                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_AvailableResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Available Resource" },
                                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_AvailableResourceFuture, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Available Resource Future" },
                                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_PlanUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Plan For User" },
                                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_ApproveUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Approve User" },
                                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_RejectUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Reject User" },
                                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Resource Request" },
                                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Resource Request" },
                                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Resource Request" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_GetProjectForDM, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get Project For Delivery Management" },
                            }
                        },
                         // PMReport Project Issues
                        new SystemPermission { Name =  PermissionNames.PmManager_PMReportProjectIssue, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "PMReport Project Issues",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProjectIssue_ProblemsOfTheWeek, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Problems Of The Week" },
                                 new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProjectIssue_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New PMReport Project Issues" },
                                 new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProjectIssue_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update PMReport Project Issues" },
                                 new SystemPermission{ Name =  PermissionNames.PmManager_PMReportProjectIssue_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete PMReport Project Issues" },
                            }
                        },
                        
                    }
                },
                //Sao Do
                new SystemPermission { Name =  PermissionNames.SaoDo, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Sao Do",
                    Childrens = new List<SystemPermission>() {
                        new SystemPermission{ Name =  PermissionNames.SaoDo_CanViewMenu, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Sao Do Menu" },
                        //Audit Result People
                        new SystemPermission { Name =  PermissionNames.SaoDo_AuditResultPeople, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Audit Result People",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditResultPeople_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Audit Result People" },
                                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditResultPeople_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Audit Result People" },
                                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditResultPeople_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Audit Result People" },
                            }
                        },
                        
                        //Audit Result 
                        new SystemPermission { Name =  PermissionNames.SaoDo_AuditResult, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Audit Result ",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditResult_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Audit Result " },
                                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditResult_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Audit Result " },
                                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditResult_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Audit Result " },
                                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditResult_UpdateNote, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update note Audit Result " },
                                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditResult_GetNote, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get note Audit Result " },
                            }
                        },
                        
                        //Audit Session
                        new SystemPermission { Name =  PermissionNames.SaoDo_AuditSession, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Audit Session",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditSession_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Audit Session" },
                                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditSession_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Audit Session" },
                                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditSession_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Audit Session" },
                                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditSession_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Audit Session" },
                                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditSession_View, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Audit Session" },
                                 new SystemPermission{ Name =  PermissionNames.SaoDo_AuditSession_AddAuditResult, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add Audit Result" },
                            }
                        },
                        
                    }
                },
                //Delivery Management
                 new SystemPermission { Name =  PermissionNames.DeliveryManagement, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delivery Management",
                    Childrens = new List<SystemPermission>() {
                        new SystemPermission{ Name =  PermissionNames.DeliveryManagement_CanViewMenu, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Delivery Management Menu" },
                        new SystemPermission{ Name =  PermissionNames.Deliverymanagement_CanViewMenu_ResourceManagement, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Delivery Management Menu Resource Management" },
                        new SystemPermission{ Name =  PermissionNames.DeliveryManagement_CanViewMenu_WeeklyReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Delivery Management Menu Weekly Report" },
                        
                        //Pm Report
                        new SystemPermission { Name =  PermissionNames.DeliveryManagement_PMReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "PM Report",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReport_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All PM Report" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReport_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New PM Report" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReport_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update PM Report" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReport_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete PM Report" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReport_Get, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get PM Report" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReport_CloseReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Close PMReport" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReport_StatisticsReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Statistics PMReport" },
                            }
                        },

                        //Pm Report Project
                        new SystemPermission { Name =  PermissionNames.DeliveryManagement_PMReportProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "PM Report Project",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProject_GetAllByPmReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All By PmReport" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProject_ResourceChangesDuringTheWeek, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Resource Changes During The Week" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProject_ResourceChangesInTheFuture, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Resource Changes In The Future" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProject_SendReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Send Report" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProject_UpdatePmReportProjectHealth, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update PmReportProject Health" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProject_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New PM Report Project" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProject_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update PM Report Project" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProject_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete PM Report Project" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProject_UpdateNote, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Note PM Report Project" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProject_GetWorkingTimeFromTimesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get Working Time From Timesheet" },
                            }
                        },

                        // Resource Request
                        new SystemPermission { Name =  PermissionNames.DeliveryManagement_ResourceRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Resource Request",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_ViewAllByProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All By Project" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_ViewAllResourceRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Resource Request" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_ViewDetailResourceRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Detail Resource Request" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_AddUserToRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add User To Resource Request" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_SearchAvailableUserForRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Available User For Request" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_AvailableResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Available Resource" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_AvailableResourceFuture, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Available Resource Future" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_PlanUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Plan For User" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_ApproveUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Approve User" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_RejectUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Reject User" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Resource Request" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Resource Request" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Resource Request" },
                            }
                        },
                         // PMReport Project Issues
                        new SystemPermission { Name =  PermissionNames.DeliveryManagement_PMReportProjectIssue, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "PMReport Project Issues",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProjectIssue_ProblemsOfTheWeek, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Problems Of The Week" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProjectIssue_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New PMReport Project Issues" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProjectIssue_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update PMReport Project Issues" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_PMReportProjectIssue_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete PMReport Project Issues" },
                            }
                        },
                         //ProjectUser
                        new SystemPermission { Name =  PermissionNames.DeliveryManagement_ProjectUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project User",
                            Childrens = new List<SystemPermission>()
                            {
                                   new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_ViewAllByProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get All By Project" },
                                   new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_ViewDetailProjectUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Detail Project User" },
                                   new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Project User" },
                                   new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Project User" },
                                   new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Project User" },
                            }
                        },
                    }
                },
                //Check List
                new SystemPermission { Name =  PermissionNames.CheckList, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Check List",
                    Childrens = new List<SystemPermission>() {
                        new SystemPermission{ Name =  PermissionNames.CheckList_CanViewMenu, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Check List Menu" },
                        //Check List Category
                        new SystemPermission { Name =  PermissionNames.CheckList_CheckListCategory, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Check List Category",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.CheckList_CheckListCategory_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Check List Category" },
                                 new SystemPermission{ Name =  PermissionNames.CheckList_CheckListCategory_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Check List Category" },
                                 new SystemPermission{ Name =  PermissionNames.CheckList_CheckListCategory_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Check List Category" },
                                 new SystemPermission{ Name =  PermissionNames.CheckList_CheckListCategory_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Check List Category" },
                            }
                        },
                        //Check List Item
                        new SystemPermission { Name =  PermissionNames.CheckList_CheckListItem, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Check List Item",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.CheckList_CheckListItem_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Check List Item" },
                                 new SystemPermission{ Name =  PermissionNames.CheckList_CheckListItem_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Check List Item" },
                                 new SystemPermission{ Name =  PermissionNames.CheckList_CheckListItem_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Check List Item" },
                                 new SystemPermission{ Name =  PermissionNames.CheckList_CheckListItem_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Check List Item" },
                            }
                        },                                             
                        //Project Checklist
                        new SystemPermission { Name =  PermissionNames.CheckList_ProjectChecklist, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project Checklist",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.CheckList_ProjectChecklist_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Project Checklist" },
                                 new SystemPermission{ Name =  PermissionNames.CheckList_ProjectChecklist_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Project Checklist" },
                                 new SystemPermission{ Name =  PermissionNames.CheckList_ProjectChecklist_ReverseActive, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Reverse Active Project Checklist" },
                                 new SystemPermission{ Name =  PermissionNames.CheckList_ProjectChecklist_AddByProjectType, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add Project Checklist by Project Type" },
                                 new SystemPermission{ Name =  PermissionNames.CheckList_ProjectChecklist_GetCheckListItemByProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get CheckList Item By Project" },
                                 new SystemPermission{ Name =  PermissionNames.CheckList_ProjectChecklist_AddCheckListItemByProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add CheckList Item By Project" },
                            }
                        },
                    }
                },
                //Timesheet
                new SystemPermission { Name =  PermissionNames.Timesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Timesheet",
                    Childrens = new List<SystemPermission>() {
                        new SystemPermission{ Name =  PermissionNames.Timesheet_CanViewMenu, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Timesheet Menu" },
                       //Timesheet
                       new SystemPermission { Name =  PermissionNames.Timesheet_Timesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "TimeSheet",
                            Childrens = new List<SystemPermission>()
                            {
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Timesheet" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_Get, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Detail Timesheet" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Timesheet" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Timesheet" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Timesheet" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_ReverseActive, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Reverse Active Timesheet" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_DoneTimesheetById, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Done Timesheet" },
                            }
                        },
                       //Timesheet Project
                       new SystemPermission { Name =  PermissionNames.Timesheet_TimesheetProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "TimeSheet Project",
                            Childrens = new List<SystemPermission>()
                            {
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_GetAllByproject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get All By Project" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_GetAllProjectTimesheetByTimesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Timesheet Project By TimeSheet" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_ViewOnlyme, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Only Me" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_ViewOnlyActiveProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Only Active Project" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_GetAllRemainProjectInTimesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get All Remain Project In Timesheet" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_ViewProjectBillInfomation, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Project Bill Infomation" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Timesheet Project" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Timesheet Project" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Timesheet Project" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_UploadFileTimesheetProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Upload File TimeSheet Project" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_DownloadFileTimesheetProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Download File TimeSheet Project" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_ViewInvoice, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View info Invoice" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_CreateInvoice, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Invoice To Finance" },
                            }
                        }
                    }
                },
            };

        }
    }
}
