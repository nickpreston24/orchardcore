UPDATE CalendarEvents
SET 
    event_name = @event_name,
    description = @description,
    status = @status,
    start_date = @start_date
WHERE
        id = @id;
