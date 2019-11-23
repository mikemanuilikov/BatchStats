function parseTime(unixTsSec) {
	var date = new Date();
	date.setTime(unixTsSec * 1000);
	return date;
};

function parseData(item) {
	return {
		batchStartTime : parseTime(item.batchStartTime),
		average : item.average,
		standardDeviation: item.standardDeviation
	};
}

export function getData() {
	const promiseFetch = fetch("https://localhost:44380/calc-data")
		.then(response => response.json())
		.then(dataArray => dataArray.map(parseData));
	return promiseFetch;
}