import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home-component.html',
  styleUrls: ['./home-component.css']
})
export class HomeComponent {
  public cars?: Cars[] = undefined;
  public carsToRent: Cars[] = [];
  public locations: Locations[] = [];
  public locationId: string = "";
  public dateFrom?: Date = undefined;
  public dateTo?: Date = undefined;
  public loading: Boolean = false;
  public finalPrice: number = 0;

  constructor(private http: HttpClient) {
    http.get<Locations[]>(environment.apiUrl + 'api/location/all-locations').subscribe(result => {
      this.locations = result;
    }, error => console.error(error));
  }

  getAllAvailableCars() {
    if (this.dateFrom == undefined ||
      this.dateTo == undefined ||
      this.locationId == "") {
      return;
    }

    this.loading = true;
    let from = 'from=' + this.dateFrom.toISOString();
    let to = 'to=' + this.dateTo.toISOString();
    let location = 'locationId=' + this.locationId;
    let parameters = [from, to, location].join('&');

    this.http.get<Cars[]>(environment.apiUrl + `api/car/?${parameters}`).subscribe(
      (response) => {
        this.cars = response;
        this.loading = false;
      },
      (error) => {
        console.log(error);
        this.loading = false;
      });
  }

  public createReservation() {
    let data: any = {
      carIds: this.carsToRent.map(car => car.id),
      from: this.dateFrom?.toISOString(),
      to: this.dateTo?.toISOString(),
      pickupLocationId: this.locationId,
      returnLocationId: this.locationId
    };

    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }

    this.http.post(environment.apiUrl + 'api/reservation', JSON.stringify(data), httpOptions).subscribe(
      (response) => console.log(response),
      (error) => console.log(error)
    );
  }

  addCar(car: Cars) {
    if (!car.isSelected) {
      this.carsToRent.push(car);
      car.isSelected = true;
    }
    else {
      let index = this.carsToRent.indexOf(car);
      this.carsToRent.splice(index, 1);
      car.isSelected = false;
    }
    this.calculateFinalPrice();
  }

  calculateFinalPrice() {
    if (this.dateFrom !== undefined && this.dateTo !== undefined) {
      let difference = this.dateTo.valueOf() - this.dateFrom.valueOf();
      let hours = difference / (1000 * 3600);

      this.finalPrice = 0;
      this.carsToRent.forEach(car => {
        this.finalPrice += car.pricePerHour * hours;
      })
    }
  }
}

interface Cars {
  id: string,
  model: string,
  brand: string,
  pricePerHour: number,
  reservationIds: string[],
  locationId: string,
  city: string,
  isSelected?: boolean
}

interface Locations {
  id: string,
  city: string,
  address: string
}
