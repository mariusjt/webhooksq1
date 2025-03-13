```
winget install Microsoft.DotNet.SDK.9
git clone https://github.com/mariusjt/webhooksq1
```

# Oppgave
Nå skal vi bli sykt gode på WebHooks. WebHooks er ofte brukt for kommunikasjon mellom apper, aka integrasjoner.
WebHooks passer veldig bra med eventbaserte systemer ettersom man kan gjøre et HTTP kall per event.

WebHooks har en server/provider og en client/consumer (riktig terminologi sold separately). 
Client subscriber på ett/flere/alle eventtyper (POST /subscribe/{EventType}, slik at server kan gjøre et HTTP kall tilbake når dette eventet oppstår. Da trenger man ikke ha en åpne connection hele tiden.

## Forslag til kravspec:
Vi lager litt boilerplate for en nettbutikk (Provider). Nettbutikken bruker et CRM (Customer Relationship Management) som trenger informasjon om brukere i nettbutikken.

Følgende eventer er viktige:
1. UserCreated
2. UserUpdated
3. UserDeleted
Vi må sørge for at CRM systemet (Consumer) kan subscribe på disse eventene, slik at nyeste informasjon om brukere kan legges inn så snart det blir oppdatert i nettbutikken.

BonusOppgaver:
1. Distribuere subscriptions med f.eks. Redis, slik at man kan skalere Provider appen horisonalt uten at det blir duplikatmeldinger til consumer.
2. Unngå socket exhaustion (aner ikke om vi får til dette med maskinene våres)
