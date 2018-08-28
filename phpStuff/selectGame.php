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
		$query = "select * from games;";
	} else {
		$query = "select * from games where " . $searchType . " = " . $searchValue . ";";
	}
	$result = $conn->query($query);
	
	if ($result->num_rows > 0) {
		// output data of each row
		while($row = $result->fetch_assoc()) {
			echo "GameID:" . $row["GAMEID"]. "-GameStatus: " . $row["GAMESTATUS"]. "-RequestStatus: " . $row["REQUESTSTATUS"]. "-Turn:" . $row["PLAYERTURN"] . "-";
		}
	} else {
		echo "0 results";
	}
	$conn->close();
?>