import { Component, Injector, OnInit } from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import {
  Router,
  RouterEvent,
  NavigationEnd,
  PRIMARY_OUTLET,
} from "@angular/router";
import { BehaviorSubject } from "rxjs";
import { filter } from "rxjs/operators";
import { MenuItem } from "@shared/layout/menu-item";

@Component({
  selector: "sidebar-menu",
  templateUrl: "./sidebar-menu.component.html",
})
export class SidebarMenuComponent extends AppComponentBase implements OnInit {
  menuItems: MenuItem[];
  menuItemsMap: { [key: number]: MenuItem } = {};
  activatedMenuItems: MenuItem[] = [];
  routerEvents: BehaviorSubject<RouterEvent> = new BehaviorSubject(undefined);
  homeRoute = "/app/home";

  constructor(injector: Injector, private router: Router) {
    super(injector);
    this.router.events.subscribe(this.routerEvents);
  }

  ngOnInit(): void {
    this.menuItems = this.getMenuItems();
    this.patchMenuItems(this.menuItems);
    this.routerEvents
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe((event) => {
        const currentUrl = event.url !== "/" ? event.url : this.homeRoute;
        const primaryUrlSegmentGroup = this.router.parseUrl(currentUrl).root
          .children[PRIMARY_OUTLET];
        if (primaryUrlSegmentGroup) {
          this.activateMenuItems("/" + primaryUrlSegmentGroup.toString());
        }
      });
  }

  getMenuItems(): MenuItem[] {
    return [
      new MenuItem(this.l("HomePage"), "/app/home", "fas fa-home"),

      new MenuItem(this.l("Admin"), "", "fas fa-user-cog", "Admin.CanViewMenu", [
        new MenuItem(
          this.l("Tenants"),
          "/app/tenants",
          "fas fa-building",
          "Pages.Tenants"
        ),
        new MenuItem(this.l("Clients"), "/app/clients", "fas fa-users", "Admin.Client"),
        new MenuItem(this.l("Configurations"), "/app/configurations", "fas fa-cog", "Admin.Configuration"),
        new MenuItem(this.l("Skills"), "/app/skills", "fas fa-users", "Admin.Skill"),
        new MenuItem(this.l("Users"), "/app/users", "fas fa-users", "Pages.Users"),
        new MenuItem(this.l("Roles"), "/app/roles", "fas fa-theater-masks", "Pages.Roles"),

      ]),
      new MenuItem(
        this.l("PM Management"),
        "",
        "fas fa-user-tie",
        "PmManager.CanViewMenu", [
        new MenuItem(
          this.l("List Project"),
          "/app/list-project",
          "fas fa-project-diagram",
          "PmManager.Project"
        )

      ]
      ),

      new MenuItem(
        this.l("Sao đỏ"),
        "/app/sao-do",
        "fas fa-user-shield",
        "SaoDo.CanViewMenu"
      ),
      new MenuItem(
        this.l("CheckList"),
        "",
        "fas fa-tasks",
        "CheckList.CanviewMenu", [
        new MenuItem(
          this.l("Checklist Category"),
          "/app/checklist-title",
          "fas fa-clipboard-list",
          "CheckList.CheckListCategory"
        ),
        new MenuItem(
          this.l("Checklist Item"),
          "/app/checklist",
          "fas fa-calendar-check",
          "CheckList.CheckListItem"
        )

      ]
      ),
      new MenuItem(
        this.l("Delivery Management"),
        "/app/delivery/weekly-report-tab",
        "fas fa-chalkboard-teacher",
        "DeliveryManagement.CanViewMenu",
      ),
      new MenuItem(
        this.l("Timesheet"),
        "",
        "fas fa-calendar-alt",
        "Timesheet.CanViewMenu", [
        new MenuItem(
          this.l("Timesheet"),
          "/app/timesheet",
          "fas fa-circle",
          ""
        ),
      ]
      ),
      new MenuItem(
        this.l("CheckPoint"),
        "",
        "fas fa-tasks",
        "", [
        new MenuItem(
          this.l("Phase"),
          "/app/phase",
          "fas fa-clipboard-list",
          "",
        ),
        

        new MenuItem(
          this.l("Đánh giá thành viên"),
          "/app/review-user",
          "fas fa-clipboard-list",
          ""

        )

        ,
        new MenuItem(
          this.l("Tags"),
          "/app/tags",
          "fas fa-clipboard-list",
          "",
        ),
        // new MenuItem(
        //   this.l("Setup đánh giá"),
        //   "/app/setup-reviewer",
        //   "fas fa-clipboard-list",
        //   ""

        // )

        // ,
        // new MenuItem(
        //   this.l("Kết quả đánh giá"),
        //   "/app/result-reviewer",
        //   "fas fa-clipboard-list",
        //   ""

        // )

        ,
        new MenuItem(
          this.l("Category"),
          "",
          "fas fa-clipboard-list",
          "", [
          new MenuItem(
            this.l("Category Criteria"),
            "/app/category-criteria",
            "fas fa-clipboard-list",
            ""
          ),

        ]
        ),


      ]
      ),

    ];
  }

  patchMenuItems(items: MenuItem[], parentId?: number): void {
    items.forEach((item: MenuItem, index: number) => {
      item.id = parentId ? Number(parentId + "" + (index + 1)) : index + 1;
      if (parentId) {
        item.parentId = parentId;
      }
      if (parentId || item.children) {
        this.menuItemsMap[item.id] = item;
      }
      if (item.children) {
        this.patchMenuItems(item.children, item.id);
      }
    });
  }

  activateMenuItems(url: string): void {
    this.deactivateMenuItems(this.menuItems);
    this.activatedMenuItems = [];
    const foundedItems = this.findMenuItemsByUrl(url, this.menuItems);
    foundedItems.forEach((item) => {
      this.activateMenuItem(item);
    });
  }

  deactivateMenuItems(items: MenuItem[]): void {
    items.forEach((item: MenuItem) => {
      item.isActive = false;
      item.isCollapsed = true;
      if (item.children) {
        this.deactivateMenuItems(item.children);
      }
    });
  }

  findMenuItemsByUrl(
    url: string,
    items: MenuItem[],
    foundedItems: MenuItem[] = []
  ): MenuItem[] {
    items.forEach((item: MenuItem) => {
      if (item.route === url) {
        foundedItems.push(item);
      } else if (item.children) {
        this.findMenuItemsByUrl(url, item.children, foundedItems);
      }
    });
    return foundedItems;
  }

  activateMenuItem(item: MenuItem): void {
    item.isActive = true;
    if (item.children) {
      item.isCollapsed = false;
    }
    this.activatedMenuItems.push(item);
    if (item.parentId) {
      this.activateMenuItem(this.menuItemsMap[item.parentId]);
    }
  }

  isMenuItemVisible(item: MenuItem): boolean {
    if (!item.permissionName) {
      return true;
    }
    return this.permission.isGranted(item.permissionName);
  }
}
