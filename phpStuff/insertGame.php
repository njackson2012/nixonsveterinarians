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
	$gameid = $_GET['GAMEID']; 
	$gamestatus = $_GET['GAMESTATUS'];
	$requeststatus = $_GET['REQUESTSTATUS'];
	$playerturn = $_GET['PLAYERTURN']; 
	
	if (is_null($gameid)){
		$query = "insert into games (GAMESTATUS, REQUESTSTATUS, PLAYERTURN) values (" . $gamestatus . ", " . $requeststatus . ", " . $playerturn . ");";
	} else {
		$query = "update games set GAMESTATUS = " . $gamestatus . ", REQUESTSTATUS = " . $requeststatus . ", PLAYERTURN = " . $playerturn . " where GAMEID = " . $gameid . ";";
	}
	if ($conn->query($query) === TRUE) {
		$query = "SELECT LAST_INSERT_ID();";
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