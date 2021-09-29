alter table medicalfiles add Description varchar (500);
alter table medicalprocess add EmergencyContactName varchar (500);
alter table medicalprocess add PhoneEmergencyContactName varchar (500);
alter table patient add TagNumber varchar(100);
alter table patient add City varchar(200);
alter table patient add Weight varchar(100);
alter table patient add WeightUnitOfMeasure varchar(100);
alter table patient add Size varchar(200);
alter table patient add SizeUnitOfMeasure varchar(100);
alter table patient add LastName varchar(200);

alter table consultation add BreathingFrequency varchar(30);
alter table consultation add HeartFrequency varchar(30);
alter table consultation add Temperature varchar(30);
alter table consultation add Symptom varchar(1000);

select * from patient
UPDATE Patient SET TAGNumber='EXP-'+ SUBSTRING( CONVERT(varchar(90), NEWID()),0,9)
select 'EXP-'+ SUBSTRING( CONVERT(varchar(90), NEWID()),0,9)