

## 📚 UserApp - BibliotekApp

**UserApp** är en webbaserad applikation byggd med ASP.NET MVC för att hantera ett litet bibliotek.
Användare kan bläddra bland böcker, låna eller reservera dem – och administratörer kan redigera informationen.

---

### ✨ Funktioner

*  Skapa ett konto, logga in och logga ut.
*  Redigera din profil (namn, e-post m.m.)
*  Visa en lista över alla böcker
*  Se detaljerad information om en bok
*  Låna en bok om den är tillgänglig / Reservera en bok om den är utlånad
*  Administratörer kan addera och redigera böcker

---

### 👥 Roller

* **Administratör** – kan redigera böcker
* **Användare** – kan låna och reservera böcker

---
### 🧪 Tester

Projektet innehåller några enhetstester för UserService och BookController för att säkerställa grundläggande funktionalitet.

---

### 🧰 Tekniker

* ASP.NET Core MVC
* Entity Framework Core
* ASP.NET Identity
* Razor Views
* Bootstrap

---

### 🚀 Kom igång

1. Klona projektet
2. Kör `Update-Database` i Package Manager Console
3. Starta applikationen

### ⚠️ Kvar att implementera :
 * Ett konsolprojekt som visar upp entiteterna genom att kommunicera med en service i Application-projektet.
 * Fler tester för samtliga tjänster och controllers för att täcka hela applikationens logik.
