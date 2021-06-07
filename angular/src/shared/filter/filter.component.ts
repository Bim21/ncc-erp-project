import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FilterDto } from 'shared/paged-listing-component-base';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.css']
})

export class FilterComponent {

  @Input() inputFilters: InputFilterDto[];
  @Input() item: any;
  @Output() emitChange = new EventEmitter<any>();
  @Output() deleteDataFilter = new EventEmitter<any>();
  selectedPropertyName: string;
  selectedComparision: number;
  value: any;

  comparisions: ComparisionDto[] = [];
  constructor() { }
  ngOnInit(): void {
    if (this.item.propertyName === '') {
      this.comparisions = [];
    }
    else {
      let comps = this.inputFilters.find(i => i.propertyName === this.item.propertyName).comparisions;
      comps.forEach(element => {
        var com = new ComparisionDto();
        com.id = element;
        com.name = COMPARISIONS[element];
        this.comparisions.push(com);
      });
    }
  }

  onChange(value: string | number, name: string): void {
    if (name === 'propertyName') {
      this.emitChange.emit({ name: 'comparision', value: undefined })
      if (value == '') {
        this.comparisions = [];
        return;
      }
      var comps = this.inputFilters.find(i => i.propertyName === value).comparisions;
      this.comparisions = [];
      comps.forEach(element => {
        var com = new ComparisionDto();
        com.id = element;
        com.name = COMPARISIONS[element];
        this.comparisions.push(com);
      });
    }
    this.emitChange.emit({ name, value })
  }

  deleteFilter() {
    this.deleteDataFilter.emit();
  }
}

export class InputFilterDto {
  propertyName: string;
  displayName: string;
  comparisions: number[];
}

export class ComparisionDto {
  id: number;
  name: string;
}


export const COMPARISIONS: string[] =
  ['Equal',
    'Less Than',
    'Less Than Or Equal',
    'Greater Than',
    'Greater Than Or Equal',
    'Not Equal',
    'Contains',
    'Starts With',
    'Ends With',
    'In']
