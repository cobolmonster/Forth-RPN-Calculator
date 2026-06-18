#! /usr/bin/env gforth

\ Calculatrice RPN scientifique extensible
\ Les nombres flottants utilisent la notation "e0" (ex: 3.0e0)

: + f+ ;
: - f- ;
: * f* ;
: / f/ ;

: sqrt fsqrt ;
: sin fsin ;
: cos fcos ;
: tan ftan ;
: log flog ;
: ln fln ;
: ^ f** ;

2.718281828459045e0 fconstant e

fvariable mem
0e0 mem f!
: ms ( -- ) fdup mem f! ;
: mr ( -- ) mem f@ ;
: mc ( -- ) 0e0 mem f! ;

variable fid

: open-or-create ( -- )
  s" custom_words.fs" r/w open-file
  if
    drop
    s" custom_words.fs" r/w create-file
    drop fid !
  else
    fid !
  then ;

: append-line ( c-addr u -- )
  open-or-create
  fid @ file-size drop
  fid @ reposition-file drop
  fid @ write-line drop
  fid @ close-file drop ;

: load-custom ( -- )
  s" custom_words.fs" ['] included catch drop ;

: starts-with-colon? ( c-addr u -- flag )
  dup 0= if drop false exit then
  over c@ [char] : = ;

2variable last-line
variable was-colon

: safe-eval ( c-addr u -- )
  2dup last-line 2!
  starts-with-colon? was-colon !
  last-line 2@ ['] evaluate catch
  ?dup if
    drop
    ." Erreur : commande invalide ou pile insuffisante" cr
  else
    was-colon @ if
      last-line 2@ append-line
      ." (fonction sauvegardee)" cr
    then
  then ;

: show-help ( -- )
  cr
  ." === Calculatrice RPN Forth ===" cr
  ." Notation : nombres puis operateur. Ex: 3.0e0 4.0e0 +" cr
  ." Operateurs   : + - * / ^ (puissance)" cr
  ." Fonctions    : sqrt sin cos tan log ln" cr
  ." Constantes   : pi  e" cr
  ." Memoire      : ms (stocker)  mr (rappeler)  mc (effacer)" cr
  ." Nouvelle fonction : : nom ... ;" cr
  ."    Exemple -> : double 2.0e0 * ;" cr
  ."    Ensuite  -> 5.0e0 double" cr
  ."    Pour dupliquer le sommet de pile (ex: pour un carre)," cr
  ."    utilisez fdup, pas dup : : carre fdup * ;" cr
  ."    (sauvegardee automatiquement dans custom_words.fs)" cr
  ." Quitter      : bye" cr
  cr ;

: get-input ( -- c-addr u )
  pad dup 80 accept ;

: dispatch ( c-addr u -- )
  2dup s" help" compare 0= if
    2drop show-help
  else
    safe-eval
  then ;

: banner ( -- )
  cr
  ." ===================================================" cr
  ." Calculatrice RPN en Forth (gforth)" cr
  ." Tapez 'help' pour l'aide, 'bye' pour quitter." cr
  ." ===================================================" cr ;

: rpn-calc ( -- )
  banner
  load-custom
  begin
    cr ." Pile : " f.s cr
    ." rpn> " get-input dispatch
  again ;

rpn-calc
