﻿using Abp.MultiTenancy;
using System.Collections.Generic;
using static ProjectManagement.Authorization.Roles.StaticRoleNames;

namespace ProjectManagement.Authorization
{
    public static class PermissionNames
    {
        //Admin 
        public const string Admin = "Admin";
        //Tenants
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
        public const string Admin_Skills = "Admin.Skills";
        public const string Admin_Skills_Create = "Admin.Skills.Create";
        public const string Admin_Skills_Edit = "Admin.Skills.Edit";
        public const string Admin_Skills_Delete = "Admin.Skills.Delete";
        //Currencies
        public const string Admin_Currencies = "Admin.Currencies";
        public const string Admin_Currencies_Create = "Admin.Currencies.Create";
        public const string Admin_Currencies_Edit = "Admin.Currencies.Edit";
        public const string Admin_Currencies_Delete = "Admin.Currencies.Delete";
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
        public const string Admin_Users_DeleteFakeUser = "Admin.Users.DeleteFakeUser";

        //role
        public const string Admin_Roles = "Admin.Roles";
        public const string Admin_Roles_Create = "Admin.Roles.Create";
        public const string Admin_Roles_Edit = "Admin.Roles.Edit";
        public const string Admin_Roles_Delete = "Admin.Roles.Delete";

        //Project
        //Projects > Outsourcing Project
        public const string Projects = "Projects";
        public const string Projects_OutsourcingProjects = "Projects.OutsourcingProjects";
        public const string Projects_OutsourcingProjects_ViewAllProject = "Projects.OutsourcingProjects.ViewAllProject";
        public const string Projects_OutsourcingProjects_ViewMyProjectOnly = "Projects.OutsourcingProjects.ViewMyProjectOnly";
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

        public const string Projects_OutsourcingProjects_ProjectDetail_TabProjectDescription = "Projects.OutsourcingProjects.ProjectDetail.TabProjectDescription";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabProjectDescription_Edit = "Projects.OutsourcingProjects.ProjectDetail.TabProjectDescription.Edit";

        public const string Projects_OutsourcingProjects_ProjectDetail_TabProjectFile = "Projects.OutsourcingProjects.ProjectDetail.TabProjectFile";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabProjectFile_UploadFile = "Projects.OutsourcingProjects.ProjectDetail.TabProjectFile.UploadFile";
        public const string Projects_OutsourcingProjects_ProjectDetail_TabProjectFile_DeleteFile = "Projects.OutsourcingProjects.ProjectDetail.TabProjectFile.DeleteFile";

        //Projects > Product Project

        public const string Projects_ProductProjects = "Projects.ProductProjects";
        public const string Projects_ProductProjects_ViewAllProject = "Projects.ProductProjects.ViewAllProject";
        public const string Projects_ProductProjects_ViewMyProjectOnly = "Projects.ProductProjects.ViewMyProjectOnly";

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
        public const string Projects_TrainingProjects_ViewAllProject = "Projects.TrainingProjects.ViewAllProject";
        public const string Projects_TrainingProjects_ViewMyProjectOnly = "Projects.TrainingProjects.ViewMyProjectOnly";
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

        //Resource Request 
        public const string ResourceRequest = "ResourceRequest";
        public const string ResourceRequest_CreateNewRequest = "ResourceRequest.CreateNewRequest";
        public const string ResourceRequest_PlanNewResourceForRequest = "ResourceRequest.PlanNewResourceForRequest";
        public const string ResourceRequest_UpdateResourceRequestPlan = "ResourceRequest.UpdateResourceRequestPlan";
        public const string ResourceRequest_RemoveResouceRequestPlan = "ResourceRequest.RemoveResouceRequestPlan";
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
        public const string Resource_TabPool_CancelMyPlan = "Resource.TabPool.CancelMyPlan";
        public const string Resource_TabPool_CancelAnyPlan = "Resource.TabPool.CancelAnyPlan";
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
        public const string Resource_TabAllResource_CancelMyPlan = "Resource.TabAllResource.CancelMyPlan";
        public const string Resource_TabAllResource_CancelAnyPlan = "Resource.TabAllResource.CancelAnyPlan";

        public const string Resource_TabAllResource_UpdateSkill = "Resource.TabAllResource.UpdateSkill";

        public const string Resource_TabVendor = "Resource.TabVendor";
        public const string Resource_TabVendor_ViewHistory = "Resource.TabVendor.ViewHistory";
        public const string Resource_TabVendor_CreatePlan = "Resource.TabVendor.CreatePlan";
        public const string Resource_TabVendor_EditPlan = "Resource.TabVendor.EditPlan";
        public const string Resource_TabVendor_ConfirmPickEmployeeFromPoolToProject = "Resource.TabVendor.ConfirmPickEmployeeFromPoolToProject";
        public const string Resource_TabVendor_ConfirmMoveEmployeeWorkingOnAProjectToOther = "Resource.TabVendor.ConfirmMoveEmployeeWorkingOnAProjectToOther";
        public const string Resource_TabVendor_ConfirmOut = "Resource.TabVendor.ConfirmOut";
        public const string Resource_TabVendor_CancelMyPlan = "Resource.TabVendor.CancelMyPlan";
        public const string Resource_TabVendor_CancelAnyPlan = "Resource.TabVendor.CancelAnyPlan";
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
        public const string Timesheets_TimesheetDetail_ViewAll = "Timesheets.TimesheetDetail.ViewAll";
        public const string Timesheets_TimesheetDetail_ViewMyProjectOnly = "Timesheets.TimesheetDetail.ViewMyProjectOnly";


        public const string Timesheets_TimesheetDetail_ViewBillRate = "Timesheets.TimesheetDetail.ViewBillRate";
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
                    PermissionNames.Admin_Currencies_Delete ,
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
                    PermissionNames.Admin_Users_DeleteFakeUser ,

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
                    PermissionNames.Projects_OutsourcingProjects_ViewMyProjectOnly ,

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
                    PermissionNames.Projects_ProductProjects_ViewMyProjectOnly ,

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
                    PermissionNames.Projects_TrainingProjects_ViewMyProjectOnly ,
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
                    PermissionNames.ResourceRequest_PlanNewResourceForRequest ,
                    PermissionNames.ResourceRequest_UpdateResourceRequestPlan ,
                    PermissionNames.ResourceRequest_RemoveResouceRequestPlan ,
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
                    PermissionNames.Resource_TabPool_CancelMyPlan ,
                    PermissionNames.Resource_TabPool_CancelAnyPlan ,
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
                    PermissionNames.Resource_TabAllResource_CancelMyPlan ,
                    PermissionNames.Resource_TabAllResource_CancelAnyPlan ,

                    PermissionNames.Resource_TabAllResource_UpdateSkill ,

                    PermissionNames.Resource_TabVendor ,
                    PermissionNames.Resource_TabVendor_ViewHistory ,
                    PermissionNames.Resource_TabVendor_CreatePlan ,
                    PermissionNames.Resource_TabVendor_EditPlan ,
                    PermissionNames.Resource_TabVendor_ConfirmPickEmployeeFromPoolToProject ,
                    PermissionNames.Resource_TabVendor_ConfirmMoveEmployeeWorkingOnAProjectToOther ,
                    PermissionNames.Resource_TabVendor_ConfirmOut ,
                    PermissionNames.Resource_TabVendor_CancelMyPlan ,
                    PermissionNames.Resource_TabVendor_CancelAnyPlan ,
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
                    PermissionNames.Timesheets_TimesheetDetail_ViewAll ,
                    PermissionNames.Timesheets_TimesheetDetail_ViewMyProjectOnly ,

                    PermissionNames.Timesheets_TimesheetDetail_ViewBillRate ,
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
                 new SystemPermission{ Name =  PermissionNames.Admin_Currencies_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete" },

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
                 new SystemPermission{ Name =  PermissionNames.Admin_Users_DeleteFakeUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Fake User" },


                 new SystemPermission{ Name =  PermissionNames.Admin_Roles, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Roles" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Roles_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Roles_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Roles_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete" },

                 new SystemPermission{ Name =  PermissionNames.Projects, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Projects" },

                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Outsourcing Projects" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ViewAllProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Project" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ViewMyProjectOnly ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View My Project Only" },

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

                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabProjectDescription, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Project Description" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabProjectDescription_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },


                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabProjectFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Project File" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabProjectFile_UploadFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Upload File" },
                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabProjectFile_DeleteFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete File" },

                //  Product Project
                
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Product Projects" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ViewAllProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Project" },
                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ViewMyProjectOnly, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View My Project Only" },
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
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ViewMyProjectOnly, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View My Project Only" },
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

                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabProjectDescription, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Project Description" },
                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabProjectDescription_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },


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

                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_Issue, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Issues" },
                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_Issue_AddMeetingNote, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add Meeting Note" },
                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_Issue_SetDone, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Set Done" },

                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_CurrentResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Current Resource" },
                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_CurrentResource_Release, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Release" },

                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_PlannedResource ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Planned Resource" },
                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_PlannedResource_CreateNewPlan,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Plan" },
                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_PlannedResource_Edit ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit Plan" },
                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_PlannedResource_ConfirmPickEmployeeFromPoolToProject ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Pick Employee From POOL To Project" },
                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Move Employee Working On A Project To Other" },
                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_PlannedResource_ConfirmOut ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Out" },
                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_PlannedResource_CancelPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Plan" },

                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_ChangedResource ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Changed Resource"},

                 new SystemPermission{ Name =  PermissionNames.ResourceRequest ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Resource Request"},
                 new SystemPermission{ Name =  PermissionNames.ResourceRequest_CreateNewRequest ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Request"},
                 new SystemPermission{ Name =  PermissionNames.ResourceRequest_PlanNewResourceForRequest ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Plan New Resource For Request"},
                 new SystemPermission{ Name =  PermissionNames.ResourceRequest_UpdateResourceRequestPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Plan"},
                 new SystemPermission{ Name =  PermissionNames.ResourceRequest_RemoveResouceRequestPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Remove Plan"},

                 new SystemPermission{ Name =  PermissionNames.ResourceRequest_SetDone ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Set Done"},
                 new SystemPermission{ Name =  PermissionNames.ResourceRequest_Cancel ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel"},
                 new SystemPermission{ Name =  PermissionNames.ResourceRequest_Edit ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                 new SystemPermission{ Name =  PermissionNames.ResourceRequest_Delete ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},
                 new SystemPermission{ Name =  PermissionNames.ResourceRequest_SendRecruitment ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Send Recruitment"},


                 new SystemPermission{ Name =  PermissionNames.Resource ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Resource"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabPool ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Pool"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabPool_ViewHistory ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View History"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabPool_CreatePlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Plan"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabPool_EditPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit Plan"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabPool_ConfirmPickEmployeeFromPoolToProject ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Pick Employee From POOL to Project"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabPool_ConfirmMoveEmployeeWorkingOnAProjectToOther ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Move Employee Working On A Project To Other"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabPool_ConfirmOut ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Out"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabPool_CancelMyPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel My Plan Only"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabPool_CancelAnyPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Any Plan"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabPool_EditTempProject ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit Temp Project"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabPool_UpdateSkill ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Skill"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabPool_EditNote ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit Note"},

                 new SystemPermission{ Name =  PermissionNames.Resource_TabAllResource ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "All Resource"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabAllResource_ViewHistory ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View History"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabAllResource_CreatePlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Plan"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabAllResource_EditPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit Plan"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabAllResource_ConfirmPickEmployeeFromPoolToProject ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Pick Employee From POOL To Project"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabAllResource_ConfirmMoveEmployeeWorkingOnAProjectToOther ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Move Employee Working On A Project To Other"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabAllResource_ConfirmOut ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Out"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabAllResource_CancelMyPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel My Plan Only"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabAllResource_CancelAnyPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Any Plan"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabAllResource_UpdateSkill ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Skill"},

                 new SystemPermission{ Name =  PermissionNames.Resource_TabVendor ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "All Resource"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabVendor_ViewHistory ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View History"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabVendor_CreatePlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Plan"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabVendor_EditPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit Plan"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabVendor_ConfirmPickEmployeeFromPoolToProject ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Pick Employee From POOL To Project"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabVendor_ConfirmMoveEmployeeWorkingOnAProjectToOther ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Move Employee Working On A Project To Other"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabVendor_ConfirmOut ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Out"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabVendor_CancelMyPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel My Plan Only"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabVendor_CancelAnyPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Any Plan"},
                 new SystemPermission{ Name =  PermissionNames.Resource_TabVendor_UpdateSkill ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Skill"},

                 new SystemPermission{ Name =  PermissionNames.Timesheets ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Timesheet"},
                 new SystemPermission{ Name =  PermissionNames.Timesheets_ViewList ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View List"},
                 new SystemPermission{ Name =  PermissionNames.Timesheets_Create ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create"},
                 new SystemPermission{ Name =  PermissionNames.Timesheets_Edit ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                 new SystemPermission{ Name =  PermissionNames.Timesheets_Delete ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},
                 new SystemPermission{ Name =  PermissionNames.Timesheets_ForceDelete ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Force Delete"},
                 new SystemPermission{ Name =  PermissionNames.Timesheets_CloseAndActive ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Close/Active"},

                 new SystemPermission{ Name =  PermissionNames.Timesheets_TimesheetDetail ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Timesheet Detail"},
                 new SystemPermission{ Name =  PermissionNames.Timesheets_TimesheetDetail_ViewAll ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Timesheet View All Project Timesheet"},
                 new SystemPermission{ Name =  PermissionNames.Timesheets_TimesheetDetail_ViewMyProjectOnly ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Timesheet View My Project Only"},
                 new SystemPermission{ Name =  PermissionNames.Timesheets_TimesheetDetail_ViewBillRate ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Bill Rate"},
                 new SystemPermission{ Name =  PermissionNames.Timesheets_TimesheetDetail_AddProjectToTimesheet ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add Project To Timesheet"},
                 new SystemPermission{ Name =  PermissionNames.Timesheets_TimesheetDetail_UploadTimesheetFile ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Upload Timesheet File"},
                 new SystemPermission{ Name =  PermissionNames.Timesheets_TimesheetDetail_ExportInvoice ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Export Invoice"},
                 new SystemPermission{ Name =  PermissionNames.Timesheets_TimesheetDetail_UpdateNote ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Note"},
                 new SystemPermission{ Name =  PermissionNames.Timesheets_TimesheetDetail_Delete ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},

                 new SystemPermission{ Name =  PermissionNames.Timesheets_TimesheetDetail_UpdateBill ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},
                 new SystemPermission{ Name =  PermissionNames.Timesheets_TimesheetDetail_UpdateBill_Create ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},
                 new SystemPermission{ Name =  PermissionNames.Timesheets_TimesheetDetail_UpdateBill_Edit ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},
                 new SystemPermission{ Name =  PermissionNames.Timesheets_TimesheetDetail_UpdateBill_SetDone ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},

            };
            public static List<SystemPermission> TreePermissions = new List<SystemPermission>()
            {

                //Admin
                new SystemPermission { Name =  PermissionNames.Admin, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Admin",
                    Childrens = new List<SystemPermission>() {

                         new SystemPermission { Name =  PermissionNames.Admin_Tenants, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tenants",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.Admin_Tenants_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Tenants_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                                 new SystemPermission{ Name =  PermissionNames.Admin_Tenants_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete" },
                            }
                        },
                          new SystemPermission { Name =  PermissionNames.Admin_Clients, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Clients",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.Admin_Clients_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Clients_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit" },
                                 new SystemPermission{ Name =  PermissionNames.Admin_Clients_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete" },
                            }
                        },
                          new SystemPermission { Name =  PermissionNames.Admin_Configuartions, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Configuration",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.Admin_Configuartions_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Configuartions_ViewKomuSetting, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Komu Setting"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Configuartions_ViewProjectSetting, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Project Setting"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Configuartions_ViewHrmSetting, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Hrm Setting"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Configuartions_ViewTimesheetSetting, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Timesheet Setting"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Configuartions_ViewFinanceSetting, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Finance Setting"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Configuartions_ViewSendReportSetting, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Send Report Setting"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Configuartions_ViewGoogleClientAppSetting, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Google Client App Setting"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Configuartions_ViewDefaultWorkingHourPerDaySetting, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Default Working Hour Per Day Setting"},

                            }
                        },

                            new SystemPermission { Name =  PermissionNames.Admin_Skills, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Skills",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.Admin_Skills_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Skills_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Komu Setting"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Skills_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Project Setting"},
                            }
                        },
                               new SystemPermission { Name =  PermissionNames.Admin_Currencies, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Currencies",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.Admin_Currencies_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Currencies_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Currencies_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},
                            }
                        },
                                new SystemPermission { Name =  PermissionNames.Admin_Users, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Users",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.Admin_Users_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Users_SyncDataFromHrm, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Sync User From HRM"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Users_ViewProjectHistory, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Project History"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Users_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Users_UpdateSkill, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Skill"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Users_UpdateRole, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Role"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Users_ActiveAndDeactive, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Active/Deactive"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Users_UploadAvatar, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Upload Avatar"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Users_ResetPassword, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Reset Password"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Users_DeleteFakeUser, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Fake User"},

                            }
                        },
                                new SystemPermission { Name =  PermissionNames.Admin_Roles, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Roles",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.Admin_Roles_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Roles_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                 new SystemPermission{ Name =  PermissionNames.Admin_Roles_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},

                            }
                        },
                    },
                },
                // Projects
                 new SystemPermission { Name =  PermissionNames.Projects, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Projects",
                    Childrens = new List<SystemPermission>() {

                         new SystemPermission { Name =  PermissionNames.Projects_OutsourcingProjects, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Outsourcing Projects",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ViewAllProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Project"},
                                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ViewMyProjectOnly, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View My Project Only"},
                                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create"},
                                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},
                                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_Close, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "close"},
                                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project Detail",
                                 Childrens = new List<SystemPermission>()
                                 {
                                    new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabGeneral, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab General",
                                     Childrens = new List<SystemPermission>(){
                                        new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabGeneral_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project Detail",
                                        }

                                    },
                                 },

                                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Resource Management",
                                     Childrens = new List<SystemPermission>(){
                                        new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Current Resources",
                                        Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_AddNewResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add New Resource"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_ViewHistory, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View History"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_PickEmployeeFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Pick Employee From POOL To Project"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_MoveEmployeeWorkingOnAProjectToOther, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Move Employee Working On A Project To Other"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_Edit,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource_Release, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Release"},
                                        }
                                        },

                                           new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource , MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Planned Resources",
                                             Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_CreateNewPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Plan"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmPickEmployeeFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Pick Employee From POOL To Project"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Move Employee Working On A Project To Other"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmOut, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "ConfirmOut"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_CancelPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Plan"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                        }
                                        },
                                          new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Resource Request",
                                             Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CreateNewRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Request"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_PlanNewResourceForRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Plan New Resource For Request"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SetDone, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Set Done"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CancelRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Request"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SendRecruitment, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Send Recruitment"},
                                        }
                                        },

                                    },

                                 },

                                     new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Weekly report",
                                     Childrens = new List<SystemPermission>(){
                                        new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_UpdateNote, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Note"},
                                        new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_UpdateProjectHealth, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Project Health"},
                                        new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_SendWeeklyReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Send Report"},
                                        new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_CurrentResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project Issues",
                                        Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_AddNewIssue, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit "},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_SetDone,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Set Done"},
                                        },
                                        },
                                         new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_CurrentResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Current Resources",
                                            Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_CurrentResource_Release, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Release"},
                                        },
                                        },
                                           new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabResourceManagement_PlannedResource , MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Planned Resources",
                                             Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CreateNewPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Plan"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmPickEmployeeFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Pick Employee From POOL To Project"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Move Employee Working On A Project To Other"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmOut, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "ConfirmOut"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CancelPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Plan"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                        }
                                        },
                                          new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabWeeklyReport_ChangedResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Changed Resources",
},

                                    },
                                 },
                                 new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Bill Info",
                                     Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabBillInfo_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},
                                     }
                                 },

                                   new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabProjectDescription, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Project Description",
                                     Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabProjectDescription_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                     }
                                 },
                                   new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabProjectFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Project File",
                                     Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabProjectFile_UploadFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Upload File"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_OutsourcingProjects_ProjectDetail_TabProjectFile_DeleteFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete File"},
                                     }
                                 }

                            },
                            }
                        },
                    },

       new SystemPermission { Name =  PermissionNames.Projects_ProductProjects, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Product Projects",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ViewAllProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Project"},
                                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ViewMyProjectOnly, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View My Project Only"},
                                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create"},
                                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},
                                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_Close, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "close"},
                                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project Detail",
                                 Childrens = new List<SystemPermission>()
                                 {
                                    new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabGeneral, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab General",
                                     Childrens = new List<SystemPermission>(){
                                        new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabGeneral_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project Detail",
                                        }

                                    },
                                 },

                                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Resource Management",
                                     Childrens = new List<SystemPermission>(){
                                        new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Current Resources",
                                        Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_AddNewResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add New Resource"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_ViewHistory, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View History"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_PickEmployeeFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Pick Employee From POOL To Project"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_MoveEmployeeWorkingOnAProjectToOther, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Move Employee Working On A Project To Other"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_Edit,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource_Release, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Release"},
                                        }
                                        },

                                           new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource , MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Planned Resources",
                                             Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_CreateNewPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Plan"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmPickEmployeeFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Pick Employee From POOL To Project"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Move Employee Working On A Project To Other"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmOut, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "ConfirmOut"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_CancelPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Plan"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                        }
                                        },
                                          new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Resource Request",
                                             Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CreateNewRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Request"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_PlanNewResourceForRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Plan New Resource For Request"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SetDone, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Set Done"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CancelRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Request"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SendRecruitment, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Send Recruitment"},
                                        }
                                        },

                                    },

                                 },


                                     new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Weekly report",
                                     Childrens = new List<SystemPermission>(){
                                        new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_UpdateNote, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Note"},
                                        new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_UpdateProjectHealth, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Project Health"},
                                        new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_SendWeeklyReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Send Report"},
                                        new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_CurrentResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project Issues",
                                        Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_AddNewIssue, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit "},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_SetDone,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Set Done"},
                                        },
                                        },
                                         new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_CurrentResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Current Resources",
                                            Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_CurrentResource_Release, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Release"},
                                        },
                                        },
                                           new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabResourceManagement_PlannedResource , MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Planned Resources",
                                             Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CreateNewPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Plan"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmPickEmployeeFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Pick Employee From POOL To Project"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Move Employee Working On A Project To Other"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmOut, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "ConfirmOut"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CancelPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Plan"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_PlannedResource_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                        }
                                        },
                                          new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabWeeklyReport_ChangedResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Changed Resources",
},

                                    },
                                 },
                                 new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabBillInfo, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Bill Info",
                                     Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabBillInfo_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabBillInfo_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabBillInfo_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},
                                     }
                                 },

                                   new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabProjectDescription, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Project Description",
                                     Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabProjectDescription_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                     }
                                 },
                                   new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabProjectFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Project File",
                                     Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabProjectFile_UploadFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Upload File"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_ProductProjects_ProjectDetail_TabProjectFile_DeleteFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete File"},
                                     }
                                 }

                            },
                            }
                        },
                    },

         new SystemPermission { Name =  PermissionNames.Projects_TrainingProjects, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Outsourcing Projects",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ViewAllProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Project"},
                                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ViewMyProjectOnly, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View My Project Only"},
                                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create"},
                                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},
                                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_Close, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "close"},
                                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project Detail",
                                 Childrens = new List<SystemPermission>()
                                 {
                                    new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabGeneral, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab General",
                                     Childrens = new List<SystemPermission>(){
                                        new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabGeneral_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project Detail",
                                        }

                                    },
                                 },

                                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Resource Management",
                                     Childrens = new List<SystemPermission>(){
                                        new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Current Resources",
                                        Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_AddNewResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add New Resource"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_ViewHistory, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View History"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_PickEmployeeFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Pick Employee From POOL To Project"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_MoveEmployeeWorkingOnAProjectToOther, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Move Employee Working On A Project To Other"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_Edit,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource_Release, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Release"},
                                        }
                                        },

                                           new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource , MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Planned Resources",
                                             Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_CreateNewPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Plan"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmPickEmployeeFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Pick Employee From POOL To Project"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Move Employee Working On A Project To Other"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_ConfirmOut, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "ConfirmOut"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_CancelPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Plan"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                        }
                                        },
                                          new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Resource Request",
                                             Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CreateNewRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Request"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_PlanNewResourceForRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Plan New Resource For Request"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SetDone, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Set Done"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_CancelRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Request"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_ResourceRequest_SendRecruitment, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Send Recruitment"},
                                        }
                                        },

                                    },

                                 },


                                     new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Weekly report",
                                     Childrens = new List<SystemPermission>(){
                                        new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_UpdateNote, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Note"},
                                        new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_UpdateProjectHealth, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Project Health"},
                                        new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_SendWeeklyReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Send Report"},
                                        new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_CurrentResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project Issues",
                                        Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_AddNewIssue, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit "},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ProjectIssue_SetDone,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Set Done"},
                                        },
                                        },
                                         new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_CurrentResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Current Resources",
                                            Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_CurrentResource_Release, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Release"},
                                        },
                                        },
                                           new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabResourceManagement_PlannedResource , MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Planned Resources",
                                             Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CreateNewPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Plan"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmPickEmployeeFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Pick Employee From POOL To Project"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Move Employee Working On A Project To Other"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_ConfirmOut, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "ConfirmOut"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_CancelPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Plan"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_PlannedResource_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                        }
                                        },
                                          new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabWeeklyReport_ChangedResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Changed Resources",
},

                                    },
                                 },
                                 new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabBillInfo, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Bill Info",
                                     Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabBillInfo_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabBillInfo_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabBillInfo_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},
                                     }
                                 },

                                   new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabProjectDescription, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Project Description",
                                     Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabProjectDescription_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                     }
                                 },
                                   new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabProjectFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Project File",
                                     Childrens = new List<SystemPermission>(){
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabProjectFile_UploadFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Upload File"},
                                             new SystemPermission{ Name =  PermissionNames.Projects_TrainingProjects_ProjectDetail_TabProjectFile_DeleteFile, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete File"},
                                     }
                                 }
                            },
                            }
                        },
                    },
                },
        },

       new SystemPermission { Name =  PermissionNames.WeeklyReport, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Weekly Report",
                    Childrens = new List<SystemPermission>() {
                        new SystemPermission { Name =  PermissionNames.WeeklyReport_CloseAndAddNew, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Close And Add New"},
                        new SystemPermission { Name =  PermissionNames.WeeklyReport_CollectTimesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Collect Timesheet"},
                        new SystemPermission { Name =  PermissionNames.WeeklyReport_Rename, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Rename"},
                        new SystemPermission { Name =  PermissionNames.WeeklyReport_View, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View"},
                         new SystemPermission { Name =  PermissionNames.WeeklyReport_ReportDetail, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Report Detail",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_UpdateNote, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Note"},
                                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_UpdateProjectHealth, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Project Health" },
                                 new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_Issue, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Issues",
                                    Childrens = new List<SystemPermission>(){
                                        new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_Issue_AddMeetingNote, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add Meeting Note"},
                                        new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_Issue_SetDone, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Set Done"},
                                    }
                                  },
                                    new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_CurrentResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Current Resource",
                                    Childrens = new List<SystemPermission>(){
                                        new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_CurrentResource_Release, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Release"},

                                    }
                                  },  new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_PlannedResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Planned Resource",
                                    Childrens = new List<SystemPermission>(){
                                        new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_PlannedResource_CreateNewPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Plan"},
                                        new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_PlannedResource_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                        new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_PlannedResource_ConfirmPickEmployeeFromPoolToProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Pick Employee From POOL To Project"},
                                        new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_PlannedResource_ConfirmMoveEmployeeWorkingOnAProjectToOther, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Move Employee Working On A Project To Other"},
                                        new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_PlannedResource_ConfirmOut, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Out "},
                                        new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_PlannedResource_CancelPlan, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Plan"},

                                    }
                                  },
                                    new SystemPermission{ Name =  PermissionNames.WeeklyReport_ReportDetail_CurrentResource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Changed Resource"},
                            }
                        },

                    },
                },

       new SystemPermission { Name =  PermissionNames.ResourceRequest, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Resource Request",
                    Childrens = new List<SystemPermission>() {
                        new SystemPermission { Name =  PermissionNames.ResourceRequest_CreateNewRequest ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Request"},
                        new SystemPermission { Name =  PermissionNames.ResourceRequest_PlanNewResourceForRequest ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Plan New Resource For Request"},
                        new SystemPermission { Name =  PermissionNames.ResourceRequest_UpdateResourceRequestPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Plan"},
                        new SystemPermission { Name =  PermissionNames.ResourceRequest_RemoveResouceRequestPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Remove Plan"},
                        new SystemPermission { Name =  PermissionNames.ResourceRequest_SetDone ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Set Done"},
                        new SystemPermission { Name =  PermissionNames.ResourceRequest_Cancel ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel"},
                        new SystemPermission { Name =  PermissionNames.ResourceRequest_Edit ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                        new SystemPermission { Name =  PermissionNames.ResourceRequest_Delete ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},
                        new SystemPermission { Name =  PermissionNames.ResourceRequest_SendRecruitment ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Send Recruitment"},
                    },
                },


                   new SystemPermission { Name =  PermissionNames.Resource, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Resource",
                    Childrens = new List<SystemPermission>() {
                        // Tab Pool
                        new SystemPermission { Name =  PermissionNames.Resource_TabPool ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Pool",
                             Childrens = new List<SystemPermission>() {
                                new SystemPermission { Name =  PermissionNames.Resource_TabPool_ViewHistory ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View History"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabPool_CreatePlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Plan"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabPool_EditPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit Plan"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabPool_ConfirmPickEmployeeFromPoolToProject ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Pick Employee From POOL To Project"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabPool_ConfirmMoveEmployeeWorkingOnAProjectToOther ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Move Employee Working On Project To Other"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabPool_ConfirmOut ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Out"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabPool_CancelMyPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel My Plan Only"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabPool_CancelAnyPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Any Plan"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabPool_EditTempProject ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit Temp Project"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabPool_UpdateSkill ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update skill"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabPool_EditNote ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit Note"},
                    },
                },

                // Tab All Resource
                     new SystemPermission { Name =  PermissionNames.Resource_TabAllResource,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab All Resource",
                             Childrens = new List<SystemPermission>() {
                                new SystemPermission { Name =  PermissionNames.Resource_TabAllResource_ViewHistory ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View History"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabAllResource_CreatePlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Plan"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabAllResource_EditPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit Plan"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabAllResource_ConfirmPickEmployeeFromPoolToProject ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Pick Employee From POOL To Project"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabAllResource_ConfirmMoveEmployeeWorkingOnAProjectToOther ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Move Employee Working On Project To Other"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabAllResource_ConfirmOut ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Out"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabAllResource_CancelMyPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel My Plan Only"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabAllResource_CancelAnyPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Any Plan"},

                                new SystemPermission { Name =  PermissionNames.Resource_TabAllResource_UpdateSkill ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update skill"},
                    },
                },
                // Tab Vendor
                     new SystemPermission { Name =  PermissionNames.Resource_TabVendor ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tab Vendor",
                             Childrens = new List<SystemPermission>() {
                                new SystemPermission { Name =  PermissionNames.Resource_TabVendor_ViewHistory ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View History"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabVendor_CreatePlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Plan"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabVendor_EditPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit Plan"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabVendor_ConfirmPickEmployeeFromPoolToProject ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Pick Employee From POOL To Project"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabVendor_ConfirmMoveEmployeeWorkingOnAProjectToOther ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Join: Move Employee Working On Project To Other"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabVendor_ConfirmOut ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Confirm Out"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabVendor_CancelMyPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel My Plan Only"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabVendor_CancelAnyPlan ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Cancel Any Plan"},
                                new SystemPermission { Name =  PermissionNames.Resource_TabVendor_UpdateSkill ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update skill"},
                    },
                },
                    }
},
                    new SystemPermission { Name =  PermissionNames.Timesheets ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Timesheet",
                             Childrens = new List<SystemPermission>() {
                                new SystemPermission { Name =  PermissionNames.Timesheets_ViewList ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View List"},
                                new SystemPermission { Name =  PermissionNames.Timesheets_Create ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create"},
                                new SystemPermission { Name =  PermissionNames.Timesheets_Edit ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                new SystemPermission { Name =  PermissionNames.Timesheets_Delete ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},
                                new SystemPermission { Name =  PermissionNames.Timesheets_ForceDelete ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Force Delete"},
                                new SystemPermission { Name =  PermissionNames.Timesheets_CloseAndActive ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Close/Active"},

                                new SystemPermission { Name =  PermissionNames.Timesheets_TimesheetDetail,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Timesheet Detail",
                                    Childrens = new List<SystemPermission>() {
                                        new SystemPermission { Name =  PermissionNames.Timesheets_TimesheetDetail_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Timesheet Project"},
                                        new SystemPermission { Name =  PermissionNames.Timesheets_TimesheetDetail_ViewMyProjectOnly, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View My Project Only"},
                                        new SystemPermission { Name =  PermissionNames.Timesheets_TimesheetDetail_ViewBillRate, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Bill Rate"},
                                        new SystemPermission { Name =  PermissionNames.Timesheets_TimesheetDetail_AddProjectToTimesheet ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Add Project To Timesheet"},
                                        new SystemPermission { Name =  PermissionNames.Timesheets_TimesheetDetail_UploadTimesheetFile ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Upload Timesheet File"},
                                        new SystemPermission { Name =  PermissionNames.Timesheets_TimesheetDetail_ExportInvoice ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Export Invoice"},
                                        new SystemPermission { Name =  PermissionNames.Timesheets_TimesheetDetail_UpdateNote ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Note"},
                                        new SystemPermission { Name =  PermissionNames.Timesheets_TimesheetDetail_Delete ,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete"},


                                    }

                                },
                                new SystemPermission { Name =  PermissionNames.Timesheets_TimesheetDetail_UpdateBill,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Bill",
                                                                    Childrens = new List<SystemPermission>() {
                                        new SystemPermission { Name =  PermissionNames.Timesheets_TimesheetDetail_UpdateBill_Create,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create"},
                                        new SystemPermission { Name =  PermissionNames.Timesheets_TimesheetDetail_UpdateBill_Edit,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit"},
                                        new SystemPermission { Name =  PermissionNames.Timesheets_TimesheetDetail_UpdateBill_SetDone,MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Set Done"},
                                                                    }
                                },


                    },
                }
        };
        }
    }
}
