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
	$pieceid = $_GET['PIECEID'];
	
	$query = "select * from pieces where PIECEID = " . $pieceid . ";";
	$result = $conn->query($query);
	
	if ($result->num_rows > 0) {
		// output data of each row
		while($row = $result->fetch_assoc()) {
			echo "PieceID: " . $row["PIECEID"]. " - GameID: " . $row["GAMEID"]. " - Location: " . $row["LOCATION"]. " - Color: " . $row["COLOR"] . " - IsKing: " . $row["ISKING"] . "<br>";
		}
	} else {
		echo "0 results";
	}
	$conn->close();
?>