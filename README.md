# Todos

## User Stories

### Lege Todo an

- Wenn es keine Todos gibt, zeige nur Textfeld f�r neues Todo.
- Fokusiere beim Start Textfeld f�r neues Todo.
- Entferne Leerzeichen vor und nach Text.
- F�ge neues Todo nur hinzu, wenn Text nicht leer ist.

### Bearbeite Todo

- Ein Todo in der Liste kann durch Doppelklick bearbeitet werden.
- Wenn ein Todo bearbeitet wird, zeige nur Textfeld zum Bearbeiten.
- Fokusiere beim Bearbeiten Textfeld.
- Die �nderung am Todo wird durch `Enter` und Fokusverlust gesichert oder durch
  `Escape` abgebrochen.
- Entferne Leerzeichen vor und nach Text.
- Wenn der Text leer ist, l�sche Todo.

### Erledige Todo

- Ein Todo in der Liste kann als erledigt markiert werden.
- Checkbox _Mark all as complete_ ist ausgew�hlt, wenn alle Todos erledigt sind,
  sonst nicht.

### L�sche Todo

- Ein Todo in der Liste kann gel�scht werden.
- Blende Aktion _Clear completed_ aus, wenn es keine erledigten Todos gibt.

### Filtere Todos

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
