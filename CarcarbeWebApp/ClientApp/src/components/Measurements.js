import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../store/Measurements';

class Measurements extends Component {

    componentWillMount() {
                
        this.props.requestMeasurements('pressure');
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
    state => state.measurementsPage,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Measurements);
