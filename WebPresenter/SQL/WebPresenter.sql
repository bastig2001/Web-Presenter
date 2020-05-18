drop table if exists Slides;
drop table if exists Presentations;

begin transaction;

create table Presentations (
    Id             varchar(256) not null primary key
,   Title          varchar(256) not null
,   Text           text
,   PermanentNotes text
);

create table Slides (
    Presentation varchar(256) not null
,   SlideNr      smallint     not null
,   Notes        text
,   Image        text
,   primary key (Presentation, SlideNr)
);

commit;

begin transaction;

insert into Presentations
values 
('1', 'First Presentation', 'Some Text', 'Some Notes')
;

insert into Slides
values
('1', 0, '', '')
;

commit;
