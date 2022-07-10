# Todos

## User Stories

- Wenn es keine Todos gibt, blende Todo-Liste `main` und Filter `footer` aus.
- Fokusiere Texteingabe f�r neues Todo, beim Start.
- Entferne Leerzeichen vor und nach Text f�r neues Todos.
- F�ge neues Todo nur hinzu, wenn Text nicht leer ist.
- Checkbox _Mark all as complete_ ist ausgew�hlt, wenn alle Todos erledigt sind,
  sonst nicht.
- Ein Todo in der Liste kann als erledigt markiert werden.
- Ein Todo in der Liste kann durch Doppelklick bearbeitet werden.
- Ein Todo in der Liste kann gel�scht werden.
- Wenn ein Todo bearbeitet wird, wird das Textfeld fokusiert und die Controls
  f�r _Toggle_ und _Destroy_ ausgeblendet.
- Die �nderung am Todo wird durch `Enter` und Fokusverlust gesichert oder durch
  `Escape` abgebrochen.
- Entferne Leerzeichen vor und nach Text eines bearbeiteten Todos.
- L�sche bearbeitetes Todo, wenn der Text leer ist.
- Blende Aktion _Clear completed_ aus, wenn es keine erledigten Todos gibt.
- Filter Todos optional nach aktiv oder erledigt.

## Messages

### Commands

- Add todo (title)
- Toggle todo (id)
- Toggle all (checked)
- Destroy todo (id)
- Clear completed
- Save todo (id, title)

### Queries

- Select todos (id, title, completed)\*

### Notifications

N/A

### Events

N/A
