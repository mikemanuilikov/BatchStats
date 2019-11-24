import React from 'react';
import { render } from 'react-dom';
import Chart from './Chart';
import { getData, parseData } from './utils';
import { HubConnectionBuilder } from '@aspnet/signalr';

import { TypeChooser } from 'react-stockcharts/lib/helper';

class ChartComponent extends React.Component {
  componentDidMount() {
    getData()
      .then(data => {
        this.setState({ data });
      })
      .then(() => {
        const hubConnection = new HubConnectionBuilder()
          .withUrl('https://batchstatsapi.azurewebsites.net/hub/aggregations')
          .build();

        hubConnection
          .start()
          .then(() => console.log('Hub connection started!'))
          .catch(err =>
            console.log('Error while establishing Hub connection :(')
          );

        hubConnection.on('PushBatchStats', avgData => {
          console.log('PushBatchStats got data: ', avgData);
          if(avgData.sensorId != "Temperature") return;
          const newData = [parseData(avgData)];
          let newState = [...this.state.data, ...newData];
          if (newState.length < 1) return;
          this.setState({ data: newState });
        });
      });
  }
  render() {
    if (this.state == null || this.state.data == null) {
      return <div>Loading...</div>;
    }
    return (
      <div style={{ backgroundColor: '#303030' }}>
        <TypeChooser>
          {type => <Chart type={type} data={this.state.data} />}
        </TypeChooser>
      </div>
    );
  }
}

render(<ChartComponent />, document.getElementById('root'));
