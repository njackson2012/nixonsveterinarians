<?php
	$servername = "den1.mysql4.gear.host";
	$username = "nixondb";
	$password = "Cy5G?_5x09e9";
	$dbname = "nixondb";
	
    // Create connection
	$conn = new mysqli($servername, $username, $password, $dbname);

	// Check connection
	if ($conn->connect_error) {
		die("Connection failed: " . $conn->connect_error);
	}
	$searchType = $_GET['SEARCHTYPE'];
	$searchValue = $_GET['SEARCHVALUE'];
	
	if (is_null($searchType)) {
		$query = "select * from pieces;";
	} else {
		$query = "select * from pieces where " . $searchType . " = " . $searchValue . ";";
	}
	$result = $conn->query($query);
	
	if ($result->num_rows > 0) {
		// output data of each row
		while($row = $result->fetch_assoc()) {
			echo "PieceID:" . $row["PIECEID"]. "-GameID:" . $row["GAMEID"]. "-Location:" . $row["LOCATION"]. "-Color:" . $row["COLOR"] . "-IsKing:" . $row["ISKING"] . "-";
		}
	} else {
		echo "0 results";
	}
	$conn->close();
?>