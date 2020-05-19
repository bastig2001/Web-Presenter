drop table if exists Presentations;

begin transaction;

create table Presentations (
    Id                varchar(256) not null primary key
,   Title             varchar(256)
,   Text              text
,   PermanentNotes    text
,   SlideNotes        text[]
,   ImagePresentation text[]
);

commit;

begin transaction;

delete from Presentations;

insert into Presentations
values 
('1', 'First Presentation', 'Some Text', 'Some Notes', array[''], array[''])
;

commit;

select *
  from Presentations
;
