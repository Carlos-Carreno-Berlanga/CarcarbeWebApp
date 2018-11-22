import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../store/WeatherForecasts';

class Measurements extends Component {
  componentWillMount() {
 
  }

  componentWillReceiveProps(nextProps) {
 
  }

  render() {
    return (
      <div>
        <h1>measurementsPage</h1>
 
      </div>
    );
  }
}

export default connect(
  state => state.weatherForecasts,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Measurements);
