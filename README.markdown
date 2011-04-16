# Welcome to the LittleProblem project.
LittleProblem started as a school project where we had to study some technology we didn't knew. We made the choice to work on some interesting technos : 

* ASP.NET MVC as we did'nt had the time to learn it yet.
* OpenId because I don't understand the need every webapp has to make me create a new login/password everytime.
* MongoDB to understand the beauty behind NoSQL dataabases.

## Liste des dépendances

I didn't have the time to translate it for now. It will be in french for some time.

De nombreuses librairies ont été utilisées pour le développement de LittleProblem.
Pour installer la plupart de ces librairies, nous avons utilisé le gestionnaire de package NuGet.

###Base de données

La base de données servant à stocker les informations de LittleProblem est MongoDB.
Il s'agit d'une base NoSQL qui stocke les informations sous forme de document.
Les bases NoSQL sont des bases qui sont nées de la réflexion que tout le monde n'a pas 
besoin d'une base relationnelle, autrement dit "Not Only SQL" ou "Not Only Relationnal".

Vous pourrez trouver plus d'information sur le site <a href="http://mongodb.com">MongoDB</a>.

Les librairies utilisées pour Mongo sont :

* [Mongo csharp driver](http://www.mongodb.org/display/DOCS/CSharp+Driver+Tutorial)
* [Fluent Mongo](https://github.com/craiggwilson/fluent-mongo) qui permet de faire des requêtes Linq sur la base Mongo

### Injection de dépendence

L'injection de dépendence est une des techniques fondamentals qui permet de rendre une application le plus modulaire possible. Pour gérer cet aspect, nous avons fait le choix de la librairie StructureMap.

* [StructureMap](http://structuremap.net/structuremap/)

### Gestion des logs

Afin de gérer les erreurs et de pouvoir analyser le comportement d'une application durant son exécution, l'enregistrement de logs est crucial.
Nous avons mis en place plusieurs choses pour le gestion des logs de notre application. 
Pour la gestion des erreurs, nous avons choisis ELMAH. Sa mise en place est extrement facile, notament grâce à Nuget.
Pour les autres types de log, nous avons pris la librairie NLog, elle aussi grâce à Nuget.

* [ELMAH](http://code.google.com/p/elmah/) : Error Logging Modules and Handlers for ASP.NET</a>
* [NLog](http://nlog-project.org/)

### Tests unitaires

Je ne devrais même pas avoir à expliquer la nécessité des tests unitaires qui sont un minimum. Je ne le ferai donc pas.

* [NUnit](http://nunit.org/)