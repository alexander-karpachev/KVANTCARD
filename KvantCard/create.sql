-- Автор: Карпачёв А.А.
-- Описание: создание таблиц в MySQL
-- Version 1.0
-- Журнал:
--------- 26.12.2018 Созданы основные таблицы и словари (1-9)

----- Простые справочники
-- 2.1. Статус родителя
create table parent_status(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  title varchar(100),
  primary key (ID)  
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;

-- 3.1. doc_type
create table document_types(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  title varchar(100),	
  primary key (ID)  
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;

-- 3.2. file_types
create table file_types(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  title varchar(100),	
  primary key (ID)  
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;

create table document_set_group(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  title varchar(100),	
  primary key (ID)  
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;

-- 4.1. квантум
create table kvantum(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  title varchar(100),	
  primary key (ID)  
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;
-- 4.2. квантум группы
create table kvantum_group(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  title varchar(100),	
  primary key (ID)  
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;
-- 4.3. квантум смены
create table smena(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  title varchar(100),	
  primary key (ID)  
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;
-- 4.4. квантум уровни
create table levels(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  title varchar(100),	
  primary key (ID)  
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;

-- 6.1. тип проекта
create table project_type(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  title varchar(100),	
  primary key (ID)  
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;
-- 6.2. статус проекта
create table project_status(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  title varchar(100),	
  primary key (ID)  
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;

-- 7.1. Команда
create table team(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  title varchar(100),
  primary key (ID)
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;

-- 7.2. роли
create table team_role(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  title varchar(100),	
  primary key (ID)
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;

-- 8.1. Тип мероприятия
create table social_activity_type(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  title varchar(100),	
  primary key (ID)
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;
-- 8.2. социальный статус 
create table publict_type(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  title varchar(100),	
  primary key (ID)
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;

-- 9.1. Должности
create table positions(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  title varchar(100),	
  primary key (ID)
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;


------- Родители
create table parent(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  
  last_name varchar(100),
  first_name varchar(100),
  middle_name varchar(100),

  phone varchar(10),
  status_id mediumint,  -- TODO FK parent_status

  primary key (ID),
  FOREIGN KEY (status_id) REFERENCES parent_status(id) ON UPDATE CASCADE on delete restrict
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;

------ Документы
create table document(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  title varchar(100),	
  type_id mediumint,	-- TODO FK type
  file_type_id mediumint,	-- TODO FK type
  binary_file blob,
  primary key (ID),  
  FOREIGN KEY (type_id) REFERENCES document_types(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (file_type_id) REFERENCES file_types(id) ON UPDATE CASCADE on delete restrict
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;

------ Группы документов
create table document_set(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  group_id mediumint,
  document_id mediumint,	-- TODO FK type
  primary key (ID),  
  FOREIGN KEY (group_id) REFERENCES document_set_group(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (document_id) REFERENCES document(id) ON UPDATE CASCADE on delete restrict
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;

------- Программы
create table program(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  level_id MEDIUMINT not null,
  kvantum_id MEDIUMINT not null,
  group_id MEDIUMINT not null,
  smena_id MEDIUMINT not null,
  start_date MEDIUMINT not null,
  end_date MEDIUMINT not null,
  primary key (ID),
  FOREIGN KEY (level_id) REFERENCES levels(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (kvantum_id) REFERENCES kvantum(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (group_id) REFERENCES kvantum_group(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (smena_id) REFERENCES smena(id) ON UPDATE CASCADE on delete restrict
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;

------- Дети 

create table student(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  
  last_name varchar(100),
  first_name varchar(100),
  middle_name varchar(100),
  
  birth_date date,
  address varchar(1000),
  phone varchar(10),
  school varchar(10),
  
  parent1_id mediumint,  -- TODO FK parents
  parent2_id mediumint,  -- TODO FK parents
  
  document_set_group mediumint, -- TODO FK document_set
  register_date date,
  current_program_id mediumint, -- TODO FK program
   
  primary key (ID), 
  FOREIGN KEY (parent1_id) REFERENCES parent(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (parent2_id) REFERENCES parent(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (document_set_group_id) REFERENCES document_set_group(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (current_program_id) REFERENCES program(id) ON UPDATE CASCADE on delete
 restrict
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;


-- Программ - архив
create table student_program_hist(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  student_id MEDIUMINT not null,
  level_id MEDIUMINT not null,
  kvantum_id MEDIUMINT not null,
  group_id MEDIUMINT not null,
  smena_id MEDIUMINT not null,
  start_date MEDIUMINT not null,
  end_date MEDIUMINT not null,
  primary key (ID),
  FOREIGN KEY (student_id) REFERENCES student(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (level_id) REFERENCES levels(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (kvantum_id) REFERENCES kvantum(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (group_id) REFERENCES kvantum_group(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (smena_id) REFERENCES smena(id) ON UPDATE CASCADE on delete restrict
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;


-- 5. Наставник
create table mentor(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  last_name varchar(100),
  first_name varchar(100),
  middle_name varchar(100),
  phone varchar(10),
  primary_kvantum_id MEDIUMINT,
  secondary_kvantum_id MEDIUMINT,
  primary key (ID),
  FOREIGN KEY (primary_kvantum_id) REFERENCES kvantum(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (secondary_kvantum_id) REFERENCES kvantum(id) ON UPDATE CASCADE on delete restrict
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;

-- 5.1. история работы в квантумах
create table kvantum_mentors_hist(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  mentor_id MEDIUMINT not null,
  kvantum_id MEDIUMINT not null,
  start_date MEDIUMINT not null,
  end_date MEDIUMINT,
  primary key (ID),
  FOREIGN KEY (kvantum_id) REFERENCES kvantum(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (mentor_id) REFERENCES mentor(id) ON UPDATE CASCADE on delete restrict
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;

-- 6. проекты
create table projects(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  title varchar(100),	
  project_type_id MEDIUMINT not null,
  document_set_group_id MEDIUMINT,
  project_status_id MEDIUMINT,
  start_date MEDIUMINT not null,
  end_date MEDIUMINT,
  description varchar(4000),	
  primary key (ID),
  FOREIGN KEY (document_set_group_id) REFERENCES document_set_group(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (project_type_id) REFERENCES project_type(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (project_status_id) REFERENCES project_status(id) ON UPDATE CASCADE on delete restrict
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;

-- 7.2. Состав команды
create table team_build(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  team_id MEDIUMINT not null,
  project_id MEDIUMINT not null,
  student_id MEDIUMINT not null,
  role_id MEDIUMINT not null,
  start_date MEDIUMINT not null,
  end_date MEDIUMINT,
  primary key (ID),
  FOREIGN KEY (team_id) REFERENCES team(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (project_id) REFERENCES projects(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (role_id) REFERENCES team_role(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (student_id) REFERENCES student(id) ON UPDATE CASCADE on delete restrict
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;


-- 7.3. Наставник команды
create table team_mentor(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  team_id MEDIUMINT not null,
  project_id MEDIUMINT not null,
  mentor_id MEDIUMINT not null,
  start_date MEDIUMINT not null,
  end_date MEDIUMINT,
  primary key (ID),
  FOREIGN KEY (team_id) REFERENCES team(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (project_id) REFERENCES projects(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (mentor_id) REFERENCES mentor(id) ON UPDATE CASCADE on delete restrict
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;

-- 7.3. Деятельность команды по проекту
create table team_activity(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  team_id MEDIUMINT not null,
  project_id MEDIUMINT not null,
  start_date MEDIUMINT not null,
  end_date MEDIUMINT,
  document_set_group_id MEDIUMINT,
  description varchar(4000),
  primary key (ID),
  FOREIGN KEY (team_id) REFERENCES team(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (project_id) REFERENCES projects(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (document_set_group_id) REFERENCES document_set_group(id) ON UPDATE CASCADE on delete restrict
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;

-- 8. Мероприятия
create table social_activity(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  type_id MEDIUMINT not null,
  publict_type_id  MEDIUMINT not null,
  project_id MEDIUMINT not null,
  start_date MEDIUMINT not null,
  end_date MEDIUMINT,
  document_set_group_id MEDIUMINT,
  description varchar(4000),
  primary key (ID),
  FOREIGN KEY (project_id) REFERENCES projects(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (type_id) REFERENCES social_activity_type(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (publict_type_id) REFERENCES publict_type(id) ON UPDATE CASCADE on delete restrict
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;

-- 8.3. Мероприятия с участием отдельных учеников
create table student_social_activity(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  student_id MEDIUMINT not null,
  project_id MEDIUMINT not null,
  social_activity_id MEDIUMINT not null,
  primary key (ID),
  FOREIGN KEY (student_id) REFERENCES student(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (project_id) REFERENCES projects(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (social_activity_id) REFERENCES social_activity(id) ON UPDATE CASCADE on delete restrict
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;

-- 8.4. Мероприятия с участием команд
create table team_social_activity(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  team_id MEDIUMINT not null,
  project_id MEDIUMINT not null,
  social_activity_id MEDIUMINT not null,
  primary key (ID),
  FOREIGN KEY (team_id) REFERENCES team(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (project_id) REFERENCES projects(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (social_activity_id) REFERENCES social_activity(id) ON UPDATE CASCADE on delete restrict
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;

-- 8.5. Участие наставников в мероприяти
create table mentor_social_activity(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  mentor_id MEDIUMINT not null,
  project_id MEDIUMINT not null,
  social_activity_id MEDIUMINT not null,
  primary key (ID),
  FOREIGN KEY (mentor_id) REFERENCES mentor(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (project_id) REFERENCES projects(id) ON UPDATE CASCADE on delete restrict,
  FOREIGN KEY (social_activity_id) REFERENCES social_activity(id) ON UPDATE CASCADE on delete restrict
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;

-- 9. Администрация
create table personnel(
  ID MEDIUMINT NOT NULL AUTO_INCREMENT,
  last_name varchar(100),
  first_name varchar(100),
  middle_name varchar(100),
  position_id mediumint,
  phone varchar(10),
  primary key (ID),
  FOREIGN KEY (position_id) REFERENCES positions(id) ON UPDATE CASCADE on delete restrict
) ENGINE=InnoDB CHARACTER SET=UTF8MB4;


-- Словари
CREATE TABLE `foreign_names` (
	`id` INT NOT NULL,
	`name` VARCHAR(25) NOT NULL,
	`meaning` VARCHAR(1000) NOT NULL,
	`gender` VARCHAR(6) NOT NULL,
	`origin` VARCHAR(24) NOT NULL,
	`PeoplesCount` INT NULL,
	`WhenPeoplesCount` DATETIME NULL,
	`Source` NVARCHAR(10) NOT NULL
);

CREATE TABLE `russian_names` (
	`ID` INT NOT NULL,
	`Name` NVARCHAR(100) NOT NULL,
	`Sex` NVARCHAR(1) NULL,
	`PeoplesCount` INT NULL,
	`WhenPeoplesCount` DATETIME NULL,
	`Source` VARCHAR(10) NULL
);

CREATE TABLE `russian_surnames` (
	`ID` INT NOT NULL,
	`Surname` NVARCHAR(100) NOT NULL,
	`Sex` NVARCHAR(1) NULL,
	`PeoplesCount` INT NULL,
	`WhenPeoplesCount` DATETIME NULL,
	`Source` VARCHAR(50) NULL
);

