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

	// Create connection
	$conn = new mysqli($servername, $username, $password, $dbname);

	// Check connection
	if ($conn->connect_error) {
		die("Connection failed: " . $conn->connect_error);
	}
	$sql = "SELECT * FROM games";
	$result = $conn->query($sql);
	echo "Connected successfully";
	echo $result;
	$conn->close();
	?>
 </body>
</html>
