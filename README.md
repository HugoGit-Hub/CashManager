# CashManager Project

Le projet CashManager vise à créer une caisse enregistreuse moderne, permettant une utilisation sans passer par la caisse traditionnelle ni les scanettes. L'ensemble du processus est géré à partir d'un simple smartphone. Le projet est divisé en deux parties distinctes : une application Flutter qui communique avec un serveur ASP.NET pour fournir le service décrit ci-dessous, et une simulation d'une application bancaire avec une interface en Next.js côté client et un serveur ASP.NET côté serveur.

## Fonctionnalités 🚀
### 1. Application Flutter

L'application Flutter agit comme l'interface principale pour la gestion de la caisse enregistreuse. Elle offre les fonctionnalités suivantes :

#### Enregistrement des transactions : 
Permet d'ajouter des articles et de gérer les ventes comme une caisse traditionnelle.
#### Communication avec le serveur : 
Échange d'informations avec le serveur ASP.NET pour stocker les données de manière sécurisée.
#### Interface utilisateur conviviale : 
Conception ergonomique pour faciliter l'utilisation par des utilisateurs non techniques.

### 2. Simulation d'une application bancaire

La simulation d'une application bancaire se compose de deux parties : le front-end en Next.js et le back-end en ASP.NET. Les fonctionnalités incluent :

#### Gestion du compte : 
Permet à l'utilisateur de visualiser le solde de son compte et l'historique des transactions.
#### Sécurité : 
Assure la confidentialité des informations financières via des protocoles de sécurité standard.
#### Intégration avec l'application Flutter :
Les transactions effectuées dans l'application Flutter sont reflétées dans l'historique des transactions bancaires.

## Configuration du Projet ⚙️

#### Application Flutter :
  Assurez-vous d'avoir Flutter et Dart installés localement.
  Clonez le dépôt et exécutez flutter run dans le répertoire de l'application.

#### Simulation d'Application Bancaire :
  Assurez-vous d'avoir Node.js et npm installés.
  Clonez le dépôt et exécutez npm install dans le répertoire du front-end Next.js.
  Exécutez le serveur ASP.NET pour la simulation bancaire.

## Génération de l'APK Flutter 📱

Utilisez le docker-compose.yml à la racine du projet pour générer l'APK de l'application Flutter de manière isolée.

## Technologies Utilisées 💻

- Flutter (Front-end de la caisse enregistreuse)
- Next.js (Front-end de l'application bancaire)
- ASP.NET (Back-end pour la caisse enregistreuse et l'application bancaire)

## Auteur 🌟
 
  - Ines Garcia
  - Ryan Thomas
  - Hugo Decuq

N'hésitez pas à contribuer en soumettant des problèmes ou des demandes de fonctionnalités. Merci de soutenir le projet CashManager! 🙌