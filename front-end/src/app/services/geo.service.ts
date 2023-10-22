import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Constants } from '../config/constants';
import { Observable, map } from 'rxjs';
import { LatLng, latLng } from 'leaflet';
import { GeoShapeApi, GeoShapeEnvelope } from './geo-shape-api';
import { GeoShape } from './geo-shape';

@Injectable({
  providedIn: 'root'
})
export class GeoService {

  constructor(private constants: Constants, private http: HttpClient) { }

  getGeoShapes () : Observable<GeoShape[]> {
    return this.http.get<GeoShapeEnvelope>(this.constants.GEO_ENDPOINT)
      .pipe(
        map<GeoShapeEnvelope, GeoShapeApi[]>(
          envelope => envelope.shapes
        ),
        map(
          (geoApiValues) =>
            geoApiValues.map(
              geoApiValue =>
                new GeoShape(geoApiValue.id, this.decodePoints(geoApiValue.points))
          )
        )
      );
  }

  /*
   * decode the polygon points in the shapes array within the geo json endpoint.
   * It will return an array of Latitude, Longitude objects.
   * from https://gist.github.com/Zir01/9ce6c17f8d773b2f23d5c8410b0c6e19#file-decodepoints-js
   */ 
  decodePoints (encoded : string) : LatLng[] {
    var len = encoded.length;
    var index = 0;
    var ar : LatLng[] = [];
    var lat = 0;
    var lng = 0;

    try {

        while (index < len) {

            var b;
            var shift = 0;
            var result = 0;
            do {
                b = encoded.charCodeAt(index++) - 63;
                result |= (b & 0x1f) << shift;
                shift += 5;
            }
            while (b >= 0x20);

            var dlat = ((result & 1) ? ~(result >> 1) : (result >> 1));
            lat += dlat;

            shift = 0;
            result = 0;
            do {
                b = encoded.charCodeAt(index++) - 63;
                result |= (b & 0x1f) << shift;
                shift += 5;
            }
            while (b >= 0x20);

            var dlng = ((result & 1) ? ~(result >> 1) : (result >> 1));
            lng += dlng;

            // bug mentioned in .id readme.md
            //ar.push(latLng((lat * 1e-5), (lng * 1e-5)));
            ar.push(latLng((lng * 1e-5), (lat * 1e-5)));
        }

    }
    catch (ex) {

        //error in encoding.

    }
    return ar;
  }
}
