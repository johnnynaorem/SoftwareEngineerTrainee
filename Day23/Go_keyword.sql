Create table TableX(
	table_No int Identity(1, 1)
)

ALTER TABLE TableX 
ADD New_col INT;

GO


INSERT INTO TableX (New_col) 
VALUES (2);

SELECT * FROM TableX

DROP TABLE TableX