drop table if exists CalendarEvents;
CREATE TABLE IF NOT EXISTS CalendarEvents
(
    id            INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
    event_name    INTEGER,
    start_date    date,
    end_date      date,
    duration      time,
    created_at    date,
    last_modified date
);

INSERT INTO CalendarEvents(event_name)
VALUES ('foo'),
       ('bar');

select *
from CalendarEvents;
