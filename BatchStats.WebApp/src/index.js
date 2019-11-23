
import React from 'react';
import { render } from 'react-dom';
import Chart from './Chart';
import { getData } from "./utils"
import { HubConnectionBuilder } from '@aspnet/signalr';

import { TypeChooser } from "react-stockcharts/lib/helper";

class ChartComponent extends React.Component {
	componentDidMount() {

		const hubConnection = new HubConnectionBuilder()
			.withUrl('https://localhost:44380/hub/aggregations')
			.build();

		getData().then(data => {
			this.setState({ data, hubConnection }, () => {
				
				let $this = this;

				$this.state.hubConnection
				.start()
				.then(() => console.log('Hub connection started!'))
				.catch(err => console.log('Error while establishing Hub connection :('));

				// $this.state.hubConnection.on('PushBatchStats', (avgData) => {
					
				// 	const data = $this.state.data.concat([avgData]);
				// 	$this.setState({ data });
				//   });
			});
		});
	}
	render() {
		if (this.state == null || this.state.data == null) {
			return <div>Loading...</div>
		}
		return (
			<TypeChooser>
				{type => <Chart type={type} data={this.state.data} />}
			</TypeChooser>
		)
	}
}

render(
	<ChartComponent />,
	document.getElementById("root")
);
