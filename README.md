# Patrones de diseños implementados en el juego

### <b>Patron Strategy:</b>
- Utilizo este patron para resolver el problema de tener 2 modos de juego diferente utilizando un unico game manager, el game manager sigue funcionando de la misma manera, solo que se abstrajo todo el comportamiento diferentes script que se encargan de la estrategia multiplayer y single player, en concreto los scripts son <b>**GameManager.cs**</b> (el manager original pero modificado para que soporte este patron), <b>**GameManagerStrategy.cs**</b> (Del cual heredan todas las estrategias), <b>**MultiplayerStrategy.cs**</b> y <b>**SinglePlayerStrategy.cs**</b>
