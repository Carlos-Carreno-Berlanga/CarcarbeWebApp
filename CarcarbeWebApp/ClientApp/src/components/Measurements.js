import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../store/Measurements';
import Plot from 'react-plotly.js';

class Measurements extends Component {

    componentWillMount() {

        this.props.requestMeasurements('pressure');
    }


    render() {
        let x = [];
        let y = [];
        let cont = 0;
        this.props.measurements.forEach(measurement => { x.push(measurement.timeStamp); cont++; y.push(measurement.value); });

        return (
            <div>
                <h1>measurementsPage</h1>
                <Plot
                    data={[
                        {
                            x,
                            y,
                            type: 'scatter',
                            mode: 'lines',
                            marker: { color: 'red' },
                        },
                    ]}
                    layout={{
                        width: 720, height: 740,

                        title: 'A Fancy Plot',
                        xaxis: {
                            autorange: true,
                            range: [x[0], x[x.length - 1]],
                            rangeselector: {
                                buttons: [
                                    {
                                        count: 1,
                                        label: '1d',
                                        step: 'day',
                                        stepmode: 'backward'
                                    },
                                    {
                                        count: 6,
                                        label: '6d',
                                        step: 'day',
                                        stepmode: 'backward'
                                    },
                                    { step: 'all' }
                                ]
                            },
                            rangeslider: { range: [x[0], x[x.length - 1]] },
                            type: 'date'
                        },
                    }}
                />
            </div>
        );
    }
}

export default connect(
    state => state.measurementsPage,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Measurements);
