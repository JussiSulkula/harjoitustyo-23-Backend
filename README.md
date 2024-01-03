# harjoitustyö

Middleware-kansio sisältää ApiKeyMiddleware.cs tiedoston ja BasicAuthenticationHandler.cs. Näissä viestintäsovellus tarkistuttaa, onko käyttäjän pyyntö asianmukaisesti valtuutettu ja varmistaa, että asiakas on luonut käyttäjätunnuksen. Middleware-rajapinta ymmärtääkseni määrittelee kaksi metodia, jotka käynnistetään, kun sovellus käynnistyy ja sammutetaan. 

Repositories-kansio sisältää tietovarastot, jotka ohjaavat tietokannan käsittelyä.

Models-kansio sisältää tietomallit, jotka määrittelevät viestintäsovelluksen tiedostomuotoja ja tiedostoja.

Services-kansio sisältää palveluita, jotka ohjaavat toimintoja, kuten viestien lähettämisen ja vastaanottamiseen viestintäsovelluksessa.

Controllers-kansio sisältää ohjaimet, joilla vastaanotetaan asiakkaan HTTP-pyynnöt. Tähän osaan kuuluvat MessagesController.cs ja UsersController.cs.

Program.cs osiosta käynnistetään viestintäsovellus. Itse sovelluksen testaaminen tapahtuu Postman nettisivua käyttämällä, jonka avulla voidaan tutkia viestintäsovelluksen rajapintoja tarkemmin ja kysyä kysymyksiä viestintäsovellukselta. Tarkoituksena on katsoa ja tarkastaa viestintäsovelluksen toimivuus.

Sovellus on yksinkertaisuudessa ASP.NET Core Web API, joka tukee viestien lähettämistä ja käyttäjien todennusta. Tämän harjoitustyön viestintäsovelluksen perustana on tietokanta, joka tallentaa viestejä ja käyttäjiä. Tähän harjoitusdtyössä käytin Microsoft SQL serveriä. Tietokantana toimii Repositories-kansion tietovarastot, josta tiedot välittyy Models ja Service-kansioihin. Sen jälkeen tulee viesti, jonka lähettämisessä toimii MessagesController, joka vastaanottaa käyttäjän lomakkeen tietoja ja tallentaa ne tietokantaan. Käyttäjät voivat rekisteröityä käyttämällä UsersControllerin toiminnallisuutta hyödyksi. Samalla käyttäjän tietoturvaa varten viestintäsovellus käyttää todennusta eli (Basic Authenticationia). Sovelluksen saadessa uuden HTTP pyynnön, ensin tarkistetaan, onko pyynnön osoite yksityinen tai julkinen. Yksityiset osoitteet vaativat todennusta. Todennustarkistus on toteutettu tässä tapauksessa service-kohteeseen, (IUserAuthenticationService). Tämän pitäisi vastata käyttäjän todentamisesta ja käyttäjätunnuksen luomisesta. Pyynnön ollessa ja käyttäjän todennus onnistuu, pyyntö ohjataan oikeaan controlleriin. 
