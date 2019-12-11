INSERT INTO barn (location)
VALUES 
	('north'), 
	('island'),
	('south');
INSERT INTO corral (capacity, currentAnimals, barnId)
VALUES 
	(10, 7, 1), 
	(3, 3, 1),
	(14, 2, 2);
INSERT INTO animal (type, corralId)
VALUES 
	('dog', 1), 
	('sheep', 2),
	('cow', 3),
	('sheep', 1),
	('goat', 3);
INSERT INTO food (type, barnId)
VALUES 
	('zerno', 1), 
	('proso', 2),
	('oves', 3),
	('grass', 1),
	('oves', 3);
INSERT INTO feeding (foodId, animalId, amount, periodicity)
VALUES 
	(1, 1, 1, 1), 
	(2, 2, 2, 2),
	(3, 3, 3, 3),
	(4, 4, 4, 1),
	(5, 5, 5, 3);
INSERT INTO sick_animals (animalId, illType, gettingTreatment, isHealthy)
VALUES 
	(1, 'ill1', true, false), 
	(2, 'ill2', false, false),
	(3, 'ill3', false, true),
	(4, 'ill4', false, false),
	(5, 'ill5', true, false)
