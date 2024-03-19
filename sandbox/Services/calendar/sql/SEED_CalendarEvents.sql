drop table if exists CalendarEvents;
CREATE TABLE IF NOT EXISTS CalendarEvents
(
    id            INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
    event_name    text,
    description   text,
    start_date    date,
    end_date      date,
    duration      time,
    created_at    date,
    last_modified date
);

INSERT INTO CalendarEvents (event_name, description)
VALUES ('Upcoming Events', 'end-users can see what activities are coming up'),
       ('Passed Events', 'end-users can see what activities have passsed'),
       ('Update Victor on changes', '...')
;

select *
from CalendarEvents;

-- delete from calendarevents where id > 0;