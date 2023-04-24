Script

1.	(Start code.)
	(Warning will come up that database is not there)
2.	(Click on Create tables.)
3.	This is the user interface for our project. It is for a company that makes kool aid.
	The company is organized around the buildings, and the reaction vessels that are used to make the kool aid.
	The buildings are where the reaction vessels are located.
	The reaction vessels are used to prepare kool aid by predefined processes.
	The processes use reactants, and the reactants are from a standard set used for all processes.
4.	First, we select the building where the reactor is located.
5.	Once a building is selected, we see the reactors that are located at that building.
	And on the right we see all of the processes that the company uses across all reactors.
6.	To prepare a batch of kool aid, we select a reactor and a process and press "Make Batch".
	The system will use the database to make sure that the process will be able to be made correctly.
	The reactor must be able to provide the temperature required and the volume required for the batch.
7.	That is the normal operation of the system.

8.	We can create, update, and delete buildings, reactors, processes, reactants and the reactants being used in a process.
9.	First buildings. We can create a new building by selecting this (press building create button).
10.	Once the dialog comes up, we can enter the data for the new building.
11.	When we press OK, the UI updates.
12. Updating a building is pretty much the same, except that the UI shows the current data for the building. (press building update button)
13. When we press OK, the UI updates.
14. When a facility is shut down or sold, we can delete the building (select Twin Rivers Facility B and press building delete button).
15. The same kind of update functions are present for all of the tables.
16. On the main form, we also have the processes, which can be updated here (gesture with mouse).
17. To add a new process, press this (press create process button).
18. And enter the information for the new process.
	If we make a mistake, we can update the process (press process update and make an edit)
19. The process does not have any reactants when it is first added to the system, we need to go to the formulary to add them. (press formulary button)
20. In the process formulary, we can choose from reactants on the left, and add them to the selected process on the right.
21. The current process can be changed by selecting the drop down on the top right...
22. ...and the reactants can be managed on the left. Reactants can be added, updated or deleted at any time.
23. When reactants are deleted, the process reactants are updated through a trigger so that no processes refer to a nonexistent reactant.
24. The same is true for buildings and reactors. Since a reactor is part of a building, when a building is deleted, the reactors it houses are, too.
25. Once a process is selected at the top, the reactants being used can be edited as required.
	(select water from the list, press update, and edit the volume)
	(select something else and change it too)
26. When a new reactant is added to the inventory, we can add it here (select create reactant)
	Enter the data for the reactant and press OK.
27. Now the reactant is available for use in a process. (go to current process and add the new reactant. Update it to set the volume.)
