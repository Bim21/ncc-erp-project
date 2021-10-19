import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'listFilter'
})
export class ListFilterPipe implements PipeTransform {

  transform(value: any[], property:string, searchText:string): any {
      return value.filter(item=> {
        return item[property].toLowerCase().includes(searchText.toLowerCase()) 
      });
  
  }
  
}
