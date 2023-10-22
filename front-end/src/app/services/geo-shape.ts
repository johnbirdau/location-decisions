import { LatLng } from "leaflet";

export class GeoShape {
    id: number;
    latLngs: LatLng[];

    constructor(id: number,latLngs: LatLng[]) {
        this.id = id;
        this.latLngs = latLngs;
      }
}
