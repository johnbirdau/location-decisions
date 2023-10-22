import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LeafletModule } from '@asymmetrik/ngx-leaflet';
import { latLng, tileLayer, Map as LMap, Polygon, LayerGroup, MapOptions, point } from 'leaflet';
import { DataService } from 'src/app/services/data.service';
import { DataValue } from '../services/data-value';
import { Constants } from '../config/constants';
import { GeoService } from '../services/geo.service';
import { GeoShape } from '../services/geo-shape';
import { combineLatestWith } from 'rxjs';

@Component({
  selector: 'app-map',
  standalone: true,
  imports: [CommonModule, LeafletModule],
  providers: [DataService, GeoService, Constants],
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {

  constructor(
    private dataService: DataService,
    private geoService: GeoService
  ){}

  ngOnInit(): void {
    this.getMapData();
  }

  onMapReady(map: LMap) {
    this.map = map;
  }
  
  map: LMap | undefined;
  dataMap : Map<number, DataValue> = new Map();
  geo : GeoShape[] | undefined;
  polygons: LayerGroup = new LayerGroup();

  getMapData() {
    var dataSub =
      this.dataService.getDataValues();

    var geoSub =
      this.geoService.getGeoShapes();

    geoSub.pipe(combineLatestWith(dataSub))
    .subscribe(
      ([geo, data]) =>
        {
          this.dataMap.clear();
          data?.forEach((value) => this.dataMap.set(value.GeoID, value));
          this.geo = geo;
          this.showGeoDataOnMap();
        }
    )
  }

  showGeoDataOnMap()
  {
    this.polygons.clearLayers();
    this.geo
      ?.map(
        geo =>
        {
          var data = this.dataMap.has(geo.id)? this.dataMap.get(geo.id) : undefined;
          if (data)
          {
            return new Polygon(geo.latLngs, {fillColor : data.color, color : "black", fillOpacity: 1.0, opacity: 1.0, weight: 2 })
              .bindTooltip(`SA1: ${data.InfoBox.SA1}<br/>Number: ${data.InfoBox.Number}<br/>Percent (%): ${data.InfoBox["Percent (%)"]}<br/>Total pop: ${data.InfoBox["Total pop"]}`);
          }
          else
          {
            return new Polygon(geo.latLngs, {})
          }
        }
      )
      .reduce<LayerGroup>((acc, curr) => acc.addLayer(curr), this.polygons);

      var polygons = this.polygons.getLayers().map<Polygon>( l => l as Polygon);
      if (polygons.length > 0 && this.map)
      {
        this.map.fitBounds(
          polygons.reduce((acc, curr) => acc.extend((curr as Polygon).getBounds()), polygons[0].getBounds()),
          {
            padding: point(1, 1),
            maxZoom: 15,
            animate: true
          }
        );
      }
  
  }

  openStreetMap = tileLayer(
    'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
    {
      attribution: '&copy; OpenStreetMap contributors'
    }
  );

  options : MapOptions = {
    layers: [this.openStreetMap, this.polygons],
    zoom: 12.5,
    center: latLng([ -37.90874, 145.0791 ])
  };

}
