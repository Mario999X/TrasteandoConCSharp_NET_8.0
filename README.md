# TrasteandoConCSharp_NET_8.0
Pequeños proyectos realizados usando el lenguaje de C# y .net 8.0

## [Poke API Testing 1](PokeApiTesting1)

Pequeñas pruebas realizadas de forma asíncrona a la poke api, con una especie de cache en memoria (un hashset). Trabajos sencillos con linq.

## [Poke API Testing 2](PokeApiTesting2)

Versión mejorada. Se ha usado (de forma algo innecesaria, pero bueno) un mapper, un DTO para almacenar los datos en un csv, a modo de backup de la información de la cache.

## [Poke API Testing 3](PokeApiTesting3)

Versión con un pequeño agregado, se ha utilizado appsettings.json para mover información "delicada" para que no este hardcodeado en el codigo, como la cabecera del csv de backup, o de la URL de la API a la que nos conectamos.

Se han instalado las siguientes dependencias
    <PackageReference Include="Microsoft.Extensions.Configuration" Version="10.0.9" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Binder" Version="10.0.9" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="10.0.9" />

Se ha generado a mano el archivo de appsettings.json, y para poder tenerlo a mano dentro del propio proyecto, se ha usado un CopyToOutputDirectory.
    <None Update="appsettings.json">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
