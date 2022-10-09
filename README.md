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
    "DefaultConnection": "Data Source=localhost;Initial Catalog=MatchTableDB;Integrated Security=SSPI;"
  }
}
```
* Execute this mandatory database script given in `DatabaseScript` folder in the repository (`DatabaseScript\DatabaseSchemaAndSeedDataScript.sql`)
  * It will create sample tables schema and insert the seed data

## After Running the Application
There are **three** ways to provide the required input in the application, You can follow one from these instructed ways:

**1. Run the application and there will be reader in the console for the inputs**

Example:
```
-TableName1 : SourceTable1
-TableName2 : SourceTable2
-PrimaryKey : SocialSecurityNumber
```

**2. Build the application and go to the application build path (eg: `{YourFolderDirectory}\TablesComparer\MatchTables\bin\Debug\net6.0` and after the console pop up, then put this command**

```MatchTables.exe -TableName1 SourceTable1 -TableName2 SourceTable2 -Primarykey SocialSecurityNumber```

**3. Run with predefined command line arguments**
To setup the arguments in visual studio: 
* Right-Click on Project. Go to Properties -> Debug -> General -> Command line arguments
* Put this command in the argument box:

```-TableName1 SourceTable1 -TableName2 SourceTable2 -Primarykey SocialSecurityNumber```

![image](https://user-images.githubusercontent.com/56506587/194725870-0ba9b45e-5b3b-41f4-882c-23f6bdb95f5b.png)

## Sample Output

![image](https://user-images.githubusercontent.com/56506587/194727921-cf2cfc41-5b62-4859-b17e-4d1332935c7d.png)

## Run Unit Test
To run unit test cases, go to the `Test Explorer` and run all the tests at once...
Sample unit test output:

![image](https://user-images.githubusercontent.com/56506587/194745274-a9a28a7f-857c-4e6f-a25c-5ffc4f7f7a0e.png)

## Further Contacts:
`Md Abdul Kuddus Parvez` 
Email : parvez.mak4132@gmail.com
