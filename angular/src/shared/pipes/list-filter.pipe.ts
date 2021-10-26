import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'listFilter'
})
export class ListFilterPipe implements PipeTransform {

  transform(value: any[], property:string, searchText:string, property2?:string): any {
    if(property2){
      return value.filter(item=> {
        return item[property].toLowerCase().includes(searchText.toLowerCase()) || item[property2].toLowerCase().includes(searchText.toLowerCase()) 
      });
    }
    else{
      return value.filter(item=> {
        return item[property].toLowerCase().includes(searchText.toLowerCase()) 
      });
    }
    
  
  }
  
}
