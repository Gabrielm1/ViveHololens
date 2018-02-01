# ViveHololens
Expérimenter la réalité partagée en combinant le casque de réalité augmentée Microsoft Hololens et le casque de réalité virtuelle HTC Vive.
## Utilisation
Il est nécessaire d'utiliser la version de Unity 5.5.2f1, vous pouvez la télécharger à cette adresse https://unity3d.com/fr/get-unity/download/archive

Une fois le projet ouvert avec Unity, une fenêtre pop-up va s'ouvrir pour vous demander de mettre SteamVR à jour, ne le faites surtout pas, ce paquet Unity ayant été modifié pour que la démo fonctionne. Je parle ici du paquet Unity et non du programme SteamVR complémentaire à Steam, pour lequel vous pouvez, sans autre faire les mises à jour.

Avant de pouvoir compiler ou utiliser le jeu, vous devrez en premier lieu configurer la gestion réseau. Pour ce faire, allez dans l'onglet \textbf{Service}. Dans cette vue, vous aurez des informations sur les différentes fonctionnalités réseau propre au moteur. Mais avant toute configuration vous devrez vous assurer d'être connecté avec votre compte Unity, dans le cas contraire vous serez invité à vous loguer. Une fois connecté à votre compte, vous aurez la possibilité de créer un Unity Project ID, en cliquant sur le Button "Create". Une fois le projet créé, vous arriverez dans la fenêtre suivante :

<p align="center"> 
<img height="300" src="https://raw.githubusercontent.com/Gabrielm1/ViveHololens/master/img/multi-param.png">      
<img height="300" src="https://raw.githubusercontent.com/Gabrielm1/ViveHololens/master/img/multi-param2.png">
</p>

Sélectionnez "Multiplayer" puis "Go to Dashboard" vous serez alors emmené sur la page web correspondant à votre Unity Project ID. Il vous sera demandé d'indiquer le nombre de joueur, rentrez deux joueurs, puis cliquez sur le bouton "Save". Retournez ensuite sur Unity et cliquez sur le bouton "Refresh Configuration" et assurez-vous que le nombre de joueur ait été rentré correctement. 

## Exécution  - HoloLens
Pour déployer une application Unity sur l'HoloLens, il vous faudra indiquer à Unity que vous souhaitez compiler votre projet pour un casque HoloLens. Pour se faire allez dans "File → Build Settings → Windows Store".
<p align="center"> 
<img src="https://raw.githubusercontent.com/Gabrielm1/ViveHololens/master/img/deploiement.png">
</p>

Cliquez sur le bouton "Build", cela ouvrira une fenêtre et vous demandera dans quel dossier vous souhaitez sauver les fichiers générés. Je vous conseille de créer un dossier que vous appellerez "App" à l'endroit où Unity ouvre l'explorateur de fichier, c'est-à-dire à la racine du projet. Une fois la compilation terminée, allez dans le dossier fraichement généré et sélectionnez le fichier .sln avec Visual Studio. Une fois votre solution ouverte, sélectionnez "Release", puis "x86" et enfin "Oridnateur Disant"

<p align="center"> 
<img src="https://raw.githubusercontent.com/Gabrielm1/ViveHololens/master/img/deploiement2.png">
</p>

À cet instant, Visual Studio vous demandera l'adresse IP de la machine distante, rentrez-la, ensuite il vous demandera le code PIN nécessaire pour le pairage. Vous trouverez ce code depuis votre HoloLens en allant dans "Settings → Update \& Security → For developers", assurez-vous que le mode développeur soit enclenché. Dans le sous-menu "Paired devices" cliquez sur le bouton "Pair", le code PIN vous sera alors donné. Rentrez-le dans la fenêtre de Visual Studio et vous pourrez déployer des applications sur votre casque.
<p align="center"> 
<img height="370"  src="https://raw.githubusercontent.com/Gabrielm1/ViveHololens/master/img/deploiement3.png">
</p>


