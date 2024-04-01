# Постановка задачи

Необходимо реализовать прототип сервиса для сбора заявок на выступление для IT конференции.

Заявка представляет собой следующие данные:

- идентификатор пользователя / автора заявки - Guid, обязателен
- тип активности - одно значение из перечисления (доклад, мастеркласс, дискуссия), обязателен
- название - строка, до 100 символов, обязательное
- краткое описание для сайта - строка, до 300 символов, не обязательное
- план - строка, до 1000 символов, обязателен

Сервис должен реализовывать следующие операции (контракты под катом):

- создание заявки
    
    ```bash
    POST /applications
    {
    	author: "ddfea950-d878-4bfe-a5d7-e9771e830cbd",
    	activity: "Report",
    	name: "Новые фичи C# vNext",
    	description: "Расскажу что нас ждет в новом релизе!",
    	outline: "очень много текста... прямо детальный план доклада!",
    }
    ===>
    {
    	id: "9c53ea53-a88d-4367-ad8a-281738690412",
    	author: "ddfea950-d878-4bfe-a5d7-e9771e830cbd",
    	activity: "Report",
    	name: "Новые фичи C# vNext",
    	description: "Расскажу что нас ждет в новом релизе!",
    	outline: "очень много текста... прямо детальный план доклада!",
    }
    ```
    
- редактирование заявки
    
    ```bash
    PUT /applications/9c53ea53-a88d-4367-ad8a-281738690412
    {
    	activity: "Report",
    	name: "Новые фичи C# theNextGeneratin",
    	description: "Расскажу что нас ждет в новейшем релизе!",
    	outline: "еще больше текста...",
    }
    ===>
    {
    	id: "9c53ea53-a88d-4367-ad8a-281738690412",
    	author: "ddfea950-d878-4bfe-a5d7-e9771e830cbd",
    	activity: "Report",
    	name: "Новые фичи C# theNextGeneratin",
    	description: "Расскажу что нас ждет в новейшем релизе!",
    	outline: "еще больше текста...",
    }
    ```
    
- удаление заявки
    
    ```bash
    DELETE /applications/9c53ea53-a88d-4367-ad8a-281738690412
    ==>
    OK, 200
    ```
    
- отправка заявки на рассмотрение программным комитетом
    
    ```bash
    POST /applications/9c53ea53-a88d-4367-ad8a-281738690412/submit
    ==>
    OK, 200
    ```
    
- получение заявок поданных после указанной даты
    
    ```bash
    GET /applications?submittedAfter="2024-01-01 23:00:00.00"
    ==>
    [
    	{
    		id: "9c53ea53-a88d-4367-ad8a-281738690412",
    		author: "ddfea950-d878-4bfe-a5d7-e9771e830cbd",
    		activity: "Report",
    		name: "Новые фичи C# theNextGeneratin",
    		description: "Расскажу что нас ждет в новейшем релизе!",
    		outline: "очень много текста...",
    	},
    	...
    ]
    ```
    
- получение заявок не поданных и старше определенной даты
    
    ```bash
    GET /applications?unsubmittedOlder="2024-01-01 23:00:00.00"
    ==>
    [
    	{
    		id: "9c53ea53-a88d-4367-ad8a-281738690412",
    		author: "ddfea950-d878-4bfe-a5d7-e9771e830cbd",
    		activity: "Report",
    		name: "Новые фичи C# theNextGeneratin",
    		description: "Расскажу что нас ждет в новейшем релизе!",
    		outline: "очень много текста...",
    	},
    	...
    ]
    ```
    
- получение текущей не поданной заявки для указанного пользователя
    
    ```bash
    GET /users/ddfea950-d878-4bfe-a5d7-e9771e830cbd/currentapplication
    ==>
    {
    	id: "9c53ea53-a88d-4367-ad8a-281738690412",
    	author: "ddfea950-d878-4bfe-a5d7-e9771e830cbd",
    	activity: "Report",
    	name: "Новые фичи C# theNextGeneratin",
    	description: "Расскажу что нас ждет в новейшем релизе!",
    	outline: "очень много текста...",
    }
    ```
    
- получение заявки по идентификатору
    
    ```bash
    GET /applications/9c53ea53-a88d-4367-ad8a-281738690412
    ==>
    {
    	id: "9c53ea53-a88d-4367-ad8a-281738690412",
    	author: "ddfea950-d878-4bfe-a5d7-e9771e830cbd",
    	activity: "Report",
    	name: "Новые фичи C# theNextGeneratin",
    	description: "Расскажу что нас ждет в новейшем релизе!",
    	outline: "очень много текста...",
    }
    ```
    
- получение списка возможных типов активности
    
    ```bash
    GET /activities
    ==>
    [
    	{ 
    		activity: "Report",
    		description: "Доклад, 35-45 минут"
    	},
    	{ 
    		activity: "Masterclass",
    		description: "Мастеркласс, 1-2 часа"
    	},
    	{ 
    		activity: "Discussion",
    		description: "Дискуссия / круглый стол, 40-50 минут"
    	}
    ]
    ```
    

##

## Критерии приемки

- у пользователя может только одна не отправленная заявка (черновика заявки)
- нельзя создать заявку не указав идентификатор пользователя
- нельзя создать заявку не указав хотя бы еще одно поле помимо идентификатора пользователя
- нельзя отредактировать заявку так, чтобы  в ней не остались заполненными идентификатор пользователя + еще одно поле
- нельзя редактировать отправленные на рассмотрение заявки
- нельзя отменить / удалить заявки отправленные на рассмотрение
- нельзя удалить или редактировать не существующую заявку
- можно отправить на рассмотрение только заявки у которых заполнены все обязательные поля
- нельзя отправить на рассмотрение не существующую заявку
- запрос на получение поданных и не поданных заявок одновременно должен считаться не корректным


## Определение готовности

- сервис реализован в полном объеме согласно заданию
- критерии приемки выполняются
- сервис хранит свое состояние в базе данных и данные не теряются после рестарта
- схема базы данных описана в миграциях и автоматически разворачивается при старте сервиса
- написана инструкция по запуску сервиса
- сервис опубликован на GitHub в публичном репозитории, актуальная версия исходного кода в ветке мастер

## Технический стек

При реализации сервиса рекомендуется использовать следующие технологии:

- asp.net web api (https://learn.microsoft.com/en-gb/aspnet/core/fundamentals/apis?view=aspnetcore-8.0)
- asp.net dependency injection framework (https://learn.microsoft.com/en-gb/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-8.0)
- Entity Framework (https://learn.microsoft.com/en-gb/aspnet/core/data/entity-framework-6?view=aspnetcore-8.0) или Dapper.Net + FluentMigrator (https://github.com/DapperLib/Dapper & [https://fluentmigrator.github.io](https://fluentmigrator.github.io/))
- postgresql и npgsql([https://www.postgresql.org](https://www.postgresql.org/) & [https://www.npgsql.org](https://www.npgsql.org/))
