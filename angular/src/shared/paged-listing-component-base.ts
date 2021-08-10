import { AppComponentBase } from 'shared/app-component-base';
import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

export class PagedResultDto {
    items: any[];
    totalCount: number;
}
export class FilterDto {
    propertyName: string;
    value: any;
    comparision: number;
    isDate?: boolean;
}
export class EntityDto {
    id: number;
}

export class PagedRequestDto {
    skipCount: number;
    maxResultCount: number;
    searchText: string;
    filterItems: FilterDto[] = [];
    sort: string;
    sortDirection: number;
}
export class PagedResultResultDto {
    result: PagedResultDto;
}

@Component({
    template: ''
})
export abstract class PagedListingComponentBase<TEntityDto> extends AppComponentBase implements OnInit {
    [x: string]: any;

    public pageSize: number = 5;
    public pageNumber: number = 1;
    public totalPages: number = 1;
    public totalItems: number;
    public searchText: string = '';
    public filterItems: FilterDto[] = [];
    public pageSizeType: number = 20;

    public advancedFiltersVisible: boolean = false;

    activatedRoute: ActivatedRoute;
    router: Router;

    constructor(injector: Injector) {
        super(injector);
        this.activatedRoute = injector.get(ActivatedRoute);
        this.router = injector.get(Router);
        this.activatedRoute.queryParams.subscribe(params => {
            this.pageNumber = params['pageNumber'] ? params['pageNumber'] : 1;
            this.pageSize = params['pageSize'] ? params['pageSize'] : 20;
            this.searchText = params['searchText'] ? params['searchText'] : '';
            this.filterItems = params['filterItems'] ? JSON.parse(params['filterItems']) : [];
            this.advancedFiltersVisible = this.filterItems.length > 0;
            this.pageSizeType = Number(params['pageSize'] ? params['pageSize'] : 20);
        });

    }

    ngOnInit(): void {
        this.refresh();
    }
    checkAddFilter() {
        this.advancedFiltersVisible = !this.advancedFiltersVisible;
        if (this.filterItems.length === 0) {
            this.addFilter();
        }

    }
    refresh(): void {
        this.getDataPage(this.pageNumber);
    }

    public showPaging(result: PagedResultDto, pageNumber: number): void {
        this.totalPages = ((result.totalCount - (result.totalCount % this.pageSize)) / this.pageSize) + 1;
        this.totalItems = result.totalCount;
        this.pageNumber = pageNumber;
    }

    public getDataPage(page: number): void {
        const req = new PagedRequestDto();
        req.maxResultCount = this.pageSize;
        req.skipCount = (page - 1) * this.pageSize;
        req.filterItems = this.filterItems;
        if (this.filterItems.length > 0){
            req.filterItems.forEach((item, index) => {
                if (item.propertyName == "") {
                    req.filterItems.splice(index, 1)
                }
            })
        }
        this.advancedFiltersVisible = this.filterItems.length > 0;
        req.searchText = this.searchText;
        this.isLoading = true;
        this.pageNumber = page;
        this.router.navigate([], {
            queryParamsHandling: "merge",
            replaceUrl: true,
            queryParams: { pageNumber: this.pageNumber, pageSize: this.pageSize, searchText: this.searchText, filterItems: JSON.stringify(this.filterItems) }
        })
            .then(_ => this.list(req, page, () => {
                this.isLoading = false;
            }));
    }

    public deleteFilterItem(index: number) {
        this.filterItems.splice(index, 1);
    }
    public addFilter() {
        this.filterItems.push({
            propertyName: '',
            comparision: 0,
            value: '',
        });
    }
    public onEmitChange(event, i) {
        const { name, value } = event
        this.filterItems[i][name] = value
    }
    changePageSize() {
        if (this.pageSize > this.totalItems) {
            this.pageNumber = 1;
        }
        this.pageSize = this.pageSizeType;
        this.refresh();
    }

    protected abstract list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void;
    protected abstract delete(entity: TEntityDto): void;
}
