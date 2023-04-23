Tables

	buildings: 
		This is the set of all buildings in the company.
		The primary key is id, there are no foreign keys.
		The primary key and name are not null.
		In 3NF.
		Sample Data:
			1, River Facility, 123 S Fourth, East Range, Wichita, KS, 67212, 3162434444
			2, Downing Facility, 123 S Fourth, West Range, Wichita, KS, 67212, 3162434446

	reactors: 
		This is the set of all reaction chambers across all buildings.
		The primary key is id, the foreign key is building_id.
		The primary key and name are not null.
		In 3NF.
		There is a trigger to delete reactors when the associated building is deleted.
		Sample Data:
			1, Reactor I, 1, 200, 5000
			2, Reactor II - Hi Temp, 1, 400, 5000

	reactants: 
		This is the set of all reactants the company uses to produce product.
		The primary key is id, there are no foreign keys.
		The primary key and name are not null.
		In 3NF.
		There is a trigger to delete process_reactants when the associated reactant is deleted.
		Sample Data:
			1, Red Dye, 5000, 500
			2, Yellow Dye, 5000, 500

	processes: 
		This is the set of all product production processes the company uses to produce product.
		The primary key is id, there are no foreign keys.
		The primary key and name are not null.
		In 3NF.
		There is a trigger to delete process_reactants when the associated process is deleted.
		Sample Data:
			1, Red Kool Aid, 75, 5000
			2, Green Kool Aid, 75, 2500

	process_reactants: 
		This is the set of all reactants that are used for the processes. It is a cross reference between reactants and processes.
		The primary key is id, the two foreign keys are reactant_id and process_id.
		The primary key is not null.
		In 3NF.
		There is a trigger to delete process_reactants when the associated reactant or process is deleted.
		Sample Data:
			1, 1, 1, 50, 500
			2, 1, 5, 50, 1
			3, 1, 6, 50, 250
			4, 1, 7, 65, 2


