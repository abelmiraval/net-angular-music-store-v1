# Commands

- Create project
```
dotnet new webapi -o MusicStore.API
```

- Create solution
```
dotnet new sln -o MusicStore
```

- Move project to solution
```
mv .\MusicStore.API\ .\MusicStore\

dotnet sln add .\MusicStore.API\

```

- Run solution
```
dotnet run --project .\MusicStore.API\
```

## Package Manager Console

- Add migration
```
add-migration Initial-Migration
```

- Remove migration
```
remove-migration

remove-migration -force
```

- Update database
```
update-database
```

## Dot Net

- Install dotnet-ef
```
dotnet tool install dotnet-ef --global
```

- Create scritps
```
dotnet ef migrations script -o .\script.sql --project .\MusicStore.DataAccess\ --startup-project .\MusicStore.API\
```

- Creeate migration
```
dotnet ef migrations add Init-Migration --project .\MusicStore.DataAccess\ --startup-project .\MusicStore.API\
```

- Update database
```
dotnet ef database update --project .\MusicStore.DataAccess\ --startup-project .\MusicStore.API\
```

## Extensions

- Auto-Using for C#
- C#
- C# Extensions
- C# Snippets
- SQL Server (mssql)
