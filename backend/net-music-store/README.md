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

