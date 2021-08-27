import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import * as moment from 'moment';
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
  isDateProperty: boolean;
  isConFirmProperty: boolean = false;

  comparisions: ComparisionDto[] = [];
  constructor() {
  }
  ngOnInit(): void {
    if (this.item.propertyName === '') {
      this.comparisions = [];
    }
    else {
      let comps = this.inputFilters.find(i => i.propertyName === this.item.propertyName)?.comparisions || [0];
      comps.forEach(element => {
        var com = new ComparisionDto();
        com.id = element;
        com.name = COMPARISIONS[element];
        this.comparisions.push(com);
      });

    }
    this.isConFirmProperty = this.item.isConfirm
    this.isDateProperty = this.item.isDate
  }
  onChange(value: string | number, name: string): void {
    if (name === 'propertyName') {
      this.item.value = ''
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
      this.inputFilters.forEach(item => {
        if (item.propertyName == value) {
          this.isDateProperty = item.isDate
          this.isConFirmProperty = item.isConfirm
          if (this.isDateProperty == true) {
            this.item.value = moment(new Date()).format("YYYY-MM-DD")
            this.item.isDate = true
          }
          else {
            this.item.isDate = false
          }
          if (this.isConFirmProperty) {
            this.item.value =true
            this.item.isConfirm = true
          }
          else {
            this.item.isConfirm = false
          }

        }
      })
    }

    this.emitChange.emit({ name, value })
  }
  onDateChange() {
    this.item.value = moment(this.item.value).format("YYYY-MM-DD")
    this.item.isDate = true
  }
  onRadioChange(event) {
    this.item.value = event.value
  }

  deleteFilter() {
    this.deleteDataFilter.emit();
  }
}

export class InputFilterDto {
  propertyName: string;
  displayName: string;
  comparisions: number[];
  isDate?: boolean
  isConfirm?: boolean;
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
