# Databázový Projekt

Tento projekt je konzolová aplikace v jazyce C#, která umožňuje správu databáze knihoven, zaměstnanců a knih. Aplikace využívá `Microsoft.Data.SqlClient` pro připojení do **Microsoft SQL Server** pro správu dat a `System.Configuration.ConfigurationManager` pro čtení konfiguračních souborů.

## Požadavky pro spuštění

1. **Microsoft SQL Server**: Aplikace vyžaduje připojení k databázi SQL Server. Je potřeba mít přístupové údaje do běžícího SQL Serveru.
2. **Visual Studio a .NET SDK**: Pro spuštění aplikace je potřeba mít Visual Studio a nainstalovaný .NET SDK (verze 6.0 nebo novější).
3. **System.Configuration.ConfigurationManager**: Tento balíček je nutný pro čtení konfiguračních souborů. Můžete jej nainstalovat pomocí NuGet Package Manager přímo ve Visual Studio
4. **Microsoft.Data.SqlClient**: Tento balíček je nutný pro komunikaci s SQL Serverem. Můžete jej nainstalovat pomocí NuGet Package Manager přímo ve Visual Studio

## Konfigurační soubor

Aplikace vyžaduje konfigurační soubor `App.config`, který obsahuje připojovací údaje k databázi. Ujistěte se, že tento soubor je správně nakonfigurován podle vaších přístupových údajů.

## Struktura projektu
Projekt obsahuje několik tříd, které reprezentují entity v databázi:

`Program`: Třída Main, zde je uživatelské rozhraní

`PraceSDatabazi`: Hlavní třída, zde jsou všechny metody pro práci s databázi 

`Knihovna`: Reprezentuje knihovnu s názvem a městem.

`Zaměstnanec`: Reprezentuje zaměstnance s jménem, příjmením a datem narození.

`Kniha`: Reprezentuje knihu s názvem, žánrem, autorem a informací o zapůjčení.

`Evidence_zamestnancu`: Reprezentuje evidenci zaměstnanců v knihovně.

`Evidence_knihy`: Reprezentuje evidenci knih v knihovně.

`Singleton`: Třída pro připojení s **Microsoft SQL Serverem**
## Použití 

Třída `PraceSDatabazi` je hlavní třída a obsahuje metody pro práci s databází, včetně přidávání, mazání, úpravy a zobrazování záznamů.

Třída `Program` je Main, zde je uživatelské rozhraní, když spustí, vypíše všechny funkce co program umožní, stačí napsat číslo funkce a program vás navede co dál, pokud došlo k chybě, program vypíše zprávu chyby. Pak můžete stisknou libovolnou klávesu pro pokračování do hlavního menu

## Konfigurace

Aplikace vyžaduje konfigurační soubor `App.config`, který obsahuje připojovací údaje k databázi. 

**Příklad konfiguračního souboru:**

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <appSettings>
        <add key="DataSource" value="193.85.203.188"/>
        <add key="Database" value="JmenoDatabaze"/>
        <add key="Name" value="PrihlasovaciJmeno"/>
        <add key="Password" value="Heslo"/>
    </appSettings>
</configuration>
```

## Spuštění aplikace

1. Naklonujte repozitář nebo stáhněte zdrojové kódy.
2. Otevřete projekt v IDE (např. Visual Studio).
3. Nainstalujte balíčky (viz `Požadavky pro spuštění`)
3. Nastavte připojovací údaje v `App.config`.
4. Spusťte aplikaci pomocí tlačítka Spustit nebo `Ctrl + F5`

## Závěr

Tato aplikace poskytuje jednoduché rozhraní pro správu databáze knihoven, zaměstnanců a knih

## Autor : Nguyen Quoc Khanh ze třídy C3a