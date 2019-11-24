import React from 'react';
import PropTypes from 'prop-types';

import { format } from 'd3-format';
import { timeFormat } from 'd3-time-format';

import { ChartCanvas, Chart } from 'react-stockcharts';
import { LineSeries } from 'react-stockcharts/lib/series';
import { XAxis, YAxis } from 'react-stockcharts/lib/axes';
import {
  CrossHairCursor,
  MouseCoordinateX,
  MouseCoordinateY
} from 'react-stockcharts/lib/coordinates';

import { discontinuousTimeScaleProvider } from 'react-stockcharts/lib/scale';
import { HoverTooltip, GroupTooltip } from 'react-stockcharts/lib/tooltip';
import { fitWidth } from 'react-stockcharts/lib/helper';
import { last } from 'react-stockcharts/lib/utils';
import { ema } from 'react-stockcharts/lib/indicator';

const dateFormat = timeFormat('%Y%m%d-%H%M%S');
const numberFormat = format('.2f');

function tooltipContent(ys) {
  return ({ currentItem, xAccessor }) => {
    return {
      x: "BATCH-" + dateFormat(xAccessor(currentItem)),
      y: ys.map(each => ({
            label: each.label,
            value: each.value(currentItem),
            stroke: each.stroke
          }))
        .filter(line => line.value)
    };
  };
}

class LineChart extends React.Component {
  render() {
    const { data: initialData, type, width, ratio } = this.props;

    const xScaleProvider = discontinuousTimeScaleProvider.inputDateAccessor(
      d => d.batchStartTime
    );
    
    const { data, xScale, xAccessor, displayXAccessor } = xScaleProvider(
      initialData
    );
    const xExtents = [xAccessor(last(data)), xAccessor(data[0])];

    const ema50 = ema()
      .id(2)
      .options({ windowSize: 50 })
      .merge((d, c) => {
        d.ema50 = c;
      })
      .accessor(d => d.ema50);

    return (
      <ChartCanvas
        ratio={ratio}
        width={width}
        height={400}
        margin={{ left: 70, right: 70, top: 20, bottom: 30 }}
        type={type}
        pointsPerPxThreshold={1}
        seriesName="Batch stats"
        data={data}
        xAccessor={xAccessor}
        displayXAccessor={displayXAccessor}
        xScale={xScale}
        xExtents={xExtents}
      >
        <Chart id={1} yExtents={d => [d.average, d.standardDeviation ]}>
          <XAxis
            axisAt="bottom"
            orient="bottom"
            ticks={5}
          //  tickInterval={5}
            stroke="rgb(211, 194, 38)"
            tickStroke="rgb(211, 194, 38)"
          />
          <YAxis
            axisAt="right"
            orient="right"
            // tickInterval={5}
            // tickValues={[40, 60]}
            //ticks={5}
            stroke="rgb(211, 194, 38)"
            tickStroke="rgb(211, 194, 38)"
          />
          <MouseCoordinateX
            at="bottom"
            orient="bottom"
            displayFormat={timeFormat('%Y-%m-%d')}
          />
          <MouseCoordinateY
            at="right"
            orient="right"
            displayFormat={format('.2f')}
          />

          <LineSeries yAccessor={d => d.average} stroke="rgb(211, 194, 38)" />

          <LineSeries yAccessor={d => d.standardDeviation} />

          <HoverTooltip
            yAccessor={ema50.accessor()}
            tooltipContent={tooltipContent([
              {
                label: 'Average',
                value: d => numberFormat(d.average),
               // stroke: 'rgb(235, 215, 37)'
              },
              {
                label: 'Standard Deviation',
                value: d => numberFormat(d.standardDeviation),
               // stroke: 'rgb(78, 173, 250)'
              }
            ])}
            fontSize={15}
          />

        </Chart>

        <CrossHairCursor />
      </ChartCanvas>
    );
  }
}

LineChart.propTypes = {
  data: PropTypes.array.isRequired,
  width: PropTypes.number.isRequired,
  ratio: PropTypes.number.isRequired,
  type: PropTypes.oneOf(['svg', 'hybrid']).isRequired
};

LineChart.defaultProps = {
  type: 'svg'
};
LineChart = fitWidth(LineChart);

export default LineChart;
