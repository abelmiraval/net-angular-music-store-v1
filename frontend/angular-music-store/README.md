## Installer

- VS Code
```
https://code.visualstudio.com
```

- Node JS, npm
```
https://github.com/coreybutler/nvm-windows
```
```
https://github.com/nvm-sh/nvm
```

- Angular CLI
```
npm install -g @angular/cli
```

- Angular vs Node JS
```
https://gist.github.com/LayZeeDK/c822cc812f75bb07b7c55d07ba2719b3
```

# Extensions
- Angular Snippets

# Commands

- Create project
```
ng new <name-project> [options] | ng n <name-project> [options]
```
```
ng new angular-music-store --strict --style=scss --skip-tests
```

- Run server
```
ng server <path-project> | ng s <path-project>
```

- Generate schematic
```
ng generate <schematic> [options] | ng g<schematic> [options]
```
```
ng generate component <name> | ng g c <name>
```
```
ng generate service <name> | ng g s <name>
```

- Build project
```
ng build <project> [options] | ng b <project> [options]
```  
```
ng build | ng b
```

- Build project with enviroment
```
ng build --configuration <name-enviroment> | ng b --c <name-enviroment>
```
```
ng build --configuration quality | ng b --c quality
```

## Dependencies

- Angular material
```
ng add @angular/material
```

- ESLint
```
ng add @angular-eslint/schematics
```

- Prettier
```
npm install -D prettier eslint-config-prettier eslint-plugin-prettier
```

## Setting Husky
```
npx husky-init && npm install
```
- In the folder .husky, add next line.

```
npm run pre-commit
```
