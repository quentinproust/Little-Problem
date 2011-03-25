<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About Us
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>About</h2>
    <h3>Liste des dépendences</h3>
    <p>
        De nombreuses librairies ont été utilisées pour le développement de LittleProblem.
        Pour installer la plupart de ces librairies, nous avons utilisé le gestionnaire de package NuGet.
    </p>
    <h4>Base de données</h4>
    <p>
        La base de données servant à stocker les informations de LittleProblem est MongoDB.
        Il s'agit d'une base NoSQL qui stocke les informations sous forme de document.
        Les bases NoSQL sont des bases qui sont nées de la réflexion que tout le monde n'a pas 
        besoin d'une base relationnelle, autrement dit "Not Only SQL" ou "Not Only Relationnal".
    </p>
    <p>
        Vous pourrez trouver plus d'information sur le site <a href="http://mongodb.com">MongoDB</a>.
        Les librairies utilisées pour Mongo sont :
    </p>
    <ul>
        <li><a href="http://www.mongodb.org/display/DOCS/CSharp+Driver+Tutorial">Mongo csharp driver</a></li>
        <li><a href="https://github.com/craiggwilson/fluent-mongo">Fluent Mongo qui permet de faire des requêtes Linq sur la base Mongo</a></li>
    </ul>
    <h4>
        Injection de dépendence
    </h4>
    <p>
        L'injection de dépendence est une des techniques fondamentals qui permet de rendre une application le plus
        modulaire possible. Pour gérer cet aspect, nous avons fait le choix de la librairie StructureMap.
    </p>
    <ul>
        <li><a href="http://structuremap.net/structuremap/">StructureMap</a></li>
    </ul>
    <h4>
        Gestion des logs
    </h4>
    <p>
        Afin de gérer les erreurs et de pouvoir analyser le comportement d'une application durant son execution,
        l'enregistrement de logs est crucial.
        Nous avons mis en place plusieurs choses pour le gestion des logs de notre application. 
        Pour la gestion des erreurs, nous avons choisis ELMAH. Sa mise en place est extrement facile, notament grâce à Nuget.
        Pour les autres types de log, nous avons pris la librairie NLog, elle aussi grâce à Nuget.
    </p>
    <ul>
        <li><a href="http://code.google.com/p/elmah/">ELMAH : Error Logging Modules and Handlers for ASP.NET</a></li>
        <li><a href="http://nlog-project.org/">NLog</a></li>
    </ul>
    <h4>
        Tests unitaires
    </h4>
    <p>
        Je ne devrais même pas avoir à expliquer la nécessité des tests unitaires qui sont un minimum.
        Je ne le ferai donc pas.
    </p>
    <ul>
        <li><a href="http://nunit.org/">NUnit</a></li>
    </ul>
</asp:Content>
