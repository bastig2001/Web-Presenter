drop table if exists Slides;
drop table if exists Presentations;
drop table if exists Presenter;

create table Presenter(
    Id       int          generated always as identity not null primary key
,   Username varchar(256)                               not null default 'General Kenobi'
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

insert into Presenter
  (Username)
values
  ('Sebi')
;
