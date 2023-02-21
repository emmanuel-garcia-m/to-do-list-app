# to-do-list-app
Es una aplicación pequeña que cumple con la funcionalidad de una lista de tareas por hacer. 
Implementa una api rest creada con .net 5.0 y aplicacion web creada con react. 

## installation

api-to-do-list-app: Implemnta dos bases de datos distintas, una para seguridad, basada en .net core identity y otra en donde se almacena los datos de la aplicación;

Para seguridad se debe ejecutar la migración sobre la base de datos llamada "Security" (o cambiando el nombre modificando la cadena de conexion).

```bash
update-database -Context ToDoListAppIdentityDbContext
```

Para la data de la aplicación se debe ejecutar la migración sobre la base de datos llamada "ToDoListProject" (o cambiando el nombre modificando la cadena de conexion).

```bash
update-database -Context ToDoListdbContext
```

web-to-do-list-app: se debe compilar y ejecutar la aplicación, en el backend se encuentra habilidatos CORS para la url "http://localhost:3000/", por lo que el proyecto frontend debe ejecutarse en ese puerto o en su defecto modificar el uso cde CORS En la api.
se puede hacer uso de los siguientes comandos:

```bash
npm run build
npm start --port 3000
```
# demo-img
![Image text](https://github.com/emmanuel-garcia-m/to-do-list-app/blob/main/demo-img.png)

# licencia
[MIT](https://choosealicense.com/licenses/mit/)
