# MatchTables
Necessary instructions are given below for setup, build and execution of this applicaiton...

## Purpose of This Project
* Compare two identical tables of it's schema, added/delete/changed records and any other changes made.

## Cloning
To clone the repowitory to your local machine run this command. **(You must have git installed in your local machine)**

```git clone https://github.com/JonParvez/TablesComparer.git```

## Basic Machine Requirements 
* [ASP.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Application Setup Requirements
* Add your local sql server connection string in the `appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=DESKTOP-0C60KR7\\PARVEZ;Initial Catalog=TestDB;Integrated Security=SSPI;"
  }
}
```
* Execute this two mandatory sql scripts given in the repository
   1. **mandatoryTableSchema.sql** 
      * It will create all the required table schema as a replica schema of the assignment
   2. **mandatoryTableData.sql** 
      * It will insert around 100 records in those two tables

## After Running the Application
There are **three** ways to provide the required input in the application, You can follow any way instructed below:

**1. Run the application and there will be reader in the console for the inputs**

Example:
```
-TableName1 : SourceTable1
-TableName2 : SourceTable2
-PrimaryKey SocialSecurityNumber
```

**2. Build the application and go to the application build path (eg: `{YourFolderDirectory}\TablesComparer\MatchTables\bin\Debug\net6.0` and after the console pop up, then put this command**

```MatchTables.exe -TableName1 SourceTable1 -TableName2 SourceTable2 -Primarykey SocialSecurityNumber```

**3. Run with predefined command line arguments**
To setup the arguments in visual studio: 
* Right-Click on Project. Go to Properties -> Debug -> General -> Command line arguments
* Put this command in the argument box:

```-TableName1 SourceTable1 -TableName2 SourceTable2 -Primarykey SocialSecurityNumber```

![image](https://user-images.githubusercontent.com/56506587/194725870-0ba9b45e-5b3b-41f4-882c-23f6bdb95f5b.png)

## Further Contacts:
`parvez.mak4132@gmail.com`
