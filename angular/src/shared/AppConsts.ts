export class AppConsts {

    static remoteServiceBaseUrl: string;
    static appBaseUrl: string;
    static appBaseHref: string; // returns angular's base-href parameter value if used during the publish

    static localeMappings: any = [];

    static readonly userManagement = {
        defaultAdminUserName: 'admin'
    };

    static readonly localization = {
        defaultLocalizationSourceName: 'ProjectManagement'
    };

    static readonly authorization = {
        encryptedAuthTokenName: 'enc_auth_token'
    };

    static readonly statusStyle = {
        PENDING: "badge badge-pill badge-primary",
        APPROVE:"badge badge-pill badge-warning",
        DONE: "badge badge-pill badge-success",
        Future: "badge badge-pill badge-danger",
        CANCELLED: "badge badge-pill badge-danger",
        Present: "badge badge-pill badge-primary",
        PENDINGCFO: "badge badge-pill badge-dark",
        Past: "badge badge-pill badge-secondary",
        Potential: "badge badge-pill badge-primary",
        InProgress: "badge badge-pill badge-success",
        Closed: "badge badge-pill badge-secondary",
        APPROVED:"badge badge-pill badge-danger",


        // Future:"badge badge-pill badge-light"
    }
    static resourceRequestStyle ={
        INPROGRESS: "badge badge-pill badge-primary",
        DONE: "badge badge-pill badge-success",
        CANCELLED: "badge badge-pill badge-danger",
        APPROVED:"badge badge-pill badge-secondary",




    }
    static readonly isSentStyle = {
        Sent: "badge badge-pill badge-success",
        Unsent: "badge badge-pill badge-secondary"
    }
    static readonly projectRole = {

        PM: "badge bg-danger",
        DEV: "badge bg-primary",
        TESTER: "badge bg-success",
        BA: "badge bg-info",
        Artist: "badge bg-warning",
    }
    static readonly ProjectTypeStyle = {
        ODC: "badge badge-primary",
        TAM: "badge badge-success",
        FIXPRICE: "badge badge-danger",
        PRODUCT: "badge badge-warning",
        NOCHARGE: "badge badge-info",
        TRAINING: "badge bg-secondary"
    }
    static readonly ProjectMilestoneStatus = {
        Paid: "badge badge-pill badge-secondary",
        UAT: "badge badge-pill badge-primary",
        Upcoming: "badge badge-pill badge-warning",
        Fail: "badge badge-pill badge-danger"
    }
    static readonly PMReportProjectIssueStatusStyle =
        {
            InProgress: "badge badge-pill badge-primary",
            Done: "badge badge-pill badge-success",
        }
    static readonly userBranchStyle = {
        0: "badge badge-pill badge-danger",
        1: "badge badge-pill badge-success",
        2: "badge badge-pill badge-primary",
        3: "badge badge-pill badge-warning",
    }
    static readonly userTypeStyle = {
        0: "badge badge-success",
        1: "badge badge-primary",
        2: "badge badge-danger",
        3: "badge badge-warning",
        4: "badge badge-secondary",
        5: "badge vendor-userType"


    }
    static readonly SaodoStatusStyle = {
        New: "badge badge-pill  badge-primary",
        InProcess: "badge badge-pill badge-warning",
        Done: "badge badge-pill badge-success"
    }
    static readonly projectHealth = {
        0: "badge  badge-success",
        1: "badge  badge-warning",
        2: "badge  badge-danger"
    }
    static readonly projectHealthBGStyle = {
        0: "badge  bg-success",
        1: "badge  bg-warning",
        2: "badge  bg-danger"
    }
    static readonly projectHealthStyle = {
        0: "text-success",
        1: "text-warning",
        2: "text-danger"
    }
    static readonly  PMReportProjectIssueStatus =
    {
        InProgress: "badge badge-pill  badge-primary",
        Done: "badge badge-pill  badge-success"
    }

    static readonly  userLevleStyle =
    {
            Intern_0 : 0,
            Intern_600K : 1,
            Intern_2M : 2,
            Intern_4M : 3,
            FresherMinus : 4,
            Fresher : 5,
            FresherPlus : 6,
            JuniorMinus : 7,
            Junior : 8,
            JuniorPlus : 9,
            MiddleMinus : 10,
            Middle : 11,
            MiddlePlus : 12,
            SeniorMinus : 13,
            Senior : 14,
            SeniorPlus : 15,
    }
    static readonly projectUserRole = {
        0: "badge bg-danger",
        1: "badge bg-primary",
        2: "badge bg-success",
        3: "badge bg-info",
        4: "badge bg-warning",
    }

    static readonly priorityRequest = [
        'bg-secondary',
        'bg-primary',
        'bg-success',
        'bg-danger'
    ]

}
