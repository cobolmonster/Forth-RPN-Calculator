# Calculatrice RPN en Forth

Une calculatrice scientifique en notation polonaise inversée (RPN), écrite en [Forth](https://fr.wikipedia.org/wiki/Forth_(langage)) et exécutée avec [gforth](https://gforth.org/). Elle gère les opérations de base, les fonctions scientifiques, une mémoire, et permet de définir ses propres fonctions à la volée — celles-ci sont sauvegardées automatiquement et rechargées à chaque lancement.

Aucune compilation requise : le script s'exécute directement avec l'interpréteur gforth.

## Installation de gforth

### Linux (Debian / Ubuntu)

```bash
sudo apt update
sudo apt install gforth
```

gforth est packagé dans la plupart des distributions. Sur Fedora : `sudo dnf install gforth`. Sur Arch : `sudo pacman -S gforth`.

### Windows

gforth ne propose pas d'installeur récent pour Windows ; on utilise le dernier installeur officiel disponible (version 0.7.0, mais toujours fonctionnelle) :

1. Télécharge l'installeur officiel : https://www.complang.tuwien.ac.at/forth/gforth/gforth-0.7.0.exe
2. Lance-le et termine l'installation (next-next-finish).
3. Le programme ne s'ajoute pas forcément au PATH. Pour le retrouver :
   - tape "gforth" dans la recherche du menu Démarrer ;
   - si un résultat apparaît, fais un clic droit dessus → **Ouvrir l'emplacement du fichier** pour voir le dossier d'installation réel ;
   - sinon, cherche manuellement dans `C:\Program Files (x86)\` un dossier `gforth`.
4. Dans ce dossier, l'exécutable `gforth.exe` se trouve directement à la racine (pas de sous-dossier `bin`). Vérifie avec :
   ```powershell
   dir *.exe
   ```
5. Teste-le :
   ```powershell
   .\gforth.exe --version
   ```

Pour éviter de retaper le chemin complet à chaque fois, ajoute ce dossier au PATH : Paramètres → Système → Informations système → Paramètres système avancés → Variables d'environnement → variable `Path` → Modifier → Nouveau → coller le chemin du dossier contenant `gforth.exe`. Redémarre ensuite le terminal.

## Exécuter le script

### Linux / macOS

```bash
gforth rpn_calculator.fs
```

ou, en mode exécutable :

```bash
chmod +x rpn_calculator.fs
./rpn_calculator.fs
```

### Windows

Si `gforth` a été ajouté au PATH :

```powershell
gforth rpn_calculator.fs
```

Sinon, avec le chemin complet vers l'exécutable :

```powershell
& "C:\Program Files (x86)\gforth\gforth.exe" rpn_calculator.fs
```

Dans les deux cas, place-toi d'abord (avec `cd`) dans le dossier contenant `rpn_calculator.fs`.

## Utilisation de la calculatrice

Une fois lancée, la calculatrice affiche l'état de la pile et une invite `rpn>`.

### Notation polonaise inversée

On tape d'abord les nombres, puis l'opérateur. Pas de parenthèses, pas de priorité d'opérateur à gérer : tout se lit de gauche à droite.

Les nombres flottants doivent être écrits avec la notation scientifique de Forth, c'est-à-dire suffixés par `e0` (ex: `3.0e0`, pas `3` ni `3.0`).

### Exemples

```
3.0e0 4.0e0 +        -> 7.0
5.0e0 sqrt           -> racine carree de 5
2.0e0 3.0e0 ^        -> 2 puissance 3 = 8.0
pi 2.0e0 *           -> 2 * pi
4.0e0 carre 5.0e0 carre + sqrt   -> hypotenuse d'un triangle 4-5 (si "carre" est defini)
```

Les résultats restent sur la pile et peuvent être réutilisés dans les calculs suivants.

### Opérateurs et fonctions disponibles

| Catégorie    | Mots disponibles |
|--------------|-------------------|
| Opérateurs   | `+` `-` `*` `/` `^` (puissance) |
| Fonctions    | `sqrt` `sin` `cos` `tan` `log` (base 10) `ln` (naturel) |
| Constantes   | `pi` `e` |
| Mémoire      | `ms` (stocker) `mr` (rappeler) `mc` (effacer) |
| Autres       | `help` (aide) `bye` (quitter) |

### Définir ses propres fonctions

On peut créer une nouvelle fonction directement dans la console avec la syntaxe native de Forth :

```
: nom-de-la-fonction ... ;
```

Exemple :

```
: carre fdup * ;
5.0e0 carre        -> 25.0
```

Attention : pour dupliquer le sommet de pile, il faut utiliser `fdup` (et non `dup`, qui agit sur une pile différente, réservée aux entiers).

Chaque fonction définie est automatiquement sauvegardée dans `custom_words.fs` (créé dans le même dossier) et sera encore disponible la prochaine fois que le programme est lancé.

## Licence

Libre d'utilisation et de modification.
