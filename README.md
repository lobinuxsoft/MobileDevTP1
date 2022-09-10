# Patrones de diseños implementados en el juego

### <b>Patron Strategy:</b>
- Utilizo este patron para resolver el problema de tener 2 modos de juego diferente utilizando un unico game manager, el game manager sigue funcionando de la misma manera, solo que se abstrajo todo el comportamiento diferentes script que se encargan de la estrategia multiplayer y single player, en concreto los scripts son <b>**GameManager.cs**</b> (el manager original pero modificado para que soporte este patron), <b>**GameManagerStrategy.cs**</b> (Del cual heredan todas las estrategias), <b>**MultiplayerStrategy.cs**</b> y <b>**SinglePlayerStrategy.cs**</b>.

### <b>Patron Flyweight y Observer</b>
- Utilizo el patron <b>Flyweight</b> para optimizar los datos que estan tanto en una escena como en la otra, y que siempre es lo mismo, por ejemplo el dinero o puntaje, es el mismo tanto en gameplay como en la escena final de puntaje, el primer patron es utilizado para que esten cargado en memoria y se pueda utilizar en varios lugares, esto lo aprovecho de los <b>Scriptable Objects</b> que proporciona <b>Unity</b>, esto me da la posibilidad de tener datos compartidos entre escenas y poder hacer pruebas aisladas sin la necesidad de tener un manager de por medio.
- El patron <b>Observer</b> lo utilizo para cuando un cambio se genera en estos datos, sin tener que estas chequeandolos constantemente y solo saber que paso en el momento en que paso.
- Estos patrones se aplican en un plugin de mi autoria el cual se llama <b>Scriptable Variables</b> este se encuentra en la carpeta **Assets/My Tools/Scriptable Variables/ScriptableVariables.dll**

### <b>Patron Singleton</b>
- Este patron lo utilizo para que solo exista un unico objeto que se encargue especificamente de una cosa, en este caso de todo lo referido al audio, este tambien es una tool propia en formato .dll, el plugin se encuentra en <b>Assets/My Tools/Audio Tools/AudioTools.dll</b>