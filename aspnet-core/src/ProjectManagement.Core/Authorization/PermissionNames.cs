using Abp.MultiTenancy;
using System.Collections.Generic;
using static ProjectManagement.Authorization.Roles.StaticRoleNames;

namespace ProjectManagement.Authorization
{
    public static class PermissionNames
    {     
        //Admin
        public const string Admin = "Admin";
             public const string Pages_Tenants = "Pages.Tenants";
            public const string Pages_Users = "Pages.Users";
            public const string Pages_Roles = "Pages.Roles";
            //Client
            public const string Admin_Client = "Admin.Client";
            public const string Admin_Client_ViewAll = "Admin.Client.ViewAll";
            public const string Admin_Client_Create = "Admin.Client.Create";
            public const string Admin_Client_Edit = "Admin.Client.Edit";
            public const string Admin_Client_Delete = "Admin.Client.Delete";

        //Pm Manager
        public const string PmManager = "PmManager";
            //Project
            public const string PmManager_Project = "PmManager.Project";
            public const string PmManager_Project_ViewAll = "PmManager.Project.ViewAll";
            public const string PmManager_Project_ViewDetail = "PmManager.Project.ViewDetail";
            public const string PmManager_Project_Create = "PmManager.Project.Create";
            public const string PmManager_Project_Update = "PmManager.Project.Update";
            public const string PmManager_Project_Delete = "PmManager.Project.Delete";

            //Timesheet
            public const string PmManager_Timesheet = "PmManager.Timesheet";
            public const string PmManager_Timesheet_ViewAll = "PmManager.Timesheet.ViewAll";
            public const string PmManager_Timesheet_Get = "PmManager.Timesheet.Get";
            public const string PmManager_Timesheet_GetTimesheetDetail = "PmManager.Timesheet.GetTimesheetDetail";
            public const string PmManager_Timesheet_Create = "PmManager.Timesheet.Create";
            public const string PmManager_Timesheet_Update = "PmManager.Timesheet.Update";
            public const string PmManager_Timesheet_Delete = "PmManager.Timesheet.Delete";
            public const string PmManager_Timesheet_DoneTimesheetById = "PmManager.Timesheet.DoneTimesheetById";

            //TimesheetProject
            public const string PmManager_TimesheetProject = "PmManager.TimesheetProject";
            public const string PmManager_TimesheetProject_GetAllByproject = "PmManager.TimesheetProject.GetAllByProject";
            public const string PmManager_TimesheetProject_GetAllRemainProjectInTimesheet = "PmManager.TimesheetProject.GetAllRemainProjectInTimesheet";
            public const string PmManager_TimesheetProject_Create = "PmManager.TimesheetProject.Create";
            public const string PmManager_TimesheetProject_Update = "PmManager.TimesheetProject.Update";
            public const string PmManager_TimesheetProject_Delete = "PmManager.TimesheetProject.Delete";

            //Project User Bill
            public const string PmManager_ProjectUserBill = "PmManager.ProjectUserBill";
            public const string PmManager_ProjectUserBill_GetAllPaging = "PmManager.ProjectUserBill.GetAllPaging";
            public const string PmManager_ProjectUserBill_GetAllByproject = "PmManager.ProjectUserBill.GetAllbyProject";
            public const string PmManager_ProjectUserBill_Create = "PmManager.ProjectUserBill.Create";
            public const string PmManager_ProjectUserBill_Update = "PmManager.ProjectUserBill.Update";
            public const string PmManager_ProjectUserBill_Delete = "PmManager.ProjectUserBill.Delete";

        //Sao Do
        public const string SaoDo = "SaoDo";
            // Check List Category
            public const string SaoDo_CheckListCategory = "SaoDo.CheckListCategory";
            public const string SaoDo_CheckListCategory_ViewAll = "SaoDo.CheckListCategory.ViewAll";
            public const string SaoDo_CheckListCategory_Create = "SaoDo.CheckListCategory.Create";
            public const string SaoDo_CheckListCategory_Update = "SaoDo.CheckListCategory.Update";
            public const string SaoDo_CheckListCategory_Delete = "SaoDo.CheckListCategory.Delete";
            // Check List Item
            public const string SaoDo_CheckListItem = "SaoDo.CheckListItem";
            public const string SaoDo_CheckListItem_ViewAll = "SaoDo.CheckListItem.ViewAll";
            public const string SaoDo_CheckListItem_Create = "SaoDo.CheckListItem.Create";
            public const string SaoDo_CheckListItem_Update = "SaoDo.CheckListItem.Update";
            public const string SaoDo_CheckListItem_Delete = "SaoDo.CheckListItem.Delete";
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

                    PermissionNames.Pages_Users,
                    PermissionNames.Pages_Tenants,
                    PermissionNames.Pages_Roles,

                    // Client
                    PermissionNames.Admin_Client,
                    PermissionNames.Admin_Client_ViewAll,
                    PermissionNames.Admin_Client_Create,
                    PermissionNames.Admin_Client_Edit,
                    PermissionNames.Admin_Client_Delete,

                    // Project
                    PermissionNames.PmManager_Project,
                    PermissionNames.PmManager_Project_ViewAll,
                    PermissionNames.PmManager_Project_ViewDetail,
                    PermissionNames.PmManager_Project_Create,
                    PermissionNames.PmManager_Project_Update,
                    PermissionNames.PmManager_Project_Delete,

                    //TimeSheet
                    PermissionNames.PmManager_Timesheet,
                    PermissionNames.PmManager_Timesheet_ViewAll,
                    PermissionNames.PmManager_Timesheet_Get,
                    PermissionNames.PmManager_Timesheet_GetTimesheetDetail,
                    PermissionNames.PmManager_Timesheet_Create,
                    PermissionNames.PmManager_Timesheet_Update,
                    PermissionNames.PmManager_Timesheet_Delete,
                    PermissionNames.PmManager_Timesheet_DoneTimesheetById,

                    //Timesheet Project
                    PermissionNames.PmManager_TimesheetProject,
                    PermissionNames.PmManager_TimesheetProject_GetAllByproject,
                    PermissionNames.PmManager_TimesheetProject_GetAllRemainProjectInTimesheet,
                    PermissionNames.PmManager_TimesheetProject_Create,
                    PermissionNames.PmManager_TimesheetProject_Update,
                    PermissionNames.PmManager_TimesheetProject_Delete,

                    //Project User Bill
                    PermissionNames.PmManager_ProjectUserBill,
                    PermissionNames.PmManager_ProjectUserBill_GetAllPaging,
                    PermissionNames.PmManager_ProjectUserBill_GetAllByproject,
                    PermissionNames.PmManager_ProjectUserBill_Create,
                    PermissionNames.PmManager_ProjectUserBill_Update,
                    PermissionNames.PmManager_ProjectUserBill_Delete,

                    //Check List Category
                    PermissionNames.SaoDo_CheckListCategory,
                    PermissionNames.SaoDo_CheckListCategory_Create,
                    PermissionNames.SaoDo_CheckListCategory_Delete,
                    PermissionNames.SaoDo_CheckListCategory_Update,
                    PermissionNames.SaoDo_CheckListCategory_ViewAll,

                    //Check List Item
                    PermissionNames.SaoDo_CheckListItem,
                    PermissionNames.SaoDo_CheckListItem_Create,
                    PermissionNames.SaoDo_CheckListItem_Delete,
                    PermissionNames.SaoDo_CheckListItem_Update,
                    PermissionNames.SaoDo_CheckListItem_ViewAll,

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

                 new SystemPermission{ Name =  PermissionNames.Pages_Users, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Users" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Roles, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Roles" },
                 new SystemPermission{ Name =  PermissionNames.Pages_Tenants, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Tenants" },

                 //Client
                 new SystemPermission{ Name =  PermissionNames.Admin_Client, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Client" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Client_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Client" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Client_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create New Client" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Client_Edit, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit Client" },
                 new SystemPermission{ Name =  PermissionNames.Admin_Client_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Client" },

                 //Project
                 new SystemPermission{ Name =  PermissionNames.PmManager_Project, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_Project_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Project" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_Project_ViewDetail, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Detail Project" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_Project_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Project" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_Project_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Project" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_Project_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Project" },

                 // TimeSheet
                 new SystemPermission{ Name =  PermissionNames.PmManager_Timesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Timesheet" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_Timesheet_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Timesheet" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_Timesheet_Get, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Detail Timesheet" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_Timesheet_GetTimesheetDetail, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Timesheet Detail" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_Timesheet_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Timesheet" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_Timesheet_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Timesheet" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_Timesheet_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Timesheet" },
                 new SystemPermission{ Name =  PermissionNames.PmManager_Timesheet_DoneTimesheetById, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Done Timesheet" },

                 //Timesheet Project
                  new SystemPermission{ Name =  PermissionNames.PmManager_TimesheetProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Timesheet Project" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_TimesheetProject_GetAllByproject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get All By Project" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_TimesheetProject_GetAllRemainProjectInTimesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get All Remain Project In Timesheet" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_TimesheetProject_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Timesheet Project" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_TimesheetProject_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Timesheet Project" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_TimesheetProject_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Timesheet Project" },

                  //Project User Bill
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUserBill, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project User Bill" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUserBill_GetAllPaging, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUserBill_GetAllByproject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get All By Project" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUserBill_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Project User Bill" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUserBill_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Edit Project User Bill" },
                  new SystemPermission{ Name =  PermissionNames.PmManager_ProjectUserBill_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Project User Bill" },

                  //Check List Category
                  new SystemPermission{ Name =  PermissionNames.SaoDo_CheckListCategory, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Check List Category" },
                  new SystemPermission{ Name =  PermissionNames.SaoDo_CheckListCategory_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Check List Category" },
                  new SystemPermission{ Name =  PermissionNames.SaoDo_CheckListCategory_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Check List Category" },
                  new SystemPermission{ Name =  PermissionNames.SaoDo_CheckListCategory_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Check List Category" },
                  new SystemPermission{ Name =  PermissionNames.SaoDo_CheckListCategory_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Check List Category" },
                  
                  //Check List Item
                  new SystemPermission{ Name =  PermissionNames.SaoDo_CheckListItem, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Check List Item" },
                  new SystemPermission{ Name =  PermissionNames.SaoDo_CheckListItem_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Check List Item" },
                  new SystemPermission{ Name =  PermissionNames.SaoDo_CheckListItem_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Check List Item" },
                  new SystemPermission{ Name =  PermissionNames.SaoDo_CheckListItem_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Check List Item" },
                  new SystemPermission{ Name =  PermissionNames.SaoDo_CheckListItem_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Check List Item" },
            };
            public static List<SystemPermission> TreePermissions = new List<SystemPermission>()
            {
                //Admin
                new SystemPermission { Name =  PermissionNames.Admin, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Admin",
                    Childrens = new List<SystemPermission>() {
                        new SystemPermission { Name = PermissionNames.Pages_Tenants, MultiTenancySides = MultiTenancySides.Host, DisplayName = "Tenants" },
                        new SystemPermission { Name = PermissionNames.Pages_Users, MultiTenancySides = MultiTenancySides.Host, DisplayName = "Users" },
                        new SystemPermission { Name = PermissionNames.Pages_Roles, MultiTenancySides = MultiTenancySides.Host, DisplayName = "Roles" },

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
                    }
                },
                //PM Manager
                new SystemPermission { Name =  PermissionNames.PmManager, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "PM Manager",
                    Childrens = new List<SystemPermission>() {
                    //Project
                       new SystemPermission { Name =  PermissionNames.PmManager_Project, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Project",
                            Childrens = new List<SystemPermission>()
                            {
                                    new SystemPermission{ Name =  PermissionNames.PmManager_Project_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Project" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_Project_ViewDetail, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Detail Project" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_Project_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Project" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_Project_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Project" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_Project_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Project" },
                            }
                        },
                    //Timesheet
                       new SystemPermission { Name =  PermissionNames.PmManager_Timesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "TimeSheet",
                            Childrens = new List<SystemPermission>()
                            {
                                    new SystemPermission{ Name =  PermissionNames.PmManager_Timesheet_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Timesheet" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_Timesheet_Get, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Detail Timesheet" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_Timesheet_GetTimesheetDetail, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View Timesheet Detail" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_Timesheet_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Timesheet" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_Timesheet_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Timesheet" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_Timesheet_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Timesheet" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_Timesheet_DoneTimesheetById, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Done Timesheet" },
                            }
                        },
                       //Timesheet Project
                       new SystemPermission { Name =  PermissionNames.PmManager_TimesheetProject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "TimeSheet Project",
                            Childrens = new List<SystemPermission>()
                            {
                                    new SystemPermission{ Name =  PermissionNames.PmManager_TimesheetProject_GetAllByproject, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get All By Project" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_TimesheetProject_GetAllRemainProjectInTimesheet, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Get All Remain Project In Timesheet" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_TimesheetProject_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Timesheet Project" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_TimesheetProject_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Timesheet Project" },
                                    new SystemPermission{ Name =  PermissionNames.PmManager_TimesheetProject_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Timesheet Project" },
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

                    }
                },
                //Sao Do
                new SystemPermission { Name =  PermissionNames.SaoDo, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Sao Do",
                    Childrens = new List<SystemPermission>() {
                        //Check List Category
                        new SystemPermission { Name =  PermissionNames.SaoDo_CheckListCategory, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Check List Category",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.SaoDo_CheckListCategory_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Check List Category" },
                                 new SystemPermission{ Name =  PermissionNames.SaoDo_CheckListCategory_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Check List Category" },
                                 new SystemPermission{ Name =  PermissionNames.SaoDo_CheckListCategory_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Check List Category" },
                                 new SystemPermission{ Name =  PermissionNames.SaoDo_CheckListCategory_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Check List Category" },
                            }
                        },
                        //Check List Item
                        new SystemPermission { Name =  PermissionNames.SaoDo_CheckListItem, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Check List Item",
                            Childrens = new List<SystemPermission>()
                            {
                                 new SystemPermission{ Name =  PermissionNames.SaoDo_CheckListItem_Create, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Create Check List Item" },
                                 new SystemPermission{ Name =  PermissionNames.SaoDo_CheckListItem_Delete, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Delete Check List Item" },
                                 new SystemPermission{ Name =  PermissionNames.SaoDo_CheckListItem_Update, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "Update Check List Item" },
                                 new SystemPermission{ Name =  PermissionNames.SaoDo_CheckListItem_ViewAll, MultiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant, DisplayName = "View All Check List Item" },
                            }
                        },
                    }
                },

            };

        }
    }
}
