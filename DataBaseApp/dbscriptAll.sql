CREATE FUNCTION output_animal()
RETURNS TABLE(animalId INT,
			 type VARCHAR(255),
			 corralId INT)
AS $$
BEGIN
	RETURN QUERY
	SELECT * FROM animal;
END
$$ LANGUAGE plpgsql;

CREATE FUNCTION output_corral()
RETURNS TABLE(corralId INT,
			 capacity INT,
			 currentAnimals INT,
			 barnId INT)
AS $$
BEGIN
	RETURN QUERY
	SELECT * FROM corral;
END
$$ LANGUAGE plpgsql;


CREATE FUNCTION output_barn()
RETURNS TABLE(barnId INT,
       location VARCHAR(255))
AS $$
BEGIN
	RETURN QUERY 
	SELECT * FROM barn;
END
$$ LANGUAGE plpgsql;

CREATE FUNCTION output_food()
RETURNS TABLE(foodId INT,
			 type VARCHAR(255),
			 barnId INT)
AS $$
BEGIN
	RETURN QUERY
	SELECT * FROM food;
END
$$ LANGUAGE plpgsql;

CREATE FUNCTION output_feeding()
RETURNS TABLE (feedingId INT,
 foodId INT, 
			   animalId INT, 
			   amount INT, 
			   periodicity INT)
AS $$
BEGIN
	RETURN QUERY
	SELECT * FROM feeding;
END
$$ LANGUAGE plpgsql;

CREATE FUNCTION output_sick_animals()
RETURNS TABLE (illnessId INT,
	 		   animalId INT,
	 		   illType VARCHAR(255),
	 		   gettingTreatment BOOL,
	 		   isHealthy BOOL)
AS $$
BEGIN
	RETURN QUERY
	SELECT * FROM sick_animals;
END
$$ LANGUAGE plpgsql;


CREATE OR REPLACE PROCEDURE delete_animal() 
LANGUAGE SQL
AS $$
	DELETE FROM animal;
$$;

CREATE OR REPLACE PROCEDURE delete_corral() 
LANGUAGE SQL
AS $$
	DELETE FROM corral;
$$;

CREATE OR REPLACE PROCEDURE delete_barn() 
LANGUAGE SQL
AS $$
	DELETE FROM barn;
$$;

CREATE OR REPLACE PROCEDURE delete_food() 
LANGUAGE SQL
AS $$
	DELETE FROM food;
$$;

CREATE OR REPLACE PROCEDURE delete_feeding() 
LANGUAGE SQL
AS $$
	DELETE FROM feeding;
$$;

CREATE OR REPLACE PROCEDURE delete_sick_animals() 
LANGUAGE SQL
AS $$
	DELETE FROM sick_animals;
$$;

CREATE OR REPLACE PROCEDURE clear_all() 
LANGUAGE SQL
AS $$
	DELETE FROM sick_animals;
	DELETE FROM feeding;
	DELETE FROM food;
	DELETE FROM animal;
	DELETE FROM corral;
	DELETE FROM barn;
$$;

CREATE OR REPLACE PROCEDURE add_animal(
			 animal_type VARCHAR(255),
			 corralId INT) 
LANGUAGE SQL
AS $$
	INSERT INTO animal(type, corralId) VALUES (animal_type, corralId)
$$;

CREATE OR REPLACE PROCEDURE add_corral(
			 capacity INTEGER,
			 currentAnimals INTEGER,
			 barnId INTEGER) 
LANGUAGE SQL
AS $$
	INSERT INTO corral(capacity, currentAnimals, barnId) 
	VALUES (capacity, currentAnimals, barnId)
$$;

CREATE OR REPLACE PROCEDURE add_barn(
			 location VARCHAR(255)) 
LANGUAGE SQL
AS $$
	INSERT INTO barn(location) 
	VALUES (location)
$$;

CREATE OR REPLACE PROCEDURE add_food(
			 type VARCHAR(255),
			 barnId INT) 
LANGUAGE SQL
AS $$
	INSERT INTO food(type, barnId) 
	VALUES (type, barnId)
$$;

CREATE OR REPLACE PROCEDURE add_feeding(
			   foodId INT, 
			   animalId INT, 
			   amount INT, 
			   periodicity INT) 
LANGUAGE SQL
AS $$
	INSERT INTO feeding(foodId, animalId, amount, periodicity) 
	VALUES (foodId, animalId, amount, periodicity)
$$;

CREATE OR REPLACE PROCEDURE add_sick_animals(
			   animalId INT,
	 		   illType VARCHAR(255),
	 		   gettingTreatment BOOL,
	 		   isHealthy BOOL) 
LANGUAGE SQL
AS $$
	INSERT INTO sick_animals(animalId, illType, gettingTreatment, isHealthy) 
	VALUES (animalId, illType, gettingTreatment, isHealthy)
$$;

CREATE FUNCTION search_animal(search_type VARCHAR(255))
RETURNS TABLE (illnessId INT,
	 		   animalId INT,
	 		   illType VARCHAR(255),
	 		   gettingTreatment BOOL,
	 		   isHealthy BOOL)
AS $$
BEGIN
	RETURN QUERY
	SELECT sick_animals.illnessId, 
		sick_animals.animalId, 
		sick_animals.illType, 
		sick_animals.gettingTreatment, 
		sick_animals.isHealthy
	FROM sick_animals
	JOIN animal ON animal.animalId = sick_animals.animalId
	WHERE animal.type = search_type;
END
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE update_animal(
	varAnimalId INT, 
	varType VARCHAR(255),
	varCorralId INT) 
LANGUAGE SQL
AS $$
	UPDATE animal SET
		type = varType,
		corralId = varCorralId
	WHERE animalId = varAnimalId
$$;

CREATE OR REPLACE PROCEDURE update_corral(
			 varCorralId INT,
			 varCapacity INT,
			 varCurrentAnimals INT,
			 varBarnId INT) 
LANGUAGE SQL
AS $$
	UPDATE corral SET
		capacity = varCapacity,
		currentAnimals = varCurrentAnimals,
		barnId = varBarnId
	WHERE corralId = varCorralId
$$;

CREATE OR REPLACE PROCEDURE update_barn(
			 varBarnId INT,
			 varLocation VARCHAR(255)) 
LANGUAGE SQL
AS $$
	UPDATE barn SET
		location = varLocation
	WHERE barnId = varBarnId
$$;

CREATE OR REPLACE PROCEDURE update_food(
			 varFoodId INT,
			 varType VARCHAR(255),
			 varBarnId INT) 
LANGUAGE SQL
AS $$
	UPDATE food SET
		type = varType,
		barnId = varBarnId
	WHERE foodId = varFoodId
$$;

CREATE OR REPLACE PROCEDURE update_feeding(
			varFeedingId INT,
			varFoodId INT, 
			varAnimalId INT, 
			varAmount INT, 
			varPeriodicity INT) 
LANGUAGE SQL
AS $$
	UPDATE feeding SET
		foodId = varFoodId,
		animalId = varAnimalId,
		amount = varAmount,
		periodicity = varPeriodicity
	WHERE feedingId = varFeedingId
$$;

CREATE OR REPLACE PROCEDURE update_sick_animals(
			varIllnessId INT,
	 		varAnimalId INT,
	 		varIllType VARCHAR(255),
	 		varGettingTreatment BOOL,
	 		varIsHealthy BOOL) 
LANGUAGE SQL
AS $$
	UPDATE sick_animals SET
		animalId = varAnimalId,
		illType = varIllType,
		gettingTreatment = varGettingTreatment,
		isHealthy = varIsHealthy
	WHERE illnessId = varIllnessId
$$;

CREATE OR REPLACE PROCEDURE delete_feeding_by_animal(animal_type VARCHAR(255))
AS $$
BEGIN
	DELETE FROM feeding
	USING animal
	WHERE animal.animalId = feeding.animalId
	AND animal.type = animal_type;
END
$$ LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE delete_animal_id(varAnimalId INT) 
LANGUAGE SQL
AS $$
	DELETE FROM animal WHERE animalId=varAnimalId
$$;

CREATE OR REPLACE PROCEDURE delete_corral_id(varCorralId INT) 
LANGUAGE SQL
AS $$
	DELETE FROM corral WHERE corralId=varCorralId
$$;

CREATE OR REPLACE PROCEDURE delete_barn_id(varBarnId INT) 
LANGUAGE SQL
AS $$
	DELETE FROM barn WHERE barnId=varBarnId
$$;

CREATE OR REPLACE PROCEDURE delete_food_id(varFoodId INT) 
LANGUAGE SQL
AS $$
	DELETE FROM food WHERE foodId=varFoodId
$$;

CREATE OR REPLACE PROCEDURE delete_feeding_id(varFeedingId INT) 
LANGUAGE SQL
AS $$
	DELETE FROM feeding WHERE feedingId=varFeedingId
$$;

CREATE OR REPLACE PROCEDURE delete_sick_animals_id(varIllnessId INT) 
LANGUAGE SQL
AS $$
	DELETE FROM sick_animals WHERE illnessId = varIllnessId
$$;



