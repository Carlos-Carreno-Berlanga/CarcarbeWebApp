import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/Measurements';
import Plot from 'react-plotly.js';
import { Tab, Tabs, TabList, TabPanel } from 'react-tabs';
import 'react-tabs/style/react-tabs.css';

class Measurements extends Component {

    constructor() {
        super();
        this.state = {

            tabIndex: 0
        };
    }

    componentWillMount() {

        this.props.requestMeasurements('pressure');
        this.props.requestMeasurements('humidity');
        this.props.requestMeasurements('temperature_humidity');
        this.props.requestMeasurements('temperature_pressure');
    }


    render() {
        let pressureX = [];
        let pressureY = [];
        if (this.props.measurements && this.props.measurements.pressure) {
            this.props.measurements.pressure.forEach(measurement => { pressureX.push(measurement.timeStamp); pressureY.push(measurement.value); });
        }

        let humidityX = [];
        let humidityY = [];
        if (this.props.measurements && this.props.measurements.humidity) {
            this.props.measurements.humidity.forEach(measurement => { humidityX.push(measurement.timeStamp); humidityY.push(measurement.value); });
        }

        let temperaturePressureX = [];
        let temperaturePressureY = [];
        if (this.props.measurements && this.props.measurements.temperature_pressure) {
            this.props.measurements.temperature_pressure.forEach(measurement => { temperaturePressureX.push(measurement.timeStamp); temperaturePressureY.push(measurement.value); });
        }

        let temperatureHumidityX = [];
        let temperatureHumidityY = [];
        if (this.props.measurements && this.props.measurements.temperature_humidity) {
            this.props.measurements.temperature_humidity.forEach(measurement => { temperatureHumidityX.push(measurement.timeStamp); temperatureHumidityY.push(measurement.value); });
        }

        return (
            <div>
                <Tabs defaultIndex={this.state.tabIndex} onSelect={tabIndex => this.setState({ tabIndex })}>
                    <TabList>
                        <Tab>
                            Pressure
                                    </Tab>
                        <Tab>
                            Humidity
                                    </Tab>
                        <Tab>
                            Temperature Sensor 1
                                    </Tab>
                        <Tab>
                            Temperature Sensor 2
                                    </Tab>
                    </TabList>
                    <TabPanel>
                        <Plot
                            data={[
                                {
                                    x: pressureX,
                                    y: pressureY,
                                    type: 'scatter',
                                    mode: 'lines',
                                    marker: { color: 'blue' }
                                }
                            ]}
                            layout={{
                                width: 1120, height: 740,

                                title: 'Pressure',
                                xaxis: {
                                    autorange: true,
                                    range: [pressureX[0], pressureX[pressureX.length - 1]],
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
                                    rangeslider: { range: [pressureX[0], pressureX[pressureX.length - 1]] },
                                    type: 'date'
                                }
                            }}
                        />
                    </TabPanel>
                    <TabPanel>
                        <Plot
                            data={[
                                {
                                    x: humidityX,
                                    y: humidityY,
                                    type: 'scatter',
                                    mode: 'lines',
                                    marker: { color: 'red' }
                                }
                            ]}
                            layout={{
                                width: 1120, height: 740,

                                title: 'Humidity',
                                xaxis: {
                                    autorange: true,
                                    range: [humidityX[0], humidityX[humidityX.length - 1]],
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
                                    rangeslider: { range: [humidityX[0], humidityX[humidityX.length - 1]] },
                                    type: 'date'
                                }
                            }}
                        />
                    </TabPanel>
                    <TabPanel>
                        <Plot
                            data={[
                                {
                                    x: temperaturePressureX,
                                    y: temperaturePressureY,
                                    type: 'scatter',
                                    mode: 'lines',
                                    marker: { color: 'yellow' }
                                }
                            ]}
                            layout={{
                                width: 1120, height: 740,

                                title: 'Temperature Sensor 1',
                                xaxis: {
                                    autorange: true,
                                    range: [temperaturePressureX[0], temperaturePressureX[temperaturePressureX.length - 1]],
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
                                    rangeslider: { range: [temperaturePressureX[0], temperaturePressureX[temperaturePressureX.length - 1]] },
                                    type: 'date'
                                }
                            }}
                        />
                    </TabPanel>
                    <TabPanel>
                        <Plot
                            data={[
                                {
                                    x: temperatureHumidityX,
                                    y: temperatureHumidityY,
                                    type: 'scatter',
                                    mode: 'lines',
                                    marker: { color: 'green' }
                                }
                            ]}
                            layout={{
                                width: 1120, height: 740,

                                title: 'Temperature Sensor 2',
                                xaxis: {
                                    autorange: true,
                                    range: [temperatureHumidityX[0], temperatureHumidityX[temperatureHumidityX.length - 1]],
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
                                    rangeslider: { range: [temperatureHumidityX[0], temperatureHumidityX[temperatureHumidityX.length - 1]] },
                                    type: 'date'
                                }
                            }}
                        />
                    </TabPanel>
                </Tabs>


            </div>
        );
    }
}

export default connect(
    state => state.measurementsPage,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Measurements);
