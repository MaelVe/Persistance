CREATE TABLE  "Utilisateur"  ( 
    "IdUtilisateur"  SERIAL PRIMARY KEY, 
    "Nom"  VARCHAR(128), 
    "Prenom"  VARCHAR(128),
    "Fonction"  VARCHAR(128),
    "NomManager"  VARCHAR(128)
);

CREATE TABLE  "Magasin"  (
    "IdMagasin"  SERIAL PRIMARY KEY, 
    "NomMagasin"  TEXT, 
    "Enseigne"  TEXT
);

CREATE TABLE "CommercialMagasin" (  
    "IdCommercialMagasin"  SERIAL PRIMARY KEY,  
    "IdUtilisateur"  INTEGER,
    "IdMagasin"  INTEGER,
    FOREIGN KEY( "IdUtilisateur" ) REFERENCES  "Utilisateur" ( "IdUtilisateur" ),
    FOREIGN KEY( "IdMagasin" ) REFERENCES  "Magasin" ( "IdMagasin" ) 
);

CREATE TABLE "Visite" ( 
    "IdVisite"  SERIAL PRIMARY KEY,  
    "DateHeureVisite"  DATE, 
    "IdUtilisateur"  INTEGER,  
    "IdMagasin"  INTEGER, 
    "VisiteRealise"  BOOLEAN,
    "Guid" VARCHAR(200), 
    "IsDelete" BOOLEAN,
    "DateUpdate" DATE,
    FOREIGN KEY( "IdMagasin" ) REFERENCES "Magasin"( "IdMagasin" ),
    FOREIGN KEY( "IdUtilisateur" ) REFERENCES  "Utilisateur" ( "IdUtilisateur" ) 
);



INSERT INTO "Magasin" ("NomMagasin", "Enseigne") VALUES ( 'Leclerc Rennes','Leclerc');
INSERT INTO "Magasin" ("NomMagasin", "Enseigne") VALUES ( 'Leclerc Nantes','Leclerc');
INSERT INTO "Magasin" ("NomMagasin", "Enseigne") VALUES ( 'Leclerc Paris','Leclerc');
INSERT INTO "Magasin" ("NomMagasin", "Enseigne") VALUES ( 'Leclerc Tokyo','Leclerc');
INSERT INTO "Magasin" ("NomMagasin", "Enseigne") VALUES ( 'Super U Tokyo','Super U');
INSERT INTO "Magasin" ("NomMagasin", "Enseigne") VALUES ( 'Super U Rennes','Super U');
INSERT INTO "Magasin" ("NomMagasin", "Enseigne") VALUES ( 'Super U Nantes','Super U');
INSERT INTO "Magasin" ("NomMagasin", "Enseigne") VALUES ( 'Super U Paris','Super U');
INSERT INTO "Magasin" ("NomMagasin", "Enseigne") VALUES ( 'Super U Laval','Super U');    
INSERT INTO "Magasin" ("NomMagasin", "Enseigne") VALUES ( 'Super U Arnac-La-Poste','Super U');
INSERT INTO "Magasin" ("NomMagasin", "Enseigne") VALUES ( 'Carrefour Rennes','Carrefour');  
INSERT INTO "Magasin" ("NomMagasin", "Enseigne") VALUES ( 'Carrefour Tokyo','Carrefour');
INSERT INTO "Magasin" ("NomMagasin", "Enseigne") VALUES ( 'Carrefour Monteton','Carrefour');
INSERT INTO "Magasin" ("NomMagasin", "Enseigne") VALUES ( 'Carrefour Routier','Carrefour');
INSERT INTO "Magasin" ("NomMagasin", "Enseigne") VALUES ( 'Carrefour Nantes','Carrefour');
INSERT INTO "Magasin" ("NomMagasin", "Enseigne") VALUES ( 'Intermarche Paris','Intermarche');
INSERT INTO "Magasin" ("NomMagasin", "Enseigne") VALUES ( 'Intermarche Simple','Intermarche');
INSERT INTO "Magasin" ("NomMagasin", "Enseigne") VALUES ( 'Intermarche ¨PourLeCLimat','Intermarche');	
INSERT INTO "Magasin" ("NomMagasin", "Enseigne") VALUES ( 'Intermarche Nantes','Intermarche');

INSERT INTO "Utilisateur" ("Nom", "Prenom", "Fonction", "NomManager") Values ('Bon', 'Jean','Manager', NULL);
INSERT INTO "Utilisateur" ("Nom", "Prenom", "Fonction", "NomManager") Values ('Rado', 'Eldo','Commercial','Bon');
INSERT INTO "Utilisateur" ("Nom", "Prenom", "Fonction", "NomManager") Values ('Covert', 'Harry','Commercial','Bon');
INSERT INTO "Utilisateur" ("Nom", "Prenom", "Fonction", "NomManager") Values ('Melmou', 'Cara','Commercial','Bon');
INSERT INTO "Utilisateur" ("Nom", "Prenom", "Fonction", "NomManager") Values ('Embourg', 'Kron','Commercial','Bon');
INSERT INTO "Utilisateur" ("Nom", "Prenom", "Fonction", "NomManager") Values ('Ados', 'Desper','Commercial','Bon');
INSERT INTO "Utilisateur" ("Nom", "Prenom", "Fonction", "NomManager") Values ('Bergen', 'Grim','Commercial','Bon');
INSERT INTO "Utilisateur" ("Nom", "Prenom", "Fonction", "NomManager") Values ('Stra', 'Opela','Manager', NULL);
INSERT INTO "Utilisateur" ("Nom", "Prenom", "Fonction", "NomManager") Values ('Ateur', 'Nordin','Commercial','Stra');
INSERT INTO "Utilisateur" ("Nom", "Prenom", "Fonction", "NomManager") Values ('Ehuletracteur', 'Edgard','Commercial','Stra');
INSERT INTO "Utilisateur" ("Nom", "Prenom", "Fonction", "NomManager") Values ('Deschamps', 'Didier','Manager', NULL);
INSERT INTO "Utilisateur" ("Nom", "Prenom", "Fonction", "NomManager") Values ('Giroud', 'Olivier','Commercial','Deschamps');
INSERT INTO "Utilisateur" ("Nom", "Prenom", "Fonction", "NomManager") Values ('Eher', 'Jacky','Commercial','Deschamps');
INSERT INTO "Utilisateur" ("Nom", "Prenom", "Fonction", "NomManager") Values ('Eher', 'Michelle','Commercial','Deschamps');