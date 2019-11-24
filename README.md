# Stream Data Aggregation by Variable Duration

![BatchStats design](ComponentDiagram.jpg)

## How to use

A chart with Temperature aggregations (average, standard deviation) is available [here](https://batchstatsapp.azurewebsites.net/).

An API used to push sensors data, perform and fetch aggregated batch data: [API](https://batchstatsapi.azurewebsites.net/).

### Awailable endpoints

- [GET /raw-data](https://batchstatsapi.azurewebsites.net/raw-data) - retrieve raw sensors data
- [GET /calc-data](https://batchstatsapi.azurewebsites.net/calc-data) - retrieve aggregated by batch data
- [WS /hub/aggregations](https://batchstatsapi.azurewebsites.net/hub/aggregations) - subscribe to batch aggregation updates
- [POST /sensors/telemetry](https://batchstatsapi.azurewebsites.net/sensors/telemetry) - push sensors telemetry

### Generate sensors data

Checkout this repo, compile and run `BatchStats.Telemetry.Gen` project - it will start sending simulated data (Temperature and Pressure measurements) to API.

Please notice that only Temperature measurements are shown in a [chart](https://batchstatsapp.azurewebsites.net/) at the moment, other stored data can be retrieved with [GET /raw-data](https://batchstatsapi.azurewebsites.net/raw-data) endpoint.
