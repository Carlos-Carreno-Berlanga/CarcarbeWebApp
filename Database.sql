CREATE TYPE mood AS ENUM ('sad', 'ok', 'happy');

CREATE TABLE public.measurement
(
id serial,
value NUMERIC (5, 2),
measurement_type MeasurementType,
time_stamp_timezone TIMESTAMPTZ  
  
)