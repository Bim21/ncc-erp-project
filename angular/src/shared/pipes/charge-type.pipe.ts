import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'chargeTypePipe'
})
export class ChargeTypePipe implements PipeTransform {

  transform(type) {
    switch(type){
      case 0 :return "d"
      case 1: return "m"
      case 2: return "h"
    }
  }

}
