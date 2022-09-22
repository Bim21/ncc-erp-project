import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges, ViewChild, ElementRef, AfterViewInit, ViewChildren, QueryList } from '@angular/core';
import { DropDownDataDto } from '@shared/filter/filter.component';

@Component({
  selector: 'app-multi-select-option',
  templateUrl: './multi-select-option.component.html',
  styleUrls: ['./multi-select-option.component.css']
})
export class MultiSelectOptionComponent implements OnInit, OnChanges, AfterViewInit {
  @Input() searchPlaceHolder = ''
  @Input() dropdownData: DropDownDataDto[] = []
  @Input() selectedValues: any[] = []
  @Input() selectedValue: any 
  @Input() multiple: boolean = true
  @Input() selectLabel: string = 'Select'
  @Input() required:  boolean = false;
  @Output() onMultiSelectionChange: EventEmitter<any[]> = new EventEmitter<any[]>()
  @Output() onSingleSelectionChange: EventEmitter<any[]> = new EventEmitter<any[]>()
  @ViewChildren(HTMLInputElement) items:QueryList<HTMLInputElement>
  public searchString: string = ''
  public tempData: DropDownDataDto[]
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
  }

  ngAfterViewInit(): void {
    this.items.changes.subscribe(rs => {
      console.log(rs)
      if(rs.length){
        rs[0].nativeElement.focus()
      }
    })
  }
  onSearch(value: string){
    if(this.searchString){
      this.tempData = this.dropdownData.filter(item => item.displayName.toLowerCase().includes(this.searchString.toLowerCase()))
      return;
    }
    this.tempData = [...this.dropdownData]
  }

  onSingleSelectChange(event: any){
    this.onSingleSelectionChange.emit(event);
  }

  onMultiSelectChange(event: any[]){
    this.onMultiSelectionChange.emit(event)
  }

  onOpenedChange(isOpened: boolean){
    if(!isOpened && this.searchString && !this.tempData.length) {
      this.tempData = [...this.dropdownData]
      this.searchString = ''
    }
  }
}