CREATE TABLE reactors ( id INTEGER PRIMARY_KEY, name TEXT NOT NULL, building_id INTEGER REFERENCES buildings(id), temp FLOAT, volume FLOAT );
CREATE TABLE buildings ( id INTEGER PRIMARY_KEY, name TEXT NOT NULL, address TEXT, city TEXT, state TEXT, zip TEXT, phone TEXT );
CREATE TABLE processes ( id INTEGER PRIMARY_KEY, desc TEXT NOT NULL, temp FLOAT, volume FLOAT, cost INTEGER );
CREATE TABLE process_reactants ( id INTEGER PRIMARY_KEY, process_id INTEGER REFERENCES processes(id), reactant_id INTEGER REFERENCES reactants(id), temp FLOAT, volume FLOAT );
CREATE TABLE reactants ( id INTEGER PRIMARY_KEY, name TEXT NOT NULL, onhand FLOAT, orderpoint FLOAT );

CREATE TRIGGER delete_process_reactants AFTER DELETE ON reactants FOR EACH ROW BEGIN DELETE FROM process_reactants WHERE process_reactants.reactant_id = OLD.id; END;
CREATE TRIGGER delete_process_reactants_2 AFTER DELETE ON processes FOR EACH ROW BEGIN DELETE FROM process_reactants WHERE process_reactants.process_id = OLD.id; END;
CREATE TRIGGER delete_reactors AFTER DELETE ON buildings FOR EACH ROW BEGIN ELETE FROM reactors WHERE reactors.building_id = OLD.id; END;

CREATE TRIGGER delete_process_reactants_3 AFTER DELETE ON buildings FOR EACH ROW BEGIN DELETE FROM process_reactants WHERE reactors.building_id = OLD.id; END;
CREATE TRIGGER delete_process_reactants_4 AFTER DELETE ON reactors FOR EACH ROW BEGIN DELETE FROM process_reactants WHERE reactors.building_id = OLD.id; END;

