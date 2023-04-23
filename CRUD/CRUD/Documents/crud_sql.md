CREATE TABLE reactors ( id INTEGER PRIMARY_KEY, name TEXT NOT NULL, building_id INTEGER REFERENCES buildings(id), temp FLOAT, volume FLOAT );
SELECT * from reactors where id=1;
UPDATE reactors SET name="NewName" WHERE id=1;
DELETE FROM reactors where id=1;

CREATE TABLE buildings ( id INTEGER PRIMARY_KEY, name TEXT NOT NULL, address TEXT, city TEXT, state TEXT, zip TEXT, phone TEXT );
SELECT * from buildings where id=1;
UPDATE buildings SET name="NewName" WHERE id=1;
DELETE FROM buildings where id=1;

CREATE TABLE processes ( id INTEGER PRIMARY_KEY, desc TEXT NOT NULL, temp FLOAT, volume FLOAT );
SELECT * from processes where id=1;
UPDATE processes SET desc="NewDesc" WHERE id=1;
DELETE FROM processes where id=1;

CREATE TABLE process_reactants ( id INTEGER PRIMARY_KEY, process_id INTEGER REFERENCES processes(id), reactant_id INTEGER REFERENCES reactants(id), temp FLOAT, volume FLOAT );
SELECT * from process_reactants where process_id=1 AND reactant_id=1;
UPDATE process_reactants SET volume="NewVolume" WHERE process_id=1 AND reactant_id=1;
DELETE FROM process_reactants where id=1;
DELETE FROM process_reactants where process_id=1 AND reactant_id=1;

CREATE TABLE reactants ( id INTEGER PRIMARY_KEY, name TEXT NOT NULL, onhand FLOAT, orderpoint FLOAT );
SELECT * from reactants where id=1;
UPDATE reactants SET name="NewName" WHERE id=1;
DELETE FROM reactants where id=1;
