SELECT *
FROM CalendarEvents
where id = @id
   or event_name = @event_name
   or event_name like '%' + @event_name + '%'

   or description = @description
   OR description like '%' + @description + '%'
;


/** 
  
  Samples
  
  select * from CalendarEvents where description like '%Taj%' or event_name like 'E%'
   select * from CalendarEvents where status = 'published' or status like '%pub%'
-- select * from CalendarEvents
  
 */
