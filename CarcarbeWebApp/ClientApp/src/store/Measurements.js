
import { cloneDeep } from 'lodash';
const requestMeasurementsType = 'REQUEST_MEASUREMENTS';
const receiveMeasurementsType = 'RECEIVE_MEASUREMENTS';
const receiveMeasurementsUpdateType = 'RECEIVE_MEASUREMENTS_UPDATE';

const initialState = {
    measurements: {}, measurement_type: null };

export const actionCreators = {

    requestMeasurements: measurement_type => async (dispatch, getState) => {
        if (measurement_type === getState().measurementsPage.measurement_type) {
            // Don't issue a duplicate request (we already have or are loading the requested data)
            return;
        }

        dispatch({ type: requestMeasurementsType, measurement_type });

        const url = `api/Measurements/${measurement_type}`;
        const response = await fetch(url);
        const measurements = await response.json();

        dispatch({ type: receiveMeasurementsType, measurement_type, measurements });
    },

    receiveMeasurementsUpdate: measurementsUpdateData => async(dispatch) => {
        dispatch({ type: receiveMeasurementsUpdateType, measurementsUpdateData});
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === requestMeasurementsType) {
        return {
            ...state,
            measurement_type: action.measurement_type,
            isLoading: true
        };
    }

    if (action.type === receiveMeasurementsType) {
        let measurementsClone = cloneDeep(state.measurements);
        measurementsClone[action.measurement_type] = action.measurements;
        return {
            ...state,
            measurement_type: action.measurement_type,
            measurements: measurementsClone,
            isLoading: false
        };
    }

    if (action.type === receiveMeasurementsUpdateType) {
        console.log("receiveMeasurementsUpdateType measurementsUpdateData", action.measurementsUpdateData);
    }

    return state;
};
