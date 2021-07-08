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
        DONE: "badge badge-pill badge-success",
        Future: "badge badge-pill badge-secondary",
        CANCELLED: "badge badge-pill badge-danger",
        Present: "badge badge-pill badge-primary",
        PENDINGCFO: "badge badge-pill badge-dark",
        Past: "badge badge-pill badge-danger",
        Potential: "badge badge-pill badge-primary",
        InProgress: "badge badge-pill badge-success",
        Closed: "badge badge-pill badge-danger"
        // Future:"badge badge-pill badge-light"
    }
    static readonly ProjectTypeStyle = {
        ODC:"badge badge-primary",
        TimeAndMaterials : "badge badge-success",
        FIXPRICE: "badge badge-danger",
        PRODUCT: "badge badge-warning",
        NoBill: "badge badge-info"
    }
    static readonly ProjectMilestoneStatus = {
        Paid: "badge badge-pill badge-secondary",
        UAT: "badge badge-pill badge-primary",
        Upcoming:"badge badge-pill badge-warning",
        Fail: "badge badge-pill badge-danger"
    }
  

}
