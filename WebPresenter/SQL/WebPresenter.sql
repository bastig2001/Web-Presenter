drop table if exists Slides;
drop table if exists Presentations;
drop table if exists Presenter;

begin transaction;

create table Presenter(
    Id       int          generated always as identity not null primary key
,   Username varchar(256)                               not null
);

create table Presentations(
    Id          int           generated always as identity not null primary key
,   Title       varchar(256)  not null
,   Text        text
,   Notes       text
,   PresenterId int           not null references Presenter
);

create table Slides(
	Id           int      generated always as identity not null primary key
,	SeqNr        smallint                               not null
,	Notes        text
,	Image        text
,	Presentation int                                    not null references Presentations
);

commit;

insert into Presenter
  (Username)
values
  ('TestUser')
;

select *
  from Presenter
;
