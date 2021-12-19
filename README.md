This project implements <a href="https://microservices.io/patterns/data/transactional-outbox.html">transactional outbox</a> pattern for eventual consistency between microservices. For relaying messages implements <a href="https://microservices.io/patterns/data/transaction-log-tailing.html">transaction log tailing</a> pattern using <a href="https://debezium.io/">Debezium</a> and Change Data Capture(CDC).

<a href="https://microservices.io/patterns/data/polling-publisher.html">Polling publisher</a> is an alternative for relaying message but this is not the focus of the project.

## How to Run
* For MS-SQL
  * up docker-compose `docker-compose.mssql.yml`
  * create migration then update database with dotnet ef cli
  * First Check CDC is enabled (You can also follow Microsoft <a href="https://docs.microsoft.com/en-us/sql/relational-databases/track-changes/enable-and-disable-change-data-capture-sql-server?view=sql-server-ver15">documentation.</a>)
    * Check CDC Enabled
        ````sql 
        SELECT NAME, IS_CDC_ENABLED
        FROM SYS.DATABASES
        ````
    * If CDC is not enable then;
        ````sql
        USE DB -- for our example LEAVE or SHIFT
        GO
        EXEC SYS.SP_CDC_ENABLE_DB
        GO
        ````
    * Activate related outbox table for CDC
        ````sql
        EXEC SYS.SP_CDC_ENABLE_TABLE
        @source_schema = N'dbo',
        @source_name   = N'OUTBOX_EVENTS',
        @role_name     = NULL
        ````
    * If previous step returns you an error then execute below scripts then run previous scripts again;
        ````sql
        SELECT SRVNAME AS OLDNAME FROM MASTER.DBO.SYSSERVERS -- fc1a11b428b0

        SELECT SERVERPROPERTY('ServerName') AS NEWNAME --7bbf97d91d5b

        SP_DROPSERVER 'fc1a11b428b0';  
        GO  
        SP_ADDSERVER '7bbf97d91d5b', local;  
        GO
        ````
    * Check CDC is activated or not for table
        ````sql
        EXEC SYS.SP_CDC_HELP_CHANGE_DATA_CAPTURE
        ````
    * Somehow if you want to disable CDC on table;
        ````sql
        EXEC SYS.SP_CDC_DISABLE_TABLE
        @source_schema = N'dbo',  
        @source_name   = N'OUTBOX_EVENTS',  
        @capture_instance = N'LEAVE_OUTBOX_EVENTS'  
        ````
  * create debezium connector by using <a href="https://github.com/cispiroglu/cdc-with-debezium-dotnet-microservices/blob/master/postman_collection/debezium-connector.postman_collection.json">postman collection</a> / create_connector-mssql
  * check connector is created / get_connectors
  * debezium and db settings is ok so ready to go. you can use swagger for creating new leave then get event then insert from shift.
  
* For PostgreSQL
  * up docker-compose `docker-compose.postgres.yml`
  * create migration then update database with dotnet ef cli
  * for postgres prefer `debezium/postgres` image because cdc configuration ok by default. configuration not necessary like ms-sql just setup debezium connector
  * create debezium connector by using <a href:="https//github.com/cispiroglu/cdc-with-debezium-dotnet-microservices/blob/master/postman_collection/debezium-connector.postman_collection.json">postman collection</a> / create_connector-postgresql
  * check connector is created / get_connectors
  * debezium and db settings is ok so ready to go. you can use swagger for creating new leave then get event then insert from shift.
  

inspired by <a href="https://github.com/suadev/microservices-change-data-capture-with-debezium">this</a> repo.
