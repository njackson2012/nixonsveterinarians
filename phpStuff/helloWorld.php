<html>
 <head>
  <title>PHP Test</title>
 </head>
 <body>
 <?php
	$servername = "den1.mysql4.gear.host";
	$username = "nixondb";
	$password = "Cy5G?_5x09e9";
	$dbname = "nixondb";
	$gameid = $_GET['message'];

	// Create connection
	$conn = new mysqli($servername, $username, $password, $dbname);

	// Check connection
	if ($conn->connect_error) {
		die("Connection failed: " . $conn->connect_error);
	}
	$sql = "SELECT * FROM games WHERE GAMEID = " . $gameid . ";";
	$result = $conn->query($sql);
	echo "Connected successfully <br>";
	if ($result->num_rows > 0) {
		// output data of each row
		while($row = $result->fetch_assoc()) {
			echo "id: " . $row["GAMEID"]. " - GameStatus: " . $row["GAMESTATUS"]. " - RequestStatus: " . $row["REQUESTSTATUS"]. " - Turn: " . $row["PLAYERTURN"] . "<br>";
		}
	} else {
		echo "0 results";
	}
	$conn->close();
	?>
 </body>
</html>
