import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { DropDownDataDto } from '@shared/filter/filter.component';

@Component({
  selector: 'app-multi-select-option',
  templateUrl: './multi-select-option.component.html',
  styleUrls: ['./multi-select-option.component.css']
})
export class MultiSelectOptionComponent implements OnInit, OnChanges {
  @Input() searchPlaceHolder = ''
  @Input() dropdownData: DropDownDataDto[] = []
  @Input() selectedIds: number[] = []
  @Input() multiple: boolean = true
  @Input() selectLabel: string = 'Select'
  @Output() onSelectionChange: EventEmitter<number[]> = new EventEmitter<number[]>()
  public searchString: string = ''
  public tempData: DropDownDataDto[]
  public selected = []
  constructor() { }
  ngOnChanges(changes: SimpleChanges): void {
    if('dropdownData' in changes){
      this.dropdownData = changes.dropdownData.currentValue
      this.tempData = [...this.dropdownData]
      this.searchString = ''
    }
  }

  ngOnInit(): void {
    this.tempData = [...this.dropdownData]
    this.selected = this.selectedIds
  }

  onSearch(value: string){
    if(this.searchString){
      this.tempData = this.tempData.filter(item => item.displayName.toLowerCase().includes(this.searchString.toLowerCase()))
      return;
    }
    this.tempData = [...this.dropdownData]
  }

  onSelectChange(event: number[]){
    this.onSelectionChange.emit(event)
  }

  onOpenedChange(isOpened: boolean){
    if(!isOpened && this.searchString && !this.tempData.length) {
      this.tempData = [...this.dropdownData]
      this.searchString = ''
    }
  }
}