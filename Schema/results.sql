drop table if exists problem_result;

create table problem_result (
	id varchar(64) primary key,
	year integer not null,
	day integer not null,
	part integer not null,
	input varchar(200) not null,
	result varchar(255),
	error varchar(500),
	accepted smallint not null,
	execution_time double precision not null,
	created timestamptz not null
);

create index i_problem_result_ydp on problem_result (year, day, part);