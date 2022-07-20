# Todos

## User Stories

### Lege Todo an

- Wenn es keine Todos gibt, zeige nur Textfeld für neues Todo.
- Fokusiere beim Start Textfeld für neues Todo.
- Entferne Leerzeichen vor und nach Text.
- Füge neues Todo nur hinzu, wenn Text nicht leer ist.

### Bearbeite Todo

- Ein Todo in der Liste kann durch Doppelklick bearbeitet werden.
- Wenn ein Todo bearbeitet wird, zeige nur Textfeld zum Bearbeiten.
- Fokusiere beim Bearbeiten Textfeld.
- Die Änderung am Todo wird durch `Enter` und Fokusverlust gesichert oder durch
  `Escape` abgebrochen.
- Entferne Leerzeichen vor und nach Text.
- Wenn der Text leer ist, lösche Todo.

### Erledige Todo

- Ein Todo in der Liste kann als erledigt markiert werden.
- Checkbox _Mark all as complete_ ist ausgewählt, wenn alle Todos erledigt sind,
  sonst nicht.

### Lösche Todo

- Ein Todo in der Liste kann gelöscht werden.
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
