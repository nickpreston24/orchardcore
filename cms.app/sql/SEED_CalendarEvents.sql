drop table if exists CalendarEvents;
CREATE TABLE IF NOT EXISTS CalendarEvents
(
    id            INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
    event_name    text,
    description   text,
    status        text,
    start_date    date,
    end_date      date,
    duration      time,
    created_at    date,
    last_modified date
);
/*

INSERT INTO CalendarEvents (event_name, description, start_date)
VALUES ('Upcoming Events', 'end-users can see what activities are coming up', date('now')),
       ('Passed Events', 'end-users can see what activities have passsed', date('now')),
       ('Update Victor on changes', '...', date('now'))
;
*/

select *
from CalendarEvents;

-- delete from calendarevents where id > 0;
