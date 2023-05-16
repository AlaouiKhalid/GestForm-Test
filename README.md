# GestForm Test

Ce Projet est un simple connexion entre un microservice en .Net Core (une API Rest) avec un front en Angular pour repondre à l'enoncé suivant:

Soit une liste aléatoire de nombres entiers, compris entre -1000 et 1000.
Pour chaque nombre n de liste, on veut effectuer les opérations suivantes:
• si le nombre est divisible par 3 : on affiche Geste
• si le nombre est divisible par 5 : on affiche Forme
• si le nombre est divisible par 3 et par 5 : on affiche Gestform (d’où le nom du test)
• sinon : on affiche le nombre n

# La partie Front: GestFormFront

Ce projet est généré avec [Angular CLI](https://github.com/angular/angular-cli) , la version 15.2.7.

Il s'agit d'un simple formulaire avec un seul input de type number 

On donne à l'utilisateur de choisir la loungueur du liste (avec un validateur de champ :un message d'erreur s'affiche en cas de nombre negatif)

Une fois la longueur est positive ,on génére une liste des nombres aleatoires comprise entre -1000 et 1000.

Automatiquement la liste sera envoyée au back pour le test( Appel HTTP en passant par le service GestFormService)

à noter que Intercepteur HTTP developpé gérera les erreurs de ces appels.

pour installer les dependences en local ,tappez `npm i `

pour lancer le front  ,tappez`ng serve` 

l'application sera lancée sur [le lien suivant](http://localhost:4200/)

NB: les erreurs CORS (Cross-origin resource sharing) ont été traitées en back

# La partie Back: GestFormAPI

Un exemple de code propre (clean-code) et de bonnes approches de l'architecture DDD (Domain Driven Design) avec le design Pattern Mediateur. 

NB: Vu que y'a pas d'appel vers la base de données , le traitement ne passe pas par la couche infrastructure puisque qu'on pas d'entitée.

La liste inserée sera verifiée pour ne pas avoir des nombres hors intervalle sinon une exception sera levée(à voir le gherkin pour le test BDD)


