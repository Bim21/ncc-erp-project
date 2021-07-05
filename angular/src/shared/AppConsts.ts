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

    static readonly statusStyle ={
        START: "badge badge-pill badge-primary",
        APPROVED:"badge badge-pill badge-success",
        END:"badge badge-pill badge-secondary",
        REJECT:"badge badge-pill badge-danger",
        PENDINGCEO:"badge badge-pill badge-warning",
        PENDINGCFO:"badge badge-pill badge-dark",
        TRANSFERED:"badge badge-pill badge-info",
        PENDINGIT:"badge badge-pill badge-light"
    }


}
