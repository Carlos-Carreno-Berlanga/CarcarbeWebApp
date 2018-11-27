CREATE TYPE mood AS ENUM ('sad', 'ok', 'happy');

--

CREATE TYPE public.measurementtype AS ENUM
    ('temperature', 'humidity', 'pressure', 'temperature_humidity', 'temperature_pressure');

----

CREATE TABLE public.measurement
(
id serial,
value NUMERIC (5, 2),
measurement_type MeasurementType,
time_stamp_timezone TIMESTAMPTZ  
  
)

INSERT INTO MEASUREMENT (value, MEASUREMENT_TYPE,TIME_STAMP_TIMEZONE)
VALUES
 (
10.2,
 'temperature',
 (SELECT NOW())
 );