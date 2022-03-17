export class Utils{
    public static getUserLevel(level) {
        switch (level) {
          case 0:
            return {
              userLevel: "Intern_0",
              style: {'background-color': '#B2BEB5'}
            }
          case 1:
            return {
              userLevel: "Intern_600k",
              style: {'background-color': '#8F9779'}
            }
          case 2:
            return {
              userLevel: "Intern_2M",
              style: {'background-color': '#665D1E', 'color': 'white'}
            }
          case 3: {
            return {
              userLevel: "Intern_4M",
              style: {'background-color': '#777'}
            }
          }
          case 4: {
            return {
              userLevel: "Fresher-",
              style: {'background-color': '#60b8ff'}
            }
          }
          case 5: {
            return {
              userLevel: "Fresher",
              style: {'background-color': '#318CE7'}
            }
          }
          case 6: {
            return {
              userLevel: "Fresher+",
              style: {'background-color': '#1f75cb'}
            }
          }
          case 7: {
            return {
              userLevel: "Junior-",
              style: {'background-color': '#ad9fa1'}
            }
          }
          case 8: {
            return {
              userLevel: "Junior",
              style: {'background-color': '#A57164'}
            }
          }
          case 9: {
            return {
              userLevel: "Junior+",
              style: {'background-color': '#3B2F2F'}
            }
          }
          case 10: {
            return {
              userLevel: "Middle-",
              style: {'background-color': '#A4C639'}
            }
          }
          case 11: {
            return {
              userLevel: "Middle",
              style: {'background-color': '#3bab17'}
            }
          }
          case 12: {
            return {
              userLevel: "Middle+",
              style: {'background-color': '#008000'}
            }
          }
          case 13: {
            return {
              userLevel: "Senior-",
              style: {'background-color': '#c36285'}
            }
          }
          case 14: {
            return {
              userLevel: "Senior",
              style: {'background-color': '#AB274F'}
            }
          }
          case 15: {
            return {
              userLevel: "Principal ",
              style: {'background-color': '#902ee1'}
            }
          }
        }
    }

    public static getTimeHistory(timeHistory: any){
      let now = Date.now()
      let time
      if(now < new Date(timeHistory).getTime()){
        time = Math.round((new Date(timeHistory).getTime() - now)/(1000 * 3600 * 24));
        if(time < 1){
          return ''
        }
        if(time == 1){
          return '1 in day';
        }
        else if(time <= 7){
          return time + ' in days';
        }
        else if(time <= 30){
          let week = Math.round(time/7)
          return week > 1 ? week + ' in weeks' : week + ' in week'
        }
        else if(time <= 365){
          let month = Math.round(time/30)
          return month > 1 ? month + ' in months' : month + ' in month'
        }
        else{
          let year = Math.round(time/365)
          return year > 1 ? year + ' in years' : year + ' in year'
        }
      }
      else{
        time = Math.round((now - new Date(timeHistory).getTime())/(1000 * 3600 * 24));
        if(time < 1){
          return ''
        }
        if(time == 1){
          return '1 day ago';
        }
        else if(time <= 7){
          return time + ' days ago';
        }
        else if(time <= 30){
          let week = Math.round(time/7)
          return week > 1 ? week + ' weeks ago' : week + ' week ago'
        }
        else if(time <= 365){
          let month = Math.round(time/30)
          return month > 1 ? month + ' months ago' : month + ' month ago'
        }
        else{
          let year = Math.round(time/365)
          return year > 1 ? year + ' years ago' : year + ' year ago'
        }
      }
    }

    public static timeFuture(timeFuture: any){

    }
}