# Enquête Balistique - Crime & Science
*Théo Le Gourrierec - Imanol LEPORQ - Jean GIRAUDEAU - Félix CHAUFFRAY*

## Installation du projet
1. Cloner le repo github
2. Ajouter le projet sur Unity Hub
3. Lancer le mode preview ou le build

Aucune autre configuration n'est nécessaire.

## Concept du jeu
Cet atelier à pour objectif de faire découvrir la balistique aux jeunes personnes.
L'utilisateur sera amené à découvrir 4 salles :

### 0. La salle d'accueil
Ici, il pourra découvrir les 4 armes se trouvant dans le jeu, il pourra manipuler les prefabs, entendre le bruit de l'arme à l'aide de la gâchette du controller, il pourra également y voir une photo réelle, l'origine ainsi qu'un texte descriptif avec une lecture vocal s'il le souhaite.
Une fois ces informations assimilées, il peut appuyer sur le bouton pour commencer le jeu, un chronomètre se lance alors et le mur disparaît.

### 1. La salle d'association
Dans ce premier jeu, l'utilisateur doit déposer la douille correspondante avec l'arme. Si la mauvaise douille a été déposée, il entend un son d'erreur, au contraire si c'est valide, il entend également un son et augmente le score sur /4. Une fois atteins 4/4, un son se joue et peut passer à la salle suivante.

### 2. La salle de cinéma
Dans le cinéma, le joueur doit trouver la balle qui a été utilisée pour le crime. Plusieurs balles y sont dispersées dans le cinéma, le but est de trouver la plus crédible et donc avec les informations appris juste avant il pourra en déduire l'arme utilisée dans la salle 3.

### 3. L'enregistrement
En lien avec la salle 2 (cinéma), le joueur doit prendre la bonne arme et la déposer dans le panier. Une fois, la bonne arme trouvée, il doit aller appuyer sur le bouton de fin qui arrêtera le chronomètre et pourra y voir le temps passé dans le jeu.

---

## Informations complémentaires

Un handmenu en tournant la main droite apparaît et permet de réinitialiser le jeu.
La vidéo affichée sur l'écran du cinéma est à configurer.