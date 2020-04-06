# LamondLu.EmailClient - A email management system
Use Mailkit to create a email client, but not only a client, it can also support to create rule to support different scene

## Concepts

### Email Connector
The email connector is like a email client, it will connect with the emailbox with POP3, IMAP, etc. If any new email received, it will sync the email into the email management system.

Each email connector have a rule pipeline with multiple rules. The rule pipeline are configurable.

### Support Rules
- Forward
- Reply
- Classify

### Forward rule
If the rule matched, the system will forward the email to assigned emailboxes.

### Reply rule
If the rule matched, the system will reply the sender with assigned email template.

### Classify rule
If the rule matched, the systesm will classify the email as assigned category. 
