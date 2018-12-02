import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { HubConnectionBuilder } from '@aspnet/signalr';

import { actionCreators } from '../store/Measurements';
import Plot from 'react-plotly.js';
import { Tab, Tabs, TabList, TabPanel } from 'react-tabs';
import 'react-tabs/style/react-tabs.css';

let hubConnection = null;
class Measurements extends Component {

    constructor() {
        super();
        this.state = {

            tabIndex: 0
        };
        this.handleNotification = this.handleNotification.bind(this);
    }

    componentWillMount() {

        this.props.requestMeasurements('pressure');
        this.props.requestMeasurements('humidity');
        this.props.requestMeasurements('temperature_humidity');
        this.props.requestMeasurements('temperature_pressure');
        this.handleNotification();
    }

    handleNotification() {
        if (hubConnection) {
            console.log("unsuscribe");
            hubConnection.off("Notify");
            hubConnection = null;
        }

        hubConnection = new HubConnectionBuilder()
            .withUrl('/Notifier')
            .build();

        hubConnection.on("Notify", (data) => {
            this.props.receiveMeasurementsUpdate(data);
            //console.log("data", data);

        });
        hubConnection.start();
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
                            useResizeHandler
                            style={{ width: "100%", height: "100%" }}
                            layout={{
                                autosize: true,
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
                            useResizeHandler
                            style={{ width: "100%", height: "100%" }}
                            layout={{
                                autosize: true,
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
                            useResizeHandler
                            style={{ width: "100%", height: "100%" }}
                            layout={{
                                autosize: true,
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
                            useResizeHandler
                            style={{ width: "100%", height: "100%" }}
                            layout={{
                                autosize: true,
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
