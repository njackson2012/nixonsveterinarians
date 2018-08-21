<html>
 <head>
  <title>PHP Test</title>
 </head>
 <body>
 <?php
	$servername = "den1.mysql4.gear.host";
	$username = "nixondb";
	$password = "Cy5G?_5x09e9";

	// Create connection
	$conn = new mysqli($servername, $username, $password);

	// Check connection
	if ($conn->connect_error) {
		die("Connection failed: " . $conn->connect_error);
	} 
	echo "Connected successfully";
	?>
 </body>
</html>
