import { DataValue } from "./data-value";

export interface DataValueEnvelope {
//  "mapTitle": "City of Monash, People aged 0 to 4 years, 2011, Enumerated, Persons",
//  "mapSubtitle": "People aged 0 to 4 years",
//  "mapDataSource": "Source: Australian Bureau of Statistics, Census of Population and Housing, 2011 (Enumerated data). Compiled and presented in atlas.id by .id, the population experts.",
//  "denominator": "Percentage of: Total Persons",
    data : DataValue[]
    // "legend": [
    //     {
    //       "Rank": 0,
    //       "From": 0.9,
    //       "To": 2.7,
    //       "label": "0.9% to 2.6%",
    //       "color": "#e6e6e6"
    //     }
    // ]
    //   "DetailedNoteHTML": null,
    //   "TableNoteHTML": null,
    //   "isTextMap": false,
    //   "page": null,
    //   "Invalid": false,
    //   "ErrorMessage": null,
    //   "changeMapUsed": false
}
