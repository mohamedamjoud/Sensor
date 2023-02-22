CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "TemperatureRequest" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_TemperatureRequest" PRIMARY KEY AUTOINCREMENT,
    "Value" INTEGER NOT NULL,
    "DateTime" TEXT NOT NULL
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230216022214_Initial', '7.0.3');

COMMIT;

BEGIN TRANSACTION;

DROP TABLE "TemperatureRequest";

CREATE TABLE "SensorState" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_SensorState" PRIMARY KEY AUTOINCREMENT,
    "Value" INTEGER NOT NULL,
    "DateTime" TEXT NOT NULL
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230217005510_1', '7.0.3');

COMMIT;

