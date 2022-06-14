﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using ProjectManagement.Authorization.Users;
using ProjectManagement.MultiTenancy;
using Abp.Dependency;
using Abp.ObjectMapping;
using NccCore.IoC;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class ProjectManagementAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }
        protected IWorkScope WorkScope { get; set; }

        protected ProjectManagementAppServiceBase()
        {
            LocalizationSourceName = ProjectManagementConsts.LocalizationSourceName;
            WorkScope = IocManager.Instance.Resolve<IWorkScope>();
            ObjectMapper = IocManager.Instance.Resolve<IObjectMapper>();
            UserManager = IocManager.Instance.Resolve<UserManager>();
            TenantManager = IocManager.Instance.Resolve<TenantManager>();
        }

        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        protected virtual async Task<User> GetUserByEmailAsync(string emailAddress)
        {
           return await WorkScope.GetAll<User>()
                .Where(x => x.EmailAddress.ToLower().Trim() == emailAddress.ToLower().Trim())
                .FirstOrDefaultAsync();
        }

    }
}
