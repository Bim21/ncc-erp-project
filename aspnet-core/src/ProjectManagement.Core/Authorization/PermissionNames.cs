using Abp.MultiTenancy;
using System.Collections.Generic;
using static ProjectManagement.Authorization.Roles.StaticRoleNames;

namespace ProjectManagement.Authorization
{
    public static class PermissionNames
    {
        //Admin
        //public const string Admin = "Admin";
        public const string Admin_CanViewMenu = "Admin.CanViewMenu";
        public const string Pages_Tenants = "Pages.Tenants";
        public const string Pages_Roles = "Pages.Roles";
        //User
        public const string Pages_Users = "Pages.Users";
        public const string Pages_Users_ViewAll = "Pages.Users.ViewAll";
        public const string Pages_Users_ViewOnlyMe = "Pages.Users.ViewOnlyMe";
        public const string Pages_Users_Create = "Pages.Users.Create";
        public const string Pages_Users_Update = "Pages.Users.Update";
        public const string Pages_Users_UpdateMySkills = "Pages.Users.UpdateMySkills";
        public const string Pages_Users_Delete = "Pages.Users.Delete";
        public const string Pages_Users_ImportUserFromFile = "Pages.Users.ImportUserFromFile";
        public const string Pages_Users_UpdateAvatar = "Pages.Users.UpdateAvatar";
        public const string Pages_Users_AutoUpdateUserFromHRM = "Pages.Users.AutoUpdateUserFromHRM";
        public const string Pages_Users_UpdateStarRateFromTimesheet = "Pages.Users.UpdateStarRateFromTimesheet";

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

        //Currency
        public const string Admin_Currency = "Admin.Currency";
        public const string Admin_Currency_ViewAll = "Admin.Currency.ViewAll";
        public const string Admin_Currency_Create = "Admin.Currency.Create";
        public const string Admin_Currency_Edit = "Admin.Currency.Edit";
        public const string Admin_Currency_Delete = "Admin.Currency.Delete";

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
        public const string PmManager_CanViewMenu_ProjectFile = "PmManager.CanViewMenu.ProjectFile";

        public const string PmManager_CanViewMenu_PMCanCreate = "PmManager.CanViewMenu.PMCanCreateProjectUser";
        public const string PmManager_CanViewMenu_PMCanUpdate = "PmManager.CanViewMenu.PMCanUpdateProjectUser";
        public const string PmManager_CanViewMenu_PMCanDelete = "PmManager.CanViewMenu.PMCanDeleteProjectUser";

        //Project
        public const string PmManager_Project = "PmManager.Project";
        public const string PmManager_Project_ViewAll = "PmManager.Project.ViewAll";
        public const string PmManager_Project_ViewonlyMe = "PmManager.Project.ViewOnlyMe";
        public const string PmManager_Project_ViewDetail = "PmManager.Project.ViewDetail";
        public const string PmManager_Project_ViewProjectInfor = "PmManager.Project.ViewProjectInfor";
        public const string PmManager_Project_Create = "PmManager.Project.Create";
        public const string PmManager_Project_Update = "PmManager.Project.Update";
        public const string PmManager_Project_Close = "PmManager.Project.Close";
        public const string PmManager_Project_Delete = "PmManager.Project.Delete";
        public const string PmManager_Project_UpdateProjectDetail = "PmManager.Project.UpdateProjectDetail";

        //ProjectFile
        public const string PmManager_ProjectFile = "PmManager.ProjectFile";
        public const string PmManager_ProjectFile_ViewAllFiles = "PmManager.ProjectFile.ViewAllFiles";
        public const string PmManager_ProjectFile_UploadNewFile = "PmManager.ProjectFile.UploadNewFile";
        public const string PmManager_ProjectFile_DeleteFile = "PmManager.ProjectFile.DeleteFile";


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
        public const string PmManager_ResourceRequest_CreateSkill = "PmManager.ResourceRequest.CreateSkill";
        public const string PmManager_ResourceRequest_DeleteSkill = "PmManager.ResourceRequest.DeleteSkill";
        public const string PmManager_ResourceRequest_GetSkillDetail = "PmManager.ResourceRequest.GetSkillDetail";

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
        public const string Timesheet_Timesheet_ForceDelete = "Timesheet.Timesheet.ForceDelete";
        public const string Timesheet_Timesheet_DoneTimesheetById = "Timesheet.Timesheet.DoneTimesheetById";
        public const string Timesheet_Timesheet_ReverseActive = "Timesheet.Timesheet.ReverseActive";

        //TimesheetProject
        public const string Timesheet_TimesheetProject = "Timesheet.TimesheetProject";
        public const string Timesheet_TimesheetProject_GetAllByproject = "Timesheet.TimesheetProject.GetAllByProject";
        public const string Timesheet_TimesheetDetail_ViewTimesheetOfAllProject = "Timesheet.Timesheet.GetAllProjectTimesheetByTimesheet";
        public const string Timesheet_TimesheetProject_ViewOnlyme = "Timesheet.Timesheet.ViewOnlyMe";
        public const string Timesheet_TimesheetProject_ViewOnlyActiveProject = "Timesheet.Timesheet.ViewOnlyActiveProject";
        public const string Timesheet_TimesheetProject_GetAllRemainProjectInTimesheet = "Timesheet.TimesheetProject.GetAllRemainProjectInTimesheet";
        public const string Timesheet_TimesheetDetail_ViewTimesheetAndBillInfoOfAllProject = "Timesheet.Timesheet.ViewProjectBillInfomation";
        public const string Timesheet_TimesheetProject_Create = "Timesheet.TimesheetProject.Create";
        public const string Timesheet_TimesheetProject_Update = "Timesheet.TimesheetProject.Update";
        public const string Timesheet_TimesheetProject_Delete = "Timesheet.TimesheetProject.Delete";
        public const string Timesheet_TimesheetProject_UploadFileTimesheetProject = "Timesheet.TimesheetProject.UploadFileTimesheetProject";
        public const string Timesheet_TimesheetProject_DownloadFileTimesheetProject = "Timesheet.TimesheetProject.DownloadFileTimesheetProject";
        public const string Timesheet_TimesheetProject_ViewInvoice = "Timesheet.TimesheetProject.ViewInvoice";
        public const string Timesheet_TimesheetProject_CreateInvoice = "Timesheet.TimesheetProject.CreateInvoice";
        public const string Timesheet_TimesheetProject_ExportInvoice = "Timesheet.TimesheetProject.ExportInvoice";
        public const string Timesheet_TimesheetProject_ViewMyProjectOnly = "Timesheet.TimesheetProject.ViewMyProjectOnly";
        public const string Timesheet_TimesheetProject_ViewAllProject = "Timesheet.TimesheetProject.ViewAllProject";

        //TimeSheetProjectBill
        public const string Timesheet_TimesheetProject_TimesheetProjectBill = "Timesheet.TimesheetProject.TimesheetProjectBill";
        public const string Timesheet_TimesheetProject_TimesheetProjectBill_GetAll = "Timesheet.TimesheetProject.TimesheetProjectBill.GetAll";
        public const string Timesheet_TimesheetProject_TimesheetProjectBill_Create = "Timesheet.TimesheetProject.TimesheetProjectBill.Create";
        public const string Timesheet_TimesheetProject_TimesheetProjectBill_Update = "Timesheet.TimesheetProject.TimesheetProjectBill.Update";
        public const string Timesheet_TimesheetProject_TimesheetProjectBill_ChangeUser = "Timesheet.TimesheetProject.TimesheetProjectBill.ChangeUser";
        public const string Timesheet_TimesheetProject_TimesheetProjectBill_UpdateFromProjectUserBill = "Timesheet.TimesheetProject.TimesheetProjectBill.UpdateFromProjectUserBill";
        public const string Timesheet_TimesheetProject_TimesheetProjectBill_UpdateOnlyMyProjectPM = "Timesheet.TimesheetProject.TimesheetProjectBill.UpdateOnlyMyProjectPM";

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
        public const string DeliveryManagement_ResourceRequest_ViewVendorResource = "DeliveryManagement.ResourceRequest.ViewVendorResource";

        public const string DeliveryManagement_ResourceRequest_AddUserToRequest = "DeliveryManagement.ResourceRequest.AddUserToRequest";
        public const string DeliveryManagement_ResourceRequest_SearchAvailableUserForRequest = "DeliveryManagement.ResourceRequest.SearchAvailableUserForRequest";
        public const string DeliveryManagement_ResourceRequest_AvailableResource = "DeliveryManagement.ResourceRequest.AvailableResource";
        public const string DeliveryManagement_ResourceRequest_AvailableResourceFuture = "DeliveryManagement.ResourceRequest.AvailableResourceFuture";
        public const string DeliveryManagement_ResourceRequest_PlanUser = "DeliveryManagement.ResourceRequest.PlanUser";
        public const string DeliveryManagement_ResourceRequest_CancelAnyPlanResource = "DeliveryManagement.ResourceRequest.CancelAnyPlanResource";
        public const string DeliveryManagement_ResourceRequest_CancelMyPlanOnly = "DeliveryManagement.ResourceRequest.CancelMyPlanOnly";

        public const string DeliveryManagement_ResourceRequest_ApproveUser = "DeliveryManagement.ResourceRequest.ApproveUser";
        public const string DeliveryManagement_ResourceRequest_RejectUser = "DeliveryManagement.ResourceRequest.RejectUser";
        public const string DeliveryManagement_ResourceRequest_Create = "DeliveryManagement.ResourceRequest.Create";
        public const string DeliveryManagement_ResourceRequest_Update = "DeliveryManagement.ResourceRequest.Update";
        public const string DeliveryManagement_ResourceRequest_Delete = "DeliveryManagement.ResourceRequest.Delete";
        public const string DeliveryManagement_ResourceRequest_GetProjectForDM = "DeliveryManagement.ResourceRequest.GetProjectForDM";
        public const string DeliveryManagement_ResourceRequest_CreateSkill = "DeliveryManagement.ResourceRequest.CreateSkill";
        public const string DeliveryManagement_ResourceRequest_DeleteSkill = "DeliveryManagement.ResourceRequest.DeleteSkill";
        public const string DeliveryManagement_ResourceRequest_GetSkillDetail = "DeliveryManagement.ResourceRequest.GetSkillDetail";

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



        public const string PmManager_ProjectUser_ConfirmMoveEmployeeToOtherProject = "PmManager.ProjectUser.ConfirmMoveEmployeeToOtherProject";
        public const string PmManager_ProjectUser_ConfirmPickUserFromPoolToProject = "PmManager.ProjectUser.ConfirmPickUserFromPoolToProject";

        public const string PmManager_ProjectUser_ProjectUser_MoveEmployeeToOtherProject = "PmManager.ProjectUser.ProjectUser.MoveEmployeeToOtherProject";
        public const string PmManager_ProjectUser_ProjectUser_PickUserFromPoolToProject = "PmManager.ProjectUser.ProjectUser.PickUserFromPoolToProject";




        public const string DeliveryManagement_ProjectUser_ConfirmMoveEmployeeToOtherProject = "DeliveryManagement.ProjectUser.ConfirmMoveEmployeeToOtherProject";
        public const string DeliveryManagement_ProjectUser_ConfirmPickUserFromPoolToProject = "DeliveryManagement.ProjectUser.ConfirmPickUserFromPoolToProject";

        public const string DeliveryManagement_ProjectUser_MoveEmployeeToOtherProject = "DeliveryManagement.ProjectUser.MoveEmployeeToOtherProject";
        public const string DeliveryManagement_ProjectUser_PickUserFromPoolToProject = "DeliveryManagement.ProjectUser.PickUserFromPoolToProject";





        //---------------------------------------------------------------------------------------------
        //Admin Tenants
        public const string Admin = "Admin";
        public const string Admin_Tenants = "Admin.Tenants";
        public const string Admin_Tenants_Create = "Admin.Tenants.Create";
        public const string Admin_Tenants_Edit = "Admin.Tenants.Edit";
        public const string Admin_Tenants_Delete = "Admin.Tenants.Delete";
        //Clients 
        public const string Admin_Clients = "Admin.Clients";
        public const string Admin_Clients_Create = "Admin.Clients.Create";
        public const string Admin_Clients_Edit = "Admin.Clients.Edit";
        public const string Admin_Clients_Delete = "Admin.Clients.Delete";
        //Configuration
        public const string Admin_Configuartions = "Admin.Configuartions";
        public const string Admin_Configuartions_Edit = "Admin.Configuartions.Edit";
        public const string Admin_Configuartions_ViewKomuSetting = "Admin.Configuartions.ViewKomuSetting";
        public const string Admin_Configuartions_ViewProjectSetting = "Admin.Configuartions.ViewProjectSetting";
        public const string Admin_Configuartions_ViewHrmSetting = "Admin.Configuartions.ViewHrmSetting";
        public const string Admin_Configuartions_ViewTimesheetSetting = "Admin.Configuartions.ViewTimesheetSetting";
        public const string Admin_Configuartions_ViewFinanceSetting = "Admin.Configuartions.ViewFinanceSetting";
        public const string Admin_Configuartions_ViewSendReportSetting = "Admin.Configuartions.ViewSendReportSetting";
        public const string Admin_Configuartions_ViewGoogleClientAppSetting = "Admin.Configuartions.ViewGoogleClientAppSetting";
        public const string Admin_Configuartions_ViewDefaultWorkingHourPerDaySetting = "Admin.Configuartions.ViewDefaultWorkingHourPerDaySetting";
        //Skill 
        public const string Admin_Skills = "Admin.Skill";
        public const string Admin_Skills_Create = "Admin.Skill.Create";
        public const string Admin_Skills_Edit = "Admin.Skill.Edit";
        public const string Admin_Skills_Delete = "Admin.Skill.Delete";
        //Currencies
        public const string Admin_Currencies = "Admin.Currencies";
        public const string Admin_Currencies_Create = "Admin.Currencies.Create";
        public const string Admin_Currencies_Edit = "Admin.Currencies.Edit";
        public const string Admin_Currenciess_Delete = "Admin.Currencies.Delete";
        //User
        public const string Admin_Users = "Admin.Users";
        public const string Admin_Users_Create = "Admin.Users.Create";
        public const string Admin_Users_SyncDataFromHrm = "Admin.Users.SyncDataFromHrm";
        public const string Admin_Users_ViewProjectHistory = "Admin.Users.ViewProjectHistory";
        public const string Admin_Users_Edit = "Admin.Users.Edit";
        public const string Admin_Users_UpdateSkill = "Admin.Users.UpdateSkill";
        public const string Admin_Users_UpdateRole = "Admin.Users.UpdateRole";
        public const string Admin_Users_ActiveAndDeactive = "Admin.Users.ActiveAndDeactive";
        public const string Admin_Users_UploadAvatar = "Admin.Users.UploadAvatar";
        public const string Admin_Users_ResetPassword = "Admin.Users.ResetPassword";
        //role
        public const string Admin_Roles = "Admin.Roles";
        public const string Admin_Roles_Create = "Admin.Roles.Create";
        public const string Admin_Roles_Edit = "Admin.Roles.Edit";
        public const string Admin_Roles_Delete = "Admin.Roles.Delete";

        //Project
        //Projects > Outsourcing Project
        public const string Projects = "Projects";
        public const string Projects_OutsourcingProjects = "Projects.OutsourcingProjects";
        public const string Projects_OutsourcingProjects_ViewAllProject = "Projects.OutsourcingProjects.ViewMyProjectOnly";
        public const string Projects_OutsourcingProjects_Create = "Projects.OutsourcingProjects.Create";
        public const string Projects_OutsourcingProjects_Edit = "Projects.OutsourcingProjects.Edit";
        public const string Projects_OutsourcingProjects_Delete = "Projects.OutsourcingProjects.Delete";
        public const string Projects_OutsourcingProjects_Close = "Projects.OutsourcingProjects.Close";
        public const string Projects_OutsourcingProjects_ProjectDetail = "Projects.OutsourcingProjects.ProjectDetail";

        public const string Projects_OutsourcingProjects_ProjectDetail_TabGeneral = "Projects.OutsourcingProjects.ProjectDetail.TabGeneral";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabGeneral_Edit = "Projects.OutsourcingProjects.ProjectDetail.TabGeneral.Edit";

        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement = "Projects.OutsourcingProjects.ProjectDetail.TabResourceManagement";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource = "Projects.OutsourcingProjects.ProjectDetail.CurrentResource";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_AddNewResource = "Projects.OutsourcingProjects.ProjectDetail.TabResourceManagement.CurrentResource.AddNewResource";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_ViewHistory = "Projects.OutsourcingProjects.ProjectDetail.TabResourceManagement.CurrentResource.ViewHistory";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_PickEmployeeFromPoolToProject = "Projects.OutsourcingProjects.ProjectDetail.TabResourceManagement.CurrentResource.PickEmployeeFromPoolToProject";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_MoveEmployeeWorkingOnAProjectToOther = "Projects.OutsourcingProjects.ProjectDetail.TabResourceManagement.CurrentResource.MoveEmployeeWorkingOnAProjectToOther";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_Edit = "Projects.OutsourcingProjects.ProjectDetail.TabResourceManagement.CurrentResource.Edit";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_Release = "Projects.OutsourcingProjects.ProjectDetail.TabResourceManagement.CurrentResource.Release";

        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource = "Projects.OutsourcingProjects.ProjectDetail.TabResourceManagement.PlannedResource";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_CreateNewPlan = "Projects.OutsourcingProjects.ProjectDetail.TabResourceManagement.PlannedResource.CreateNewPlan";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmPickEmployeeFromPoolToProject = "Projects.OutsourcingProjects.ProjectDetail.TabResourceManagement.PlannedResource.ConfirmPickEmployeeFromPoolToOther";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther = "Projects.OutsourcingProjects.ProjectDetail.TabResourceManagement.PlannedResource.ConfirmMoveEmployeeWorkingOnAProjectToOther";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmOut = "Projects.OutsourcingProjects.ProjectDetail.TabResourceManagement.PlannedResource.ConfirmOut";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_CancelPlan = "Projects.OutsourcingProjects.ProjectDetail.TabResourceManagement.PlannedResource.CancelPlan";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_Edit = "Projects.OutsourcingProjects.ProjectDetail.TabResourceManagement.PlannedResource.Edit";

        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest = "Projects.OutsourcingProjects.ProjectDetail.TabResourceManagement.ResourceRequest";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CreateNewRequest = "Projects.OutsourcingProjects.ProjectDetail.TabResourceManagement.ResourceRequest.CreateNewRequest";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_PlanNewResourceForRequest = "Projects.OutsourcingProjects.ProjectDetail.TabResourceManagement.ResourceRequest.PlanNewResourceForRequest";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SetDone = "Projects.OutsourcingProjects.ProjectDetail.TabResourceManagement.ResourceRequest.SetDone";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CancelRequest = "Projects.OutsourcingProjects.ProjectDetail.TabResourceManagement.ResourceRequest.CancelRequest";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Edit = "Projects.OutsourcingProjects.ProjectDetail.TabResourceManagement.ResourceRequest.Edit";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Delete = "Projects.OutsourcingProjects.ProjectDetail.TabResourceManagement.ResourceRequest.Delete";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SendRecruitment = "Projects.OutsourcingProjects.ProjectDetail.TabResourceManagement.ResourceRequest.SendRecruitment";

        public const string Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport = "Projects.OutsourcingProjects.ProjectDetail.TabWeeklyReport";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_UpdateNote = "Projects.OutsourcingProjects.ProjectDetail.UpdateNote";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_UpdateProjectHealth = "Projects.OutsourcingProjects.ProjectDetail.UpdateProjectHealth";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_SendWeeklyReport = "Projects.OutsourcingProjects.ProjectDetail.SendWeeklyReport";

        public const string Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue = "Projects.OutsourcingProjects.ProjectDetail.TabWeeklyReport.ProjectIssue";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_AddNewIssue = "Projects.OutsourcingProjects.ProjectDetail.TabWeeklyReport.ProjectIssue.AddNewIssue";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Edit = "Projects.OutsourcingProjects.ProjectDetail.TabWeeklyReport.ProjectIssue.Edit";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Delete = "Projects.OutsourcingProjects.ProjectDetail.TabWeeklyReport.ProjectIssue.Delete";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_SetDone = "Projects.OutsourcingProjects.ProjectDetail.TabWeeklyReport.ProjectIssue.SetDone";

        public const string Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_CurrentResource = "Projects.OutsourcingProjects.ProjectDetail.TabWeeklyReport.CurrentResource";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_CurrentResource_Release = "Projects.OutsourcingProjects.ProjectDetail.TabWeeklyReport.CurrentResource.Release";

        public const string Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource = "Projects.OutsourcingProjects.ProjectDetail.TabWeeklyReport.PlannedResource";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CreateNewPlan = "Projects.OutsourcingProjects.ProjectDetail.TabWeeklyReport.PlannedResource.CreateNewPlan";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_Edit = "Projects.OutsourcingProjects.ProjectDetail.TabWeeklyReport.PlannedResource.Edit";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmPickEmployeeFromPoolToProject = "Projects.OutsourcingProjects.ProjectDetail.TabWeeklyReport.PlannedResource.ConfirmPickEmployeeFromPoolToProject";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther = "Projects.OutsourcingProjects.ProjectDetail.TabWeeklyReport.PlannedResource.ConfirmMoveEmployeeWorkingOnAProjectToOther";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmOut = "Projects.OutsourcingProjects.ProjectDetail.TabWeeklyReport.PlannedResource.ConfirmOut";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CancelPlan = "Projects.OutsourcingProjects.ProjectDetail.TabWeeklyReport.PlannedResource.CancelPlan";

        public const string Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ChangedResource = "Projects.OutsourcingProjects.ProjectDetail.TabWeeklyReport.ChangedResource";

        public const string Projects_OutsourcingProjects_ProjectDetail_TabBillInfo = "Projects.OutsourcingProjects.ProjectDetail.TabBillInfo";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Create = "Projects.OutsourcingProjects.ProjectDetail.TabBillInfo.Create";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Edit = "Projects.OutsourcingProjects.ProjectDetail.TabBillInfo.Edit";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Delete = "Projects.OutsourcingProjects.ProjectDetail.TabBillInfo.Delete";

        public const string Projects_OutsourcingProjects_ProjectDetail_TabProjectDescription= "Projects.OutsourcingProjects.ProjectDetail.TabProjectDescription";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabProjectDescription_Edit = "Projects.OutsourcingProjects.ProjectDetail.TabProjectDescription.Edit";

        public const string Projects_OutsourcingProjects_ProjectDetail_TabProjectFile = "Projects.OutsourcingProjects.ProjectDetail.TabProjectFile";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabProjectFile_UploadFile = "Projects.OutsourcingProjects.ProjectDetail.TabProjectFile.UploadFile";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabProjectFile_DeleteFile = "Projects.OutsourcingProjects.ProjectDetail.TabProjectFile.DeleteFile";

        //Projects > Product Project
     
        public const string Projects_ProductProjects = "Projects.ProductProjects";
        public const string Projects_ProductProjects_ViewAllProject = "Projects.ProductProjects.ViewMyProjectOnly";
        public const string Projects_ProductProjects_Create = "Projects.ProductProjects.Create";
        public const string Projects_ProductProjects_Edit = "Projects.ProductProjects.Edit";
        public const string Projects_ProductProjects_Delete = "Projects.ProductProjects.Delete";
        public const string Projects_ProductProjects_Close = "Projects.ProductProjects.Close";
        public const string Projects_ProductProjects_ProjectDetail = "Projects.ProductProjects.ProjectDetail";

        public const string Projects_ProductProjects_ProjectDetail_TabGeneral = "Projects.ProductProjects.ProjectDetail.TabGeneral";
        public const string Projects_ProductProjects_ProjectDetail_TabGeneral_Edit = "Projects.ProductProjects.ProjectDetail.TabGeneral.Edit";

        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement = "Projects.ProductProjects.ProjectDetail.TabResourceManagement";
        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.CurrentResource";
        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_AddNewResource = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.CurrentResource.AddNewResource";
        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_ViewHistory = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.CurrentResource.ViewHistory";
        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_PickEmployeeFromPoolToProject = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.CurrentResource.PickEmployeeFromPoolToProject";
        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_PickEmployeeWorkingOnAProjectToOther = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.CurrentResource.PickEmployeeWorkingOnAProjectToOther";
        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_MoveEmployeeWorkingOnAProjectToOther = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.CurrentResource.MoveEmployeeWorkingOnAProjectToOther";
        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_Edit = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.CurrentResource.Edit";
        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_Release = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.CurrentResource.Release";

        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.PlannedResource";
        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_CreateNewPlan = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.PlannedResource.CreateNewPlan";
        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmPickEmployeeFromPoolToProject = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.PlannedResource.ConfirmPickEmployeeFromPoolToProject";
        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.PlannedResource.ConfirmMoveEmployeeWorkingOnAProjectToOther";
        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmOut = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.PlannedResource.ConfirmOut";
        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_CancelPlan = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.PlannedResource.CancelPlan";
        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_Edit = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.PlannedResource.Edit";

        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.ResourceRequest";
        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CreateNewRequest = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.ResourceRequest.CreateNewRequest";
        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_PlanNewResourceForRequest = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.ResourceRequest.PlanNewResourceForRequest";
        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SetDone = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.ResourceRequest.SetDone";
        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CancelRequest = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.ResourceRequest.CancelRequest";
        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Edit = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.ResourceRequest.Edit";
        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Delete = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.ResourceRequest.Delete";
        public const string Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SendRecruitment = "Projects.ProductProjects.ProjectDetail.TabResourceManagement.ResourceRequest.SendRecruitment";

        public const string Projects_ProductProjects_ProjectDetail_TabWeeklyReport = "Projects.ProductProjects.ProjectDetail.TabWeeklyReport";
        public const string Projects_ProductProjects_ProjectDetail_TabWeeklyReport_UpdateNote = "Projects.ProductProjects.ProjectDetail.UpdateNote";
        public const string Projects_ProductProjects_ProjectDetail_TabWeeklyReport_UpdateProjectHealth = "Projects.ProductProjects.ProjectDetail.UpdateProjectHealth";
        public const string Projects_ProductProjects_ProjectDetail_TabWeeklyReport_SendWeeklyReport = "Projects.ProductProjects.ProjectDetail.SendWeeklyReport";

        public const string Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectIssue = "Projects.ProductProjects.ProjectDetail.TabWeeklyReport.ProjectIssue";
        public const string Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_AddNewIssue = "Projects.ProductProjects.ProjectDetail.TabWeeklyReport.ProjectIssue.AddNewIssue";
        public const string Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Edit = "Projects.ProductProjects.ProjectDetail.TabWeeklyReport.ProjectIssue.Edit";
        public const string Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Delete = "Projects.ProductProjects.ProjectDetail.TabWeeklyReport.ProjectIssue.Delete";
        public const string Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_SetDone = "Projects.ProductProjects.ProjectDetail.TabWeeklyReport.ProjectIssue.SetDone";

        public const string Projects_ProductProjects_ProjectDetail_TabWeeklyReport_CurrentResource = "Projects.ProductProjects.ProjectDetail.TabWeeklyReport.CurrentResource";
        public const string Projects_ProductProjects_ProjectDetail_TabWeeklyReport_CurrentResource_Release = "Projects.ProductProjects.ProjectDetail.TabWeeklyReport.CurrentResource.Release";

        public const string Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource = "Projects.ProductProjects.ProjectDetail.TabWeeklyReport.PlannedResource";
        public const string Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CreateNewPlan = "Projects.ProductProjects.ProjectDetail.TabWeeklyReport.PlannedResource.CreateNewPlan";
        public const string Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_Edit = "Projects.ProductProjects.ProjectDetail.TabWeeklyReport.PlannedResource.Edit";
        public const string Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmPickEmployeeFromPoolToProject = "Projects.ProductProjects.ProjectDetail.TabWeeklyReport.PlannedResource.ConfirmPickEmployeeFromPoolToProject";
        public const string Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther = "Projects.ProductProjects.ProjectDetail.TabWeeklyReport.PlannedResource.ConfirmMoveEmployeeWorkingOnAProjectToOther";
        public const string Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmOut = "Projects.ProductProjects.ProjectDetail.TabWeeklyReport.PlannedResource.ConfirmOut";
        public const string Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CancelPlan = "Projects.ProductProjects.ProjectDetail.TabWeeklyReport.PlannedResource.CancelPlan";

        public const string Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ChangedResource = "Projects.ProductProjects.ProjectDetail.TabWeeklyReport.ChangedResource";

        public const string Projects_ProductProjects_ProjectDetail_TabBillInfo = "Projects.ProductProjects.ProjectDetail.TabBillInfo";
        public const string Projects_ProductProjects_ProjectDetail_TabBillInfo_Create = "Projects.ProductProjects.ProjectDetail.TabBillInfo.Create";
        public const string Projects_ProductProjects_ProjectDetail_TabBillInfo_Edit = "Projects.ProductProjects.ProjectDetail.TabBillInfo.Edit";
        public const string Projects_ProductProjects_ProjectDetail_TabBillInfo_Delete = "Projects.ProductProjects.ProjectDetail.TabBillInfo.Delete";

        public const string Projects_ProductProjects_ProjectDetail_TabProjectDescription = "Projects.ProductProjects.ProjectDetail.TabProjectDescription";
        public const string Projects_ProductProjects_ProjectDetail_TabProjectDescription_Edit = "Projects.ProductProjects.ProjectDetail.TabProjectDescription.Edit";

        public const string Projects_ProductProjects_ProjectDetail_TabProjectFile = "Projects.ProductProjects.ProjectDetail.TabProjectFile";
        public const string Projects_ProductProjects_ProjectDetail_TabProjectFile_UploadFile = "Projects.ProductProjects.ProjectDetail.TabProjectFile.UploadFile";
        public const string Projects_ProductProjects_ProjectDetail_TabProjectFile_DeleteFile = "Projects.ProductProjects.ProjectDetail.TabProjectFile.DeleteFile";


    //Projects > training Project
     
        public const string Projects_TrainingProjects = "Projects.TrainingProjects";
        public const string Projects_TrainingProjects_ViewAllProject = "Projects.TrainingProjects.ViewMyProjectOnly";
        public const string Projects_TrainingProjects_Create = "Projects.TrainingProjects.Create";
        public const string Projects_TrainingProjects_Edit = "Projects.TrainingProjects.Edit";
        public const string Projects_TrainingProjects_Delete = "Projects.TrainingProjects.Delete";
        public const string Projects_TrainingProjects_Close = "Projects.TrainingProjects.Close";
        public const string Projects_TrainingProjects_ProjectDetail = "Projects.TrainingProjects.ProjectDetail";

        public const string Projects_TrainingProjects_ProjectDetail_TabGeneral = "Projects.TrainingProjects.ProjectDetail.TabGeneral";
        public const string Projects_TrainingProjects_ProjectDetail_TabGeneral_Edit = "Projects.TrainingProjects.ProjectDetail.TabGeneral.Edit";

        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement";
        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.CurrentResource";
        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_AddNewResource = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.CurrentResource.AddNewResource";
        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_ViewHistory = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.CurrentResource.ViewHistory";
        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_PickEmployeeFromPoolToProject = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.CurrentResource.PickEmployeeFromPoolToProject";
        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_PickEmployeeWorkingOnAProjectToOther = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.CurrentResource.PickEmployeeWorkingOnAProjectToOther";
        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_MoveEmployeeWorkingOnAProjectToOther = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.CurrentResource.MoveEmployeeWorkingOnAProjectToOther";
        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_Edit = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.CurrentResource.Edit";
        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_Release = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.CurrentResource.Release";

        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.PlannedResource";
        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_CreateNewPlan = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.PlannedResource.CreateNewPlan";
        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmPickEmployeeFromPoolToProject = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.PlannedResource.ConfirmPickEmployeeFromPoolToProject";
        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.PlannedResource.ConfirmMoveEmployeeWorkingOnAProjectToOther";
        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmOut = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.PlannedResource.ConfirmOut";
        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_CancelPlan = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.PlannedResource.CancelPlan";
        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_Edit = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.PlannedResource.Edit";

        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.ResourceRequest";
        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CreateNewRequest = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.ResourceRequest.CreateNewRequest";
        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_PlanNewResourceForRequest = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.ResourceRequest.PlanNewResourceForRequest";
        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SetDone = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.ResourceRequest.SetDone";
        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CancelRequest = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.ResourceRequest.CancelRequest";
        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Edit = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.ResourceRequest.Edit";
        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Delete = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.ResourceRequest.Delete";
        public const string Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SendRecruitment = "Projects.TrainingProjects.ProjectDetail.TabResourceManagement.ResourceRequest.SendRecruitment";

        public const string Projects_TrainingProjects_ProjectDetail_TabWeeklyReport = "Projects.TrainingProjects.ProjectDetail.TabWeeklyReport";
        public const string Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_UpdateNote = "Projects.TrainingProjects.ProjectDetail.UpdateNote";
        public const string Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_UpdateProjectHealth = "Projects.TrainingProjects.ProjectDetail.UpdateProjectHealth";
        public const string Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_SendWeeklyReport = "Projects.TrainingProjects.ProjectDetail.SendWeeklyReport";

        public const string Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue = "Projects.TrainingProjects.ProjectDetail.TabWeeklyReport.ProjectIssue";
        public const string Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_AddNewIssue = "Projects.TrainingProjects.ProjectDetail.TabWeeklyReport.ProjectIssue.AddNewIssue";
        public const string Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Edit = "Projects.TrainingProjects.ProjectDetail.TabWeeklyReport.ProjectIssue.Edit";
        public const string Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Delete = "Projects.TrainingProjects.ProjectDetail.TabWeeklyReport.ProjectIssue.Delete";
        public const string Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_SetDone = "Projects.TrainingProjects.ProjectDetail.TabWeeklyReport.ProjectIssue.SetDone";

        public const string Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_CurrentResource = "Projects.TrainingProjects.ProjectDetail.TabWeeklyReport.CurrentResource";
        public const string Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_CurrentResource_Release = "Projects.TrainingProjects.ProjectDetail.TabWeeklyReport.CurrentResource.Release";

        public const string Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource = "Projects.TrainingProjects.ProjectDetail.TabWeeklyReport.PlannedResource";
        public const string Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CreateNewPlan = "Projects.TrainingProjects.ProjectDetail.TabWeeklyReport.PlannedResource.CreateNewPlan";
        public const string Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_Edit = "Projects.TrainingProjects.ProjectDetail.TabWeeklyReport.PlannedResource.Edit";
        public const string Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmPickEmployeeFromPoolToProject = "Projects.TrainingProjects.ProjectDetail.TabWeeklyReport.PlannedResource.ConfirmPickEmployeeFromPoolToProject";
        public const string Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther = "Projects.TrainingProjects.ProjectDetail.TabWeeklyReport.PlannedResource.ConfirmMoveEmployeeWorkingOnAProjectToOther";
        public const string Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmOut = "Projects.TrainingProjects.ProjectDetail.TabWeeklyReport.PlannedResource.ConfirmOut";
        public const string Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CancelPlan = "Projects.TrainingProjects.ProjectDetail.TabWeeklyReport.PlannedResource.CancelPlan";

        public const string Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ChangedResource = "Projects.TrainingProjects.ProjectDetail.TabWeeklyReport.ChangedResource";

        public const string Projects_TrainingProjects_ProjectDetail_TabBillInfo = "Projects.TrainingProjects.ProjectDetail.TabBillInfo";
        public const string Projects_TrainingProjects_ProjectDetail_TabBillInfo_Create = "Projects.TrainingProjects.ProjectDetail.TabBillInfo.Create";
        public const string Projects_TrainingProjects_ProjectDetail_TabBillInfo_Edit = "Projects.TrainingProjects.ProjectDetail.TabBillInfo.Edit";
        public const string Projects_TrainingProjects_ProjectDetail_TabBillInfo_Delete = "Projects.TrainingProjects.ProjectDetail.TabBillInfo.Delete";

        public const string Projects_TrainingProjects_ProjectDetail_TabProjectDescription = "Projects.TrainingProjects.ProjectDetail.TabProjectDescription";
        public const string Projects_TrainingProjects_ProjectDetail_TabProjectDescription_Edit = "Projects.TrainingProjects.ProjectDetail.TabProjectDescription.Edit";

        public const string Projects_TrainingProjects_ProjectDetail_TabProjectFile = "Projects.TrainingProjects.ProjectDetail.TabProjectFile";
        public const string Projects_TrainingProjects_ProjectDetail_TabProjectFile_UploadFile = "Projects.TrainingProjects.ProjectDetail.TabProjectFile.UploadFile";
        public const string Projects_TrainingProjects_ProjectDetail_TabProjectFile_DeleteFile = "Projects.TrainingProjects.ProjectDetail.TabProjectFile.DeleteFile";

        // Weekly report
        public const string WeeklyReport = "WeeklyReport";
        public const string WeeklyReport_CloseAndAddNew = "WeeklyReport.CloseAndAddNew";
        public const string WeeklyReport_CollectTimesheet = "WeeklyReport.CollectTimesheet";
        public const string WeeklyReport_Rename = "WeeklyReport.Rename";
        public const string WeeklyReport_View = "WeeklyReport.View";

        public const string WeeklyReport_ReportDetail = "WeeklyReport.ReportDetail";
        public const string WeeklyReport_ReportDetail_UpdateNote = "WeeklyReport.ReportDetail.UpdateNote";
        public const string WeeklyReport_ReportDetail_UpdateProjectHealth = "WeeklyReport.ReportDetail.UpdateProjectHealth";

        public const string WeeklyReport_ReportDetail_Issue = "WeeklyReport.ReportDetail.Issue";
        public const string WeeklyReport_ReportDetail_Issue_AddMeetingNote = "WeeklyReport.ReportDetail.Issue.AddMeetingNote";
        public const string WeeklyReport_ReportDetail_Issue_SetDone = "WeeklyReport.ReportDetail.Issue.SetDone";

        public const string WeeklyReport_ReportDetail_CurrentResource = "WeeklyReport.ReportDetail.CurrentResource";
        public const string WeeklyReport_ReportDetail_CurrentResource_Release = "WeeklyReport.ReportDetail.CurrentResource.Release";

        public const string WeeklyReport_ReportDetail_PlannedResource = "WeeklyReport.ReportDetail.PlannedResource";
        public const string WeeklyReport_ReportDetail_PlannedResource_CreateNewPlan = "WeeklyReport.ReportDetail.PlannedResource.CreateNewPlan";
        public const string WeeklyReport_ReportDetail_PlannedResource_Edit = "WeeklyReport.ReportDetail.PlannedResource.Edit";
        public const string WeeklyReport_ReportDetail_PlannedResource_ConfirmPickEmployeeFromPoolToProject = "WeeklyReport.ReportDetail.PlannedResource.ConfirmPickEmployeeFromPoolToProject";
        public const string WeeklyReport_ReportDetail_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther = "WeeklyReport.ReportDetail.PlannedResource.ConfirmMoveEmployeeWorkingOnAProjectToOther";
        public const string WeeklyReport_ReportDetail_PlannedResource_ConfirmOut = "WeeklyReport.ReportDetail.PlannedResource.ConfirmOut";
        public const string WeeklyReport_ReportDetail_PlannedResource_CancelPlan = "WeeklyReport.ReportDetail.PlannedResource.CancelPlan";

        public const string WeeklyReport_ReportDetail_ChangedResource = "WeeklyReport.ReportDetail.ChangedResource";

        public const string ResourceRequest = "ResourceRequest";
        public const string ResourceRequest_CreateNewRequest = "ResourceRequest.CreateNewRequest";
        public const string ResourceRequest_SetDone = "ResourceRequest.SetDone";
        public const string ResourceRequest_Cancel = "ResourceRequest.Cancel";
        public const string ResourceRequest_Edit = "ResourceRequest.Edit";
        public const string ResourceRequest_Delete = "ResourceRequest.Delete";
        public const string ResourceRequest_SendRecruitment = "ResourceRequest.SendRecruitment";

        //Resource
        public const string Resource = "Resource";
        public const string Resource_TabPool = "Resource.TabPool";
        public const string Resource_TabPool_ViewHistory = "Resource.TabPool.ViewHistory";
        public const string Resource_TabPool_CreatePlan = "Resource.TabPool.CreatePlan";
        public const string Resource_TabPool_EditPlan = "Resource.TabPool.EditPlan";
        public const string Resource_TabPool_ConfirmPickEmployeeFromPoolToProject = "Resource.TabPool.ConfirmPickEmployeeFromPoolToProject";
        public const string Resource_TabPool_ConfirmMoveEmployeeWorkingOnAProjectToOther = "Resource.TabPool.ConfirmMoveEmployeeWorkingOnAProjectToOther";
        public const string Resource_TabPool_ConfirmOut = "Resource.TabPool.ConfirmOut";
        public const string Resource_TabPool_CancelPlan = "Resource.TabPool.CancelPlan";
        public const string Resource_TabPool_EditTempProject = "Resource.TabPool.EditTempProject";
        public const string Resource_TabPool_UpdateSkill = "Resource.TabPool.UpdateSkill";
        public const string Resource_TabPool_EditNote = "Resource.TabPool.EditNote";

        public const string Resource_TabAllResource = "Resource.TabAllResource";
        public const string Resource_TabAllResource_ViewHistory = "Resource.TabAllResource.ViewHistory";
        public const string Resource_TabAllResource_CreatePlan = "Resource.TabAllResource.CreatePlan";
        public const string Resource_TabAllResource_EditPlan = "Resource.TabAllResource.EditPlan";
        public const string Resource_TabAllResource_ConfirmPickEmployeeFromPoolToProject = "Resource.TabAllResource.ConfirmPickEmployeeFromPoolToProject";
        public const string Resource_TabAllResource_ConfirmMoveEmployeeWorkingOnAProjectToOther = "Resource.TabAllResource.ConfirmMoveEmployeeWorkingOnAProjectToOther";
        public const string Resource_TabAllResource_ConfirmOut = "Resource.TabAllResource.ConfirmOut";
        public const string Resource_TabAllResource_CancelPlan = "Resource.TabAllResource.CancelPlan";
        public const string Resource_TabAllResource_UpdateSkill = "Resource.TabAllResource.UpdateSkill";

        public const string Resource_TabVendor = "Resource.TabVendor";
        public const string Resource_TabVendor_ViewHistory = "Resource.TabVendor.ViewHistory";
        public const string Resource_TabVendor_CreatePlan = "Resource.TabVendor.CreatePlan";
        public const string Resource_TabVendor_EditPlan = "Resource.TabVendor.EditPlan";
        public const string Resource_TabVendor_ConfirmPickEmployeeFromPoolToProject = "Resource.TabVendor.ConfirmPickEmployeeFromPoolToProject";
        public const string Resource_TabVendor_ConfirmMoveEmployeeWorkingOnAProjectToOther = "Resource.TabVendor.ConfirmMoveEmployeeWorkingOnAProjectToOther";
        public const string Resource_TabVendor_ConfirmOut = "Resource.TabVendor.ConfirmOut";
        public const string Resource_TabVendor_CancelPlan = "Resource.TabVendor.CancelPlan";
        public const string Resource_TabVendor_UpdateSkill = "Resource.TabVendor.UpdateSkill";

        //Timesheet
        public const string Timesheets = "Timesheets";
        public const string Timesheets_ViewList = "Timesheets.ViewList";
        public const string Timesheets_Create = "Timesheets.Create";
        public const string Timesheets_Edit = "Timesheets.Edit";
        public const string Timesheets_Delete = "Timesheets.Delete";
        public const string Timesheets_ForceDelete = "Timesheets.ForceDelete";
        public const string Timesheets_CloseAndActive = "Timesheets.CloseOrActive";

        public const string Timesheets_TimesheetDetail = "Timesheets.TimesheetDetail";
        public const string Timesheets_TimesheetDetail_AddProjectToTimesheet = "Timesheets.TimesheetDetail.AddProjectToTimesheet";
        public const string Timesheets_TimesheetDetail_UploadTimesheetFile = "Timesheets.TimesheetDetail.UploadTimesheetFile";
        public const string Timesheets_TimesheetDetail_ExportInvoice = "Timesheets.TimesheetDetail.ExportInvoice";
        public const string Timesheets_TimesheetDetail_UpdateNote = "Timesheets.TimesheetDetail.UpdateNote";
        public const string Timesheets_TimesheetDetail_Delete = "Timesheets.TimesheetDetail.Delete";

        public const string Timesheets_TimesheetDetail_UpdateBill = "Timesheets.TimesheetDetail.UpdateBill";
        public const string Timesheets_TimesheetDetail_UpdateBill_Create = "Timesheets.TimesheetDetail.UpdateBill.Create";
        public const string Timesheets_TimesheetDetail_UpdateBill_Edit = "Timesheets.TimesheetDetail.UpdateBill.Edit";
        public const string Timesheets_TimesheetDetail_UpdateBill_SetDone = "Timesheets.TimesheetDetail.UpdateBill.SetDone";
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
                    PermissionNames.PmManager_CanViewMenu_ProjectFile,

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
                            PermissionNames.Pages_Users_ViewOnlyMe,
                            PermissionNames.Pages_Users_Create,
                            PermissionNames.Pages_Users_Update,
                            PermissionNames.Pages_Users_UpdateMySkills,
                            PermissionNames.Pages_Users_Delete,
                            PermissionNames.Pages_Users_ImportUserFromFile,
                            PermissionNames.Pages_Users_UpdateAvatar,
                            PermissionNames.Pages_Users_AutoUpdateUserFromHRM,
                            PermissionNames.Pages_Users_UpdateStarRateFromTimesheet,

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

                    //Currency
                    PermissionNames.Admin_Currency,
                    PermissionNames.Admin_Currency_ViewAll,
                    PermissionNames.Admin_Currency_Create,
                    PermissionNames.Admin_Currency_Edit,
                    PermissionNames.Admin_Currency_Delete,

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
                    PermissionNames.PmManager_Project_ViewProjectInfor,
                    PermissionNames.PmManager_Project_Create,
                    PermissionNames.PmManager_Project_Update,
                    PermissionNames.PmManager_Project_Close,
                    PermissionNames.PmManager_Project_Delete,
                    PermissionNames.PmManager_Project_UpdateProjectDetail,

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
                    PermissionNames.Timesheet_TimesheetDetail_ViewTimesheetOfAllProject,
                    PermissionNames.Timesheet_TimesheetProject_GetAllRemainProjectInTimesheet,
                    PermissionNames.Timesheet_TimesheetDetail_ViewTimesheetAndBillInfoOfAllProject,
                    PermissionNames.Timesheet_TimesheetProject_Create,
                    PermissionNames.Timesheet_TimesheetProject_Update,
                    PermissionNames.Timesheet_TimesheetProject_Delete,
                    PermissionNames.Timesheet_TimesheetProject_UploadFileTimesheetProject,
                    PermissionNames.Timesheet_TimesheetProject_DownloadFileTimesheetProject,
                    PermissionNames.Timesheet_TimesheetProject_ViewInvoice,
                    PermissionNames.Timesheet_TimesheetProject_CreateInvoice,
                    PermissionNames.Timesheet_TimesheetProject_ExportInvoice,
                    PermissionNames.Timesheet_TimesheetProject_ViewMyProjectOnly,
                    PermissionNames.Timesheet_TimesheetProject_ViewAllProject,

                    //Timesheet Project Bill
                    PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill,
                    PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_GetAll,
                    PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_Create,
                    PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_Update,
                    PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_ChangeUser,
                    PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_UpdateFromProjectUserBill,
                    PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_UpdateOnlyMyProjectPM,

                    //ProjectFile
                    PermissionNames.PmManager_ProjectFile,
                    PermissionNames.PmManager_ProjectFile_ViewAllFiles,
                    PermissionNames.PmManager_ProjectFile_UploadNewFile,
                    PermissionNames.PmManager_ProjectFile_DeleteFile,


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
                    PermissionNames.PmManager_ProjectUser_ConfirmMoveEmployeeToOtherProject,
                    PermissionNames.PmManager_ProjectUser_ConfirmPickUserFromPoolToProject,
                    PermissionNames.PmManager_ProjectUser_ProjectUser_MoveEmployeeToOtherProject,
                    PermissionNames.PmManager_ProjectUser_ProjectUser_PickUserFromPoolToProject,
                     

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
                    PermissionNames.PmManager_ResourceRequest_CreateSkill,
                    PermissionNames.PmManager_ResourceRequest_DeleteSkill,
                    PermissionNames.PmManager_ResourceRequest_GetSkillDetail,

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
                    PermissionNames.DeliveryManagement_ResourceRequest_ViewVendorResource,

                    PermissionNames.DeliveryManagement_ResourceRequest_AddUserToRequest,
                    PermissionNames.DeliveryManagement_ResourceRequest_SearchAvailableUserForRequest,
                    PermissionNames.DeliveryManagement_ResourceRequest_AvailableResource,
                    PermissionNames.DeliveryManagement_ResourceRequest_AvailableResourceFuture,
                    PermissionNames.DeliveryManagement_ResourceRequest_PlanUser,
                    PermissionNames.DeliveryManagement_ResourceRequest_CancelAnyPlanResource,
                    PermissionNames.DeliveryManagement_ResourceRequest_CancelMyPlanOnly,

                    PermissionNames.DeliveryManagement_ResourceRequest_ApproveUser,
                    PermissionNames.DeliveryManagement_ResourceRequest_RejectUser,
                    PermissionNames.DeliveryManagement_ResourceRequest_Create,
                    PermissionNames.DeliveryManagement_ResourceRequest_Update,
                    PermissionNames.DeliveryManagement_ResourceRequest_Delete,
                    PermissionNames.DeliveryManagement_ResourceRequest_GetProjectForDM,
                    PermissionNames.DeliveryManagement_ResourceRequest_CreateSkill,
                    PermissionNames.DeliveryManagement_ResourceRequest_DeleteSkill,
                    PermissionNames.DeliveryManagement_ResourceRequest_GetSkillDetail,

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
                    PermissionNames.DeliveryManagement_ProjectUser_ConfirmMoveEmployeeToOtherProject,
                    PermissionNames.DeliveryManagement_ProjectUser_ConfirmPickUserFromPoolToProject,
                    PermissionNames.DeliveryManagement_ProjectUser_MoveEmployeeToOtherProject,
                    PermissionNames.DeliveryManagement_ProjectUser_PickUserFromPoolToProject,

                    //Project Milestone
                    PermissionNames.PmManager_ProjectMilestone,
                    PermissionNames.PmManager_ProjectMilestone_Create,
                    PermissionNames.PmManager_ProjectMilestone_Delete,
                    PermissionNames.PmManager_ProjectMilestone_Update,
                    PermissionNames.PmManager_ProjectMilestone_ViewAll,






                            //---------------------------------------------------------------------------------------------
                            //Admin Tenants
        PermissionNames.Admin,
        PermissionNames.Admin_Tenants,
        PermissionNames.Admin_Tenants_Create,
        PermissionNames.Admin_Tenants_Edit,
        PermissionNames.Admin_Tenants_Delete,
        //Clients 
        PermissionNames.Admin_Clients,
        PermissionNames.Admin_Clients_Create,
        PermissionNames.Admin_Clients_Edit,
        PermissionNames.Admin_Clients_Delete,
        //Configuration
        PermissionNames.Admin_Configuartions,
        PermissionNames.Admin_Configuartions_Edit,
        PermissionNames.Admin_Configuartions_ViewKomuSetting,
        PermissionNames.Admin_Configuartions_ViewProjectSetting ,
        PermissionNames.Admin_Configuartions_ViewHrmSetting ,
        PermissionNames.Admin_Configuartions_ViewTimesheetSetting ,
        PermissionNames.Admin_Configuartions_ViewFinanceSetting ,
        PermissionNames.Admin_Configuartions_ViewSendReportSetting ,
        PermissionNames.Admin_Configuartions_ViewGoogleClientAppSetting ,
        PermissionNames.Admin_Configuartions_ViewDefaultWorkingHourPerDaySetting ,
        //Skill 
        PermissionNames.Admin_Skills,
        PermissionNames.Admin_Skills_Create ,
        PermissionNames.Admin_Skills_Edit ,
        PermissionNames.Admin_Skills_Delete ,
        //Currencies
        PermissionNames.Admin_Currencies ,
        PermissionNames.Admin_Currencies_Create ,
        PermissionNames.Admin_Currencies_Edit ,
        PermissionNames.Admin_Currenciess_Delete ,
        //User
        PermissionNames.Admin_Users ,
        PermissionNames.Admin_Users_Create ,
        PermissionNames.Admin_Users_SyncDataFromHrm ,
        PermissionNames.Admin_Users_ViewProjectHistory ,
        PermissionNames.Admin_Users_Edit ,
        PermissionNames.Admin_Users_UpdateSkill ,
        PermissionNames.Admin_Users_UpdateRole ,
        PermissionNames.Admin_Users_ActiveAndDeactive ,
        PermissionNames.Admin_Users_UploadAvatar ,
        PermissionNames.Admin_Users_ResetPassword ,
        //role
        PermissionNames.Admin_Roles,
        PermissionNames.Admin_Roles_Create ,
        PermissionNames.Admin_Roles_Edit ,
        PermissionNames.Admin_Roles_Delete,

        //Project
        //Projects > Outsourcing Project
        PermissionNames.Projects ,
        PermissionNames.Projects_OutsourcingProjects ,
        PermissionNames.Projects_OutsourcingProjects_ViewAllProject ,
        PermissionNames.Projects_OutsourcingProjects_Create ,
        PermissionNames.Projects_OutsourcingProjects_Edit ,
        PermissionNames.Projects_OutsourcingProjects_Delete ,
        PermissionNames.Projects_OutsourcingProjects_Close ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail ,

        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabGeneral ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabGeneral_Edit ,

        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_AddNewResource ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_ViewHistory ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_PickEmployeeFromPoolToProject , 
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_MoveEmployeeWorkingOnAProjectToOther ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_Edit ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_Release ,

        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource , 
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_CreateNewPlan , 
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmPickEmployeeFromPoolToProject , 
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmOut ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_CancelPlan , 
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_Edit , 

        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest , 
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CreateNewRequest ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_PlanNewResourceForRequest , 
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SetDone ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CancelRequest , 
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Edit ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Delete ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SendRecruitment , 

        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_UpdateNote ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_UpdateProjectHealth ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_SendWeeklyReport ,

        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue , 
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_AddNewIssue , 
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Edit , 
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Delete , 
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_SetDone ,

        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_CurrentResource ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_CurrentResource_Release , 

        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CreateNewPlan ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_Edit ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmPickEmployeeFromPoolToProject ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmOut ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CancelPlan , 

        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ChangedResource , 

        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo , 
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Create ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Edit , 
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Delete , 

        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabProjectDescription , 
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabProjectDescription_Edit ,

        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabProjectFile , 
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabProjectFile_UploadFile ,
        PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabProjectFile_DeleteFile , 

        //Projects > Product Project

        PermissionNames.Projects_ProductProjects ,
        PermissionNames.Projects_ProductProjects_ViewAllProject , 
        PermissionNames.Projects_ProductProjects_Create , 
        PermissionNames.Projects_ProductProjects_Edit , 
        PermissionNames.Projects_ProductProjects_Delete , 
        PermissionNames.Projects_ProductProjects_Close , 
        PermissionNames.Projects_ProductProjects_ProjectDetail ,

        PermissionNames.Projects_ProductProjects_ProjectDetail_TabGeneral , 
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabGeneral_Edit , 

        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement ,
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource ,
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_AddNewResource ,
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_ViewHistory ,
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_PickEmployeeFromPoolToProject , 
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_PickEmployeeWorkingOnAProjectToOther , 
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_MoveEmployeeWorkingOnAProjectToOther ,
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_Edit ,
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_Release , 

        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource , 
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_CreateNewPlan , 
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmPickEmployeeFromPoolToProject ,
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther ,
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmOut , 
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_CancelPlan ,
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_Edit ,

        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest , 
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CreateNewRequest ,
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_PlanNewResourceForRequest ,
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SetDone , 
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CancelRequest , 
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Edit , 
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Delete ,
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SendRecruitment , 

        PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport , 
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_UpdateNote , 
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_UpdateProjectHealth ,
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_SendWeeklyReport , 

        PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectIssue , 
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_AddNewIssue , 
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Edit , 
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Delete , 
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_SetDone , 

        PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_CurrentResource ,
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_CurrentResource_Release , 

        PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource ,
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CreateNewPlan ,
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_Edit ,
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmPickEmployeeFromPoolToProject , 
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther ,
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmOut , 
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CancelPlan , 

        PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ChangedResource ,

        PermissionNames.Projects_ProductProjects_ProjectDetail_TabBillInfo ,
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabBillInfo_Create , 
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabBillInfo_Edit , 
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabBillInfo_Delete , 

        PermissionNames.Projects_ProductProjects_ProjectDetail_TabProjectDescription , 
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabProjectDescription_Edit , 

        PermissionNames.Projects_ProductProjects_ProjectDetail_TabProjectFile ,
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabProjectFile_UploadFile , 
        PermissionNames.Projects_ProductProjects_ProjectDetail_TabProjectFile_DeleteFile , 


        //Projects > training Project

        PermissionNames.Projects_TrainingProjects ,
        PermissionNames.Projects_TrainingProjects_ViewAllProject , 
        PermissionNames.Projects_TrainingProjects_Create , 
        PermissionNames.Projects_TrainingProjects_Edit , 
        PermissionNames.Projects_TrainingProjects_Delete , 
        PermissionNames.Projects_TrainingProjects_Close , 
        PermissionNames.Projects_TrainingProjects_ProjectDetail , 

        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabGeneral , 
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabGeneral_Edit ,

        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement ,
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource ,
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_AddNewResource , 
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_ViewHistory , 
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_PickEmployeeFromPoolToProject , 
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_PickEmployeeWorkingOnAProjectToOther , 
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_MoveEmployeeWorkingOnAProjectToOther , 
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_Edit ,
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_Release , 

        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource ,
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_CreateNewPlan ,
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmPickEmployeeFromPoolToProject ,
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther ,
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmOut ,
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_CancelPlan , 
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_Edit , 

        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest ,
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CreateNewRequest ,
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_PlanNewResourceForRequest , 
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SetDone ,
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CancelRequest , 
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Edit ,
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Delete , 
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SendRecruitment , 

        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport , 
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_UpdateNote ,
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_UpdateProjectHealth ,
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_SendWeeklyReport , 

        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue ,
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_AddNewIssue,
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Edit ,
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Delete , 
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_SetDone ,

        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_CurrentResource , 
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_CurrentResource_Release ,

        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource ,
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CreateNewPlan , 
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_Edit ,
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmPickEmployeeFromPoolToProject ,
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther , 
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmOut , 
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CancelPlan ,

        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ChangedResource , 

        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabBillInfo , 
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabBillInfo_Create , 
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabBillInfo_Edit , 
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabBillInfo_Delete , 

        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabProjectDescription ,
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabProjectDescription_Edit ,

        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabProjectFile , 
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabProjectFile_UploadFile , 
        PermissionNames.Projects_TrainingProjects_ProjectDetail_TabProjectFile_DeleteFile , 

        // Weekly report
        PermissionNames.WeeklyReport ,
        PermissionNames.WeeklyReport_CloseAndAddNew , 
        PermissionNames.WeeklyReport_CollectTimesheet , 
        PermissionNames.WeeklyReport_Rename , 
        PermissionNames.WeeklyReport_View , 

        PermissionNames.WeeklyReport_ReportDetail ,
        PermissionNames.WeeklyReport_ReportDetail_UpdateNote ,
        PermissionNames.WeeklyReport_ReportDetail_UpdateProjectHealth , 

        PermissionNames.WeeklyReport_ReportDetail_Issue , 
        PermissionNames.WeeklyReport_ReportDetail_Issue_AddMeetingNote ,
        PermissionNames.WeeklyReport_ReportDetail_Issue_SetDone ,

        PermissionNames.WeeklyReport_ReportDetail_CurrentResource ,
        PermissionNames.WeeklyReport_ReportDetail_CurrentResource_Release , 

        PermissionNames.WeeklyReport_ReportDetail_PlannedResource , 
        PermissionNames.WeeklyReport_ReportDetail_PlannedResource_CreateNewPlan , 
        PermissionNames.WeeklyReport_ReportDetail_PlannedResource_Edit , 
        PermissionNames.WeeklyReport_ReportDetail_PlannedResource_ConfirmPickEmployeeFromPoolToProject ,
        PermissionNames.WeeklyReport_ReportDetail_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther , 
        PermissionNames.WeeklyReport_ReportDetail_PlannedResource_ConfirmOut , 
        PermissionNames.WeeklyReport_ReportDetail_PlannedResource_CancelPlan , 

        PermissionNames.WeeklyReport_ReportDetail_ChangedResource , 

        PermissionNames.ResourceRequest , 
        PermissionNames.ResourceRequest_CreateNewRequest ,
        PermissionNames.ResourceRequest_SetDone , 
        PermissionNames.ResourceRequest_Cancel ,
        PermissionNames.ResourceRequest_Edit , 
        PermissionNames.ResourceRequest_Delete , 
        PermissionNames.ResourceRequest_SendRecruitment ,

        //Resource
        PermissionNames.Resource , 
        PermissionNames.Resource_TabPool , 
        PermissionNames.Resource_TabPool_ViewHistory , 
        PermissionNames.Resource_TabPool_CreatePlan , 
        PermissionNames.Resource_TabPool_EditPlan , 
        PermissionNames.Resource_TabPool_ConfirmPickEmployeeFromPoolToProject , 
        PermissionNames.Resource_TabPool_ConfirmMoveEmployeeWorkingOnAProjectToOther ,
        PermissionNames.Resource_TabPool_ConfirmOut , 
        PermissionNames.Resource_TabPool_CancelPlan , 
        PermissionNames.Resource_TabPool_EditTempProject ,
        PermissionNames.Resource_TabPool_UpdateSkill ,
        PermissionNames.Resource_TabPool_EditNote , 

        PermissionNames.Resource_TabAllResource , 
        PermissionNames.Resource_TabAllResource_ViewHistory , 
        PermissionNames.Resource_TabAllResource_CreatePlan , 
        PermissionNames.Resource_TabAllResource_EditPlan ,
        PermissionNames.Resource_TabAllResource_ConfirmPickEmployeeFromPoolToProject , 
        PermissionNames.Resource_TabAllResource_ConfirmMoveEmployeeWorkingOnAProjectToOther ,
        PermissionNames.Resource_TabAllResource_ConfirmOut ,
        PermissionNames.Resource_TabAllResource_CancelPlan , 
        PermissionNames.Resource_TabAllResource_UpdateSkill , 

        PermissionNames.Resource_TabVendor , 
        PermissionNames.Resource_TabVendor_ViewHistory ,
        PermissionNames.Resource_TabVendor_CreatePlan , 
        PermissionNames.Resource_TabVendor_EditPlan , 
        PermissionNames.Resource_TabVendor_ConfirmPickEmployeeFromPoolToProject , 
        PermissionNames.Resource_TabVendor_ConfirmMoveEmployeeWorkingOnAProjectToOther ,
        PermissionNames.Resource_TabVendor_ConfirmOut ,
        PermissionNames.Resource_TabVendor_CancelPlan ,
        PermissionNames.Resource_TabVendor_UpdateSkill ,

        //Timesheet
        PermissionNames.Timesheets , 
        PermissionNames.Timesheets_ViewList , 
        PermissionNames.Timesheets_Create ,
        PermissionNames.Timesheets_Edit , 
        PermissionNames.Timesheets_Delete , 
        PermissionNames.Timesheets_ForceDelete ,
        PermissionNames.Timesheets_CloseAndActive , 

        PermissionNames.Timesheets_TimesheetDetail ,
        PermissionNames.Timesheets_TimesheetDetail_AddProjectToTimesheet , 
        PermissionNames.Timesheets_TimesheetDetail_UploadTimesheetFile , 
        PermissionNames.Timesheets_TimesheetDetail_ExportInvoice ,
        PermissionNames.Timesheets_TimesheetDetail_UpdateNote , 
        PermissionNames.Timesheets_TimesheetDetail_Delete ,

        PermissionNames.Timesheets_TimesheetDetail_UpdateBill , 
        PermissionNames.Timesheets_TimesheetDetail_UpdateBill_Create ,
        PermissionNames.Timesheets_TimesheetDetail_UpdateBill_Edit ,
        PermissionNames.Timesheets_TimesheetDetail_UpdateBill_SetDone , 




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
                new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu_ProjectFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View PM Manager Menu ProjectFile" },
                new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu_PMCanCreate, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "PM Can Create ProjectUser" },
                new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu_PMCanUpdate, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "PM Can Update ProjectUser" },
                new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu_PMCanDelete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "PM Can Delete ProjectUser" },

                 //Admin
                 new SystemPermission{ Name =  PermissionNames.Pages_Roles, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Roles" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Tenants, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tenants" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Users, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Users" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Users_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All User" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Users_ViewOnlyMe, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Only Me" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Users_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create new User" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Users_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update User" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Users_UpdateMySkills, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update My Skills" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Users_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete User" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Users_ImportUserFromFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Import User From File" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Users_UpdateAvatar, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Avatar" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Users_AutoUpdateUserFromHRM, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update All User From HRM" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Users_UpdateStarRateFromTimesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Star Rating From Timesheet" },

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
                 
                 //Currency
                 new SystemPermission{ Name =  PermissionNames.Admin_Currency, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Currency" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Currency_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Currency" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Currency_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Currency" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Currency_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit One Currency" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Currency_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete One Currency" },

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
                 new SystemPermission{ Name =  PermissionNames.PmManager_Project_Close, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Close Project" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_Project_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Project" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_Project_UpdateProjectDetail, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update For Project Detail" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_Project_ViewProjectInfor, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Project Information" },

                 // TimeSheet
                 new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Timesheet" },
                 new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All" },
                 new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_Get, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Detail" },
                 new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create" },
                 new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update" },
                 new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete" },
                 new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_ForceDelete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Force Delete" },
                 new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_ReverseActive, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Reverse Active Timesheet" },
                 new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_DoneTimesheetById, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Done Timesheet" },

                 //Timesheet Project
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Timesheet Project" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_GetAllByproject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get All By Project" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetDetail_ViewTimesheetOfAllProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Timesheet Of All Project" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_ViewOnlyme, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Only Me" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_ViewOnlyActiveProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Only Active Project" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_GetAllRemainProjectInTimesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get All Remain Project In Timesheet" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetDetail_ViewTimesheetAndBillInfoOfAllProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Project Bill Infomation" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Timesheet Project" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Timesheet Project" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Timesheet Project" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_UploadFileTimesheetProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Upload File TimeSheet Project" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_DownloadFileTimesheetProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Download File TimeSheet Project" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_ViewInvoice, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View info Invoice" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_CreateInvoice, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Invoice To Finance" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_ExportInvoice, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Export Invoice Timesheet For One Project" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_ViewMyProjectOnly, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Only My Project" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_ViewAllProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Project" },

                 //Timesheet Project Bill
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "TimeSheet Project Bill" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_GetAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get All TimeSheet Project Bill" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New TimeSheet Project Bill" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Working Time For User" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_ChangeUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Change User" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_UpdateFromProjectUserBill, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update From Project User Bill" },
                  new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_UpdateOnlyMyProjectPM, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Only Update Project Of PM4444444444444444666666664444444444444444444444444444444444444466666666666666644444444444444444444444444444466666666666666666666666666666666666666644444444444444444444444444444444444444444444444" },

                  //Project User Bill
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project File" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectFile_ViewAllFiles, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Files" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectFile_UploadNewFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Upload New File" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectFile_DeleteFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete File" },

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
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUser_ConfirmMoveEmployeeToOtherProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm to move employee working on a project to other" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUser_ConfirmPickUserFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm to pick employee from POOL to project" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUser_ProjectUser_MoveEmployeeToOtherProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "move employee working on a project to other" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUser_ProjectUser_PickUserFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "pick employee from POOL to project" },






                  //ProjectUser by DM
                  new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project User" },
                  new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_ViewAllByProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get All By Project" },
                  new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_ViewDetailProjectUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Detail Project User" },
                  new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Project User" },
                  new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Project User" },
                  new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Project User" },

                  new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_ConfirmMoveEmployeeToOtherProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm to move employee working on a project to other" },
                  new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_ConfirmPickUserFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm to pick employee from POOL to project" },
                  new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_MoveEmployeeToOtherProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "move employee working on a project to other" },
                  new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_PickUserFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "pick employee from POOL to project" },



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
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_ViewVendorResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Vendor resource" },

                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_ViewDetailResourceRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Detail Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_AddUserToRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add User To Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_SearchAvailableUserForRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Available User For Request" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_AvailableResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Available Resource" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_AvailableResourceFuture, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Available Resource Future" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_PlanUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Plan For User" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_CancelAnyPlanResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Any Plan Resource" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_CancelMyPlanOnly, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel My Plan Resource Only" },


                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_ApproveUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Approve User" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_RejectUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Reject User" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_GetProjectForDM, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get Project For Delivery Management" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_CreateSkill, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Skill Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_DeleteSkill, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Skill Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_GetSkillDetail, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get Skill Detail" },
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
                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_CreateSkill, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Skill Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_DeleteSkill, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Skill Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_GetSkillDetail, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "GetSkillDetail Resource Request" },

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







                 new SystemPermission{ Name =  PermissionNames.Admin, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Admin" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Tenants, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tenants" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Tenants_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Tenants_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Tenants_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete" },

                 new SystemPermission{ Name =  PermissionNames.Admin_Clients, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Clients" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Clients_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Clients_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Clients_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete" },

                 new SystemPermission{ Name =  PermissionNames.Admin_Configuartions, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Configuartions" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Configuartions_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Configuartions_ViewKomuSetting, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "view Komu Setting" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Configuartions_ViewProjectSetting, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "view Project Setting" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Configuartions_ViewHrmSetting, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "view HRM Setting" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Configuartions_ViewTimesheetSetting, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "view Timesheet Setting" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Configuartions_ViewFinanceSetting , MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "view Finance Setting" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Configuartions_ViewSendReportSetting , MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "view Send Report Setting" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Configuartions_ViewGoogleClientAppSetting, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "view Google Client App Setting" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Configuartions_ViewDefaultWorkingHourPerDaySetting, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "view Default Working Hour Per Day Setting" },

                 new SystemPermission{ Name =  PermissionNames.Admin_Skills, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "skills" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Skills_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Skills_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Skills_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete" },

                 new SystemPermission{ Name =  PermissionNames.Admin_Currencies, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Currencies" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Currencies_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Currencies_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Currenciess_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete" },

                 new SystemPermission{ Name =  PermissionNames.Admin_Users, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Users" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Users_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Users_SyncDataFromHrm, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Sync Data From HRM" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Users_ViewProjectHistory, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Project History" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Users_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit User Info" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Users_UpdateSkill, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Skill" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Users_UpdateRole, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Role" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Users_ActiveAndDeactive, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Active/Deactive User" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Users_UploadAvatar, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Upload Avatar" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Users_ResetPassword, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Reset Password" },

                 new SystemPermission{ Name =  PermissionNames.Admin_Roles, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Roles" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Roles_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Roles_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Roles_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete" },

                 new SystemPermission{ Name =  PermissionNames.Projects, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Projects" },

                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Outsourcing Projects" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ViewAllProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Project" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_Close, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Close" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project Detail" },

                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabGeneral, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab General" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabGeneral_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },

                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Resource Management" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Current Resource" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_AddNewResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add New Resource" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_ViewHistory, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View History" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_PickEmployeeFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Pick Employee From POOL To Project" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_MoveEmployeeWorkingOnAProjectToOther, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Move Employee Working On A Project To Other" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_Release, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Release" },

                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Planned Resource" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_CreateNewPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Plan" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmPickEmployeeFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Pick Employee From POOL To Project" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "confirm Join: Move Employee Working On A Project To Other" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmOut, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Out" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_CancelPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Plan" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },

                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CreateNewRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Request" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_PlanNewResourceForRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Plan New Resource For Request" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SetDone, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Set Done" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CancelRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Request" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit Request" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Request" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SendRecruitment, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Send Recruitment" },

                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Weekly Report" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_UpdateNote, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update note" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_UpdateProjectHealth, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Project Health" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_SendWeeklyReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Send Report" },

                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project Issues" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_AddNewIssue, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add New Issue" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_SetDone, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Set Done" },

                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_CurrentResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Current Resource" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_CurrentResource_Release, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Release" },

                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Planned Resource" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CreateNewPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Plan" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmPickEmployeeFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Pick Employee From Pool To Project" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Move Employee Working On A Project To Other" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmOut, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Out" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CancelPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Plan" },

                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ChangedResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Changed Resource" },

                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Bill Info" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete" },

                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Project Description" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },


                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabProjectFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Project File" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabProjectFile_UploadFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Upload File" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabProjectFile_DeleteFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete File" },

                //  Product Project
                
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Product Projects" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ViewAllProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Project" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_Close, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Close" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project Detail" },

                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabGeneral, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab General" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabGeneral_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },

                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Resource Management" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Current Resource" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_AddNewResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add New Resource" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_ViewHistory, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View History" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_PickEmployeeFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Pick Employee From POOL To Project" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_MoveEmployeeWorkingOnAProjectToOther, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Move Employee Working On A Project To Other" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_Release, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Release" },

                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Planned Resource" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_CreateNewPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Plan" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmPickEmployeeFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Pick Employee From POOL To Project" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "confirm Join: Move Employee Working On A Project To Other" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmOut, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Out" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_CancelPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Plan" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },

                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CreateNewRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Request" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_PlanNewResourceForRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Plan New Resource For Request" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SetDone, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Set Done" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CancelRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Request" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit Request" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Request" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SendRecruitment, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Send Recruitment" },

                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Weekly Report" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_UpdateNote, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update note" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_UpdateProjectHealth, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Project Health" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_SendWeeklyReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Send Report" },

                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectIssue, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project Issues" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_AddNewIssue, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add New Issue" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_SetDone, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Set Done" },

                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_CurrentResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Current Resource" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_CurrentResource_Release, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Release" },

                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Planned Resource" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CreateNewPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Plan" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmPickEmployeeFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Pick Employee From Pool To Project" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Move Employee Working On A Project To Other" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmOut, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Out" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CancelPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Plan" },

                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ChangedResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Changed Resource" },

                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabBillInfo, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Bill Info" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabBillInfo_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabBillInfo_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabBillInfo_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete" },

                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabProjectDescription, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Project Description" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabProjectDescription_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },


                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabProjectFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Project File" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabProjectFile_UploadFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Upload File" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabProjectFile_DeleteFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete File" },



                //  Training Projects
                new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Product Projects" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ViewAllProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Project" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_Close, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Close" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project Detail" },

                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabGeneral, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab General" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabGeneral_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },

                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Resource Management" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Current Resource" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_AddNewResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add New Resource" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_ViewHistory, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View History" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_PickEmployeeFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Pick Employee From POOL To Project" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_MoveEmployeeWorkingOnAProjectToOther, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Move Employee Working On A Project To Other" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_Release, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Release" },

                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Planned Resource" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_CreateNewPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Plan" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmPickEmployeeFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Pick Employee From POOL To Project" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "confirm Join: Move Employee Working On A Project To Other" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmOut, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Out" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_CancelPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Plan" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },

                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Resource Request" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CreateNewRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Request" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_PlanNewResourceForRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Plan New Resource For Request" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SetDone, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Set Done" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CancelRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Request" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit Request" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Request" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SendRecruitment, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Send Recruitment" },

                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Weekly Report" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_UpdateNote, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update note" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_UpdateProjectHealth, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Project Health" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_SendWeeklyReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Send Report" },

                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project Issues" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_AddNewIssue, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add New Issue" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_SetDone, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Set Done" },

                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_CurrentResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Current Resource" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_CurrentResource_Release, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Release" },

                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Planned Resource" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CreateNewPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Plan" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmPickEmployeeFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Pick Employee From Pool To Project" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Move Employee Working On A Project To Other" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmOut, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Out" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CancelPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Plan" },

                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ChangedResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Changed Resource" },

                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabBillInfo, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Bill Info" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabBillInfo_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabBillInfo_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabBillInfo_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete" },

                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabBillInfo_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Project Description" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabBillInfo_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },


                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabProjectFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Project File" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabProjectFile_UploadFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Upload File" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabProjectFile_DeleteFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete File" },

                //  Weekly Report
                 new SystemPermission{ Name =  PermissionNames.WeeklyReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Weekly Report" },
                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_CloseAndAddNew, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Clost And Add New" },
                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_CollectTimesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Collect Timesheet" },
                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_Rename, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Rename" },
                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_View, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Wiew Info" },

                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Report Detail" },
                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_UpdateNote, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Note" },
                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_UpdateProjectHealth, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Project Health" },





























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
                                 new SystemPermission{ Name =  PermissionNames.Pages_Users_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All User"},
                                 new SystemPermission{ Name =  PermissionNames.Pages_Users_ViewOnlyMe, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Only Me" },
                                 new SystemPermission{ Name =  PermissionNames.Pages_Users_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create new User" },
                                 new SystemPermission{ Name =  PermissionNames.Pages_Users_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update User"},
                                 new SystemPermission{ Name =  PermissionNames.Pages_Users_UpdateMySkills, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update My Skills" },
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
                        //Currency
                        new SystemPermission { Name =  PermissionNames.Admin_Currency, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Currency",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.Admin_Currency_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Currency" },
                                 new SystemPermission{ Name =  PermissionNames.Admin_Currency_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Currency" },
                                 new SystemPermission{ Name =  PermissionNames.Admin_Currency_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit One Currency" },
                                 new SystemPermission{ Name =  PermissionNames.Admin_Currency_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete One Currency" },
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
                                    new SystemPermission{ Name =  PermissionNames.PmManager_CanViewMenu_ProjectFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View PM Manager Menu Project File" },
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
                                    new SystemPermission{ Name =  PermissionNames.PmManager_Project_Close, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Close Project" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_Project_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Project" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_Project_UpdateProjectDetail, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update For Project Detail" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_Project_ViewProjectInfor, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Project Information" },

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
                                   new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUser_ConfirmMoveEmployeeToOtherProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm to move employee working on a project to other" },
                                   new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUser_ConfirmPickUserFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm to pick employee from POOL to project" },
                                   new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUser_ProjectUser_MoveEmployeeToOtherProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "move employee working on a project to other" },
                                   new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUser_ProjectUser_PickUserFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "pick employee from POOL to project" },
                            }
                        },
                        //Project File
                        new SystemPermission { Name =  PermissionNames.PmManager_ProjectFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project File",
                            Childrens = new List<SystemPermission>()
                            {
                                    new SystemPermission{ Name =  PermissionNames.PmManager_ProjectFile_ViewAllFiles, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Files" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_ProjectFile_UploadNewFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Upload New File" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_ProjectFile_DeleteFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete File" },
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
                                new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_CreateSkill, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Skill Resource Request" },
                                new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_DeleteSkill, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Skill Resource Request" },
                                new SystemPermission{ Name =  PermissionNames.PmManager_ResourceRequest_GetSkillDetail, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get Skill Detail" },
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
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_ViewVendorResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Vendor Request" },

                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_ViewDetailResourceRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Detail Resource Request" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_AddUserToRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add User To Resource Request" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_SearchAvailableUserForRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Available User For Request" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_AvailableResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Available Resource" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_AvailableResourceFuture, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Available Resource Future" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_PlanUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Plan For User" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_CancelAnyPlanResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Any Plan Resource" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_CancelMyPlanOnly, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel My Plan Only" },


                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_ApproveUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Approve User" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_RejectUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Reject User" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Resource Request" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Resource Request" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Resource Request" },
                                new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_CreateSkill, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Skill Resource Request" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_DeleteSkill, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Skill Resource Request" },
                                 new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ResourceRequest_GetSkillDetail, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get Skill Detail" },
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

                                   new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_ConfirmMoveEmployeeToOtherProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm to move employee working on a project to other" },
                                   new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_ConfirmPickUserFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm to pick employee from POOL to project" },
                                   new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_MoveEmployeeToOtherProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "move employee working on a project to other" },
                                   new SystemPermission{ Name =  PermissionNames.DeliveryManagement_ProjectUser_PickUserFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "pick employee from POOL to project" },
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
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_Get, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Detail" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Force Delete" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_ReverseActive, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Reverse Active Timesheet" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_Timesheet_DoneTimesheetById, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Done Timesheet" },
                            }
                        },
                       //Timesheet Project
                       new SystemPermission { Name =  PermissionNames.Timesheet_TimesheetProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "TimeSheet Project",
                            Childrens = new List<SystemPermission>()
                            {
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_GetAllByproject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get All By Project" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetDetail_ViewTimesheetOfAllProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Timesheet Of All Project" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_ViewOnlyme, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Only Me" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_ViewOnlyActiveProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Only Active Project" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_GetAllRemainProjectInTimesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get All Remain Project In Timesheet" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetDetail_ViewTimesheetAndBillInfoOfAllProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Project Bill Infomation" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Timesheet Project" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Timesheet Project" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Timesheet Project" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_UploadFileTimesheetProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Upload File TimeSheet Project" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_DownloadFileTimesheetProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Download File TimeSheet Project" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_ViewInvoice, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View info Invoice" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_CreateInvoice, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Invoice To Finance" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_ExportInvoice, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Export Invoice Timesheet For One Project" },
                                    new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "TimeSheet Project Bill",
                                    //timesheet bill info
                                    Childrens = new List<SystemPermission>()
                                    {
                                          new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_GetAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get All TimeSheet Project Bill" },
                                          new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New TimeSheet Project Bill" },
                                          new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Working Time For User" },
                                          new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_ChangeUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Change User" },
                                          new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_UpdateFromProjectUserBill, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update From Project User Bill" },
                                          new SystemPermission{ Name =  PermissionNames.Timesheet_TimesheetProject_TimesheetProjectBill_UpdateOnlyMyProjectPM, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Only Update Project Of PM" },
                                    }
                                    },
                                    new SystemPermission{ Name = PermissionNames.Timesheet_TimesheetProject_ViewMyProjectOnly, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Only My Project"},
                                    new SystemPermission{ Name = PermissionNames.Timesheet_TimesheetProject_ViewAllProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Project"}
                            }
                        }
                    }
                },
            };

        }
    }
}
