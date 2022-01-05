export interface IUser {
  userName: string;
  name: string;
  surname: string;
  emailAddress: string;
  isActive: true;
  fullName: string;
  lastLoginTime: string;
  creationTime: string;
  roleNames: [string];
  id: number;
  fullNameNormal: string;
  branch: number;
  avatarPath: string;
  userType: number;
  userLevel: number;
  userSkills: any[] | undefined;
  userCode: string;
  poolNote: string;
}