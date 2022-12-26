using Business.Entities;
using Business.Interface;
using Business.Interface.Dynamic;
using Business.Interface.Marketing;
using Business.Service.Dynamic;
using Business.Service.Marketing;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Business.Interface.IPartyTypeService;
using Business.Interface.Department;
using Business.Service.Department;
using Business.Interface.Designation;
using Business.Service.Designation;
using Business.Interface.ProductImages;
using Business.Interface.Marketing.CommunicatonLog;
using Business.Service.Marketing.CommunicationLogService;
using Business.Interface.Marketing.IVisitingDetailService;
using Business.Service.Marketing.VisitingDetailService;
using Business.Interface.Marketing.IMeeting;
using Business.Service.Marketing.Meeting;
using Business.Service.Marketing.CompanySales;
using Business.Interface.Marketing.ICompanyTarget;
using Business.Service.Master.MarketingCompanyFinanicalYearMaster;
using Business.Interface.IMaster.IMarketingCompanyFinanicalYearMaster;
using Business.Service.Marketing.SalesTarget;
using Business.Interface.Marketing.ISalesTarger;
using Business.Interface.Marketing.IEmployeeSalesTargetSummary;
using Business.Service.Marketing.EmployeeSalesTargetSummary;
using Business.Interface.Marketing.IPackage;
using Business.Service.Master.PackageService;
using Business.Interface.IEmployee;
using Business.Interface.IFormMaster;
using Business.Interface.ICustomer;
using Business.Service.Customer;

namespace Business.Service
{
    public static class DependencyContainer
    {
        /// <summary>
        /// Connects our interfaces and their implementations from multiple projects 
        /// into single point of reference. That is the purpose of IoC layer.
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterServices(IServiceCollection services)
        {
            // Register Repo
            services.AddScoped<ISiteUserService, SiteUserRepository>();
            services.AddScoped<ISiteRoleRepository, SiteRoleRepository>();
            services.AddTransient<IUserStore<UserMasterMetadata>, UserStore>();
            services.AddTransient<IRoleStore<RoleMasterMetadata>, RoleStore>();
            services.AddTransient<ISiteCompanyRepository, CompanyService>();
            services.AddTransient<ISuperAdminService, SuperAdminService>();
            services.AddTransient<IVisitorService, VisitorService>();
            services.AddTransient<IViewRenderService, ViewRenderService>();
            services.AddTransient<IMasterEntity, MasterEntity>();
            services.AddTransient<IRequestTypeControl, RequestTypeControlService>();
            services.AddTransient<IRequestTypeModule, RequestTypeModuleService>();
            services.AddTransient<IRequestTypeControlScreenMapping, RequestTypeControlScreenMappingService>();
            services.AddTransient<IRequestType, RequestTypeService>();
            services.AddTransient<IRequestService, RequestService>();
            services.AddTransient<IEntity, EntityService>();
            services.AddTransient<IMarketingFeedbackService, MarketingFeedbackService>();
            services.AddTransient<IMasterService, MasterService>();
            services.AddTransient<IPartyTypeService, PartyTypeService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IDepartmentGroupService, DepartmentGroupService>();
            services.AddTransient<IDesignationService, DesignationService>();
            services.AddTransient<IDesignationGroupService, DesignationGroupService>();
            services.AddTransient<IProductImages, Business.Service.ProductImages.ProductImages>();
            services.AddTransient<IMarketingCommunicationLogService, MarketingCommunicationLogService>();
            services.AddTransient<IMarketingVisitingDetailService, MarketingVisitingDetailService>();
            services.AddTransient<IMasterService, MasterService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<ISecurityOfficerService, SecurityOfficerService>();
            services.AddTransient<IMarketingMeeting, MarketinggMeetingService>();
            services.AddTransient<IMarketingCompanyTargetService, MarketingCompanyTargetService>();
            services.AddTransient<IMarketingCompanyFinancialYear, MarketingCompanyFinancialYearService>();
            services.AddTransient<IMarketingSalesTargetService, MarketingSalesTargetService>();
            services.AddTransient<IEmployeeSalesTargetSummaryService, EmployeeSalesTargetSummaryService>();
            services.AddTransient<IPackageService, PackageService>();
            services.AddTransient<IFormMasterService, Business.Service.FormMasterService.FormMasterService>();
            services.AddTransient<ICustomerService, CustomerService>();

            // Add application services.
            //services.AddTransient<IEmailService, EmailService>();
        }
    }
}
