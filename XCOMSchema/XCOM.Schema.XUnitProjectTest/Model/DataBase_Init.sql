CREATE database if NOT EXISTS `XCOM-TestDB` default character set utf8mb4 collate utf8mb4_general_ci;
USE `XCOM-TestDB`;


drop table if exists `express`;

create table `express`
(
   SysNo                   int not null auto_increment,
   CompanyName    varchar(100) default NULL,

   organization_name    varchar(100) default NULL comment '组织名称',
   appeal_max_times     int not null comment '申诉最大次数',
   appeal_max_day       int not null comment '申诉最大天数',
   primary key (id)
)
ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='评级配置表';