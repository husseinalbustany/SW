import { Component, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-starship',
  templateUrl: './starship.component.html'
})
export class StarshipComponent {
  public starships: StarShip[] = [];
  public LoadingData: string = "Loading data please wait....";
  public ValidNumber: string = "";


  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    const distance = (<HTMLInputElement>document.getElementById('inputDistance')).value;
    console.log(distance);
    if (isNaN(Number(distance))) {
      console.log("distance is wrong");
      this.ValidNumber = "Please enter a positive valid number";
      this.LoadingData = "";
    }
    else {
      console.log("Distance entered ");
      http.get<StarShip[]>(baseUrl + 'StarShip?distance=' + distance).subscribe(result => {
        this.starships = result;
        this.LoadingData = "";
      }, error => console.error(error));
    }
  }
}




interface StarShip {
  name:string
  numberofstops: string;

}
