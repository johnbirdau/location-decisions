export interface GeoShapeApi {
    id: number,
    //     "shapeName": "2118001",
    //     "shapeType": null,
    points: string
    //     "centroid": "wtntZxsjfF",
    //     "envelope": "{intZ`akfFuT??qYtT??pY",
    //     "shapeOptions": null,
    //     "xtn": null,
    //     "holes": [
      
    //     ],
    //     "shapes": [
      
    //     ],
    //     "url": null,
    //     "altLabel": null,
    //     "data": null,
    //     "clickUrl": "/monash/entity/[entitytype]/[entityid]?ShapeId=2118001&Year=2011"
}

export interface GeoShapeEnvelope {
    // id: "6",
    // layerName: "",
    // isVisible: false,
    // shapeType: "",
    // zIndex: 0,
    // shapeOptions: null,
    //xtn: null,
    shapes: GeoShapeApi[]
}
