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
 
	// Strings must be escaped to prevent SQL injection attack.
	$pieceid = $_GET['PIECEID'];
	$gameid = $_GET['GAMEID']; 
	$location = $_GET['LOCATION'];
	$color = $_GET['COLOR'];
	$isking = $_GET['ISKING']; 
	
	if (is_null($pieceid)){
		$query = "insert into pieces (GAMEID, LOCATION, COLOR, ISKING) values ((SELECT GAMEID from games where GAMEID = " . $gameid . "), " . $location . ", " . $color . ", " . $isking . ");";
	} else {
		$query = "update games set GAMEID = (select GAMEID from games where GAMEID = " . $gameid . "), LOCATION = " . $location . ", COLOR = " . $color . ", ISKING = " . $isking . " where PIECEID = " . $pieceid . ";";
	}
	if ($conn->query($query) === TRUE) {
		$query = "SELECT LAST_INSERT_ID();";
		$result = conn->query($query);
		if ($result === TRUE) {
			echo $result;
		} else {
			echo "Error: " . $query . "<br>" . $conn->error;
		}
	} else {
		echo "Error: " . $query . "<br>" . $conn->error;
	}
	$conn->close();
?>