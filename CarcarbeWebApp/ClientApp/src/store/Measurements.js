

const requestMeasurementsType = 'REQUEST_MEASUREMENTS';
const receiveMeasurementsType = 'RECEIVE_MEASUREMENTS';

const initialState = { measurements: [], measurement_type: null};

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
        return {
            ...state,
            measurement_type: action.measurement_type,
            measurements: action.measurements,
            isLoading: false
        };
    }
   
    return state;
};
