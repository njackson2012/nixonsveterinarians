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
	$gameid = $_GET['GAMEID'];
	
	$query = "delete from games where GAMEID = " . $gameid . ";";

	if ($conn->query($query) === TRUE) {
		echo "Record deleted successfully";
	} else {
		echo "Error: " . $query . "<br>" . $conn->error;
	}
	
	$conn->close();
?>